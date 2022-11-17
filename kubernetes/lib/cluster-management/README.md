# Cluster Management

1. Which command-line tool allows you to interact with Etcd and perform backups?

> [!ANSWER]
>
> etcdctl is the command-line tool for Etcd.

2. Which of the following are options for a highly-available Etcd architecture? Choose two.

[x] External Etcd

[ ] Layered Etcd

[x] Stacked Etcd

[ ] Single Etcd Server

> [!ANSWER]
>
> External etcd, or managing Etcd separately from other control plane components, is one option for an HA setup.
>
> Stacked etcd, or managing Etcd alongside other control plane components, is one option for an HA setup.

3. What command can you use to allow Pods to be scheduled on a previously-drained node after Node maintenance is complete?

> [!ANSWER]
>
> `kubectl uncordon` will allow Pods to be scheduled on previously-drained Node.

4. Which tool can help you perform a Kubernetes upgrade?

> [!ANSWER]
>
> `kubeadm` includes functionality to help you upgrade Kubernetes clusters.

5. Which command allows you to upgrade control plane components?

> [!ANSWER]
>
> `kubeadm upgrade apply` will upgrade the control plane.

6. What software does Kubernetes use to store data about the state of the cluster?

> [!ANSWER]
>
> Etcd is the Backend data store or Kubernetes.

7. Which command is used to safely evict your pods from a node before maintenance on the node?

> [!ANSWER]
>
> `kubectl drain` is used to take a node down for maintenance.

8. Which tool provides a command-line interface for Kubernetes?

> [!ANSWER]
>
> kubectl provides a command-line interface to the Kubernetes API.

9. How can you make Kubernetes highly available?

> [!ANSWER]
>
> Have multiple control plane nodes.

10. Which tool(s) allow you to create Kubernetes clusters? Choose two.

[ ] kubectl

[ ] Helm

[x] kubeadm

[x] Minikube

> [!ANSWER]
>
> Helm allows you to template k8s objects, it does not help build clusters.
> 
> kubeadm allows you to build Kubernetes clusters.
> 
> Minikube allows you to easily create single-node clusters.
