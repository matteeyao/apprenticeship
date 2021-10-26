# Containers

## Overview

Docker ensures that Docker containers give us the same environment on all machines

**The definition of a container is that it is an instance of an image running as a process on your machine**

## Docker Images

All containers are started by running a **docker image**

An image is the application we want to run

Example of familiar applications we'd want to run in a container are `Node`, `Ruby`, or `Postgres`

A docker image will generally consist of a collection of files, libraries, and dependencies

## LifeCycle of a Docker Container

Docker containers are prepared to die at any time: you can stop, kill and destroy them quickly

When you do kill a container, all data created during its existence is wiped out by default

It is in this sense that we could say containers, are ephemeral

By "ephemeral", we mean that a container can be stopped and destroyed, then rebuilt and replaced w/ an absolute minimum setup and configuration

Containers are perfect for temporal tasks and they can also perfectly run long-running daemons like web servers

By default, all containers are created equal; they all get the same proportion of CPU cycles and block IO, and they could use as much memory as they need

Here is a visualization of the Docker Container Lifecycle:

![Docker Container LifeCycle](../../img/docker-container-lifecycle.png)

## Running a Container

Containers are runtime environments

You will run one main process in one Docker container

So you can think of this one Docker container doing one job for your project

For example, if you have a full stack application you want to run on your computer, you might have one container to host your `Mongo` database, another to host your `Node` server, and then one more to run your `React` code

And that's it - running those containers would allow you to run a full stack app

Let's talk about how to run a simple container

We'll be working a lot w/ the `nginx` image in the future so we'll use that as an example

Nginx is a very popular open source high-performance HTTP server

Nginx is also very easy to setup w/ Docker so that's an extra

```
docker container run -d -p 8080:80 --name web nginx
```

Let's break this command down into its separate parts:

1. `docker container run` is the command telling docker you want to start up a new container using the following options

2. `-d` → this flag means starting up the container in `detached` mode. Containers started in detached mode exit when the root process used to run the container exits

3. `-p 8080:80` → this lets docker know that you want to expose a port on your local machine and that any traffic on that port should route to the container IP

    * The internal host IP is on the left: `8080`

    * The IP for the container is on the right: `80`

    * W/ this configuration you can go to `http://localhost:8080` and see your container running

4. `--name web` → the name flag allows you to directly name a container

5. `nginx` → the final part of this command is the image we want to use for running this container

## What happens when you start a container

When a Docker container starts up several things happen

Using our last example from above we are going to break down each of those processes

```
docker container run -d -p 8080:80 --name web nginx
```

The container start up does the following:

1. Docker looks for the image locally (in this case `nginx`) in your image cache

2. If it's not found locally then Docker looks into the remote image repository (which defaults to Docker Hub)

3. Docker then downloads the latest version of the image (`nginx:latest` by default)

4. A new container is created based on that image and prepares to start

5. Docker gives the container the virtual IP on a private network inside of the Docker Engine

6. Docker opens up port `8080` (the `-p` flag) on localhost and forwards any traffic to port `80` in the container

7. Starts the container by using the CMD in the image's `Dockerfile`
