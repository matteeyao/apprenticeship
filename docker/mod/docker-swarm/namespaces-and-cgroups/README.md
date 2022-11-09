# Namespaces and Cgroups

**Namespaces**: A Linux related technology that isolates processes by partitioning the resources that are available to them. Namespaces prevent processes from interfering with one another. Docker leverages namespaces to isolate resources for containers.

Docker uses namespaces to isolate containers. This technology allows containers to operate independently and securely.

Docker uses namespaces such as the following to isolate resources for containers:

* `pid`: Process isolation

* `net`: Network interfaces

* `ipc`: `Inter-process communication

* `mnt`: Filesystem mounts

* `uts`: Kernel and version identifiers

* `user namespaces`: Requires special configuration; Allows container processes to run as root inside the container while mapping that user to an unprivileged user on the host

**Control Groups (cgroups)**: Control groups limit processes to a specific set of resources. Docker uses `cgroups` to enforce rules around resource usage by containers, such as limiting memory or CPU usage.
