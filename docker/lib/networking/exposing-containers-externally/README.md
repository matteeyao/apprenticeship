# Exposing Containers Externally

1. Which of the following is true of a service that has a port published in `ingress` mode?

[ ] The service will only listen on nodes that are running tasks associated with the service.

[x] The service will listen on all nodes on the cluster.

[ ] The service will only listen on worker nodes that are running the service's tasks and manager nodes.

[ ] The service will only listen on a manager.

> With `ingress` mode services listen on all nodes in the cluster. 
