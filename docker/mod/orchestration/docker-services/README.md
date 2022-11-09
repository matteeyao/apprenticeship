# Introduction to Docker Services

**Service**: A collection of one or more replica containers running the same image in a swarm. A Service is used to run an application on a Docker Swarm. A service specifies a set of one or more replica tasks. These tasks will be distributed automatically across the nodes in the cluster and executed as **containers**.

*Task**: A replica container that is running as part of a service.

Here are some common tasks and the commands that are associated with them:

1. Create a service with:

```
docker service create <IMAGE>
```

...or...

```
docker service create [OPTIONS] <IMAGE> <ARGS>
```

* `--replica` ▶︎ Specify the number of replica tasks to create for the service.

* `--name` ▶︎ Specift a name for the service.

* `-p <PUBLISHED_PORT>:<SERVICE_PORT>` ▶︎ Publish a port so the service can be accessed externally. The port is published on every node in the swarm.

2. To provide a name for a service, we can use:

```
docker service create --name <NAME> <IMAGE>
```

For example:

```
docker service create --name nginx --replicas 3 -p 8080:80 nginx

curl localhost:8080
```

3. Set the number of replicas with:

```
docker service create --replicas <REPLICAS> <IMAGE>
```

4. Publish a port for the service. By default, the port will listen on all nodes in the cluster (workers and managers). Requests can be routed to a container on any node, regardless of which node is accessed by the client. The format will look like this:

```
docker service create -p 8080:80 <IMAGE>
```

> [!NOTE]
>
> We can use templates to pass dynamic data to certain flags when creating a service, for instance this example sets an environment variable containing the node hostname for each replica container.
> 
> The following flags accept templates:
> 
> * `--hostname`
>
> * `--mount`
>
> * `--env`

This command sets an environment variable for each container that contains the hostname of the node that container is running on:

```
docker service create --name node-hostname --replicas 3 --env
NODE_HOSTNAME="{{.Node.Hostname}}" \
nginx
```

View docker containers:

```
docker ps

docker exec <CONTAINER_ID> printenv
```

5. We can list current services in the cluster with:

```
docker service ls
```

6. We can list the tasks/containers for a service with:

```
docker service ps <SERVICE>
```

7. To inspect a service or get more information about a service:

```
docker service inspect <SERVICE>
docker service inspect --pretty <SERVICE>
```

8. Make changes to a service:

```
docker service update --replicas 2 <SERVICE>
```

9. There are two different ways to change the number of replicas for a service:

```
docker service update --replicas 2 <SERVICE>
docker service scale SERVICE=REPLICAS
```

10. Delete an existing service with:

```
docker service rm <SERVICE>
```

## Replicated vs. Global Services

Replicated services run the requested number of replica tasks across the swarm cluster:

```
docker service create --replicas 3 nginx
```

Global services run one task on each node in the cluster:

```
docker service create --mode global nginx
```

11. We can create global services. Instead of running a specific number of replicas, they run exactly one task on each node in the cluster. The command appears as:

```
docker service create --mode global <IMAGE>
```

```
docker service rm nginx
```

## Scaling Services

Scaling services means changing the number of replica tasks.

There are two ways to scale a service.

Update the service w/ a new number of replicas.

```
docker service update --replicas <REPLICAS> <SERVICE>
```

Use docker service scale.

```
docker service scale <SERVICE>=<REPLICAS>
```

e.g.

```
docker service scale nginx=4

docker service ps nginx
```
