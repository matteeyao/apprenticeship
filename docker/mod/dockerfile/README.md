# The Dockerfile

**Dockerfile**: A file that defines a series of directives and is used to build an image.

An example of a Dockerfile:

```
# Simple nginx image
FROM ubuntu:bionic

ENV NGINX_VERSION 1.14.0-0ubuntu1.7

RUN apt-get update && apt-get install -y curl
RUN apt-get update && apt-get install -y nginx=$NGINX_VERSION

CMD ["nginx", "-g", "daemon off;"]
```

If you want to create your own images, you can do so w/ a Dockerfile.

Build an image with:

```
docker build -t TAG_NAME DOCKERFILE_LOCATION
```

A Dockerfile is a set of instructions which are used to construct a Docker image. These instructions are called **directives**.

Dockerfile Directives:

* `FROM`: Specifies the base image to build from. Starts a new build stage and sets the base image. Usually must be the first directive in the Dockerfile (except ARG can be placed before `from`).

* `ENV`: Sets environment variables that are visible in later build steps as well as during container runtime. These can be referenced in the Dockerfile itself and are visible to the container at runtime.

* `RUN`: Executes a command and commits the result to the image file system. Creates a new layer on top of the previous layer by running a command inside that new layer and committing the changes.

* `CMD`: Specify a default command used to run a container at execution time. This gets overridden if a command gets specified at container runtime.

* `ENTRYPOINT`: Sets the default executable for containers. This can still be overridden at container runtime, but requires a special flag. When `ENTRYPOINT` and `CMD` are both used, `ENTRYPOINT` sets the default executable, and
  `CMD` sets default arguments.

Then you can run your image:

```
docker run IMAGE
```

## Additional exercise

```
docker images
```

```
mkdir onboarding
```

```
cd onboarding/
```

```
vim dockerfile
```

```dockerfile
FROM ubuntu:16.04
LABEL maintainer="ell.marquez@linuxacademy.com"
RUN apt-get update
RUN apt-get install -y python3
```

```
docker build .
```
