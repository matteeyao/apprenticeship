# Exposing Containers Externally

You can expose containers by publishing ports. This maps a port on the host (or all swarm hosts in the case of a Docker swarm) to a port within the container.

Publish a port on the host, mapping it to a port on the container:

```zsh
docker run -p <HOST_PORT>:<CONTAINER_PORT>
```

Publish a port on all swarm hosts, mapping it to a port on the service's containers:

```zsh
docker service create -p <HOST_PORT>:<SERVICE_PORT>
```

### Examples and Usages

Publish a port on a container. The host port is the port that will listen on the host. Requests to that port on the host will be forwarded to the <CONTAINER_PORT> inside the container.

```zsh
docker run -d -p <HOST_PORT>:<CONTAINER_PORT> IMAGE
```

`-P`, `--publish-all`: Publish all ports documented using `EXPOSE` for the image. These ports are all published to random ports.

For example, let's publish a port for simple Nginx container:

```zsh
docker run -d -p 8080:80 --name nginx_pub nginx
```

```zsh
curl localhost:80
```

## Examining Published Ports

You can display port mappings for a container using the `docker port` command.

List port mappings for a container:

```zsh
docker port <CONTAINER>
```

Also displays some port information:

```zsh
docker ps
```

### Examples and Usages

Examine the existing published ports for an existing container:

```zsh
docker port nginx_pub
```

You'll see that internal port `80` is being published on port 8080, on the host.

You can also see that information w/ `docker ps`.

## Hosts versus Ingress

We can also publish ports for services. By default, the routing mesh causes the host port to listen on all nodes in the cluster. Requests can be forwarded to any task, even if they are on another node.

```zsh
docker service create -p <HOST_PORT>:<CONTAINER_PORT> <IMAGE>
```

Docker swarm supports two modes for publishing ports for exposing services externally.

### Ingress

* The default, used if no mode is specified.

* Uses a **routing mesh**. The published port listens on every node in the cluster, and transparently directs incoming traffic to any task that is part of the service, on any node.

For example,

```zsh
docker service create -p 8081:80 --name nginx_ingress_pub nginx
```

`curl` to view response from the server:

```zsh
curl localhost:8081
```

```zsh
docker service ps nginx_ingress_pub
```

### Host

* Publishes the port directly on the host where a task is running.

* Cannot have multiple replicas on the same node if you use a static port.

* Traffic to the published port on the node goes directly to the task running on that specific node.

Publish a service port using host mode:

```zsh
docker service create --publish mode=host, target=80, published=8080
```

We can publish service ports in host mode. This mode is much more restrictive. It does not use a routing mesh. Requests to the host port on a host are forwarded to the task running on that same host. Therefore, in this mode, we cannot have more than one task for the service per host, and hosts that are not running a task for the service won't listen on the host port.

```zsh
docker service create -p mode=host,published=<HOST_PORT>,target=<CONTAINER_PORT> <IMAGE>
```

For example,

```zsh
docker service create -p mode=host, published=8082, target=80 --name nginx_host_pub nginx
```

```zsh
docker service ps nginx_host_pub
```

```zsh
curl localhost:8082
```
