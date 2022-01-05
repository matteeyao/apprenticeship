# A beginner's guide to Docker - how to create your first Docker application

Docker allows users to create independent and isolated environments to launch and deploy applications. These environments are called containers

This will let the developer run a container on any machine

As you can see, w/ Docker, there are no more dependency or compilation problems. All you have to do is launch your container and your application will launch immediately

## But, is Docker a virtual machine?

Here is one of the most asked question about Docker. The answer is: actually, not quite.

It may look like a virtual machine at first but the functionality is not the same.

Unlike Docker, a virtual machine will include a complete operating system. It will work independently and act like a computer.

Docker will only share the resources of the host machine in order to run its environments.

![Docker versus Virtual machines](https://www.freecodecamp.org/news/content/images/2019/11/Blog.-Are-containers-..VM-Image-1-1024x435.png)

## Now let's create your first application

Now that you know what Docker is, it's time to create your first application

The purpose of this short tutorial is to create a Python program that displays a sentence. This program will have to be launched through a Dockerfile

You will see, it's not very complicated once you understand the process

> [!NOTE]
> You will not need to install Python on your computer. It will be up to the Docker environment to contain Python in order to execute your code.

## Install Docker on your machine

First, update your packages:

```zsh
$ sudo apt update
```

Next, install docker w/ `apt-get`:

```zsh
$ sudo apt install docker.io
```

Finally, verify that Docker is installed correctly:

```zsh
$ sudo docker run hello-world
```

## Create your project

To create your first Docker application, create a folder on your computer. It must contain the following two files:

* A `main.py` file (python file that will contain the code to be executed)

* A `Dockerfile` file (Docker file that will contain the necessary instructions to create the environment)

Normally you should have this folder architecture:

```
.
├── Dockerfile
└── main.py
0 directories, 2 files
```

## Edit the Python file

You can add the following code to the `main.py` file:

```py
#!/usr/bin/env python3

print("Docker is magic!")
```

Nothing exceptional, but once you see "Docker is magic!" displayed in your terminal you will know that your Docker is working

## Edit the Docker file

Same theory: the first thing to do when you want to create your Dockerfile is to ask yourself what you want to do. Our goal here is to launch Python code

To do this, our Docker must contain all the dependencies necessary to launch Python. A linux (Ubuntu) w/ Python installed on it should be enough

The first step to take when you create a Docker file is to access the DockerHub website. This site contains many pre-designed images to save you time (for example: all images for linux or code languages)

In our case, we will use 'Python'. The first result is [the official image](https://hub.docker.com/_/python) created to execute Python:

```Dockerfile
# A dockerfile must always start by importing the base image.
# We use the keyword 'FROM' to do that.
# In our example, we want import the python image.
# So we write 'python' for the image name and 'latest' for the version.
FROM python:latest

# In order to launch our python code, we must import it into our image.
# We use the keyword 'COPY' to do that.
# The first parameter 'main.py' is the name of the file on the host.
# The second parameter '/' is the path where to put the file on the image.
# Here we put the file at the image root folder.
COPY main.py /

# We need to define the command to launch when we are going to run the image.
# We use the keyword 'CMD' to do that.
# The following command will execute "python ./main.py".
CMD [ "python", "./main.py" ]
```

## Create the Docker image

Once your code is ready and the Dockerfile is written, all you have to do is create your image to contain your application:

```zsh
$ docker build -t python-test . 
```

The `-t` option allows you to define the name of your image. In our case we have chosen `python-test` but you can put whatever you want

## Run the Docker image

Once the image is created, your code is ready to be launched:

```zsh
$ docker run python-test
```

You need to put the name of your image after `docker run`

There you go, that’s it. You should normally see “Docker is magic!” displayed in your terminal

## Useful commands for Docker

* List your images:

```zsh
$ docker image ls
```

* Delete a specific image:

```zsh
$ docker image rm [image name]
```

* Delete all existing images:

```zsh
$ docker image rm $(docker images -a -q)
```

* List all existing containers (running and not running)

```zsh
$ docker ps -a
```

* Stop a specific container:

```zsh
$ docker stop [container name]
```

* Stop all running containers:

```zsh
$ docker stop $(docker ps -a -q)
```

* Delete a specific container (only if stopped):

```zsh
$ docker rm [container name]
```

* Delete all containers (only if stopped):

```zsh
$ docker rm $(docker ps -a -q)
```

* Display logs of a container:

```zsh
$ docker logs [container name]
```
