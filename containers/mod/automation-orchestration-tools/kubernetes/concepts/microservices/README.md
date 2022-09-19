# Microservices

## What are Microservices?

One of the best ways to demonstrate the value of Kubernetes is in managing microservice applications.

Microservices are small, independent services that work together to form a whole application.

Many applications are designed w/ a monolithic architecture, meaning that all parts of the application are combined in one large executable.

![Fig. 1 Monolith Application](../../../../../img/automation-orchestration-tools/kubernetes/concepts/microservices/fig01.png)

Microservice architectures break the application up into several small services.

![Fig. 2 Microservice Application](../../../../../img/automation-orchestration-tools/kubernetes/concepts/microservices/fig02.png)

Here are a few advantages of microservices:

* **Scalability** ▶︎ Individual microservices are independently scalable. If your search service is under a large amount of load, you can scale that service by itself, w/o scaling the whole application.

* **Cleaner code** ▶︎ When services are relatively independent, it is easier to make a change in one area of the application w/o breaking things in other areas.

* **Reliability** ▶︎ Problems in one area of the application are less likely to affect other areas.

* **Variety of tools** ▶︎ Different parts of the application can be built using different tools, languages, and frameworks. This means that the right tool can be used for every job.

Implementing microservices means deploying, scaling, and managing a lot of individual components. Kubernetes is a great tool for accomplishing all of this. In the world of microservices, the benefits of Kubernetes really shine.

## Deploying the Robot Shop App

Now we are ready to get hands-on w/ microservices in Kubernetes. In this lesson, we will deploy a sample microservice application called "Stan's Robot Shop". This is an open-source sample microservice app made by Instana.

Let's begin by cloning the robot-shop Git repository. This repository contains ready-made YAML files that we can use to quickly and easily install the application.

```
cd ~/
git clone https://github.com/linuxacademy/robot-shop.git
```

Now we can install the app in our cluster, under a namespace called robot-shop.

```
kubectl create namespace robot-shop
kubectl -n robot-shop create -f ~/robot-shop/K8s/descriptors/
```

Let's check on the pods in the app as they come up:

```
kubectl get pods -n robot-shop -w
```

Once the pods are up, you should be able to access the app in your browser. Use the public IP of one of the nodes in your cluster and port 30080.

```
http://$kube_server_public_ip:30080
```
