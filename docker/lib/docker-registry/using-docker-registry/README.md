# Using Docker Registry

1. Which of the following are insecure ways to allow a Docker client to authenticate against a registry that uses a self-signed certificate? (Choose two)

[x] We add the registry to `insecure-registries` in `/etc/docker/daemon.json`.

[x] We pass the `--insecure-registry` flag to the Docker daemon.

[ ] We use the `--skip-tls` flag with `docker login`.

[ ] We add the self-signed certificate as a trusted registry certificate under `/etc/docker/certs.d/`.

2. Which of the following is a secure method for allowing a Docker client to authenticate with a registry that uses a self-signed certificate?

[ ] We add the self-signed certificate as a trusted registry certificate under `/etc/docker/certs.d/`.

[ ] `docker login --trust-ca`

[ ] `docker login --accept-cert`

[ ] We add the registry to the `insecure-registries` list in `/etc/docker/daemon.json`.

> Utilizing `/etc/docker/certs.d/` is the secure way to authenticate with a registry that uses a self-signed certificate.
