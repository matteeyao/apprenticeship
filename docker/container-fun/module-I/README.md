# Let's run some containers

For the first phase of this project we are just going to get comfortable running and monitoring multiple containers at once. For this project you'll be writing **long** container commands. We recommend having a text file open for formulating your container commands before you input them into the terminal

We'll start by creating three containers. For each of the following containers make sure you are running them detached using `--detach` or `-d` and naming each of them w/ `--name`. Name each container w/ the image it is running (it's easier to keep track that way). **Recall** that containers cannot listen on the same local ports.

1. **Run one container with the `nginx` image****:

* The `Nginx` image provides an open source and easy to use proxy server.

* Have this container listening on `80:80`

```zsh
docker container run --name nginx -d -p 80:80 nginx
```

2. **Run one container with the `httpd (apache)` image**:

* The `httpd` is an image that provides a popular HTTP server

* This image has an exposed port available within the image, and you can find it yourself in the image's Dockerfile

    * Start by looking at the [httpd](https://hub.docker.com/_/httpd) image on Docker Hub. There you will find and click a link to the Dockerfile for the latest supported version of this image (which will be tagged `latest`)

    * Once you've followed the link you will be viewing the Dockerfile, but what you are specifically looking for in this file is the command `EXPOSE`. This is where you find the port that `httpd` is listening for internally

    * Once you've found the port `httpd` exposes internally set up your container to run using the `-p` flag w/ a localhost port on the left and the exposed `httpd` internal port on the right

```zsh
docker container run --name httpd -d -p 8080:80 httpd
```

3. **Run one container w/ the `mysql` image**:

* Have this container publishing the ports for `3306:3306`

* One of the common environmental flag arguments passed to images of databases is the flag to set a password

    * For this exercise, you'll use the `--environment` or `-e` flag and pass in the password you'd like `mysql` to use when it sets itself up `MYSQL_ROOT_PASSWORD=<your-password>`

```zsh
docker container run --name mysql -d -p 3306:3306 --environment MYSQL_ROOT_PASSWORD=my-secret-pw mysql
```

You can inspect your new mysql container to make sure your password was configured properly by using `docker container inspect mysql` and seeing the password you set under the "Env" key

The `nginx` and `httpd` images are built so that if you travel to the port you exposed on your local machine you'll be able to see a response. CHeck that your `nginx` container is running properly by doing either of the following:

1. Running the `curl` command by using the command: `curl localhost:80` in your terminal

2. Using your browser to navigate to `http://localhost:80`

Do the same for `httpd` on whatever local port you chose to expose. You should see a message from both of those ports and therefore you'll know your containers are running!

When you run `docker container ls -a` you should see something like this:

```zsh
CONTAINER ID        IMAGE               COMMAND                  CREATED              STATUS              PORTS                               NAMES
0edb7e43d044        mysql               "docker-entrypoint.s…"   5 seconds ago        Up 4 seconds        0.0.0.0:3306->3306/tcp, 33060/tcp   mysql
d558d946c6a0        httpd               "httpd-foreground"       About a minute ago   Up About a minute   0.0.0.0:8080->80/tcp                httpd
4b76779e1da6        nginx               "nginx -g 'daemon of…"   About a minute ago   Up About a minute   0.0.0.0:80->80/tcp                  nginx
```

## Time to Clean Up

Now let's clean all those containers up w/ `docker container stop` and `docker container rm` (both can accept multiple container names or container `ID`s). Use `docker container ls -a` to ensure all your containers have been stopped and removed

Amazing! Now let's see a little more of what containers can do!

## Solution

The Commands to create the three containers are as follows:

1. **Run one container with the `nginx` image**:

```zsh
docker container run --name nginx -d -p 80:80 nginx
```

2. **Run one container with the `httpd (apache)` image**:

```zsh
docker container run --name httpd -d -p 8080:80 httpd
```

3. **Run one container w/ the `mysql` image**:

```zsh
docker container run --name mysql -d -p 3306:3306 --environment MYSQL_ROOT_PASSWORD=my-secret-pw mysql
```
