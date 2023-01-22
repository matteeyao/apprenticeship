# Introducing the hostPath volume

* A `hostPath` volume points to a specific file or directory in the filesystem of the host node, as shown in the next figure

  * Pods running on the same node and using the same path in their `hostPath` volume have access to the same files, whereas pods on other nodes do not

![Fig. 1 A `hostPath` volume mounts a file or directory from the worker node's filesystem into the container.](../../../../../../img/kubernetes-in-action.demo/chpt07/section04/access-files-on-worker-nodes-filesystem/hostpath-volume/diag01.png)

* A `hostPath` volume is not a good place to store the data of a database unless you ensure that the pod running the database always runs on the same node

  * B/c the contents of the volume are stored on the filesystem of a specific node, the database pod will not be able to access the data if it gets rescheduled to another node

  * Typically, a `hostPath` volume is used in cases where the pod needs to read or write files in the node's filesystem that the processes running on the node read or generate, such as system-level logs

* The `hostPath` volume type is one of the most dangerous volume types in Kubernetes and is usually reserved for use in privileged pods only

  * If you allow unrestricted use of the `hostPath` volume, users of the cluster can do anything the want on the node

  * For example, they can use it to mount the Docker socket file (typically `/var/run/docker.sock`) in their container and then run the Docker client within the container to run any command on the host node as the root user
