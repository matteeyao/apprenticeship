# Running a Container

Now that we know how to install and configure Docker, we're ready to run some containers.

We can run containers using the `docker run` command.

## Docker Run

Here are some key commands and processes:

### Run a container

```
docker run [OPTIONS] IMAGE[:TAG] [COMMAND] [ARG...]
```

* `IMAGE`: The container image to run. By default, these are pulled from Docker Hub. Run a container using an image called `hello-world`. In this example, the tag is unspecified, so the latest tag will automatically be used.

```
docker run hello-world
```

* `COMMAND` and `ARG`: Run a command inside the container. This command runs a container using the `busybox` image. Inside the container, plus it runs the command `echo` with the arguments `hello world!`.

    * `COMMAND`: Command to run inside the container.

    * `ARG`: Arguments to pass when running the command.

```
docker run busybox echo hello world!
```

* `TAG`: A specific image tag. Usually used to pull a specific version. The command below specifies a certain tag, running a container with tag `1.15.11` of the `nginx` image.

```
docker run nginx:1.15.11
```

* `-d`: Runs the container in detached mode. The `docker run` command will exit immediately and the container will run in the background.

* `--name NAME`: A container is usually assigned a random name by default, but you can provide a more descriptive name w/ this flag.

* `--restart RESTART`: Specify when the container should be automatically restarted.

  * `no` (default): Never restart the container.

  * `on-failure`: Only if the container fails (exits with a non-zero exit code).

  * `always`: Always restart the container whether it succeeds or fails. Also starts the container automatically upon daemon startup.

  * `unless-stopped`: Always restart the container whether it succeeds or fails, and on daemon startup unless the container is manually stopped.

Other commonly-used options:

* `-p <HOST_PORT>:<CONTAINER_PORT>`: Publish a container's port or expose a container's port by mapping it to a port on the host. This process is necessary to access a port on a running container. The `<HOST_PORT>` is the port that listens on the host machine, and traffic to that port is mapped to the `<CONTAINER_PORT>` on the container. You can use `-p` multiple times to map multiple ports.

* `--rm`: Automatically remove the container when it exits. Cannot be used w/ `--restart`.

* `--memory <MEMORY>`: Set a hard limit on memory usage.

* `--memory-reservation <MEMORY>`: Set a soft limit on memory usage. This limit is used only if Docker detects memory contention on the host. The container will be restricted to this limit if Docker detects memort contention (not enough memory on the host).

Here's an example of these `docker run` flags in action:

```
docker run -d --name nginx --restart unless-stopped -p 8080:80 --memory 500M \
--memory-reservation 256M nginx
```

## Managing Containers

Some commands for running containers:

### List running containers

```
docker ps
```

### List all containers, including stopped containers

```
docker ps -a
```

### Stop a running container

```
docker container stop <CONTAINER_NAME | ID>
```

### Start a stopped container

```
docker container start <CONTAINER_NAME | ID>
```

### Delete a container (must be stopped first)

```
docker container rm <container name or ID>
```
