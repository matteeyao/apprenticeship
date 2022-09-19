# Image and Container Management

```
docker container ls
```

```
docker container ls -a
```

```
docker images
```

```
docker container rm <CONTAINER_ID> | <CONTAINER_NAME>
```

Neither containers will be on out system:

```
docker container ls -a
```

```
docker images
```

```
docker rmi <IMAGE_ID>
```

```
docker login
```

```
docker images
```

```
docker tag <IMAGE_ID> <USERNAME>/<REPO_NAME>:<TAG>
```

Verify docker image tags have been updated:

```
docker images
```

```
docker push <USERNAME>/<REPO_NAME>
```

Erase image:

```
docker rmi <IMAGE_ID>
```

```
docker images
```

```
docker pull <USERNAME>/<REPO_NAME>:<TAG>
```
