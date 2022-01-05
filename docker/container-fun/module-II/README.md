# The Shell Within

Looking at a container from the outside can be interesting but by now you must be wondering if it's possible to see what is happening *inside* a container? This is totally possible using the `Docker CLI` (Command Line Interface)

To enter a container you'll write something like the following:

```zsh
docker container run -it <IMAGENAME> <ARG>
```

The `-it` is actually two separate flags you are adding to the `docker container run` command:

* `-t` simulates a terminal (like what SSH does)

* `-i` keeps a session open to receive terminal input

* the `<ARG>` part of the command is where we can pass an argument for what we'd like this container to do

## Phase 1: Interacting w/ the Shell

The `nginx` image comes w/ `bash` as part of the image, meaning that if you start a container using `nginx` as the image and hand it the argument of `bash` to a shell inside the container, run the following command in your terminal to enter the container:

```zsh
docker container run -it --name web nginx bash
```

Bam, you are inside a container. You'll see something like this prompt:

```zsh
root@da9a8ab14300:/#
```

This doesn't mean you are the root of your OS, but rather at the root of the container. You'll see that you can `ls` and do many of the things you could do within a shell normally like update configuration files or download packages from the internet

To exit this shell (and container) you can use the `exit` command. This will stop your container b/c your **containers will only run as long as the command that it ran on startup runs**. To get around this you can use the `docker container exec` command to start a container that will persist past when the startup command has finished

You can see your stopped container still exists by running `docker container ls -a`

You can restart the container: `docker container start web`, which will restart your container in the background, and then run: `docker container exec -it web bash`. Okay you are back in bash now, try to `exit` again. Now check out the running containers by using `docker container ls` and you'll see your `web` container still running! The `exec` command is what allowed you to exit the container's `bash` command while keeping the container running

## Phase 2: Ubuntu

Now let's try using a shell to interact w/ a container. Create a new container named `ubuntu` using `ubuntu` as the image, and this time let's try installing something. 

```zsh
docker container run -it --name ubuntu ubuntu bash
```

Once you have created your container and are in the `bash` shell:

1. Update the built-in package manager for ubuntu using the command `apt-get update`

2. Then download the package `curl` by running: `apt-get install -y curl`

3. Make sure `curl` works by testing the following: `curl parrot.live`

```zsh
apt-get update apt-get install -y curl curl parrot.live
```

4. Exit the shell and make sure it is no longer running by using the command `docker container ls`

```zsh
exit docker container rm ubuntu
```

Now at this point if you started up that container you were just interacting w/ it would still have `curl` installed. But what would happen if you started another container using the `ubuntu` image?

Try running: 

```zsh
docker container run -it --name notliketheother ubuntu bash
```

What happens if you try to `curl` something from this container? This `notliketheother` container doesn't have `curl` installed. So though there are two containers running the same image, you can alter the image in one container w/o effecting the other.

> [!IMPORTANT]
> Using the Ubuntu image versus the Whole Ubuntu OS? If you have Linux experience, or are currently running Docker through a Linux distribution, you might be asking what happens when you run a Ubuntu container? How is it different from the Ubuntu OS already running on your computer? If you run the `docker image ls` command you can see that the "distribution based images" like Ubuntu, Debian, CentOS, Alpine, etc. are all **very** small, at most a few hundred MB. These images are not full OS's but just the base utilities that you would expect if you were running that full OS. Mostly they are just used as the image you are building `FROM` in a Dockerfile. W'll talk about more about Dockerfiles soon. What is important to know is that these "distribution based images" are used commonly so that you can use the built in package managers (`apt`, `yum`, or `apk`) and get the same package versions you'd expect if you were using the full OS.

## Solution

Start up your Ubuntu Shell:

```zsh
docker container run -it --name ubuntu ubuntu bash
```

Install Curl and Test:

```zsh
apt-get update apt-get install -y curl curl parrot.live
```

Exit Out and Remove:

```zsh
exit docker container rm ubuntu
```
