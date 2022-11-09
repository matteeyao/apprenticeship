# The Dockerfile

1. Describe what the `RUN` directive does.

[ ] The `RUN` directive executes a command on the host when building an image.

[ ] The `RUN` directive automatically runs a command when a new container gets created.

[ ] The `RUN` directive sets the default command for the image.

[x] The `RUN` directive executes a command and commits the resulting changed files as a new layer in the image.

> This accurately describes what `RUN` does.

2. What does the `CMD` directive do?

[ ] It runs a command within the image and commits it to the result.

[ ] It runs a command on the host when the container starts.

[ ] It executes a command during the build process.

[x] It sets the default command for the image that runs if no other command is specified.

> The `CMD` directive sets the default command.

3. Which of the following statements truly applies to the `ENV` directive?

[ ] It sets environment variables that are only visible during later build steps.

[ ] It sets an environment variable on the host while the container is running.

[ ] It sets environment variables that are only visible at the container runtime.

[x] It sets environment variables that are made available in subsequent build steps and to containers at the runtime.

> The `ENV` directive sets environment variables, and they're visible during subsequent build steps and at the container runtime.
