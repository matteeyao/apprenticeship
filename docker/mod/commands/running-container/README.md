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

## 2.2.3 Running the container

With the image built and ready, you can now run the container with the following command:

```zsh
$ docker run --name kiada-container -p 1234:8080 -d kiada
9d62e8a9c37e056a82bb1efad57789e947df58669f94adc2006c087a03c54e02
```

This tells Docker to run a new container called `kiada-container` from the `kiada` image. The container is detached from the console (`-d` flag) and runs in the background. Port `1234` on the host computer is mapped to port `8080` in the container (specified by the `-p 1234:8080` option), so you can access the app at http://localhost:1234.

The following figure should help you visualize how everything fits together. Note that the Linux VM exists only if you use macOS or Windows. If you use Linux directly, there is no VM and the box depicting port `1234` is at the edge of the local computer.

![Fig. 2 Visualizing your running container](../../../../kubernetes/img/kubernetes-in-action.demo/chpt02/diag02.png)

### Accessing your app

Now access the application at http://localhost:1234 using `curl` or your internet browser:

```zsh
$ curl localhost:1234
Kiada version 0.1. Request processed by "44d76963e8e1". Client IP: ::ffff:172.17.0.1
```

> [!NOTE]
>
> If the Docker Daemon runs on a different machine, you must replace `localhost` with the IP of that machine. You can look it up in the `DOCKER_HOST` environment variable.

If all went well, you should see the response sent by the application. In my case, it returns `44d76963e8e1` as its hostname. In your case, you’ll see a different hexadecimal number. That’s the ID of the container. You’ll also see it displayed when you list the running containers next.

### Listing all running containers

To list all the containers that are running on your computer, run the following command. Its output has been edited to make it more readable—the last two lines of the output are the continuation of the first two.

```zsh
$ docker ps 
CONTAINER ID    IMAGE           COMMAND         CREATED         ...
44d76963e8e1    kiada:latest    "node app.js"   6 minutes ago   ...

... STATUS          PORTS                     NAMES
... Up 6 minutes    0.0.0.0:1234->8080/tcp    kiada-container
```

For each container, Docker prints its ID and name, the image it uses, and the command it executes. It also shows when the container was created, what status it has, and which host ports are mapped to the container.

### Getting additional information about a container

The `docker ps` command shows the most basic information about the containers. To see additional information, you can use `docker inspect`:

```zsh
$ docker inspect kiada-container
```

Docker prints a long JSON-formatted document containing a lot of information about the container, such as its state, config, and network settings, including its IP address.

### Inspecting the application log

Docker captures and stores everything the application writes to the standard output and error streams. This is typically the place where applications write their logs. You can use the `docker logs` command to see the output:

```zsh
$ docker logs kiada-container
Kiada - Kubernetes in Action Demo Application
---------------------------------------------
Kiada 0.1 starting...
Local hostname is 44d76963e8e1
Listening on port 8080
Received request for / from ::ffff:172.17.0.1
```

You now know the basic commands for executing and inspecting an application in a container. Next, you’ll learn how to distribute it.
