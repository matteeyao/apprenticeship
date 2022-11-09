# Enabling Docker Content Trust (DCT)

1. How can you enable Docker Content Trust (DCT) in Docker Community Edition (CE)?

[ ] Set the `CONTENT_TRUST` environment variable to 1.

[x] Set the `DOCKER_CONTENT_TRUST` environment variable to 1.

[ ] Pass the `--content-trust` flag to dockerd.

[ ] Set `"content-trust": true` in `/etc/docker/daemon.json`.

> Setting this environment variable to 1 will enable DCT.
