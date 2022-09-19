# Container Lifecycle

```
docker images
```

See if we have any containers running:

```
docker container ls
```

See if we have any containers running, including those in a stopped state:

```
docker container ls -a
```

```
docker container run -it ubuntu:16.04
```

In another terminal:

```
docker container ls
```

```
docker container ps -a
```

```
docker images
```

```
docker container run -it ubuntu:16.04
```

```
docker container ls -a
```

```
docker container start <CONTAINER_ID> | <CONTAINER_NAME>
```

```
docker container ls
```

```
docker attach <CONTAINER_ID>
```

```
ls
exit
```

```
docker container ls -a
```
