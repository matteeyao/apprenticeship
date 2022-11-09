# Voluntary and involuntary disruptions

Pods do not disappear until someone (a person or a controller) destroys them, or there is an unavoidable hardware or system software error.

We call these unavoidable cases _involuntary disruptions_ to an application. Examples are:

* a hardware failure of the physical machine backing the node

* cluster administrator deletes VM (instance) by mistake

* cloud provider or hypervisor failure makes VM disappear

* a kernel panic

* the node disappears from the cluster due to cluster network partition

* eviction of a pod due to the node being out-of-resources.

Except for the out-of-resources condition, all these conditions should be familiar to most users; they are not specific to Kubernetes.

We call other cases _voluntary disruptions_. These include both actions initiated by the application owner and those initiated by a Cluster Administrator. Typical application owner actions include:

* deleting the deployment or other controller that manages the pod

* updating a deployment's pod template causing a restart

* directly deleting a pod (e.g. by accident)

Cluster administrator actions include:

* Draining a node for repair or upgrade.

* Draining a node from a cluster to scale the cluster down (learn about Cluster Autoscaling ).

* Removing a pod from a node to permit something else to fit on that node.

These actions might be taken directly by the cluster administrator, or by automation run by the cluster administrator, or by your cluster hosting provider.

Ask your cluster administrator or consult your cloud provider or distribution documentation to determine if any sources of voluntary disruptions are enabled for your cluster. If none are enabled, you can skip creating Pod Disruption Budgets.
