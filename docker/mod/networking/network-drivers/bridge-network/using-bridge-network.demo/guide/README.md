# Using a Docker Bridge Network

## Introduction

By default, all containers on a host can communicate with one another over a default bridge network. However, in some cases, you may want to isolate groups of containers by allowing them to communicate over their own isolated network.

In this lab, you will have the opportunity to create a custom bridge network designed to facilitate communication between containers on a Docker host.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```zsh
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

### Create the Bridge Network

2. Create a bridge network called `prices-net`.

```zsh
docker network create --driver bridge prices-net
```

### Create the `base-price` Container

3. Create a container for the component that serves base prices.

```zsh
docker run --name base-price -d --network prices-net linuxacademycontent/prices-base-price:1
```

### Create the `sales` Container

4. Create a container for the component that serves products on sale.

```zsh
docker run --name sales -d --network prices-net linuxacademycontent/prices-sales:1
```

### Create the `total-price` Container

5. Create a container for the component that serves the total prices of products:

```zsh
docker run --name total-price -d --network prices-net -p 8080:80 linuxacademycontent/prices-total-price:1
```

6. Verify that everything is set up correctly.

```zsh
curl localhost:8080
```

You should get a list of products and their final prices. The `total-prices` container calculates these prices by first querying the other two containers. This communication takes place over the `prices-net` bridge network.
