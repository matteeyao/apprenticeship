# Docker Enterprise Edition

## Docker Community Edition (CE)

Free and open-source Docker engine

* All Docker engine updates

* Docker Swarm

* Orchestration

* Networking

* Security

## Docker Enterprise Edition

Licensed version (not free)

* Owned by Mirantis

* All features of CE

* Universal Control Plane (UCP) ▶︎ web interface used to help manage your infrastructure

* Docker Trusted Registry (DTR) ▶︎ Docker registry

* Vulnerability scanning ▶︎ Automatically scan Docker images for security vulnerabilities

* Federated application management ▶︎ centralized, application management

## Installing Docker EE

To install Docker EE, we will need to do the following:

* **Provision servers**: In cloud playground, create three servers w/ the **Ubuntu 18.04 Bionic Beaver LTS** image. Use the following sizes:

    * Large ▶︎ Universal Control Plane (UCP) Manager

    * Small ▶︎ UCP Worker

    * Medium ▶︎ Docker Trusted Registry (DTR)

* Install the **Mirantis Launchpad** tool.

* Configure and create the cluster ▶︎ Create a cluster configuration file and build the cluster using Launchpad.

Edit sudoers file:

```zsh
sudo visudo
```

At the bottom of the file, add:

```zsh
# ...

cloud_user ALL=(ALL) NOPASSWD: ALL
```

Next, create an SSH key. Mirantis Launchpad is going to need to be able to SSH in all 3 of these servers in order to do its work.

```zsh
ssh-keygen -t rsa

# → Your identification has been saved in /home/cloud_user/.ssh/id_rsa.
# → Your public key has been saved in /home/cloud_user/.ssh/id_rsa.pub.
```

So, the next step is to go ahead and copy that public key to all 3 of our servers so that we can use it to authenticate w/ those 3 servers, or more specifically, so that Mirantis Launchpad will be able to use it to authenticate.

```zsh
ssh-copy-id cloud_user@<UCP_MANAGER_SERVER_PRIVATE_IP_ADDRESS>

ssh-copy-id cloud_user@<UCP_WORKER_SERVER_PRIVATE_IP_ADDRESS>

ssh-copy-id cloud_user@<UCP_DOCKER_TRUSTED_REGISTRY_SERVER_PRIVATE_IP_ADDRESS>
```

Install Mirantis Launchpad:

```zsh
wget https://github.com/Mirantis/launchpad/releases/download/0.14.0/launchpad-linux-x64
```

Rename that file to `launchpad`:

```zsh
mv launchpad-linux-64 launchpad
```

Make the file `launchpad` executable:

```zsh
chmod +x launchpad
```

```zsh
./launchpad version
```

Register Docker EE cluster:

```zsh
./launchpad register
```

The way **Launchpad** works is that we create a YAML file that defines the configuration for our cluster and then we can use Launchpad to go ahead and build or modify the cluster using that YAML file.

```zsh
vi cluster.yaml
```

Build UCP cluster. Apply the configuration that we specified in our `cluster.yaml` file:

```zsh
./launchpad apply 
```

## Learning Summary

To install Docker EE, we need a Docker Hub account and a Docker EE license. We can find a repository URL which we can use to install Docker EE on Docker Hub.

1. We'll need a Docker Hub account. We can create one at https://hub.docker.com.

2. Start a Docker EE free trial: https://hub.docker.com/editions/enterprise/docker-ee-trial.

3. Get a unique Docker EE URL from the trial.

4. Go to https://hub.docker.com/my-content.

5. Click **Setup**.

6. Copy the URL.

7. Set up some temporary environment variables. Enter the unique Docker EE URL for the `DOCKER_EE_URL` variable:

```zsh
DOCKER_EE_URL=<YOUR_DOCKER_EE_URL>
DOCKER_EE_VERSION=18.09
```

8. Install required packages:

```zsh
sudo apt-get install -y \
  apt-transport-https \
  ca-certificates \
  curl \
  software-properties-common
```

9. Add the GPG and `apt -repository` using the Docker EE URL:

```zsh
curl -fsSL "${DOCKER_EE_URL}/ubuntu/gpg" | sudo apt-key add -

sudo add-apt-repository \
  "deb [arch=$(dpkg --print-architecture)] $DOCKER_EE_URL/ubuntu \
  $(lsb_release -cs) \
  stable-$DOCKER_EE_VERSION"
```

10. Install Docker EE:

```zsh
sudo apt-get update

sudo apt-get install -y docker-ee=5:18.09.4~3-0~ubuntu-bionic
```

11. Give `cloud_user` access to use Docker:

```zsh
sudo usermod -a -G docker cloud_user
```

12. Log out of the server and log back in again, then test the Docker EE installation:

```zsh
docker version
```
