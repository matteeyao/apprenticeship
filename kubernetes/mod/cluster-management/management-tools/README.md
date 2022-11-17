# Introduction to K8s Management Tools

## K8s Management Tools

There is a variety of management tools available for Kubernetes. These tools interface w/ Kubernetes to provide additional functionality. When using Kubernetes, it is a good idea to be aware of some of these tools.

## kubectl

**kubectl** is the official command line interface for Kubernetes. It is the main method you will use to work w/ Kubernetes in this course.

## kubeadm

We have already used **kubeadm**, which is a tool for quickly and easily creating Kubernetes clusters.

## Minikube

**Minikube** allows you to automatically set up a local single-node Kubernetes cluster. It is great for getting Kubernetes up and running quickly for development purposes.

Applicable for setting up a quick k8s cluster for development or automation purposes.

## Helm

**Helm** provides templating and package management for Kubernetes objects. You can use it to manage your own templates (known as charts). You can also download and use shared templates.

## Kompose

**Kompose** helps you translate Docker compose files into Kubernetes objects. If you are using Docker compose for some part of your workflow, you can move your application to Kubernetes easily w/ Kompose.

Used when transitioning from Docker Compose to Kubernetes.

## Kustomize

**Kustomize** is a configuration management tool for managing Kubernetes object configurations. It allows you to share and re-use templated configurations for Kubernetes applications.
