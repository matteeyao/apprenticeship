# Network Troubleshooting

There are several ways you can gather information to troubleshoot networking issues.

To view container logs, enter:

```zsh
docker logs <CONTAINER>
```

To view logs for all tasks of a service, execute:

```zsh
docker service logs <SERVICE>
```

To view Docker Daemon logs, input:

```zsh
journalctl -u docker
```

...or...

```zsh
sudo journalctl -u docker
```

### Fetch container logs

First, create a container:

```zsh
docker run --name log-container busybox echo Here is my container log!
```

```zsh
docker logs log-container
```

You can do the same thing w/ services:

```zsh
docker service create --name log-svc --replicas 3 -p 8080:80 nginx
```

Generate log output by making a request to the service:

```zsh
curl localhost:8080
```

```zsh
docker service logs log-svc
```

### Docker Daemon Logs

```zsh
sudo journalctl -u docker
```

## Network Troubleshooting (cont.)

A great way to troubleshoot network issues is to run a container within the context of a Docker network. You can use it to test connectivity and gather information.

We can use the `nicolaka/netshoot` image to perform network troubleshooting. It comes packaged with a variety of useful networking-related tools.

You can run `netshoot` by using the container image `nicolaka/netshoot`.

We can inject a container into another container's networking sandbox for troubleshooting purposes:

```zsh
docker run --network container:CONTAINER_NAME nicolaka/netshoot
```

You can even run `netshoot` within the networking namespace of an existing container!

For example, let's create a network:

```zsh
docker network create custom-net
```

Attach a container to that network:

```zsh
docker run -d --name custom-net-nginx --network custom-net nginx
```

Spin up a `netshoot` container image and attach it to the network:

```zsh
docker run --rm --network custom-net nicolaka/netshoot curl custom-net-nginx:80
```

Or insert the `netshoot` container into the sandbox of another container `custom-net-nginx`:

```zsh
docker run --rm --network container:custom-net-nginx nicolaka/netshoot curl localhost:80
```

Run container w/ none network driver (completely separated and isolated in terms of network):

```zsh
docker run -d --name none-net-nginx --network none
```

Use same technique to insert another container into that sandbox even for a container that is running w/ the `none` network driver:

```zsh
docker run --rm --network contaienr:none-net-nginx nicolaka/netshoot curl localhost:80
```
