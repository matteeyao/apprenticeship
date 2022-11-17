# Troubleshooting your k8s cluster

## Kube API server

If the K8s API server is down, you will not be able to use kubectl to interact w/ the cluster. You may get a message that looks something like:

```zsh
cloud_user@k8s-control:~$ kubectl get nodes
The connection to the server localhost:6443 was refused - did you specify the right host or port?
```

Assuming your kubeconfig is set up correctly, this may mean the API server is down.

**Possible remediation include:**

* Make sure the `docker` and `kubelet` services are up and running on your control plane node(s).

## Checking node status

Check the status of your nodes to see if any of them are experiencing issues.

Use `kubectl get nodes` to see the overall status of each node:

```zsh
kubectl get nodes
```

Use `kubectl describe node` to get more information on any nodes that are not in the `READY` state:

```zsh
kubectl describe node <NODE_NAME>
```

## Checking k8s services

If a node is having problems, it may be because a **service** is down on that node.

Each node runs the `kubelet` and `container runtime` (i.e. Docker) services.

1. View the status of a service w/ `systemctl status`:

```zsh
$ systemctl status kubelet
```

2. Start a stopped service w/ `systemctl start`:

```zsh
$ systemctl start kubelet
```

3. Enable a service so it starts automatically on system startup w/ `systemctl enable`:

```zsh
$ systemctl enable kubelet
```

## Checking system pods

In a `kubeadm` cluster, several K8s components run as pods in the `kube-system` namespace.

Check the status of these components with `kubectl get pods` and `kubectl describe pod`.

```zsh
$ kubectl get pods -n kube-system

$ kubectl describe pod <PODNAME> -n kube-system
```

## Hands-on demonstration

```zsh
kubectl get nodes
```

```zsh
kubectl describe node <NODE_NAME>
```

```zsh
sudo systemctl status kubelet
```

```zsh
kubectl get nodes
```

Switching over to worker node:

```zsh
sudo systemctl stop kubelet
```

```zsh
sudo systemctl disable kubelet
```

```zsh
sudo systemctl status kubelet
```

Switching over to control plane node, you should see one of the worker nodes' status as `NotReady`:

```zsh
kubectl get nodes
```

This is b/c kubelet is regularly checking in w/ the Kubernetes API server, and if kubelet stops checking in w/ the control plane, then the control plane is going to assume that there's something wrong w/ that node.

Back on the worker node:

```zsh
sudo systemctl start kubelet
```

```zsh
sudo systemctl enable kubelet
```

```zsh
sudo systemctl status kubelet
```

Come back to the control plane node.

```zsh
kubectl get nodes
```

Let's take a look at the Kubernetes system pods:

```zsh
kubectl get pods -n kube-system
```
