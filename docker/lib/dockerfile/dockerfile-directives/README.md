# Additional Dockerfile Directives

1. Dave needs Docker to use a custom stop signal for halting his software. How can he build an image that will instruct Docker on which stop signal to use?

[ ] Dave should locate the process and kill it manually.

[x] Dave should use the `STOPSIGNAL` directive.

[ ] Dave should use the `docker stop` command.

[ ] Dave should use the `STOP` directive.

> The `STOPSIGNAL` directive instructs Docker on which stop signal to use for halting a container process.

2. Which of the following statements does not apply to the `WORKDIR` directive?

[ ] It sets the working directory for subsequent build steps.

[ ] It sets the working directory for the container at runtime.

[x] It affects only the build and does not impact containers that run from the image.

[ ] It can use both absolute and relative paths.

> The `WORKDIR` directive affects the containers by setting the working directory at the container runtime.

3. What does the `HEALTHCHECK` directive do?

[ ] It sets a command that will be used to fix the container if it becomes unhealthy.

[ ] It restarts the container if it becomes unhealthy.

[ ] It sets a command that will be used to inform the container of the health status of the docker daemon.

[x] It sets a command that will be used by the Docker daemon to determine whether the container is healthy.

> The `HEALTHCHECK` directive sets a command that is used to determine container health.
