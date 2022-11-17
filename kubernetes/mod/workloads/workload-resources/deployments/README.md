# K8s Deployments Overview

## What is a Deployment?

A K8s object that defines a **desired state** for a ReplicaSet (a set of replica Pods; these are pods that are all running the same containers w/ the same configuration; multiple copies of the same pod). The Deployment Controller seeks to maintain the desired state by creating, deleting, and replacing Pods w/ new configurations.

## Desired State

A Deployment's Desired State includes...

1. **replicas** ▶︎ The number of replica Pods the Deployment will seek to maintain

2. **selector** ▶︎ A label selector used to identify the replica Pods managed by the Deployment

3. **template** ▶︎ A template Pod definition used to create replica Pods

## Use Cases

There are many use cases for Deployments, such as:

* Easily scale an application up or down by changing the number of replicas.

* Perform rolling updates to deploy a new software version.

* Roll back to a previous software version.

## Hands-On Demonstration

Login to control plane node/server.

Create file:

```zsh
vi my-deployment.yml
```

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-deployment
spec:
  replicas: 3
  selector:
    matchLabels:
      app: my-deployment
  template:
    metadata:
      labels:
        app: my-deployment
    spec:
      containers:
      - name: nginx
        image: nginx:1.19.1
        ports:
        - containerPort: 80
```

The `selector` is used to actually locate which pods are being managed by the deployment. So any pods that match this selector, whether created by some other mechanism other than the deployment, are going to be managed by the deployment.

```zsh
kubectl create -f my-deployment.yml
```

View deployments:

```zsh
kubectl get deployments
```

```zsh
kubectl get pods
```
