# Kubernetes

Kubernetes is a container orchestration tool.

Kubernetes is a tool for automated management of containerized applications, also known as a container orchestration tool.

Kubernetes allows you to easily build and manage your container infrastructure and automation.

Enables you to develop self-healing applications, automated scaling, and easily automate deployments. Self-Healing Applications are applications that are able to automatically detect when something is broken and automatically take steps to correct the problem w/o the need for human involvement.

## What are Containers?

Kubernetes is all about managing containers.

Containers wrap software in independent, portable packages, making it easy to quickly run software in a variety of environments.

When you wrap your software in a container, you can quickly and easily run it (almost) anywhere. That makes containers great for automation!

Containers are a bit like virtual machines, but they are smaller and start up faster. This means that automation can move quickly and efficiently w/ containers.

## Orchestration

W/ containers, you can run a variety of software components across a cluster of generic servers.

This can help ensure high availability and make it easier to scale resources.

This raises some questions:

* How can I ensure that multiple instances of a piece of software are spread across multiple servers for high availability?

* How can I deploy new code changes and roll them out across the entire cluster?

* How can I create new containers to handle additional load (scale up)>

Of course, these kinds of tasks can all be done manually, but that is a lot of work!

The answer is to use an orchestration tool to automate these kinds of management tasks. This is what Kubernetes does.

The problem:

* Containers on Node 1 cannot communicate w/ containers on Node 2.

* All the containers on a node share the host IP space and must coordinate which ports they use on that node.

* If a container must be replaced, it will require a new IP address, and any hard-coded IP addresses will break.

Enter Kubernetes...

Pods are the smallest unit of Kubernetes and can be either a single container or a group of containers. All containers in a pod have the same IP address, and all pods in a cluster have unique IPs in the same IP space.

## Kubernetes Cluster

* All containers can communicate w/ all other containers w/o NAT.

* All nodes can communicate w/ all container (and vice versa) w/o NAT.

* The IP that a container sees itself as is the IP that others see it as.

## Kube-scheduler

The kube-scheduler watches newly created pods that have no node assignment and selects the node for them to run on.

## kube-controller manager

The kube-controller-manager helps run the node, replication, endpoint, and service account and token controllers on the master.

## Learning summary

The node controller in Kubernetes is responsible for checking the cloud provider to determine if a node has been deleted in the cloud after it stops responding.

The volume controller in Kubernetes is responsible for creating, attaching, and mounting volumes, and interacting with the cloud provider to orchestrate volumes.

In Kubernetes, the replication controller is responsible for maintaining the correct number of pods for every replication controller object in the system.
