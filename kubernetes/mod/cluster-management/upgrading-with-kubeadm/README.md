# Upgrading K8s w/ Kubeadm

## Upgrading w/ kubeadm

When using Kubernetes, you will likely want to periodically upgrade Kubernetes to keep your cluster up to date.

Luckily, kubeadm makes this process easier.

## Control plane upgrade steps

* Upgrade kubeadm on the control plane node.

* Drain the control plane node.

* Plan the upgrade (`kubeadm upgrade plan`).

* Apply the upgrade (`kubeadm upgrade apply`).

* Upgrade kubelet and kubectl on the control plane node.

* Uncordon the control plane node.

## Worker node upgrade steps

* Drain the node.

* Upgrade kubeadm.

* Upgrade the kubelet configuration (`kubeadm upgrade node`).

* Upgrade kubelet and kubectl.

* Uncordon the node.

## Hands-on demonstration

Log into Kubernetes control plane server.

First, drain the node:

```zsh
kubectl drain k8s-control --ignore-daemonsets
```

Next, upgrade kubeadm:

```zsh
sudo apt-get update && \
> sudo apt-get install -y --allow-change-held-packages kubeadm=1.22.2-00
```

View kubeadm version:

```zsh
kubeadm version
```

Use kubeadm to upgrade internal control plane components:

```zsh
sudo kubeadm upgrade plan v1.22.2
```

```zsh
sudo kubeadm upgrade apply v1.22.2
```

Upgrade kubelet and kubectl:

```zsh
sudo apt-get update && \
> sudo apt-get install -y --allow-change-held-packages kubelet=1.22.2-00 kubectl=1.22.2-00
```

Just in case our kubelet service had any changes to the service file, run:

```zsh
sudo systemctl daemon-reload
```

Restart kubelet:

```zsh
sudo systemctl restart kubelet
```

Let the cluster know that workloads can be scheduled on that node once more:

```zsh
kubectl uncordon k8s-control
```

Running...

```zsh
kubectl get nodes
```

...you will see that control-plane nodes are on 1.22.2.

Now, let's upgrade our worker nodes.

Just like we did w/ the control plane node, run:

```zsh
kubectl drain k8s-worker1 --ignore-daemonsets --force
```

Now that we have drained the node, let's go to the first worker node to perform the upgrade. Login into worker 1. Repeat similar process that we used for control plane node:

```zsh
sudo apt-get update && \
> sudo apt-get install -y --allow-change-held-packages kubeadm=1.22.2-00
```

```zsh
sudo kubeadm upgrade node
```

Upgrade kubelet and kubectl:

```zsh
sudo apt-get update && \
> sudo apt-get install -y --allow-change-held-packages kubelet=1.22.2-00 kubectl=1.22.2-00
```

```zsh
sudo systemctl daemon-reload
```

```zsh
sudo systemctl restart kubelet
```

Now that worker 1 is upgraded, let's go back to the control plane node:

```zsh
kubectl uncordon k8s-worker1
```

```zsh
kubectl get nodes
```

Repeat same process for worker node 2:

```zsh
kubectl drain k8s-worker2 --ignore-daemonsets --force
```

On the second worker node:

```zsh
sudo apt-get update && \
> sudo apt-get install -y --allow-change-held-packages kubeadm=1.22.2-00
```

```zsh
sudo kubeadm upgrade node
```

```zsh
sudo apt-get update && \
> sudo apt-get install -y --allow-change-held-packages kubelet=1.22.2-00 kubectl=1.22.2-00
```

```zsh
sudo systemctl daemon-reload
```

```zsh
sudo systemctl restart kubelet
```

Back on the control plane node...

```zsh
kubectl uncordon k8s-worker2
```

```zsh
kubectl get nodes
```

...should show all nodes in version `v1.22.2`.
