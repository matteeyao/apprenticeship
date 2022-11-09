# Signing Images

**Docker Content Trust (DCT)** provides a secure way to verify the integrity of images before you pull or run them on your systems.

W/ **DCT**, the image creator signs each image w/ a certificate, which clients can use to verify the image before running it.

Generate a delegation key pair. This gives users access to sign images for a repository:

```zsh
docker trust key generate <SIGNER_NAME>
```

Add a signer (user) to a repo:

```zsh
docker trust signer add --key <KEY_FILE> <SIGNER_NAME> <REPO>
```

We can sign and push an image to the registry with:

```zsh
docker trust sign <REPO>:<TAG>
```

The above command works similarly to `docker push`, but also signs the image.
