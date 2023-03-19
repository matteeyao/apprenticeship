## 1.2.1 Understanding how Kubernetes transforms a computer cluster

It frees application developers from the need to implement infrastructure-related mechanisms in their applications; instead, they rely on Kubernetes to provide them. This includes things like:

* _service discovery_ ▶︎ a mechanism that allows applications to find other applications and use the services they provide,

* _horizontal scaling_ ▶︎ replicating your application to adjust to fluctuations in load,

* _load-balancing_ ▶︎ distributing load across all the application replicas,

* _self-healing_ ▶︎ keeping the system healthy by automatically restarting failed applications and moving them to healthy nodes after their nodes fail,

* _leader election_ ▶︎ a mechanism that decides which instance of the application should be active while the others remain idle but ready to take over if the active instance fails

## Kubernetes components

* An **API server**

  * The users, management devices, Command line interfaces all talk to the API server to interact w/ the k8s cluster

* An **etcd** service

  * **etcd** is a distributed reliable key-value store used by K8s to store all data used to manage the cluster. When you have multiple nodes and multiple masters in your cluster, **etcd** stores all that information on all the nodes in the cluster in a distributed manner

  * **etcd** is responsible for implementing locks within the cluster to ensure there are no conflicts between the Masters

* A **kubelet** service

  * The agent that runs on each node in the cluster

  * The agent is responsible for making sure that the containers are running on the nodes as expected

* A **Container Runtime** i.e. Docker

* **Controllers**

  * Brains behind the orchestration

  * Responsible for noticing and responding when nodes, containers or endpoints go down-the controllers decide when to bring up new containers in such cases

* **Schedulers**

  * **Schedulers** are responsible for distributing work or containers across multiple nodes

  * **Schedulers** look for newly created containers and assigns them to Nodes

> [!NOTE]
> 
> An additional component on the Node is the kube-proxy. It takes care of networking within K8s.
