## 1.2.1 Understanding how Kubernetes transforms a computer cluster

It frees application developers from the need to implement infrastructure-related mechanisms in their applications; instead, they rely on Kubernetes to provide them. This includes things like:

* _service discovery_ ▶︎ a mechanism that allows applications to find other applications and use the services they provide,

* _horizontal scaling_ ▶︎ replicating your application to adjust to fluctuations in load,

* _load-balancing_ ▶︎ distributing load across all the application replicas,

* _self-healing_ ▶︎ keeping the system healthy by automatically restarting failed applications and moving them to healthy nodes after their nodes fail,

* _leader election_ ▶︎ a mechanism that decides which instance of the application should be active while the others remain idle but ready to take over if the active instance fails
