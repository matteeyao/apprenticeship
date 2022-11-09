# Installing Docker

## Introduction

Your development team has asked for a cloud server with Docker installed. To meet this requirement you need to configure the Docker repository in your environment and create a new container called "Hello World" using the Centos:6 images.

Reminder: `cat /etc/issue` will tell you the host operating system.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

## Confirm Packages are Installed

1. Install the packages:

```
sudo yum install -y yum-utils lvm2 device-mapper-persistent-data
```

2. Alternatively, if the packages are likely installed, the following command can be used to verify the installation without installing the package if one is missing. Each of these commands must be run and verified individually:

```
rpm -qa |grep yum-utils

rpm -qa |grep lvm2

rpm -qa |grep device-mapper-persistent-data
```

## Add the Repository

1. Add the repository:

```
sudo yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
```

2. Confirm the repository has been added by checking the repo directory:

```
ls /etc/yum.repos.d/
```

## Install Docker

1. Install Docker:

```
sudo yum install docker-ce
```

...or...

```
sudo yum install docker-ce -y
```

## Enable and Start Docker

1. Enable Docker:

```
sudo systemctl enable docker
```

2. Start Docker:

```
sudo systemctl start docker
```

## Add Your User to the Docker Group

1. Add your user to the Docker group:

```
sudo usermod -a -G docker cloud_user
```

2. Verify the addition:

```
grep docker /etc/group
```

3. Log out of the system and back in to confirm the changes.

## Create Your First Container

1. Create the hello-world container:

```
docker run hello-world
```

...or...

```
sudo docker run hello-world
```
