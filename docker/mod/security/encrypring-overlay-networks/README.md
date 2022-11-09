# Encrypting Overlay Networks

Recall that Overlay Networks are those distributed cluster networks that allow your containers and services to communicate transparently across your swarm cluster regardless of which node in the cluster they may be on. But what if we want to encrypt all communication going across those overlay networks?

You can encrypt communication between containers on overlay networks in order to provide greater security within your swarm cluster.

Use the `--opt encrypted` flag when creating an overlay network to encrypt it:

```zsh
docker network create --opt encrypted --driver overlay <NETWORK_NAME>
```

To demonstrate encrypting overlay networks, first, let's create a Docker Overlay Network:

```zsh
docker network create --opt encrypted --driver overlay <NETWORK_NAME>
```

Create a simple service in the network specified above:

```zsh
docker service create --name <SERVICE_NAME> --network <NETWORK_NAME> --replicas 3 nginx
```

To demonstrate communication over the network, let's create another service:

```zsh
docker service create --name encrypted-overlay-busybox --network <NETWORK_NAME> radial/busyboxplus:curl sh -c 'curl encrypted-overlay-nginx:80 && sleep 3600'
```

Check the log for the `busybox` service:

```zsh
docker service logs encrypted-overlay-busybox
```

## MTLS in Docker Swarm

Docker Swarm provides additional security by **encrypting communication** between various components in the cluster.

Swarm-level communication between nodes is encrypted using MTLS (Mutually Authenticated Transport Layer Security). This means that both participants in such communication authenticate with each other (usually using certificates), and the communication is encrypted.

This also means that even if an attacker were able to view swarm-level network communications between two nodes, they would likely be unable to extract any sensitive information from that data.

### Mutually Authenticated Transport Layer Security (MTLS)

* Both participants in communication exchange certificates and all communication is **authenticated** and **encrypted**.

* When a swarm is initialized, a root certificate authority (CA) is created, which is used to generate certificates for all nodes as they join the cluster.

* Worker and manager tokens are generated using the CA and are used to join new nodes to the cluster.

* Used for all cluster-level communication between swarm nodes.

* Enabled by default, you don't need to do anything to set it up.
