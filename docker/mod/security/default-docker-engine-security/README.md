# Default Docker Engine Security

## Namespaces and Cgroups

Namespaces and Control Groups (cgroups) provide **isolation** to containers.

Docker uses namespaces to isolate container processes from one other and the host. This prevents an attacker from affecting or gaining control of other containers or the host if they manage to gain control of one container.

**Isolation** means that container processes cannot see or affect other containers or processes running directly on the host system.

**Isolation** limits the impact of certain **exploits** or **privilege escalation** attacks. If one container is compromised, it is less likely that it can be used to gain any further access outside the container.

## Docker Daemon Attack Surface

The Docker daemon must run with root access. Additionally, be aware of this before allowing anything or anyone to interact with the daemon. It could be used to gain access to the entire host.

It is important to note that the Docker daemon itself requires **root** privileges. Therefore, you should be aware of the potential **attack surface** presented by the Docker daemon.

**Only allow trusted users to access the daemon**. Control of the Docker daemon could allow the entire host to be compromised.

Be aware of this if you are building any automation that accesses the Docker daemon, or granting any users direct access to it.

## Linux Kernel Capabilities

Docker leverages Linux **capabilities** to assign granular permissions to container processes and fine-tune what container processes can access. For example, listening on a low port (below 1024) usually requires a process to run as root, but Docker uses Linux capabilities to allow a container to listen on port 80 without running as root.

This means that a process can run as **root** inside a container, but does not have access to do everything root could normally do on the host.

For example, Docker uses the `net_bind_service` capability to allow container processes to bind to a port below 1024 w/o running as root.
