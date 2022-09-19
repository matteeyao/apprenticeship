# Creating a Kubernetes Cluster

## About this lab

In this hands-on lab, we will install and configure a Kubernetes cluster consisting of 1 master and 2 nodes. Once the installation and configuration are complete, we will have a 3-node Kubernetes cluster that uses Flannel as the network overlay.

![Fig. 1 Kubernetes Cluster](../../../../../img/automation-orchestration-tools/kubernetes/orchestration/create-cluster.demo/diag01.png)

## Learning objectives

[ ] Install Docker and Kubernetes on All Servers

[ ] Create and Scale a Deployment using kubectl

## Guide

### Logging in

Use the credentials provided on the hands-on lab overview page to log into the master and server nodes as `cloud_user`. It's probably a good idea to have three terminals open, one for each node.

### Install Docker and Kubernetes on All Servers

Most of these commands need to be run on each of the nodes. Pay attention though. Down at Step 10, we are going to do a little bit on **JUST** the master, and down at Step 15 we'll run something on **JUST** the nodes. There are notes down there, just be watching for them.

1. Once we have logged in, we need to elevate privileges using `sudo`:

```
sudo su
```

2. Disable SELinux:

```
setenforce 0
sed -i --follow-symlinks 's/SELINUX=enforcing/SELINUX=disabled/g' /etc/sysconfig/selinux
```

3. Enable the `br_netfilter` module for cluster communication:

```
modprobe br_netfilter
echo '1' > /proc/sys/net/bridge/bridge-nf-call-iptables
```

4. Ensure that the Docker dependencies are satisfied:

```
yum install -y yum-utils device-mapper-persistent-data lvm2
```

5. Add the Docker repo and install Docker:

```
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
yum install -y docker-ce
```

6. Set the cgroup driver for Docker to systemd, reload systemd, then enable and start Docker:

```
sed -i '/^ExecStart/ s/$/ --exec-opt native.cgroupdriver=systemd/' /usr/lib/systemd/system/docker.service
systemctl daemon-reload
systemctl enable docker --now
```

Verify systemd as the cgroup driver:

```
docker info | grep -i cgroup
```

7. Add the Kubernetes repo:

```
cat << EOF > /etc/yum.repos.d/kubernetes.repo
[kubernetes]
name=Kubernetes
baseurl=https://packages.cloud.google.com/yum/repos/kubernetes-el7-x86_64
enabled=1
gpgcheck=0
repo_gpgcheck=0
gpgkey=https://packages.cloud.google.com/yum/doc/yum-key.gpg
  https://packages.cloud.google.com/yum/doc/rpm-package-key.gpg
EOF
```

8. Install Kubernetes `v1.14.0`:

```
yum install -y kubelet-1.14.0-0 kubeadm-1.14.0-0 kubectl-1.14.0-0 kubernetes-cni-0.7.5
```

9. Enable the `kubelet` service. The `kubelet` service will fail to start until the cluster is initialized, this is expected:

```
systemctl enable kubelet
```

> [!NOTE]
>
> Complete the following section on the MASTER ONLY!

10. Initialize the cluster using the IP range for Flannel:

```
kubeadm init --pod-network-cidr=10.244.0.0/16
```

11. Copy the `kubeadmn join` command that is in the output. We will need this later.

12. Exit `sudo`, copy the `admin.conf` to your home directory, and take ownership.

```
mkdir -p $HOME/.kube
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
sudo chown $(id -u):$(id -g) $HOME/.kube/config
```

13. Deploy Flannel:

```
kubectl apply -f https://raw.githubusercontent.com/flannel-io/flannel/master/Documentation/kube-flannel-old.yaml
```

14. Check the cluster state:

```
kubectl get pods --all-namespaces
```

> [!NOTE]
>
> Complete the following steps on the NODES ONLY!

15. Run the `join` command that you copied earlier, this requires running the command prefaced with `sudo` on the nodes (if we hadn't run `sudo su` to begin with). Then we'll check the nodes from the master.

```
kubectl get nodes
```

### Create and Scale a Deployment Using kubectl

> [!NOTE]
>
> These commands will only be run on the master node.

16. Create a simple deployment:

```
kubectl create deployment nginx --image=nginx
```

17. Inspect the pod:

```
kubectl get pods
```

18. Scale the deployment:

```
kubectl scale deployment nginx --replicas=4
```

19. Inspect the pods. We should have four now:

```
kubectl get pods
```

### Conclusion

We've created Kubernetes cluster, and deployed Nginx on it. Then we scaled up to four pods.
