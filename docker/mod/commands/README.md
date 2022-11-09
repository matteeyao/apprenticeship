# Perform a clean restart of a Docker instance:

1. Stop the container(s) using the following command:

```zsh
docker-compose down
```

2. Delete all containers using the following command:

```zsh
docker rm -f $(docker ps -a -q)
```

3. Delete all volumes using the following command:

```zsh
docker volume rm $(docker volume ls -q)
```

4. Restart the containers using the following command:

```zsh
docker-compose up -d
```


* `docker-compose run` creates a new container each time it is invoked, your changes do not persist

* If you want your changes to persist, use `docker-compose exec`, which runs your command in the running container
