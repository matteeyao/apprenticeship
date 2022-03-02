# Networks

## DNS Round Robin Test

Let's utilize Docker containers and networks to create a small [`Round-robin DNS`](https://en.wikipedia.org/wiki/Round-robin_DNS), meaning that we want to have multiple containers on one network that can all respond to the same DNS address

Start off by creating a new virtual Docker network:

```zsh
docker network create funtime
```

Check out the `docker network ls` command and make sure you see your new network is listed. It should have the default [`bridge`](https://docs.docker.com/network/bridge/) driver. Before creating the next two containers you'll want to research the `--net-alias` flag to make sure both containers will respond to the same alias

Next we'll be using two containers running the `elasticsearch` image. The `elasticsearch` image provides a RESTful search engine that exposes port 9200 by default. The `elasticsearch` image is popular b/c of how easy it is to setup and use. The two things you'll need to know about the `elasticsearch` image for this exercise are:

1. On container bootup the `elasticsearch` image will randomly assign itself a new name

2. When you `curl` a container running the `elasticsearch` image, it will return information about the container-including the randomized name it previously created

Now create two `detached` (`-d`) containers on the new network you created. Both containers rill run the `elasticsearch:2` image and use `--net-alias`. Inspect one of your net containers using `docker container inspect <CONTAINER_NAME_OR_ID>`. Under the "Networks" key you can see all the information for the network this container is currently on. You should see the name of your created network here.

Run our two Containers:

```zsh
docker container run -d --net funtime --net-alias party elasticsearch:2 docker
container run -d --net funtime --net-alias party elasticsearch:2
```

Now, let's make sure your containers are setup properly

Create another container:

1. on the same network

2. w/ the `alpine` image w/ the `nslookup` command

3. finish this line by ending w/ the name of your network alias

Make sure everything is configured:

```zsh
docker container run --net funtime alpine nslookup party
```

The `alpine nslookup` command will return any IPs it finds on the network alias, and the name of the network. My network name in the below example is 'funtime', and my network alias is 'party':

```zsh
Name:      party
Address 1: 172.21.0.2 party.funtime
Address 2: 172.21.0.3 party.funtime
```

Finally run one more container - and this one will be simple. We want to make sure our two containers are responding to the same alias. To do this, we'll `curl` the port that both of the two `elasticsearch` containers are on. So our two `elasticsearch` containers expose the port 9200 but only **within** the network. Outside of the network we **can't** access these containers

So we'll create one more container to interact w/ our twin `elasticsearch` containers. Run a new container off the network you created w/ the `centos` (another Linux distribution) image and the command to `curl -s <network alias name>:9200`. Restart this last container a couple of times and check the logs for the `centos` container each time you restart. Each `elasticsearch` container will have a randomly generated "name" so as you `curl` the port they both share inside the network you should see one of the two containers responding everytime:

```zsh
docker container run --name curler --net funtime centos curl -s party:9200
```

```zsh
docker container restart curler
```

```zsh
docker container logs curler
```

## Solution

Create the network:

```zsh
docker network create funtime
```

Run our two Containers:

```zsh
docker container run -d --net funtime --net-alias party elasticsearch:2 docker
container run -d --net funtime --net-alias party elasticsearch:2
```

Make sure everything is configured:

```zsh
docker container run --net funtime alpine nslookup party
```

Now let's query them:

```zsh
docker container run --name curler --net funtime centos curl -s party:9200
```

```zsh
docker container restart curler
```

```zsh
docker container logs curler
```
