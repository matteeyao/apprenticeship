# Security in Docker

* Imaging a host w/ Docker installed on it; this host has a set of its own processes running such as a number of operating system processes, the docker daemon itself, the SSH server etc.

* Let's run an Ubuntu docker container that runs a process that sleeps for an hour:

```zsh
docker run ubuntu sleep 3600
```

* We've learned that unlike virtual machines, containers are not completely isolated from their host

  * Containers and the hosts share the same kernel

  * Containers are isolated using namespaces in Linux

  * The host has a namespace and the containers have their own namespace

  * All the processes run by the containers are in fact run on the host itself, but in their own namespaces

* As far as the docker container is concerned, it is in its own namespace and it can see its own processes only, unable to see anything outside of it or in any other namespace

  * So when you list the processes from within the docker container, you should see the sleep process w/ a process ID of 1

```zsh
ps aux

USER               PID  %CPU %MEM      VSZ    RSS   TTY  STAT STARTED      TIME COMMAND
root                 1   0.0  0.0     4528    828   ?    Ss   03:06        0:00 sleep 3600
```

* For the docker host, all processes of its own as well as those in the child namespaces are visible as just another process in the system

  * So when you list the processes on the host, you see a list of processes including the sleep command, but w/ a different process ID

  * This is b/c the processes can have different process IDs in different namespaces and that's how Docker isolates containers within a system ▶︎ process isolation

## Users

* Let us now look at users in context of security

  * The docker host has a set of users, a root user as well as a number of non-root users

  * By default, Docker runs processes within containers as the root user

  * This can be seen in the output of the commands we ran earlier

  * Both within the container and outside the container on the host, the process is run as the root user

  * Now if you do not want the process within the container to run as the root user, you may set the user using the user option w/ the docker run command and specify the new user ID

  * You will see that the process now runs w/ the new user id

* Another way to enforce user security is to have this defined in the Docker image itself at the time of creation

  * For example, we will use the default ubuntu image and set the user ID to 1000 using the USER instruction

```Dockerfile
FROM ubuntu

USER 1001
```

  * Then build the custom image:

```zsh
docker build -t my-ubuntu-image .
```

  * We can now run this image w/o specifying the user ID and the process will be run w/ the user ID 1000

```zsh
docker run my-ubuntu-image sleep 3600
```

```zsh
ps aux

USER               PID  %CPU %MEM      VSZ    RSS   TTY  STAT STARTED      TIME COMMAND
root                 1   0.0  0.0     4528    828   ?    Ss   03:06        0:00 sleep 3600
```

* What happens when you run containers as the root user?

  * Is the root user within the container the same as the root user on the host?

  * Can the process inside the container do anything that the root user can do on the system?

  * If so, isn't that dangerous?

    * Well, Docker implements a set of security features that limits the abilities of the root user within the container

    * So the root user within the container isn't really like the root user on the host

## Linux capabilities

* Docker uses Linux capabilities to implement this

  * As we all know the root user is the most powerful usr on a system

  * The root user can literally do anything, and so does a process run by the root user; the process has unrestricted access to the system

  * From modifying files and permissions on files, Access Control, creating or killing processes, setting group ID or user ID, performing network related operations such as binding to network ports, broadcasting on a network, controlling network ports; system related operation such as rebooting the host, manipulating system clock and many more

  * All of these are the different capabilities on a Linux system and you can see a full list at this location

  * You can now control and limit what capabilities are made available to a process

* By default, Docker runs a container w/ a limited set of capabilities

  * And so the processes running within the container do not have the privileges to say, reboot the host or perform operations that can disrupt the host or other containers running on the same host

  * In the case that you wish to override this behavior and enable all privileges to the container, use the privileged flag

* If you wish to override this behavior and provide additional privileges than what is available, use the cap-add option in the `docker run` command:

```zsh
docker run --cap-add MAC_ADMIN ubuntu
```

* Similarly, you can drop privileges as well using the cap-drop option:

```zsh
docker run --cap-drop KILL ubuntu
```

* In the case that you wish to run the container w/ all privileges enabled, use the privileged flag

```zsh
docker run --privileged ubuntu
```
