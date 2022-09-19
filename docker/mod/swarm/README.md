# Docker Swarm

* **Cluster management integrated w/ Docker Engine** ▶︎ Docker CLI allows you to create a swarm of Docker Engines where you can deploy your application. No additional software needed.

* **Decentralized design** ▶︎ Docker Engine handles specialization at runtime, allowing you to deploy an entire swarm from a single disk image.

* **Declarative service model** ▶︎ Allows you to define the desired state of the various services in your application stack.

* **Scaling** ▶︎ You can scale up or down and the swarm manager automatically adapts by adding or removing tasks to maintain the desired state.

* **Desired state reconciliation** ▶︎ The swarm manager constantly monitors the cluster state. It will recognize any differences between the actual state and the desired state and act to reconcile the two.

* **Multi-host networking** ▶︎ Swarm manager will automatically assign addresses to containers on an overlay network when the container is initialized or updated.

* **Service Discovery** ▶︎ Each service in the swarm is assigned a unique DNS name.

* **Load Balancing** ▶︎ Ports for services can be exposed to an external load balancer and internally the swarm lets you configure how to distribute service containers between your worker nodes.

* **Secure by default** ▶︎ Each worker node in the swarm enforces TLS mutual authentication and encryption between itself and the nodes.

* **Rolling updates** ▶︎ Swarm lets you control the delay between service deployments to different sets of nodes. If something goes wrong, you can roll back to a previous state.

```
ip a
```

```
docker swarm init --advertise-addr 172.31.23.86
```

```
docker swarm join --token SWTKN-1-0s7... 172.31.23.86.2377
```

In the manager node:

```
docker node ls
```

To prep environment:

```
sudo vim /etc/hosts
```

```
docker run -d httpd
```

View container running within our environment:

```
docker ps
```

Run container in swarm:

```
docker service --help
```

```
docker service create --replicas 2 -p 80:80 --name myweb nginx
```

```
docker service ls
```

```
docker service ps myweb
```

```
docker ps
```

```
sudo yum install elinks
```


```
elinks 18.235.248.239:80
```

```
elinks 18.206.118.128:80
```

```
docker service ps myweb
```

## Learning summary

1. Docker Swarm offers which of the following functionalities?

* **Scaling** ▶︎ You can scale up or down and the swarm manager automatically adapts by adding or removing tasks to maintain the desired state.

* **Rolling updates** ▶︎ Swarm lets you control the delay between service deployments to different sets of nodes.

* **Desired state reconciliation** ▶︎ The swarm manager constantly monitors the cluster state and ill recognize any differences between the actual state and desired state and act to reconcile the two.

2. Which of the following commands will allow you to create a service called 'myweb' with 2 replicas from the 'httpd' image?

```
docker service create --name myweb --replicas=2 httpd
```
