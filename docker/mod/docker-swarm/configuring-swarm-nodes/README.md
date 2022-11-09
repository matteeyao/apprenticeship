# Configuring Swarm Nodes

Install Docker CE:

```
sudo apt-get update

sudo apt-get -y install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common
    
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | suo apt-key add -

sudo apt-get-repository \
    "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
    $(lsb_release -cs) \
    stable"
    
sudo apt-get update

sudo usermod -a -G docker cloud_user

exit
```

W/ a manager set up, we can add some worker nodes to the swarm.

1. First, install Docker CE on both worker nodes.

2. Retrieve a `join-token` from the manager.

3. Run the following command on the Swarm manager to get a join command:

```
docker swarm join-token worker
```

4. Now copy the `docker swarm join` command provided in the output and run it on all workers. The command's execution appear similar to the following command:

```
docker swarm join --token <TOKEN> <SWARM_MANAGER_PRIVATE_IP>:2377
```

```
docker info
```

5. On the Swarm Manager,  verify that all workers have successfully joined the swarm:

```
docker node ls
```

All nodes should appear in the list, including the manager.
