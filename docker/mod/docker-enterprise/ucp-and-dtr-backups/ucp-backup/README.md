# Backup the UCP

Remember that we must back up Docker Swarm separately when backing up UCP.

1. On the UCP server, retrieve the UCP instance ID:

```zsh
docker container run --rm \
        --name ucp \
        -v /var/run/docker.sock:/var/run/docker.sock \
        docker/ucp:3.1.5 \
        id
```

2. Enter the UCP instance ID from the previous command for the `--id` flag, and create an encrypted backup:

```zsh
docker container run \
        --log-driver none --rm \
        --interactive \
        --name ucp \
        -v /var/run/docker.sock:/var/run/docker.sock \
        docker/ucp:3.1.5 backup \
        --passphrase "secretsecret" \
        --id <YOUR_UCP_INSTANCE_ID> > /home/cloud_user/ucp-backup.tar
```

3. List the contents of the backup file:

```zsh
gpg --decrypt /home/cloud_user/ucp-backup.tar | tar --list
```
