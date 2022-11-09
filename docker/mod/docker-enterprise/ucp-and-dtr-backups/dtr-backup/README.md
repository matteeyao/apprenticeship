# Back Up the DTR

1. On the DTR server, retrieve the DTR replica ID:

```zsh
docker volume ls
```

Look for a volume name that begins with `dtr-registry-`. The string of letters and numbers at the end of this volume make up the name of the DTR replica ID.

2. Back up the registry images:

```zsh
sudo tar -zvcf dtr-backup-images.tar \
   $(dirname $(docker volume inspect --format '{{.Mountpoint}}' dtr-registry-<REPLICA_ID>))
```

3. Back up DTR metadata:

```zsh
read -sp 'ucp password: ' UCP_PASSWORD; \
docker run --log-driver none -i --rm \
     --env UCP_PASSWORD=$UCP_PASSWORD \
     docker/dtr:2.6.6 backup \
     --ucp-url https://<UCP Manager Private IP> \
     --ucp-insecure-tls \
     --ucp-username admin \
     --existing-replica-id <replica-id> > dtr-backup-metadata.tar
```
