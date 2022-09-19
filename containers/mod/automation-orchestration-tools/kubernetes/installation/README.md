# Installing Kubernetes

```
ssh cloud_user@<SERVER_IP_ADDRESS>
```

```
sudo su
```

Disable SELinux:

```
setenforce 0
```

```
sed -i --follow-symlinks 's/SELINUX=enforcing/SELINUX=disabled/g' /etc/sysconfig/selinux
```

```
modprobe br_netfilter
```

Allow Kubernetes to set IP tables:

```
echo '1' > /proc/sys/net/bridge/bridge-nf-call-iptables
```

Turn off swap:

```
swapoff -a
```

Comment out `/root/swap swap swap sw 0 0`:

```
vim /etc/fstab
```

Install prerequisites for Docker:

```
yum install -y yum-utils device-mapper-persistent-data lvm2
```

```
yum-config-manager --add-repo https://download.docker.com/linux/centos/docker-ce.repo
```

```
yum install -y docker-ce
```

Change the cgroup driver that Docker is using:

```
sed -i '/^ExecStart/ s/$/ --exec-opt native.cgroupdriver=systemd/' /usr/lib/systemd/system/docker.service
```

Recall that the cgroup driver is used to report on memory usage and additional metrics on a cluster.

Ensure daemon detects change:

```
systemctl daemon-reload
```

```
systemctl enable docker --now
```

```
systemctl status docker
```

Ensure cgroup driver is `systemd`:

```
docker info | grep -i cgroup
```

```
cat << EOF > /etc/yum.repos.d/kubernetes.repo
> [kubernetes]
> name=Kubernetes
> baseurl=https://packages.cloud.google.com/yum/repos/kubernetes-el7-x86_64
> enabled=1
> gpgcheck=0
> repo_gpgcheck=0
> gpgkey=https://packages.cloud.google.com/yum/doc/yum-key.gpg
>  https://packages.cloud.google.com/yum/dock/rpm-package-key.gpg
> EOF
```

```
yum install -y kubectl kubeadm kubelet
```

```
systemctl enable kubelet
```

Command to run only on the master:

```
kubeadm init --pod-network-cidr=10.244.0.0/16
```

Exit out of sudo:

```
exit
```

Setup kubectl

```
mkdir -p $HOME/.kube
```

```
sudo cp -i /etc/kubernetes/admin.conf $HOME/.kube/config
```

```
sudo chown $(id -u):$(id -g) $HOME/.kube/config
```

```
kubectl get nodes
```

Apply network overlay to node:

```
kubectl apply -f https://raw.githubusercontent.com/coreos/flannel/master/Documentation/kube-flannel.yml
```

Within the worker nodes:

```
sudo su
```

```
kubeadm join 172.31.116.163:6443 --token jurs1z.hmjs5u0wnq4ckbrl
>   --discovery-token-ca-cert-hash sha256:c8c65ae900b0209...01
```

On the master:

```
kubectl get nodes
```
