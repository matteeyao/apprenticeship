# Dockerfile

A **Dockerfile** is analogous to a recipe, specifying the required ingredients for an application. Let's take a simple example of a nodejs application whose Dockerfile looks as follows:

```Dockerfile
FROM node:boron

# Create app directory
WORKDIR /home/code

# Install app dependencies
RUN npm install

EXPOSE 8080

CMD [ "npm", "start" ]
```

Notice line number 9 which says `EXPOSE 8080`

Definition from the official docs for the [EXPOSE instruction](https://docs.docker.com/engine/reference/builder/#expose) says:

> The EXPOSE `instruction` informs Docker that the container listens on the specified network ports at runtime. EXPOSE does not make the ports of the container accessible to the host.

Wait! What? The container listens on the network port and is not accessible to the host? What does this even mean?

## A simpler explanation

The `EXPOSE` instruction exposes the specified port and makes it available only for inter-container communication. Let's understand this w/ the help of an example:

Let's say we have two containers, a nodejs application and a redis server. Our node app needs to communicate w/ the redis server for several reasons →

For the node app to be able to talk to the redis server, the redis container needs to expose the port. Have a look at the [Dockerfile](https://github.com/docker-library/redis/blob/ebde981d2737c5a618481f766d253afff13aeb9f/3.2/32bit/Dockerfile#L81) of the [official redis image](https://hub.docker.com/_/redis/) and you will see a line saying `EXPOSE 6379`. This is what helps the two containers to communicate w/ each other

So when your nodejs app container tries to connect to port 6379 of the redis container, the `EXPOSE` instruction is what makes this possible

> Note: For the node app server to be able to communicate w/ the redis container, it's important that both the containers are running in the same docker network

## Binding the container port w/ the host

So `EXPOSE` helps in inter-container communication. What if there's a need to bind the port of the container w/ that of the host machine on which the container is running?

Pass the `-p` (lower case p) as a option to the `docker run` instruction as follows:

```zsh
docker run -p <HOST_PORT>:<CONTAINER:PORT>IMAGE_NAME
```

Find out more about this in the [official documentation](https://docs.docker.com/engine/reference/run/#expose-incoming-ports)

## Final thoughts

We don't think there's any need to use the `EXPOSE` instruction in your own Dockerfile if the docker is simply a web app. You might want to use it when you are distributing your Dockerfile to others and letting them connect to the application

One benefit of using the `EXPOSE` instruction, whether you are running a multi-container application or not, is that it helps others understand what port the application listens by just reading the Dockerfile w/o the need of going through your code

You shouldn't be worrying about it unless you are running a multi-container application on the same machine. If at all you need inter-container communication, go through the [docker networking documentation](https://docs.docker.com/engine/userguide/networking/)

If you find the docker networking concept a bit complex, don't worry about it, there's [`docker compose`](https://docs.docker.com/compose/) to your rescue. **Docker compose handles the network layer for you** by itself and allows you to refer other containers by the service name mentioned in the `docker-compose.yml` file
