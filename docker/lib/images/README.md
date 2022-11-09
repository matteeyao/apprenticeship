# Image Basics

1. How would we go about keeping track of changes made to an image in source control (i.e., git)?

[ ] Maintain tags for each new version within the Docker registry.

[ ] We would use Docker Trusted Registry (DTR) to handle this.

[ ] We would push the image layers to a source control repository.

[x] We would store the Dockerfile in source control.

> We can keep the Dockerfile in source control to track any changes made to the Dockerfile.

2. Which of the following commands will build an image from a Dockerfile located in the current directory and tag it as `my-custom-image:1`?

[ ] `docker build -tag my-custom-image:1 .`

[ ] `docker build .`

[x] `docker build -t my-custom-image:1 .`

[ ] `docker build -tag my-custom-image:1`

> This command will build the image and tag it appropriately.

3. Which of the following is true about building docker images?

[ ] Every layer of the image is re-built every time `docker build` is executed.

[ ] Only layers that have changed since the last build (and any following layers) are built.

[ ] Every Dockerfile can have only one FROM directive.

[ ] Docker images are built using Docker Compose files.

> `docker build` only builds changed layers and the layers that follow them.
