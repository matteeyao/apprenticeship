# Built-In Network Drivers

1. Which of the following network drivers is the default for connecting containers on the same host?

[ ] `overlay`

[ ] `host`

[x] bridge

[ ] `macvlan`

> The bridge network driver is the default and is used to connect containers on the same host.

2. Which of the following statements about the `overlay` network driver is accurate?

[x] Networking components are created on nodes dynamically when tasks get scheduled on the node.

[ ] The `overlay` driver only allows communication between containers running on the same host.

[ ] The network is set up on every node in the cluster as soon as the network faces creation.

[ ] The network must be set up manually on each node.

> The `overlay` network driver dynamically creates networking components on the node when a relevant task gets scheduled on that node.

3. When creating a container, how would we specify that the container should be attached to an existing network called `my-network`?

[ ] We can use `docker run -n my-network nginx`.

[x] We can use `docker run --network my-network nginx`.

[ ] We can use `docker run --network-alias web nginx`.

[ ] We can use `docker run --attach my-network nginx`.

> This command will attach the container to an existing network called `my-network`.

4. Which network driver connects containers directly to a network stack on the host machine without any isolation?

[ ] `none`

[ ] `macvlan`

[ ] `host`

[ ] bridge

> The `host` network driver connects containers directly to the host's network devices without any isolation.
