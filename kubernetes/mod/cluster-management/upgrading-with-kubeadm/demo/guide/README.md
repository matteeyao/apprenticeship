# Performing a Kubernetes Upgrade with kubeadm

## Introduction

When you are managing Kubernetes in the real world, it is essential that you are able to keep your cluster up to date. This lab will allow you to practice the process of upgrading a Kubernetes cluster to a newer Kubernetes version using kubeadm. This will ensure you are comfortable with the upgrade process and ready to manage real-world Kubernetes clusters.

## Solution

Log in to the control plane server using the credentials provided:

```zsh
ssh cloud_user@<CONTROL_PLANE_SERVER_PUBLIC_IP_ADDRESS>
```

> [!NOTE]
> 
> We'll be switching between nodes throughout the lab, so be sure to check the Bash prompt for which node to run the commands on.

### Upgrade the Control Plane

1. Upgrade kubeadm:

```zsh
[cloud_user@k8s-control]$ sudo apt-get update && \
sudo apt-get install -y --allow-change-held-packages kubeadm=1.22.2-00
```

2. Make sure it upgraded correctly:

```zsh
[cloud_user@k8s-control]$ kubeadm version
```

3. Drain the control plane node:

```zsh
[cloud_user@k8s-control]$ kubectl drain k8s-control --ignore-daemonsets
```

`--ignore-daemonsets` ensures that any daemonsets do not prevent the command from succeeding.

4. Plan the upgrade:

```zsh
[cloud_user@k8s-control]$ sudo kubeadm upgrade plan v1.22.2
```

5. Upgrade the control plane components:

```zsh
[cloud_user@k8s-control]$ sudo kubeadm upgrade apply v1.22.2
```

6. Upgrade kubelet and kubectl on the control plane node:

```zsh
[cloud_user@k8s-control]$ sudo apt-get update && \
sudo apt-get install -y --allow-change-held-packages kubelet=1.22.2-00 kubectl=1.22.2-00
```

7. Restart kubelet:

```zsh
[cloud_user@k8s-control]$ sudo systemctl daemon-reload
[cloud_user@k8s-control]$ sudo systemctl restart kubelet
```

8. Uncordon the control plane node:

```zsh
[cloud_user@k8s-control]$ kubectl uncordon k8s-control
```

9. Verify the control plane is working:

```zsh
[cloud_user@k8s-control]$ kubectl get nodes
```

If it shows a `NotReady` status, run the command again after a minute or so. It should become `Ready`.

### Upgrade the Worker Nodes

> [!NOTE]
> 
> In a real-world scenario, you should not perform upgrades on all worker nodes at the same time. Make sure enough nodes are available at any given time to provide uninterrupted service.

#### Worker Node 1

1. Run the following on the _control plane node_ to drain worker node 1:

```zsh
[cloud_user@k8s-control]$ kubectl drain k8s-worker1 --ignore-daemonsets --force
```

You may get an error message that certain pods couldn't be deleted, which is fine.

2. In a new terminal window, log in to worker node 1:

```zsh
ssh cloud_user@<WORKER_1_PUBLIC_IP_ADDRESS>
```

3. Upgrade kubeadm on worker node 1:

```zsh
[cloud_user@k8s-worker1]$ sudo apt-get update && \
sudo apt-get install -y --allow-change-held-packages kubeadm=1.22.2-00
[cloud_user@k8s-worker1]$ kubeadm version
```

4. Back on _worker node 1_, upgrade the kubelet configuration on the worker node:

```zsh
[cloud_user@k8s-worker1]$ sudo kubeadm upgrade node
```

5. Upgrade kubelet and kubectl on worker node 1:

```zsh
[cloud_user@k8s-worker1]$ sudo apt-get update && \
sudo apt-get install -y --allow-change-held-packages kubelet=1.22.2-00 kubectl=1.22.2-00
```

6. Restart kubelet:

```zsh
[cloud_user@k8s-worker1]$ sudo systemctl daemon-reload
[cloud_user@k8s-worker1]$ sudo systemctl restart kubelet
```

7. From the _control plane node_, uncordon worker node 1:

```zsh
[cloud_user@k8s-control]$ kubectl uncordon k8s-worker1
```

#### Worker Node 2

1. From the _control plane node_, drain worker node 2:

```zsh
[cloud_user@k8s-control]$ kubectl drain k8s-worker2 --ignore-daemonsets --force
```

2. In a new terminal window, log in to worker node 2:

```zsh
ssh cloud_user@<WORKER_2_PUBLIC_IP_ADDRESS>
```

3. Upgrade kubeadm:

```zsh
[cloud_user@k8s-worker2]$ sudo apt-get update && \
sudo apt-get install -y --allow-change-held-packages kubeadm=1.22.2-00
[cloud_user@k8s-worker2]$ kubeadm version
```

4. Back on _worker node 2_, perform the upgrade:

```zsh
[cloud_user@k8s-worker2]$ sudo kubeadm upgrade node
[cloud_user@k8s-worker2]$ sudo apt-get update && \
sudo apt-get install -y --allow-change-held-packages kubelet=1.22.2-00 kubectl=1.22.2-00
[cloud_user@k8s-worker2]$ sudo systemctl daemon-reload
[cloud_user@k8s-worker2]$ sudo systemctl restart kubelet
```

5. From the _control plane node_, uncordon worker node 2:

```zsh
[cloud_user@k8s-control]$ kubectl uncordon k8s-worker2
```

6. Still in the _control plane node_, verify the cluster is upgraded and working:

```zsh
[cloud_user@k8s-control]$ kubectl get nodes
```

If they show a `NotReady` status, run the command again after a minute or so. They should become `Ready`.
