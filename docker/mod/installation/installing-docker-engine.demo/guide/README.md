# Installing and Configuring the Docker Engine

## Introduction

Docker CE is a great way to get started using the Docker engine. It is free and open-source, but provides a high-quality container runtime. This lab will help you practice the steps involved in installing and configuring the Docker Engine. You will practice installing Docker CE and configuring a logging driver. This lab will help provide you with some basic insight into how the Docker Engine is installed and configured on systems in the real world.

## Instructions

Your company is ready to start using Docker on some of their servers. In order to get started, they want you to set up and configure Docker CE on a server that has already been set up. You will need to make sure that the server meets the following specifications:

* Docker CE is installed and running on the server.

* Use Docker CE version `5:18.09.5~3-0~ubuntu-bionic`.

* The user `cloud_user` has permission to run docker commands.

* The default logging driver is set to `syslog`.

## Solution

Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

### Install Docker CE on the server

1. First, set up the Docker Repository.

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
```

2. Install docker packages.

```
sudo apt-get update
sudo apt-get install -y docker-ce=5:18.09.5~3-0~ubuntu-bionic docker-ce-cli=5:18.09.5~3-0~ubuntu-bionic containerd.io
```

3. Verify that your installation is working.

```
sudo docker version
```

### Give `cloud_user` access to run docker commands

1. Add `cloud_user` to the docker group.

```
sudo usermod -a -G docker cloud_user
```

Log out of the server, then log back in.

2. Once you are logged back on, you can verify `cloud-user`'s access:

```
docker version
```

### Set the default logging driver to `syslog`

1. Edit `daemon.json`:

```
sudo vi /etc/docker/daemon.json
```

2. Add configuration to `daemon.json` to set the default logging driver.

```
{
  "log-driver": "syslog"
}
```

3. Restart docker.

```
sudo systemctl restart docker
```

4. Verify that the logging driver was set properly like so:

```
docker info | grep Logging
```

5. This command should return a line that says:

```
Logging Driver: syslog
```
