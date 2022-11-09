# Deploying a Service on an Overlay Network

## Introduction

Bridge networks are a powerful tool for controlling communication between containers on a single host, but what if you need to provide isolated networking between containers in Docker Swarm? With Docker Swarm, you can use custom overlay networks to allow groups of containers to communicate transparently, even if they are running on different swarm nodes.

In this lab, you will have the opportunity to work with overlay networks. You will set up a custom overlay network and deploy three different services that communicate with each other using this network.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```zsh
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

### Create the Overlay Network

2. Create the `prices-overlay-net` overlay network.

```zsh
docker network create --driver overlay prices-overlay-net
```

### Create the `base-price` Service

3. Create the `base-price` service:

```zsh
docker service create --name base-price --network prices-overlay-net --replicas 3 linuxacademycontent/prices-base-price:1
```

### Create the `sales` Service

4. Create the `sales` service.

```zsh
docker service create --name sales --network prices-overlay-net --replicas 3 linuxacademycontent/prices-sales:1
```

### Create the `total-price` Service

5. Create the `total-price` service:

```zsh
docker service create --name total-price --network prices-overlay-net --replicas 2 -p 8080:80 linuxacademycontent/prices-total-price:1
```

6. Verify that you get the total price data.

```zsh
curl localhost:8080
```

You should see a list of products and the total price for each. These prices are calculated by communicating with the `base-price` and `sales` services using the custom overlay network.
