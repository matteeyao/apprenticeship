# Installing Docker CE (CentOS)

To install Docker CE on CentOS, we will need to do the following:

### 1. Provision a Server

* Image: CentOS 7

* Size: Small

### 2. Install Required Packages

* Install some required packages (yum-utils, device-mapper-persistent-data, and lvm2)

```
sudo yum install -y  () \
device-mapper-persistent-data \
lvm2
```

### 3. Add the Docker Repo

* Add the Docker CE `yum` repository:

```
sudo yum-config-manager \
    --add-repo \
    https://download.docker.com/linux/centos/docker-ce.repo
```

### 4. Install Docker and Containerd packages

* Install the Docker CE packages:

```
sudo yum install -y docker-ce-18.09.5 docker-ce-cli-18.09.5 containerd.io
```

### 5. Start and Enable the Docker Service

* Start and enable the Docker service:

```
sudo systemctl start docker
sudo systemctl enable docker
```

### 6. Configure cloud_user to be able to use Docker

* Add cloud_user to the docker group, then log out and back in.

* To grant a user permission to run Docker commands, add the user to the Docker group. The user will have access to Docker after their next login.

```
sudo usermod -a -G docker <user>
```

### 7. Run a Container to test the installation

* Test the installation by running the hello-world image

* We can test our Docker installation by running a simple container. This container should output some text, and then exit.

```
docker run hello-world
```
