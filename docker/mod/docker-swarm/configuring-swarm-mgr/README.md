# Configuring a Swarm Manager

```
sudo apt-get update

sudo apt-get -y install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common
```

Set up GPG key:

```
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | suo apt-key add -
```

Followed by:

```
sudo apt-get update

sudo apt-get install -y docker-ce=5:18.09.5~3-0~ubutu-bionic docker-ce-cli=5:18.09.5~3-0~ubutu-bionic containerd.io
```

Ensure user has access to run docker commands:

```
sudo usermod -a -G docker cloud_user
```

Setting up a new swarm is relatively simple. All we have to do is create the first swarm manager:

* Install Docker CE on the Swarm Manager server.

* Initialize the swarm w/ `docker swarm init`:

> [!NOTE]
>
> Set `--advertise-addr` to an address that other nodes in the swarm will see this node as.

```
docker swarm init --advertise-addr <ADVERTISE_ADDRESS>
```

Once the swarm is initialized, you can see some info about the swarm w/ `docker info`.

* List nodes in the swarm:

> [!NOTE]
>
> At this point there will only be one since only the manager was just initialized.

```
docker node ls
```
