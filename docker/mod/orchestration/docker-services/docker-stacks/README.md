# Introduction to Docker Stacks

Services are capable of running a single, replicated application across nodes in the cluster, but what if you need to deploy a more complex application consisting of multiple services?

A Stack is a collection of interrelated services that can be deployed and scaled as a unit.

A Stack is a complex, multi-service application running in a swarm.

Docker stacks are similar to the multi-container applications created using Docker Compose. However, they can be scaled and executed across the swarm just like normal swarm services.

First, let's create a docker compose file:

```
vi simple-stack.yml
```

```yml
version: '3'
services:
  web:
    image: nginx
    # Publish/expose ports:
    ports:
      - "8080:80"
    # Scale service:
    deploy:
      replicas: 3
  busybox:
    image: radial/busyboxplus:curl
    command: /bin/sh -c "while true; do echo $$MESSAGE; curl web:80; sleep 10; done"
    # Environment variables:
    environment:
      - MESSAGE=Hello!
```

We can define a stack using a Docker Compose file, and then run it in the swarm with:

1. Deploy a new Stack to the cluster using a compose file:

```
docker stack deploy -c <COMPOSE_FILE> <STACK_NAME>
```

Remember that we can redeploy the stack with the same compose file to make changes to it.

2. List current stacks:

```
docker stack ls
```

3. List the tasks associated w/ a stack:

```
docker stack ps <STACK_NAME>
```

4. List the services associated w/ a stack:

```
docker stack services <STACK_NAME>
```

```
docker service logs <SERVICE_NAME>
```

5. Delete a stack with:

```
docker stack rm <STACK_NAME>
```
