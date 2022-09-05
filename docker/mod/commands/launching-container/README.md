# Docker CLI Commands

## Launching a Container

### Run a container:

```zsh
docker run <IMAGE>
```

### Run container and drop to shell:

```zsh
docker run -it <IMAGE>
docker run --interactive --tty <IMAGE>
```

```zsh
docker run -it --name <CONTAINER_NAME> <IMAGE>
```

### Container restart settings:

```zsh
docker run --restart (always|no|on-failure[:maxretries]|unless-stopped) <IMAGE>
```

### Remove container when exited

```zsh
docker run --rm <image>
```

```zsh
docker run -it --name <CONTAINER_NAME> --rm <IMAGE>
```

### Run container in background:

```zsh
docker run -d <IMAGE>
```

* Set container to detach or run in the background. The `t` flag will ensure that the running container has a persistent IP address.

```zsh
docker run -dt --restart <always / unless-stopped / on-failure / no> --name <CONTAINER_NAME> <IMAGE>
```

### List running containers:

```zsh
docker container ls
```

* List running containers, including those in a stopped state:

```zsh
docker container ls -a
```
