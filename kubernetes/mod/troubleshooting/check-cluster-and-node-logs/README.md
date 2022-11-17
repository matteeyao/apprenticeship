# Checking Cluster and Node Logs

## Service logs

You can check the logs for K8s-related services on each node using `journalctl`.

```zsh
$ sudo journalctl -u kubelet

$ sudo jorunalctl -u docker
```

## Cluster component logs

The Kubernetes cluster components have log output directed to `/var/log`. For example:

```zsh
/var/log/kube-apiserver.log

/var/log/kube-scheduler.log

/var/log/kube-controller-manager.log
```

These log files aren't going to appear for clusters that were created using kubeadm such as the cluster that we have been using throughout this course, and even for clusters that you'll be using on the Certified Kubernetes Administrator exam. The reason why the logs won't show up in a kubeadm cluster is b/c all of these components actually run as system pods, so they don't run directly on the host. They run within containers. So the log files are not going to show up on the host in a kubeadm cluster, but in a kubeadm cluster, you can still access those logs by using the `kubectl logs` command on those system pods.

Note that these log files may not appear for kubeadm clusters, since some components run inside containers. In that case, you can access them w/ `kubectl logs`.

## Hands-on demonstration

Show the logs for the kubelet service:

```zsh
sudo journalctl -u kubelet
```

View system pods:

```zsh
kubectl get pods -n kube-system
```

```zsh
kubectl logs -n kube-system kube-apiserver-e68924b5cb1c.mylabserver.com
```
