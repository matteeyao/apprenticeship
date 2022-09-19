# Redirection

## Docker Container Ports

View running processes:

```
docker container ls
```

```
docker container ls -a
```

```
docker image ls
```

```
docker container run -d nginx
```

```
docker container ls
```

```
docker image history <IMAGE_NAME>
```

```
docker container ls
```

```
docker container inspect <IMAGE_ID> | grep IPAdd
```

```
elinks <IPAddress>
```

```
docker container run -d -P nginx
```

```
docker container ls
```

```
docker container run -d -p 80:80 httpd
```

```
curl -4 icanhazip.com
```

```
elinks 54.67.14.33
```

```
docker container ls
```

## Docker Container Volumes

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
docker volume ls
```

```
docker volume create devvolume
```

```
docker volume ls
```

```
docker volume inspect <VOLUME_NAME> | devvolume
```

```
docker container run -d --name devcont --mount source=devvolume,target=/app nginx
```

```
docker container ls
```

```
docker container inspect
```

```
sudo ls /var/lib/docker/volume
```

List out information within:

```
sudo ls /var/lib/docker/volumes/devvolume/_data
```

```
docker container exec -it devcont sh
```

```
ls
echo "Hello!" >> /app/hello.txt
ls app
exit
sudo ls /var/lib/docker/volumes/devvolume/_data
```

```
docker container stop devcont
```

```
docker container rm devcont
```

```
sudo ls /var/lib/docker/volumes/devvolume/_data
```

Create new container w/ `devvolume` connected:

```
docker container run -d --name devcont2 -v devvolume:/app nginx
```

Verify container contains `hello.txt` file:

```
docker container exec -it devcont2 sh
ls
cat /app/hello.txt
echo "Goodbye!" >> /app/bye.txt
exit
```

```
sudo ls /var/lib/docker/volumes/devvolume/_data
```
