# Container Management

### Stop and restart a container:

```zsh
docker stop <CONTAINER_NAME>
docker restart <CONTAINER_NAME>
```

### Remove a container:

```zsh
docker rm <CONTAINER_NAME>
```

### Remove all stopped containers:

```zsh
docker container prune
```

### Rename a container:

```zsh
docker rename <CONTAINER_NAME> <NEW_CONTAINER_NAME>
```

### View container metrics:

```zsh
docker stats [<CONTAINER_NAME>]
```
