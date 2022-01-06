# Persisting Data in Docker

I'll start by reiterating, **containers are Ephemeral and once a container is removed, it and its data are gone**. A Container should never change, it should just be stopped and redeployed. There is a problem w/ that though: what about data we might need from a container? What if you need to share data across multiple containers? Or store a container's data on a remote host rather than locally?

In other words we want **persistent data**, data that will be generated and not tied to the container life cycle. Docker has two options for containers to store files in the host machine, so that the files are persisted even after the container stops: *volumes* and *bind mounts*

Here is a brief summary of the two concepts w/ more detailed info below:

1. **Bind Mounts** - can be stored anywhere on the container host and mounted on the running container. Can exist anywhere in the host filesystem

2. **Volumes** - the Host filesystem stores volumes but they are managed by Docker in `C:\ProgramData\docker\volumes`

No matter which type of mount you choose to use, the data looks the same from within the container:

![Bind Mounts and Volumes](../../../img/types-of-mounts.png)

## Volumes

Volumes are stored in a part of the host filesystem which is managed by Docker (`/var/lib/docker/volumes/` on Linux). Volumes are the best way to persist data in Docker. Non-Docker processes should not touch these files

You can create a volume explicitly using `docker volume create`, or Docker can create a volume for you when you add a `-v` flag to the `docker container run` command, which would look something like this:

```zsh
docker run -v /path/in/container
```

When you create a volume, it is stored within a directory on the Docker host. When you mount a volume into a container, the directory can then be accessed by the container. You can then destroy that container, but the volume will persist. **Volumes are not tied to the container lifecycle** You can start a container w/ a volume, stop and remove that container, and then deploy a new container w/ the same volume and the data inside the volume won't change. You have to manually remove volumes

A volume can also be mounted into multiple containers simultaneously. When no running container is using a volume, the volume is still available to Docker. You can remove any unused volumes using `docker volume prune`

Volumes also support the use of drivers, which allow you to store your data on remote hosts or cloud providers, among other exciting possibilities

**Named Volume**: By default when you create a volume it will be identified by it's unique ID. You can however assign a name, making the volume a "named volume"

### Volume Summary

Running the `volume` command will bypass the Union File System and store your data in an alternate location on your host

* The volume includes it's own management commands under `docker volume`

* Connect to none, one, or multiple containers at once

* Not subject to commit, save, or export commands

* By default they only have a unique ID, but you can assign name

```zsh
docker container run -d --name mysql -e MYSQL_ALLOW_EMPTY_PASSWORD=true -v --mount source=mysql_data,target=/var/lib/mysql
```

```zsh
docker container ls
```

```zsh
docker image inspect mysql
```

```zsh
docker volume ls
```

```zsh
docker volume inspect mysql_data
```

Volumes live outside the container lifecycle:

```zsh
docker container rm -f mysql
```

```zsh
docker container ls
```

```zsh
docker container ls -a
```

```zsh
docker volume ls
```

Run a new container on the `mysql_data` volume:

```zsh
docker container run -d --name mysql2 -e MYSQL_ALLOW_EMPTY_PASSWORD=true -v --mount source=mysql_data,target=/var/lib/mysql
```

```zsh
docker container inspect mysql2
```

```zsh
docker volume ls
```

Clean up containers:

```zsh
docker container rm -f mysql2
```

Remove volume:

```zsh
docker volume rm mysql_data
```

## Bind Mounts

Bind mounts may be stored anywhere on the host system. When you use a bind mount, a file or directory on the host machine is mounted and mapped into a container. The file or directory is referenced by it's full or relative path on the host machine, meaning that you'll basically have two different references pointing to the same file in memory. Your Non-Docker processes on the Docker host or a Docker container can modify a bind mounted file at any time. You can't use Docker CLI commands to directly manage bind mounts

To start a Docker container w/ a bind mount would look something like this:

```zsh
docker run -d \
  --name devtest \
  --mount type=bind,source="$(pwd)"/target,target=/app \
  nginx:latest
```

Bind mounts perform well, but there are security reasons why they are not used as often as volumes are. Processes within a container where a bind mount is located can change a host's file system directly, meaning that a process within a docker container could create, modify, or delete important system files or directories on the host computer. As you can probably imagine, that can lead to some serious security concerns. In general, you should use `volumes` when possible

### Bind Mount Summary

Bind Mounts can be stored anywhere on the container host and mounted on a running container:

* Basically just two paths (one in the container, and one on the host) will be pointing to the same file(s)

* Can't use Bind Mount in a Dockerfile, must be mounted at container run

```zsh
docker container run -p 80:80 -d --name custom_nginx --mount type=bind,source="$(pwd)",target="/usr/share/nginx/html" nginx
```

```zsh
docker container ls
```

```zsh
docker container run -p 8080:80 --name nginx -d ndinx
```

```zsh
docker container ls
```

Check file system of both containers and confirm both file systems are different

```zsh
docker container exec -it nginx bash
```

```zsh
docker container exec -it custom_nginx bash
```
