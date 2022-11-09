# Docker Registries

* **Docker Registry**: A central location for storing and distributing images.

  * A Docker Registry is responsible for storing and distributing Docker images.

* **Docker Hub**: The default, public registry that's operated by Docker.

  * We have already pulled images from the default public registry, Docker Hub.

We can operate our own private registry for free using the `registry` image.

To create a basic registry, simply run a container using the registry image and publish port `5000`.

Registry Server:

* Distribution - Ubuntu 18.04 Bionic Beaver LTS

* Size - Small

Run a simple registry with:

```
docker run -d -p 5000:5000 --restart=always --name registry registry:2
```

## Configuring a Registry

You can override individual values in the default registry configuration by supplying environment variables w/ `docker run -e`.

Name the variable `REGISTRY_` followed by each configuration key, all uppercase and separated by underscores.

For example, to change the config:

```
log:
  level: info
```

Set the environment variable:

```
REGISTRY_LOG_LEVEL=debug
```

Configure our registry using environment variables with:

```
docker run -d -p 5000:5000 --restart=always --name registry \
  -e REGISTRY_LOG_LEVEL=debug registry:2
```

You can also create your own registries using Docker's open source registry software, or Docker Trusted Registry, the non-free enterprise solution.

## Securing a Registry

By default, the registry is completely unsecured. It does not use TLS and does not require authentication.

You can take some basic steps to secure your registry:

* Use TLS w/ a certificate.

* Require user authentication.

```
docker container stop registry && docker container rm -v registry
```

```
mkdir ~/registry
```

```
cd ~/registry
```

```
mkdir auth
```

```
docker run --entrypoint htpassword registry:2 -Bbn testuser password > auth/htpasswd
```

```
ls auth/htpasswd
```

```
cat auth/htpasswd
```

```
mkdir certs
```

```
openssl req \
  -newkey rsa:4096 -nodes -sha256 -keyout certs/domain.key \
  -x509 -days 365 -out cert/domain.crt
```

```
docker run -d -p 443:443 --restart=always --name registry \
  -v /home/cloud_user/registry/certs:/certs \
  -v /home/cloud_user/registry/auth:/auth \
  -e REGISTRY_HTTP_ADDR=0.0.0.0.443 \
  -e REGISTRY_HTTP_TLS_CERTIFICATE=/certs/domain.crt \
  -e REGISTRY_HTTP_TLS_KEY=/certs/domain.key \
  -e REGISTRY_AUTH=htpassword \
  -e "REGISTRY_AUTH_HTPASSWD_REALM=Registry Realm" \
  -e REGISTRY_AUTH_HTPASSWD_PATH=/auth/htpasswd \
  registry:2
```

```
curl -k https://localhost:443
```
