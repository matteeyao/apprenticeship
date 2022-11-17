# Troubleshooting

1. Which of the following statements is true about container logs in Kubernetes? (select all that apply)

[x] You can access container logs using kubectl.

[ ] When retrieving logs, you must specify the container name if the Pod has only one container.

[x] When retrieving logs, you must specify the container name if the Pod has multiple containers.

[ ] Only data written to standard output (stdout) will appear in container logs.

2. In a cluster built with kubeadm, how can you check the status of cluster components such as kube-apiserver?

[ ] Check the status of the kube-apiserver service with systemctl.

[x] Check the status of Pods in the kube-system Namespace.

> [!ANSWER]
> 
> In a kubeadm cluster, kube-apiserver is not set up as a systemctl service.
>
> In a kubeadm cluster, cluster components such as kube-apiserver run as static Pods in the kube-system Namespace.

3. What command can you use to view the status of all Nodes in the cluster?

`kubectl get nodes`

> [!ANSWER]
>
> This command will display all Nodes, along with their status.

4. In a cluster built with kubeadm, which of the following commands would let you view the logs for kubelet?

[ ] `kubectl logs kubelet`

[ ] `cat /etc/kubernetes/manifests/kube-apiserver.yaml`

[x] `sudo journalctl -u kubelet`

[ ] `kubectl logs kubelet -n kube-system`

> [!ANSWER]
>
> Kubelet does not run as a Pod in a kubeadm cluster.
> 
> In a kubeadm cluster, kubelet runs as a standard service. Therefore, you can view its logs with journalctl.

5. Your kubeadm cluster is having issues resolving Service DNS names. Where would you look to make sure the cluster DNS is up and running?

[ ] Use the `kubeadm dns status` command.

[ ] Check the DNS log on the control plane Node with journalctl.

[x] Look for DNS Pods in the kube-system Namespace.

[ ] Check the status of the kube-dns service with systemctl.

> [!ANSWER]
>
> In a kubeadm cluster, DNS does not run as a systemd service.
>
> In a kubeadm cluster, the DNS runs as Pods within the kube-system Namespace.

6. What command can you use to view a container's logs?

`kubectl logs`

> [!ANSWER]
>
> `kubectl logs` can be used to obtain container logs.

7. In a cluster built with kubeadm, how can you find logs for the Kubernetes API Server?

> [!ANSWER]
>
> `kubectl logs -n kube-system <api-server-pod-name>`
> 
> In a kubeadm cluster, kube-apiserver runs as a system pod. Therefore, you can get the logs with the `kubectl logs` command.

8. Which command allows you to get detailed information about a Pod's status in a human-readable format?

> [!ANSWER]
>
> `kubectl describe pod`
>
> `kubectl describe` displays detailed status information in a human-readable way.

9. You have a Pod called `my-pod` in the `dev` Namespace with multiple containers. There is one container called `busybox` within the Pod. How can you execute the `ls` command inside that container?

> [!ANSWER]
>
> `kubectl exec -n dev my-pod -c busybox -- ls`

10. How can you explore the Kubernetes network from inside that network?

[ ] Create a ClusterIP Service

[x] Create a Pod and run commands inside that Pod.

[ ] Use kubeadm proxy to connect to the cluster network.

[ ] Create a NodePort Service

> [!ANSWER]
>
> This is a good strategy for interacting with the Kubernetes network from within.
