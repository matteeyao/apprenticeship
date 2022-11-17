# Building a Kubernetes Cluster

## What is kubeadm?

**kubeadm** is a tool that will simplify the process of setting up our Kubernetes cluster.

## Playground Server Setup

* Set up **3 server**: 1 will function as the control plane server, and 2 will serve as worker nodes.

* **Distribution** ▶︎ Use the distribution **Ubuntu 20.04 Focal Fossa LTS**

* **Size** ▶︎ Select **Medium** for the size.

## Hands-on demonstration

Set a hostname for servers:

```zsh
sudo hostnamectl set-hostname k8s-control
```

Within your two worker nodes, run:

```zsh
sudo hostnamectl set-hostname k8s-worker1
```

```zsh
sudo hostnamectl set-hostname k8s-worker2
```

Within the control plane server, setup a host file w/ mappings for all of these servers so that they can talk to each other using the hostnames:

```zsh
sudo vi /etc/hosts
```

```bash
127.0.0.1 localhost
# The following lines are desirable for IPv6 capable hosts
::1 ip6-localhost ip6-loopback
# Cloud Server Hostname mapping
172.31.98.59  554027035a1c.mylabserver.com

172.31.98.59    k8s-control
# Private IP address for worker 1
172.31.99.1     k8s-worker1
# Private IP address for worker 2
172.31.100.216  k8s-worker2
```

Repeat this step for the worker servers.

Logout of all three servers and log back in.

Next, install and configure **containerd** on all three servers:

```zsh
cat << EOF | sudo tee /etc/modules-load.d/containerd.conf
> overlay
> br_netfilter
> EOF
```

```zsh
sudo modprobe overlay
sudo modprobe br_netfilter
```

Setup system-level configurations:

```zsh
cat <<EOF | sudo tee /etc/sysctl.d/99-kubernetes-cri.conf
> net.bridge.bridge-nf-call-iptables  = 1
> net.ipv4.ip_forward                 = 1
> net.bridge.nf-call-ip6tables        = 1
> EOF
```

Apply the system-level configurations:

```zsh
sudo sysctl --system
```

Now, we can install **containerd**:

```zsh
sudo apt-get update && sudo apt-get install -y containerd
```

Setup **containerd** configuration file:

```zsh
sudo mkdir -p /etc/containerd
```

Generate **containerd** configuration file:

```zsh
sudo containerd config default | sudo tee /etc/containerd/config.toml
```

To ensure **containerd** is using that configuration, restart **containerd**:

```zsh
sudo systemctl restart containerd
```

Install the Kubernetes packages by disabling `swap` and installing several required packages:

```zsh
sudo swapoff -a
```

```zsh
sudo apt-get update && sudo apt-get install -y apt-transport-https curl
```

Setup package repository for Kubernetes packages:

```zsh
curl -s https://packages.cloud.google.com/apt/doc/apt-key.gpg | sudo apt-key add -
```

Configure the repository itself:

```zsh
cat << EOF | sudo tee /etc/apt/sources.list.d/kubernetes.list
> deb https://apt.kubernetes.io/ kubernetes-xenial main
> EOF
```

Update package listings from new repository:

```zsh
sudo apt-get update
```

Install Kubernetes packages:

```zsh
sudo apt-get install -y kubelet=1.24.0-00 kubeadm=1.24.0-00 kubectl=1.24.0-00
```

Run:

```zsh
sudo apt-mark hold kubelet kubeadm kubectl
```

...to ensure that those packages are not automatically upgraded. 

Repeat the process for installing **containerd** and Kubernetes packages for the worker nodes.

In the control plane server, initialize the cluster:

```zsh
sudo kubeadm init --pod-network-cidr 192.168.0.0/16 --kubernetes-version 1.24.0
```

Now that our cluster is initialized, we need to set up our `kube-config`, a file that will allow us to authenticate and interact w/ the cluster using `kubectl` commands.

Run the provided command from the cluster initialization output:

```zsh
mkdir -p $HOME/.kube
  sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
  sudo chown $(id -u):$(id -g) $HOME/.kube/config
```

Test the configuration by running `kubectl` command:

```zsh
kubectl get nodes
```

Before we add out worker nodes to the cluster, let's go ahead and configure the networking plugin:

```zsh
kubectl apply -f https://docks.projectcalico.org/manifests/calico.yaml
```

We'll be using the `calico` networking plugin.

Join the worker nodes to the cluster by generating the required token and join command:

```zsh
kubeadm token create --print-join-command
```

Run the command in the worker nodes:

```zsh
sudo kubeadm join 172.31.98.59:6443 --token dp3k6d.7vc8n61sv7bj0ilw --discovery-token-ca-cert-hash sha256:<SHA256_HASH>
```

View worker nodes with:

```zsh
kubectl get nodes
```
