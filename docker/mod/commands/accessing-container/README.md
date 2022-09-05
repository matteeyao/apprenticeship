# Accessing the Container

### Start a container that has been stopped or exited:

```zsh
docker start <CONTAINER_NAME>
```

### Run a command against container:

```zsh
docker exec <CONTAINER_NAME> <COMMAND>
```

For example, to add NGINX:

```zsh
docker exec <CONTAINER_NAME> apk add nginx
```

To view `default.conf`:

```zsh
docker exec <CONTAINER_NAME> cat /etc/nginx/conf.d/default.conf
```

### Drop into shell of container:

```zsh
docker exec -it <CONTAINER_NAME> <SHELL>
```

```zsh
docker exec -it <CONTAINER_NAME> (bash|ash)
```

### Copy files to container/copy container file to localhost:

```zsh
docker cp <SOURCE> <CONTAINER>:<DESTINATION>
docker cp <CONTAINER_NAME>:<SOURCE> <DESTINATION>
```

e.g.

```zsh
docker cp <CONTAINER_NAME>:/etc/nginx/conf.d/default.conf .
```

```zsh
docker cp default.conf <CONTAINER_NAME>:etc/nginx/conf.d/default.conf
```
