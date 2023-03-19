# Exposing services externally

* ClusterIP services like the ones you created in the previous section are only accessible within the cluster

  * B/c clients must be able to access the Kiada service from outside the cluster, creating a ClusterIP won't suffice

* If you need to make a service available to the outside world, you can do one of the following:

  * assign an additional IP to a node and set it as one of the service's `externalIPs`,

  * set the service's type to `NodePort` and access the service through the node's port(s),

  * ask Kubernetes to provision a load balancer by setting the type to `LoadBalancer`, or

  * expose the service through an Ingress object

* A rarely used method is to specify an additional IP in the `spec.externalIPs` field of the Service object

  * By doing this, you're telling K8s to treat any traffic directed to that IP address as traffic to be processed by the service

  * When you ensure that this traffic arrives at a node w/ the service's external IP as its destination, K8s forwards it to one of the pods that back the service

* A more common way to make a service available externally is to set its type to `NodePort`

  * K8s makes the service available on a network port on all cluster nodes (the so-called node port, from which this service type gets its name)

  * Like `ClusterIP` services, the service gets an internal cluster IP, but is also accessible through the node port on each of the cluster nodes

  * Usually, you then provision an external load balancer that redirects traffic to these node ports

  * The clients can connect to your service via the load-balancer's IP address

* Instead of using a `NodePort` service and manually setting up the load balancer, K8s can also do this for you if you set the service type to `LoadBalancer`

  * However, not all clusters support this service type, as the provisioning of the load balancer depends on the infrastructure the cluster is running on

  * Most cloud providers support LoadBalancer services in their clusters, whereas clusters deployed on premises require an add-on such as MetalLB, a load-balancer implementation for bare-metal K8s clusters

* The final way to expose a group of pods externally is radically different

  * Instead of exposing the service externally via node ports and load balancers, you can use an Ingress object

  * How this object exposes the service depends on the underlying ingress controller, but it allows you to expose many services through a single externally reachable IP address

## 11.2.1 Exposing pods through a NodePort service

* See [nodeport-service](nodeport-service/README.md)

## 11.2.2 Exposing a service through an external load balancer

* See [external-load-balancer](external-load-balancer/README.md)

## 11.2.3 Configuring the external traffic policy for a service

* See [external-traffic-policy](external-traffic-policy/README.md)
