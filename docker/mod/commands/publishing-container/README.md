# Publishing a Container

### View container information:

```zsh
docker inspect <CONTAINER_NAME>
```

```zsh
docker exec -dt <WEB_CONTAINER_NAME> nginx -g 'pid /tmp/nginx.pid; daemon off; &' 
```

```zsh
docker inspect <WEB_CONTAINER_NAME>
```

```zsh
docker inspect <WEB_CONTAINER_NAME> | grep <SEARCH_TERM>
```

e.g.

```zsh
docker inspect <WEB_CONTAINER_NAME> | grep IP
```

### Create a container image:

```zsh
docker commit <WEB_CONTAINER_NAME> web-base
```

### Map container port to host server

```zsh
docker run -p <HOST_PORT>:<CONTAINER_PORT> <CONTAINER_NAME>
```

```zsh
docker run -p 80:80 -dt --name <NEW_CONTAINER_NAME> web-base
```
