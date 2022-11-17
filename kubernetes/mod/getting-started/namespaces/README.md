# Using Namespaces in K8s

## What is a namespace?

Namespaces are virtual clusters backed by the same physical cluster. Kubernetes objects, such as pods and containers, live in namespaces. Namespaces are a way to separate and organize objects in your cluster.

## List existing namespaces

You can list existing namespaces w/ `kubectl`:

```zsh
kubectl get namespaces
```

All clusters have a default namespace. This is used when no other namespace is specified. kubeadm also creates a `kube-system` namespace for system components.

## Specify a namespace

WHen using kubectl, you may need to specify a namespace. You can do this w/ the `--namespace` flag.

Note that if you do not specify a namespace, the `default` namespace is assumed.

```zsh
kubectl get pods --namespace my-namespace
```

## Create a namespace

You can create your own namespaces with `kubectl`:

```zsh
kubectl create namespace my-namespace
```

## Hands-on demonstration

```zsh
kubectl get namespaces
```

Default namespaces include `kube-node-lease` and `kube-public`

* `default` namespace is what is assumed when you don't specify a namespace

* `kube-system` contains various system components that are part of the cluster itself

```zsh
kubectl get pods --namespace kube-system
```

```zsh
kubectl get pods --all-namespaces
```
