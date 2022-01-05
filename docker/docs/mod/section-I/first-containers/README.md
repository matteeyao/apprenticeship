# First Containers

## Running your first containers

Now that you are taking your first steps towards becoming a Docker master we'll start w/ the always traditional "HelloWorld"

Before starting, ensure that Docker is installed correctly and is ready to accept your commands. Type the following command in a new Terminal window:

```zsh
$ docker -v
```

We'll start off by running a container based off the `alpine` image. Alpine is a small distribution of Linux that we'll be talking about more in the future but for now we'll be using it for simple `echo` command

Use the `docker container run` command, with the `alpine` image, and the command to `echo "Hello World"`

```zsh
docker container run alpine echo "Hello World"
```

When you run the above command for the first time, you should see an output in your Terminal window similar to this:

```zsh
Unable to find image 'alpine:latest' locally
 
latest: Pulling from library/alpine
 
2fdfe1cd78c2: Pull complete
 
Digest: sha256:ccba511b...
 
Status: Downloaded newer image for alpine:latest
 
Hello World
```

That was easy! Let's try it again - run `docker container run alpine echo "Hello World"`. This time there is nothing but "Hello World" returned. That's b/c Docker now already has the image for `alpine` downloaded. You can view the image using the `docker image ls` command:

```zsh
REPOSITORY                              TAG                 IMAGE ID            CREATED             SIZE
alpine                                  latest              caf27325b298        4 weeks ago         5.53MB
```

Now use `docker container ls` to get a list of any containers that are currently running. You won't see much, b/c there aren't any containers running. However you can view stopped and running containers using `docker container ls -a`. There you'll see:

```zsh
CONTAINER ID        IMAGE               COMMAND                  CREATED             STATUS                         PORTS                NAMES
8924d558c494        alpine              "echo 'Hello World'"     3 minutes ago       Exited (0) 3 minutes ago              optimistic_chandrasekhar
```

The `exited` status means this container is no longer running. It's always a good idea to clean up your containers you don't intend to use again. We can do that using `docker container rm <CONTAINER_ID_OR_CONTAINER_NAME>`. Once you've cleaned up that container make sure it's done by checking `docker container ls -a`

## Running a process inside a container

Let's try doing something a little more involved w/ our next container. This time we'll use another Linux distribution, `centos`, b/c it has `ping` built into the image

So you'll run a container based off the `centos` image, we'll have it ping 5 times. So set up your `docker container run` w/ the `centos` image and then add the command you'd like it to run `ping -c 5 127.0.0.1`. You should see the container `ping` 5 times before stopping:

```zsh
docker container run centos ping -c 5 127.0.0.1
```

```zsh
PING 127.0.0.1 (127.0.0.1) 56(84) bytes of data.
64 bytes from 127.0.0.1: icmp_seq=1 ttl=64 time=0.168 ms
64 bytes from 127.0.0.1: icmp_seq=2 ttl=64 time=0.110 ms
64 bytes from 127.0.0.1: icmp_seq=3 ttl=64 time=0.110 ms
64 bytes from 127.0.0.1: icmp_seq=4 ttl=64 time=0.102 ms
64 bytes from 127.0.0.1: icmp_seq=5 ttl=64 time=0.102 ms

--- 127.0.0.1 ping statistics ---
5 packets transmitted, 5 received, 0% packet loss, time 4130ms
rtt min/avg/max/mdev = 0.102/0.118/0.168/0.026 ms
```

Once the command the container was booted w/ is finished, the container automatically stopped itself. Let's clean up the stopped containers using `docker container rm <CONTAINER_ID_OR_CONTAINER_NAME>` and make sure all containers are removed using `docker container ls -a`
