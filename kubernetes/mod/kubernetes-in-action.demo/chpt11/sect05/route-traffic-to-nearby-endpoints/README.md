# Configuring services to route traffic to nearby endpoints

* When you deploy pods, they are distributed across the nodes in the cluster

  * If cluster nodes span different availability zones or regions and the pods deployed on those nodes exchange traffic w/ each other, network performance and traffic costs can become an issue

  * In this case, it makes sense for services to forward traffic to pods that aren't far from the pod where the traffic originates

* In other cases, a pod may need to communicate only w/ service endpoints on the same node as the pod

  * Not for performance or cost reasons, but b/c only the node-local endpoints can provide the service in the proper context

## 11.5.1 Forwarding traffic only within the same node w/ internalTrafficPolicy

* See [internal-traffic-policy](internal-traffic-policy/README.md)

## 11.5.2 Topology-aware hints

* See [topology-aware-hints](topology-aware-hints/README.md)
