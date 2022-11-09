# Running a Container

1. Amanda wants to execute a one-time job using a Docker container. However, occasionally, this job fails and needs to restart. Amanda doesn't want to restart it manually if it fails. Which command should she use to make sure that the container executes the one-time job successfully?

[ ] `docker run --restart failure-only cleanup-job`

[ ] `docker run --restart unless-stopped cleanup-job`

[ ] `docker run --recover-failure cleanup-job`

[x] `docker run --restart on-failure cleanup-job`

> This restart policy will only restart the container if it exits with a non-zero exit code.

2. Dylan is getting ready to run a container. He needs this container to auto-restart whenever its process exits, but he doesn't want it to restart if the container had manually stopped earlier. Which restart policy should he use?

[x] `unless-stopped`

[ ] `on-failure`

[ ] `manual-control`

[ ] `always`

> This restart policy will always restart the container unless it was stopped explicitly.
