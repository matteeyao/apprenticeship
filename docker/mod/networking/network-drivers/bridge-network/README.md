# Creating a Bridge Network

You can create and manage your own networks w/ the `docker network` commands. If you do not specify a network driver, bridge will be used.

Bridge is the default driver, so any network that is created w/o specifying the driver will be a bridge network.

**Create a bridge network:**

```zsh
docker network create <NETWORK>
```

**Run a new container, connecting it to the specified network:**

```zsh
docker run --network <NETWORK>
```

```zsh
docker network connect <NETWORK> <CONTAINER> 
```

## Demonstration

```zsh
docker network create my-net
```

Specify `--network` tag to specify which network to connect container to:

```zsh
docker run -d --name my-net-busybox --network my-net radial/busyboxplus:curl sleep 3600
```

Run Nginx container as well:

```zsh
docker run -d --name my-net-nginx nginx
```

Connect Nginx container to `my-net` network:

```zsh
docker network connect my-net my-net-nginx
```

```zsh
docker exec my-net-busybox curl my-net-nginx:80
```

## Embedded DNS

Docker networks implement an **embedded DNS** server, allowing containers and services to locate and communicate w/ one another.

Containers can communicate w/ other containers and services using the service or container `name`, or `network alias`.

**Provide a network alias to a container:**

```zsh
docker run --network-alias <ALIAS>

docker network connect --alias <ALIAS>
```

By default, container and services on the same network can communicate with each other simply using their container or service names. Docker provides DNS resolution on the network that allows this to work. We can supply a network alias to provide an additional name by which a container or service is reached.

For example:

```zsh
docker run -d --name my-net-nginx2 --network my-net --network-alias my-nginx-alias nginx

docker exec my-net-busybox curl my-net-nginx2:80
```

or use alias:

```zsh
dockeer exec my-net-busybox curl my-nginx-alias:80
```

```zsh
docker run -d --name my-net-nginx3 nginx
```

```zsh
docker network connect --alias another-alias my-net my-net-nginx3
```

```zsh
docker exec my-net-busybox curl another-alias:80
```

## Managing Networks

**List networks:**

```zsh
docker network ls
```

**Inspects a network:**

```zsh
docker network inspect <NETWORK>
```

**Connects a container to a network:**

```zsh
docker network connect <CONTAINER> <NETWORK>
```

**Disconnects a running container from an existing network:**

```zsh
docker network disconnect <CONTAINER> <NETWORK>
```

**Deletes a network:**

```zsh
docker network rm <NETWORK>
```
