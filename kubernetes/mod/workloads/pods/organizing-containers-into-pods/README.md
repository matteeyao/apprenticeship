# 5.1.2 Organizing containers into pods

* Think of each pod as a separate computer

  * Unlike virtual machines, which typically host multiple applications, you typically run only one application in each pod

  * You never need to combine multiple applications in a single pod, as pods have almost no resource overhead

  * You can have as many pods as you need, so instead of stuffing all your applications into a single pod, you should divide them so that each pod runs only closely related application processes

## Splitting a multi-tier application stack into multiple pods

* Imagine a simple system composed of a front-end web server and a back-end database

  * We've already established that the front-end server and the database shouldn't run in the same container, as all the features built into containers were designed around the expectation that not more than one process runs in a container

  * If not in a single container, should you then run them in separate containers that are all in the same pod?

* Although nothing prevents you from running both the front-end server and the database in a single pod, this isn't the best approach

  * We've established that all containers of a pod always run co-located, but do the web server and the database have to run on the same computer?

  * The answer is obviously no, as they can easily communicate over the network

  * Therefore you shouldn't run them in the same pod

* If both the front-end and the back-end are in the same pod, both run on the same cluster node

  * If you have a two-node cluster and only create this one pod, you are using only a single worker node and aren't taking advantage of the computing resources available on the second node

  * This means wasted CPU, memory, disk storage and bandwidth

  * Splitting the containers into two pods allows K8s to place the front-end pod on one node and the back-end pod on the other, thereby improving the utilization of your hardware

## Splitting into multiple pods to enable individual scaling

* Another reason not to use a single pod has to do w/ horizontal scaling

  * A pod is not only the basic unit of deployment, but also the basic unit of scaling

  * In chapter 2, you scaled the Deployment object and K8s created additional pods-additional replicas of your application

  * K8s doesn't replicate containers within a pod

  * It replicates the entire pod

* Front-end components usually have different scaling requirements than back-end components, so we typically scale them individually

  * When your pod contains both the front-end and back-end containers and K8s replicates it, you end up w/ multiple instances of both the front-end and back-end containers, which isn't always what you want

  *  Stateful back-ends, such as databases, usually can't be scaled

  * At least not as easily as stateless front ends

  * If a container has to be scaled separately from the other components, this is a clear indication that it must be deployed in a separate pod

![Fig. 1 Splitting an application stack into pods](../../../../img/workloads/pods/organizing-containers-into-pods/diag01.png)

* Splitting application stacks into multiple pods is the correct approach

  * But then, when does one run multiple containers in the same pod?

> [!TIP]
> 
> Introducing sidecar containers 
