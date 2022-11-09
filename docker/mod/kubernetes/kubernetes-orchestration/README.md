# Kubernetes Orchestration in Docker

**Docker Kubernetes Service** allows us to orchestrate container workloads in UCP using Kubernetes. This combines the UCP interface w/ the flexibiliy of a Kubernetes cluster.

## Orchestrator Type

Each UCP node has an **orchestrator type**, which determines whether the node will run workloads managed by Docker Swarm or Kubernetes.

* **Docker Swarm** ▶︎ The node will run Docker Swarm-managed workloads.

* **Kubernetes** ▶︎ The node will run Kubernetes-managed workloads.

* **Mixed** ▶︎ THe node will run workloads managed by both Docker Swarm and Kubernetes. This mode is not recommended for use in production.

## Namespaces

Every Kubernetes object exists in a **namespace**. Namespaces allow you to organize your objects and keep them separate between different teams or applications.

When no namespace is specified in Kubernetes, a namespace called **default** will be assumed.

## Creating Objects

You can create Kubernetes objects using YAML in the UCP interface.
