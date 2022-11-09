# Building Services in Docker

## Introduction

Services are the most basic and straightforward way to run containers using a Docker swarm. They allow you to execute multiple replica containers across all nodes in the Swarm cluster.

In this lab, you will have the opportunity to work with Docker services. You will practice scaling services by changing the number of replicas for an existing service. You will also have the opportunity to create a new service and run it in the cluster.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

### Scale the `products-fruit` service to 5 replicas

1. Scale the service.

```
docker service update --replicas 5 products-fruit
```

You can also do it this way (both do the same thing):

```
docker service scale products-fruit=5
```

### Create the `products-vegetables` service

1. Create the `products-vegetables` service.

```
docker service create --name products-vegetables -p 8081:80 --replicas 3 linuxacademycontent/vegetable-service:1.0.0
```

2. Verify that the service is working.

```
curl localhost:8081
```

You should see some JSON data containing a list of vegetables.
