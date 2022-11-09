# Scheduling

In Kubernetes, **scheduling** refers to the process of selecting a node on which to run a workload.

Scheduling is handled by the **Kubernetes Scheduler**, which assigns the pod to a specific node.

## Node Taints

Node **taints** control which pods will be allowed to run on which nodes. Pods can have tolerations, which override taints for the specific pod. Tolerations allow pods to run on tainted nodes.

Each taint has an **effect**.

The **NoExecute** effect does two things to pods that do not have the appropriate toleration:

* Prevents new pods from being scheduled on the node.

* Evicts existing pods.

    * Meaning, the **NoExecute** effect will kick those pods off the node and remove them.

## Resource Requests

In the container spec, you can specify **resource requests** for resources such as memory and CPU.

The scheduler will avoid scheduling pods on nodes that do not have enough available resources to satisfy the requests.
