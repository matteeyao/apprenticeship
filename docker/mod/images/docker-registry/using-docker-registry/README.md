# Using Docker Registries

### Download an image from a registry to the local system:

```
docker pull <IMAGE[:TAG]>
```

### Search Docker Hub from the command line with:

```
docker search <SEARCH_TERM>
```

### Authenticate against a remote registry:

```
docker login <REGISTRY>
```

> [!NOTE]
>
> When working w/ Docker registries, if the registry is not specified, the default registry will be used (Docker Hub). We can omit the `REGISTRY_URL` w/ Docker Hub.

There are two ways to authenticate w/ a private registry that uses an untrusted or self-signed certificate.

* **Secure**: Adds the registry's public certificate to `/etc/docker/certs.d/<registry public hostname>`.

* **Insecure**: Adds the registry to the `insecure-registries` list in `daemon.json`, or pass it to `dockerd` with the `--insecure-registry` flag.

### Push an image to a registry:

Upload the image to a remote registry:

```
docker push <IMAGE>
```

There are multiple ways to use a registry w/ a self-signed certificate.

* Turn off certificate verification (very insecure).

* Provide the public certificate to the Docker engine.

To push and pull images from your private registry, tag the images w/ the registry hostname (and optionally, port).

```
<registry public hostname>/<image name>:<tag>
```

## Authenticate w/ Insecure method

```
sudo vi /etc/docker/daemon.json
```

```json
{
  "insecure-registries": ["<PUBLIC_DOCKER_REGISTRY_HOSTNAME>"]
}
```

Restart Docker:

```
sudo systemctl restart docker
```

## Authenticate w/ Secure method

```
docker logout <PUBLIC_DOCKER_REGISTRY_HOSTNAME>
```

Clear out `daemon.json`:

```
sudo vi /etc/docker/daemon.json
```

```
sudo systemctl restart docker
```

```
docker login <PUBLIC_DOCKER_REGISTRY_HOSTNAME>
```

Provide the public certificate to the Docker engine:

```
sudo mkdir -p /etc/docker/certs.d/<PUBLIC_DOCKER_REGISTRY_HOSTNAME>
```

```
sudo scp cloud_user@<PUBLIC_DOCKER_REGISTRY_HOSTNAME>:/home/cloud_user/registry/certs/domain.crt /etc/docker/certs.d/<PUBLIC_DOCKER_REGISTRY_HOSTNAME>
```

```
docker login <PUBLIC_DOCKER_REGISTRY_HOSTNAME>
```

```
docker tag <IMAGE> <PUBLIC_DOCKER_REGISTRY_HOSTNAME>/<TAG>
```

```
docker push <IMAGE> <PUBLIC_DOCKER_REGISTRY_HOSTNAME>/<TAG>
```

```
docker image rm <PUBLIC_DOCKER_REGISTRY_HOSTNAME>/<TAG>
docker image rm <IMAGE>
docker pull <PUBLIC_DOCKER_REGISTRY_HOSTNAME>/<TAG>
```
