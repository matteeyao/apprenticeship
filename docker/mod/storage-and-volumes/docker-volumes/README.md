# Docker Volumes

There are two different types of data mounts on Docker:

* **Bind Mount**: Mounts a specific directory on the host to the container. It is useful for sharing configuration files, plus other data between the container and host.

* **Named Volume**: Mounts a directory to the container, but Docker controls the location of the volume on disk dynamically.

## Bind Mounts versus Volumes

When mounting external storage to a container, you can use either a bind mount or a volume.

### Bind Mounts

* Mount a specific path on the host machine to the container.

* Not portable, dependent on the host machine's file system and directory structure.

### Volumes

* Stores data on the host file system, but the storage location is managed by Docker.

* More portable.

* Can mount the same volume to multiple containers.

* Work in more scenarios.

## Working with Volumes

There are different syntaxes for adding bind mounts or volumes to containers:

### `-v` syntax

```
docker run -v <SOURCE>:<DESTINATION>[:OPTIONS]
```
  * `SOURCE`: If this is a volume name, it will create a volume. If this is a path, it will create a bind mount.

  * `DESTINATION`: Location to mount the data inside the container.

  * `OPTIONS`: Comma-separated list of options. For example, `ro` for read-only.

* A bind-mount. The fact that the source begins with a forward slash `/` makes this a bind mount.

```
docker run -v /opt/data:/tmp nginx
```

* A named volume. The fact that the source is just a string means that this is a volume. If no volume exists with the provided name, then it will be automatically created.

```
docker run -v my-vol:/tmp nginx
```

### `--mount` syntax

```
docker run --mount [key=value] [,key=value...]
```

  * `type`: bind (bind mount), volume, or tmpfs (temporary in-memory storage)

  * `source, src`: Volume name or bind mount path.

  * `destination, dst, target`: Path to mount inside the container.

  * `readonly`: Make the volume or bind mount read-only.

* A bind mount:

```
docker run --mount source=/opt/data,destination=/tmp nginx
```

* A named volume:

```
docker run --mount source=my-vol,destination=/tmp nginx
```

## Managing Volumes

We can mount the same volume to multiple containers, allowing them to share data. We can also create and manage volumes by themselves without running a container. Here are some common and useful commands:

### Creates a volume:

```
docker volume create <VOLUME_NAME>
```

### List current volumes:

```
docker volume ls
```

### Inspects or gets detailed information about a volume:

```
docker volume inspect <VOLUME_NAME>
```

### Deletes a volume:

```
docker volume rm <VOLUME_NAME>
```

## Walk-through

### `-mount` syntax

Create a bind mount:

```
cd ~/
mkdir message
echo Hello, World! > messag/message.txt
cat message/message.txt
docker run --mount type=bind,source=/home/cloud_user/message,destination=/root,readonly busybox cat /root/message.txt
```

Create a volume:

```
docker run --mount type=volume,source=my-volume,destination=/root busybox sh -c 'echo hello > /root/message.txt && cat /root/message.txt'
```

### `-v` syntax

Create a bind mount:

```
docker run -v /home/cloud_user/message:/root:ro busybox cat /root/message.txt
```

Create a volume:

```
docker run -v my-volume:/root busybox cat /root/message.txt
```

## Create a shared volume

```
docker run --mount type=volume,source=shared-volume,destination=/root busybox sh -c 'echo I wrote this! > /root/message.txt'
```

```
docker run --mount type=volume,source=shared-volume,destination=/anotherplace busybox cat /another/message.txt
```

## Learning summary

You can mount the same volume to multiple containers, allowing them to interact w/ one another by sharing data.

Just mount the volume to both using the same volume name:

```
docker run --name container1 --mount source=shared-vol ...
```

```
docker run --name container2 --mount source=shared-vol ...
```

You can also use `--mount` w/ services.
