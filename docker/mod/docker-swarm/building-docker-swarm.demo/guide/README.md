# Building a Docker Swarm

## Introduction

Docker swarm allows you to quickly move beyond simply using Docker to run containers. With swarm, you can easily set up a cluster of Docker servers capable of providing useful orchestration features. This lab will allow you to become familiar with the process of setting up a simple swarm cluster on a set of servers. You will configure a swarm master and two worker nodes, forming a working swarm cluster.

## Instructions

Your company is ready to move forward with using Docker to run their applications. However, they have some complex container apps that can take advantage of the cluster management and orchestration features of Docker swarm. You have been asked to stand up a simple Docker swarm cluster to be used for some initial testing. A set of servers has already been provisioned for this purpose. The swarm cluster should meet the following criteria:

* One Swarm manager.

* Two worker nodes.

* All nodes should use Docker CE version `5:18.09.5~3-0~ubuntu-bionic`.

* Both worker nodes should be joined to the cluster.

* `cloud_user` should be able to run docker commands on all three servers.

## Solution

Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

## Install Docker CE on all three nodes

1. On all three servers, install Docker CE.

```
sudo apt-get update

sudo apt-get -y install \
  apt-transport-https \
  ca-certificates \
  curl \
  gnupg-agent \
  software-properties-common

curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -

sudo add-apt-repository \
   "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
   $(lsb_release -cs) \
   stable"

sudo apt-get update

sudo apt-get install -y docker-ce=5:18.09.5~3-0~ubuntu-bionic docker-ce-cli=5:18.09.5~3-0~ubuntu-bionic containerd.io
```

2. Add `cloud_user` to the Docker group so that you can run docker commands as `cloud_user`.

```
sudo usermod -a -G docker cloud_user
```

Log out each server, then log back in.

3. You can verify the installation on each server like so:

```
docker version
```

## Configure the swarm manager

1. On the swarm manager server, initialize the swarm. Be sure to replace `<SWARM_MANAGER_PRIVATE_IP>` in this command with the actual Private IP of the swarm manager (NOT the public IP).

```
docker swarm init --advertise-addr <SWARM_MANAGER_PRIVATE_IP>
```

## Join the worker nodes to the cluster

1. On the swarm manager, get a join command with a token:

```
docker swarm join-token worker
```

This should provide a command that begins `docker swarm join ...`. Copy that command and run it on both worker servers.

2. Go back to the swarm manager and list the nodes.

```
docker node ls
```

Verify that you can see all three servers listed (including the manager). All three should have a status of `READY`. Once all three servers are ready, you have built your own Docker swarm cluster.
