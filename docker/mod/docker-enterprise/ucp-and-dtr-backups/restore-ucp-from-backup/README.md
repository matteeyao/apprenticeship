# Restore UCP from Backup

1. We must first uninstall UCP on the UCP manager server:

```zsh
docker container run --rm -it \
         -v /var/run/docker.sock:/var/run/docker.sock \
         --name ucp \
         docker/ucp:3.1.5 uninstall-ucp --interactive
```

2. Restore UCP from the backup:

```zsh
docker container run --rm -i --name ucp \
           -v /var/run/docker.sock:/var/run/docker.sock  \
           docker/ucp:3.1.5 restore --passphrase "secretsecret" < /home/cloud_user/ucp-backup.tar
```
