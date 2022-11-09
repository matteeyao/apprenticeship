# Building a Docker Application Stack

## About this lab

Stacks are one of the most powerful orchestration features available in Docker Swarm. They allow you to easily manage complex applications consisting of multiple interdependent components running in separate containers.

In this lab, you will have the opportunity to work with Docker stacks by building a multi-component application as a Docker stack. You will also learn how to manage existing stacks by scaling a stack's services after it has already been deployed. This will give you some hands-on insight into Docker stacks.

## Learning Objectives

[ ] Build and deploy the application stack.

[ ] Scale the Fruit and Vegetable services in the stack.

## Additional Resources

Your supermarket company is in the process of improving their Docker-based applications. They have built a set of three RESTful data services that communicate with each other as part of a larger infrastructure. You have been given the task of designing a Docker application stack so that these three services can be easily managed and scaled as a unit. A Docker Swarm cluster has already been set up for you to use.

Here is some background information on the three services:

1. **Fruit Service:**

    * Provides a list of fruits sold in the company's stores.

    * You can use the Docker image tag `linuxacademycontent/fruit-service:1.0.1` to run this service.

    * Listens on port `80`.

    * The service should be named `fruit` inside the stack.

2. **Vegetable Service:**

    * Provides a list of vegetables sold in the company's stores.

    * You can use the Docker image tag `linuxacademycontent/vegetable-service:1.0.0` to run this service.

    * Listens on port `80`.

    * The service should be named `vegetables` inside the stack.

3. **All Products Service:**

    * Queries the other two services, combining their data into a single list of all produce.

    * You can use the Docker image tag `linuxacademycontent/all-products:1.0.0` to run this service.
    
    * Listens on port `80`.

    * Use the environment variables `FRUIT_HOST` and `FRUIT_PORT` to set the host and port which will be used to query the fruit service.

    * Use the environment variables `VEGETABLE_HOST` and `VEGETABLE_PORT` to set the host and port which will be used to query the vegetable service.

### Step One

Deploy a Docker application stack that meets the following specifications:

* The stack is called `produce`.

* The stack runs the _Fruit_, _Vegetable_, and _All Products_ services.

* The _All Products_ service is able to query the _Fruit_ and _Vegetable_ services.

* The _All Products_ service is published on port `8080`.

One you have deployed the stack, you can verify whether it is working by querying the _All Products_ service:

```
curl localhost:8080
```

If the stack is set up correctly, you should get a combined list of fruits and vegetables.

### Step Two

Once you have deployed the stack and verified that it is working, modify the stack by scaling both the Fruit and Vegetable services up to `3` replicas.
