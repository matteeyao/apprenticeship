# Building a Kubernetes 1.24 Cluster with `kubeadm`

## Introduction

This lab will allow you to practice the process of building a new Kubernetes cluster. You will be given a set of Linux servers, and you will have the opportunity to turn these servers into a functioning Kubernetes cluster. This will help you build the skills necessary to create your own Kubernetes clusters in the real world.

## Solution

Log in to the lab server using the credentials provided:

```zsh
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

### Install Packages

1. Log into the control plane node.

> [!NOTE]
> 
> The following steps must be performed on all three nodes.

2. Create configuration file for containerd:

```zsh
cat <<EOF | sudo tee /etc/modules-load.d/containerd.conf
overlay
br_netfilter
EOF
```

This file will instruct the server to load the `overlay` and `br_netfilter` kernel modules on startup. So every time the server starts up, those kernel modules will be loaded and are modules required by `containerd`.

3. We want to ensure that the modules are loaded immediately w/o the need to restart the server. Load modules:

```zsh
sudo modprobe overlay
sudo modprobe br_netfilter
```

4. Set system configurations for Kubernetes networking:

```zsh
cat <<EOF | sudo tee /etc/sysctl.d/99-kubernetes-cri.conf
net.bridge.bridge-nf-call-iptables = 1
net.ipv4.ip_forward = 1
net.bridge.bridge-nf-call-ip6tables = 1
EOF
```

5. Apply new settings:

```zsh
sudo sysctl --system
```

The command will read the above file and apply those settings immediately.

6. Install containerd:

```zsh
sudo apt-get update && sudo apt-get install -y containerd
```

7. Create default configuration file for containerd:

```zsh
sudo mkdir -p /etc/containerd
```

8. Generate default containerd configuration and save to the newly created default file:

```zsh
sudo containerd config default | sudo tee /etc/containerd/config.toml
```

9. Restart containerd to ensure new configuration file usage:

```zsh
sudo systemctl restart containerd
```

10. Verify that containerd is running:

```zsh
sudo systemctl status containerd
```

11. Disable swap:

```zsh
sudo swapoff -a
```

12. Install dependency packages:

```zsh
sudo apt-get update && sudo apt-get install -y apt-transport-https curl
```

13. Download and add GPG key:

```zsh
curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -
```

14. Add Kubernetes to repository list:

```zsh
cat <<EOF | sudo tee /etc/apt/sources.list.d/kubernetes.list
deb https://apt.kubernetes.io/ kubernetes-xenial main
EOF
```

15. Update package listings:

```zsh
sudo apt-get update
```

16. Install Kubernetes packages:

> [!NOTE]
> 
> If you get a `dpkg lock` message, just wait a minute or two before trying the command again.

```zsh
sudo apt-get install -y kubelet=1.24.0-00 kubeadm=1.24.0-00 kubectl=1.24.0-00
```

17. Turn off automatic updates:

```zsh
sudo apt-mark hold kubelet kubeadm kubectl
```

18. Log into both worker nodes to perform previous steps.

### Initialize the Cluster

1. Initialize the Kubernetes cluster on the control plane node using `kubeadm`:

> [!NOTE]
> 
> This is only performed on the Control Plane Node.

```zsh
sudo kubeadm init --pod-network-cidr 192.168.0.0/16 --kubernetes-version 1.24.0
```

2. Set `kubectl` access:

```zsh
mkdir -p $HOME/.kube
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
sudo chown $(id -u):$(id -g) $HOME/.kube/config
```

3. Test access to cluster:

```zsh
kubectl get nodes
```

### Install the Calico Network Add-On

1. On the control plane node, install Calico Networking:

```zsh
kubectl apply -f https://docs.projectcalico.org/manifests/calico.yaml
```

2. Check status of the control plane node:

```zsh
kubectl get nodes
```

### Join the Worker Nodes to the Cluster

1. In the control plane node, create the token and copy the `kubeadm join` command:

> [!NOTE]
> 
> The join command can also be found in the output from `kubeadm init` command.

```zsh
kubeadm token create --print-join-command
```

2. In both worker nodes, paste the `kubeadm join` command to join the cluster. Use `sudo` to run it as root:

```zsh
sudo kubeadm join ...
```

3. In the control plane node, view cluster status:

> [!NOTE]
> 
> You may have to wait a few moments to allow all nodes to become ready.
