# Services and DNS

1. A Kubernetes ClusterIP service called `user-db` exists in the `auth-gateway` namespace. The `user-db` Service's cluster IP is `10.23.254.63`. Which of the following addresses could be used to communicate with this service from a pod located in the `default` namespace? Choose two.

[x] 10.23.254.63

> The Service's cluster IP address can be used to communicate with the Service from anywhere within the cluster.

[ ] 10-23-254-63.auth-gateway.pod.cluster.local

[x] user-db.auth-gateway.svc.cluster.local

> The Service's fully-qualified domain name can be used to locate the Service, even from another Namespace.

[ ] user-db

2. What should we use if we need to run multiple copies of a single image in a swarm?

[ ] We should run the `docker-compose` command?

[ ] We should use a stack.

[ ] We should use a task.

[x] We should use a service.

> Services are used to run multiple replicas that use the same image.
