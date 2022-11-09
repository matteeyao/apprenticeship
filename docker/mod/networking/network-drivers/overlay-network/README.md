# Deploying a Service on a Docker Overlay Network

You can create a network w/ the overlay network to provide connectivity between services and containers in Docker swarm.

By default, services are attached to a default overlay network called **ingress**. You can create your own networks to provide isolated communication between services and containers.

**To create an overlay network, run:**

```zsh
docker network create --driver overlay <NETWORK_NAME>
```

**To create a service attached to the existing overlay network, enter:**

```zsh
docker service create --network <NETWORK_NAME>
```

## Implementation

Create network:

```zsh
docker network create --driver overlay --attachable my-overlay
```

Create service:

```zsh
docker service create --name overlay-service --network my-overlay --replicas 3 nginx
```

```zsh
docker run --rm --network my-overlay radial/busyboxplus:curl curl overlay-service:80
```
