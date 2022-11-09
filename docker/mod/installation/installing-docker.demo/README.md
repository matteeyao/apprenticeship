# Installing Docker

## About this lab

![Fig. 1 Lab summary](../../img/installation.demo/diag01.png)

Your development team has asked for a cloud server with Docker installed. To meet this requirement, you need to configure the Docker repository in your environment and create a new container called `Hello World` using the Centos:6 images.

Reminder: `cat /etc/os-release` will tell you the host operating system. Also, please wait an extra minute before connecting via ssh to make sure the lab is fully provisioned.

## Learning objectives

[ ] Verify the Package Installation

[ ] Add the Repository

[ ] Install Docker

[ ] Enable and Start Docker

[ ] Add a User to the Docker Group

[ ] Create Your First Container

## Commands

```
sudo yum install -y yum-utils lvm2 device-mapper-persistent-data
```

```
rpm -qa | grep lvm2
```

```
sudo yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
```

```
ls /etc/yum.repos.d/
```

```
sudo yum install docker-ce
```

```
sudo systemctl enable docker
```

```
sudo systemctl start docker
```

```
sudo usermod -a -G docker user
```

```
grep docker /etc/group
```

```
exit
```

```
docker run hello-world
```

```
docker ps
```

```
docker ps -a
```
