# Getting Started

1. Which flag can you use with kubeadm to supply a custom configuration file?

> `--config` allows you to pass in your own config file.

2. What is the Kubernetes control plane?

> A collection of components that manage the cluster globally.

3. What does kubeadm do?

> Kubeadm is a cluster setup tool and simplifies the process of building Kubernetes clusters.

4. Which Kubernetes component manages containers on an individual node?

[ ] kube-scheduler

[ ] kube-controller-manager

[x] kubelet

[ ] kube-proxy

> kube-scheduler is a control plane component and does not interact with just a single node. The kubelet is the agent that manages containers on each node.

5. What is a Namespace?

> In Kubernetes, namespace provides a mechanism for isolating groups of resources within a single cluster. Names of resources need to be unique within a namespace, but not across namespaces. Namespace-based scoping is applicable only for namespaced objects (e.g. Deployments, Services, etc) and not for cluster-wide objects (e.g. StorageClass, Nodes, PersistentVolumes, etc).
