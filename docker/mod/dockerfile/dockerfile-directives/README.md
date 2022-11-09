# Additional Dockerfile Directives

Simple nginx image:

```zsh
FROM ubuntu:bionic

ENV NGINX_VERSION 1.14.0-0ubuntu1.2

RUN apt-get update && apt-get install -y curl
RUN apt-get update && apt-get install -y nginx=$NGINX_VERSION

WORKDIR /var
WORKDIR www # → relative
WORKDIR html
WORKDIR /var/www/html # → absolute
ADD index.html ./

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]

STOPSIGNAL SIGTERM
HEALTHCHECK CMD curl localhost:80
```

Directives:

* `EXPOSE`: Documents which ports are intended to be published when running a container.

> [!Note]
> 
> This does not actually publish the ports.

* `WORKDIR`: Sets the current working directory for subsequent directives such as `ADD`, `COPY`, `CMD`, `ENTRYPOINT`, etc., both for subsequent build steps and for the container at runtime. We can use `WORKDIR` multiple times to change directories throughout the Dockerfile. If the `WORKDIR` begins with a forward slash `/`, then it will set an absolute path. Otherwise, it will set the working directory relative to the previous working directory. The relative path sets the new working directory relative to the previous working directory.

* `COPY` : Copies files from the build host (local machine) into the image file system.

* `ADD`: Copies files from the build host into the image file system. Unlike `COPY`, `ADD` can also extract an archive into the image file system and add files from a remote URL. Can pull files using a URL and extract an archive into loose files in the image.

* `STOPSIGNAL`: Specify a custom signal that will be used to stop the container process.

* `HEALTHCHECK`: Specify a command to run in order to perform a custom health check to verify that the container is working properly.

Build container:

```
docker build -t custom-nginx .
```

Run container:

```
dockr run -d -p 8080:80 custom=nginx
```

```
curl localhost:8080
```

Clean up container:

```
docker ps
```

```
docker container rm -f <CONTAINER_ID>
```
