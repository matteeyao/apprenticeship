# Container Networking in Docker

## Docker Networks

So we've discussed how every container should run one process, and one process only. How do we get containers to talk to each other though? Say your app is running in one container but your database is running in another. How do you connect these two containers so they can exchange information? Turn the spotlight onto **Docker networks**

One of the reasons Docker containers and services are so powerful is that you can connect them together, or connect them to non-Docker workloads Docker containers and services do not even need to be aware that they are deployed on Docker, or whether their peers are also Docker workloads or not

A very high-level overview of network usage looks like this: you create a new network, which creates a subnet for this network alone. You start a container and attach it to this network, and all containers attached to the same network will be able to interact w/ each other, as if they were on a LAN. Then you can connect from one process running in one container to a process running in another container, as long as they are on the same network

## Default Bridge Network

You've used Docker networks if you've booted up even one container. Every container that is started up is connected to a private virtual network called `bridge`. You can see a list of your current Docker networks by using `docker network ls`. You can additionally see information about the network, including the containers connected to it, by using the `docker network inspect <NETWORKNAME>` command.

The `bridge` driver creates a private network internal to the host so containers on this network can communicate. External access to a container is granted by exposing ports of containers. Docker secures the network by managing rules that block connectivity between different Docker networks. Bridge networks apply to containers running on the same Docker daemon host.

Behind the scenes, the Docker Engine creates the necessary Linux bridges, internal interfaces, `iptables` rules, and host routes to make this connectivity possible. In the example highlighted below, a Docker bridge network is created and two containers are attached to it. W/ no extra configuration, the Docker Engine does the necessary wiring, provides service discovery for the containers, and configures security rules to prevent communication to other networks. A built-in `IPAM` driver provides the container interfaces w/ private IP addresses from the subnet of the bridge network

If you'd like to read more about how Docker container networks are implemented and scalable we recommend checking out the [docs](https://success.docker.com/article/networking)

For our purposes the main thing to remember is that when your containers are on the same virtual network you don't actually need to expose a port, (specify the `-p` command), in order for those containers to talk to each other. You can additionally have docker containers attached to more than one virtual network

## User defined Networks

**User-defined bridge networks are superior to the default bridge network**. You should always be creating your own network whenever you are running multiple containers. There are several reasons to always create your own network:

1. User-defined bridges provide better isolation

    * Containers connected to the same user-defined bridge network automatically expose all ports to each other, and no ports to the outside world

    * Allowing containerized applications to communicate w/ each other w/o opening access to the outside world

2. User-defined bridges provide automatic DNS resolution btwn containers

3. Containers can be attached and detached from user-defined networks painlessly

4. Each user-defined network creates a configurable bridge

5. Linked containers on the default bridge network share environment variables

Here is an excellent example by Mark Church:

```zsh
docker network create --driver bridge mybridge
docker container run -d --net mybridge --name db redis
docker container run -d --net mybridge -e DB=db -p 8000:5000 --name web chrch/web
```

![Docker network](../../../img/docker-network.png)

A network is created and then two containers are connected to that network. There is one exposed local port on 8000, and all traffic that hits that port will be routed into the container on port 5000. The two containers can still interact w/ each other through the network, and the `db` container never has to expose sensitive data by exposing a port

## Drivers

For our purposes we will only be using the `bridge` driver, but here is a brief overview of other Docker network drivers

1. `bridge`: is the default network driver that all containers are built on. If you don't specify a driver, this is the type of network you will be creating. Bridge networks are best when you need multiple containers to communicate on the same Docker host

2. `overlay`: overlay networks connect multiple Docker daemons together and enable Docker Swarm services to communicate w/ each other

3. `host`: this driver is for standalone containers using Docker Swarm. The `host` driver will remove network isolation btwn the container and the Docker host, and use the host's networking directly

4. `macvlan`: Macvlan networks allow you to assign a MAC address to a container, making it appear as a physical device on your network

5. `none`: for this container, disable all networking

## Docker Networking Demo

```zsh
docker network ls
```

Create a custom network in Docker, allowing multiple containers to have DNS resolution, allowing each to talk to each other by name and through the same network w/o ever having to expose a port:

```zsh
docker network create <NAMEOFNETWORK>
```

Create a new container:

```zsh
docker container run --name nginx -d --net <NAMEOFNETWORK> nginx:alpine
```

`nginx` above is the name of the container and can be replaced w/ any name

Check your container is running:

```zsh
docker container ls
```

```zsh
docker container inspect <CONTAINERID>
```

Let's create another container:

```zsh
docker container run --name nginx2 -d --net <NAMEOFNETWORK> nginx:alpine
```

`nginx2` above is the name of the container and can be replaced w/ any name

To have both containers communicate w/ each other:

```zsh
docker container exec -it nginx ping nginx2
```

or

```zsh
docker container exec -it nginx2 ping nginx
```
