# Installing Docker CE (Ubuntu)

To install Docker CE on Ubuntu, we will need to do the following:

### 1. Provision a Server

* Image: Ubuntu 18.04 Bionic Beaver LTS

* Size: Small

### 2. Install Required Packages

```
sudo apt-get update

sudo apt-get -y install \
  apt-transport-https \
  ca-certificates \
  curl \
  gnupg-agent \
  software-properties-common
```

### 3. Add the Docker GPG Key and Repo

* Add the Docker repo's GNU Privacy Guard (GPG) key:

```
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo apt-key add -
```

* It's a good idea to verify the key fingerprint. This is an optional step, but highly recommended. We should receive an output indicating that the key was found:

```
sudo apt-key fingerprint 0EBFCD88
```

* Add the Docker Ubuntu repository:

```
sudo add-apt-repository \
     "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
     $(lsb_release -cs) \
     stable"
```

### 4. Install Docker and Containerd packages

* Install packages:

```
sudo apt-get update

sudo apt-get install -y docker-ce=5:18.09.5~3-0~ubuntu-bionic \
docker-ce-cli=5:18.09.5~3-0~ubuntu-bionic containerd.io
```

### 5. Configure cloud_user to be able to use Docker

* Add `cloud_user` to the docker group, then log out and back in.

* To provide a user with permission to run Docker commands, add the user to the Docker group. The user will have access to Docker after their next login.

```
sudo usermod -a -G docker <user>
```

### 6. Run a container to test the installation

* Test the installation by running the hello-world image. We can test our Docker installation by running a simple container. This container should output some text, and then exit.

```
docker run hello-world
```
