# Restore DTR Backup

1. Stop the existing DTR replica with:

```zsh
 docker run -it --rm \
     docker/dtr:2.6.6 destroy \
         --ucp-insecure-tls \
         --ucp-username admin \
         --ucp-url https://<UCP_MANAGER_PRIVATE_IP>
```

Restore images with:

```zsh
sudo tar -xzf dtr-backup-images.tar -C /var/lib/docker/volumes
```

Restore DTR metadata with:

```zsh
read -sp 'ucp password: ' UCP_PASSWORD; \
docker run -i --rm \
    --env UCP_PASSWORD=$UCP_PASSWORD \
    docker/dtr:2.6.6 restore \
    --dtr-use-default-storage \
    --ucp-url https://<UCP_MANAGER_PRIVATE_IP> \
    --ucp-insecure-tls \
    --ucp-username admin \
    --ucp-node <HOSTNAME> \
    --replica-id <REPLICA-ID> \
    --dtr-external-url <DTR-EXTERNAL-URL> < dtr-backup-metadata.tar
```