# Enabling Docker Content Trust (DCT)

Docker Content Trust can be enabled by setting the `DOCKER_CONTENT_TRUST` environment variable to `1`. With Docker Content Trust enabled, the system will not run images if they are unsigned or the signature is not valid.

In Docker Enterprise Edition, you can also enable it within `daemon.json`.

When DCT is enabled, Docker will only pull and run `signed images`. Attempting to pull and/or run an unsigned image will result in an error message.

Note that when `DOCKER_CONTENT_TRUST=1`, `docker push` will automatically sign the image before pushing it.

Set `DOCKER_CONTENT_TRUST` environment variable to `1`:

```zsh
export DOCKER_CONTENT_TRUST=1
```
