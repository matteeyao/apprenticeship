# Configuring Backups for UCP and DTR

In a production environment, it is important to **regularly back up** your UCP infrastructure so that you can quickly recover in the event of data loss.

The basic steps for backing up your UCP and DTR infrastructure are:

1. Back up the Docker swarm.

> [!NOTE]
> 
> This is done the same way for a UCP swarm as it is for a regular, non-UCP swarm.

2. Back up UCP.

```zsh
sudo docker container run \
  --rm \
  --log-driver none \
  --name ucp \
  --volume /var/run/docker.sock:/var/run/docker.sock \
  --volume /tmp:/backup \
  mirantis/ucp:3.3.2 backup \
  --file ucp_backup.tar \
  --passphrase "mysecretphrase" \
  --include-logs=false
```

```zsh
ls /tmp
```

To verify backup, run:

```zsh
gpg --decrypt /tmp/ucp_backup.tar | tar --list
```

3. Back up DTR Images.

* Log into the DTR server.

* Get a copy of the replica and store it in a `REPLICA_ID` environment variable:

```zsh
REPLICA_ID=$(sudo docker ps --format '{{.Names}}' -f name=dtr-rethink | cut -f 3 -d '-') && echo $REPLICA_ID
```

* Create a TAR file called `dtr-backup-images.tar` and backup the contents of that replica ID container's volume. So we have a particular volume here on the disk that DTR is using to store the actual image files. We are manually accessing that volume's actual data here on the host, and taking those image contents and putting them into that TAR file.

```zsh
sudo tar -cvf dtr-backup-images.tar /var/lib/docker/volumes/dtr-registry-$REPLICA_ID
```

* Verify the contents:

```zsh
tar -tf dtr-backup-images.tar
```

4. Back up DTR Metadata.

* Create an environment variable:

```zsh
UCP_PRIVATE_IP=172.31.46.172
```

* Run the actual DTR container itself:

```zsh
read -sp 'ucp password: ' UCP_PASSWORD; \
sudo docker run --log-driver none -i --rm \
  --env UCP_PASSWORD=$UCP_PASSWORD \
  mirantis/dtr:2.8.2 backup \
  --ucp-url https://$UCP_PRIVATE_IP \
  --ucp-insecure-tls \
  --ucp=username admin \
  --existing-replica-id $REPLICA_ID > dtr-backup-metadata.tar
```
