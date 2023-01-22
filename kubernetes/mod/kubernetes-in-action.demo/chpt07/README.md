# Chapter 7. Mounting storage volumes into the Pod's containers

## Learning objectives

- [ ] Persisting files across container restarts

- [ ] Sharing files between containers of the same pod

- [ ] Sharing files between pods

- [ ] Attaching network storage to pods

- [ ] Accessing the host node filesystem from within a pod

## Directory

### Modules

* 7.1 [Introducing volumes](section01/volumes-overview/README.md)

* 7.2 [Using an emptyDir volume](section02/emptydir-volume/README.md)

* 7.3 [Using external storage in pods](section03/external-storage/README.md)

* 7.4 [Accessing files on the worker node's filesystem](section04/access-files-on-worker-nodes-filesystem/README.md)

### Fortune pod with or without a volume

- [fortune-no-volume.yaml](fortune-no-volume.yaml) - YAML manifest file for the `fortune-no-volume` pod

- [fortune-emptydir.yaml](fortune-emptydir.yaml) - YAML manifest file for the `fortune-emptydir` pod with an emptyDir volume

- [fortune.yaml](fortune.yaml) - YAML manifest file for the `fortune` pod with two containers that share a volume

### MongoDB pod with external volume

- [mongodb-pod-gcepd.yaml](mongodb-pod-gcepd.yaml) - YAML manifest file for the `mongodb` pod using a GCE Persistent Disk volume

- [mongodb-pod-aws.yaml](mongodb-pod-aws.yaml) - YAML manifest file for the `mongodb` pod using an AWS Elastic Block Store volume

- [mongodb-pod-nfs.yaml](mongodb-pod-nfs.yaml) - YAML manifest file for the `mongodb` pod using an NFS volume

- [mongodb-pod-hostpath.yaml](mongodb-pod-hostpath.yaml) - YAML manifest file for the `mongodb` pod using a hostPath volume (for use in _Minikube_)

- [mongodb-pod-hostpath-kind.yaml](mongodb-pod-hostpath-kind.yaml) - YAML manifest file for the `mongodb` pod using a hostPath volume (for use in _kind_)

### Using a hostPath volume

- [node-explorer.yaml](node-explorer.yaml) - YAML manifest file for the `node-explorer` pod

- [node-explorer-directory.yaml](node-explorer-directory.yaml) - YAML manifest file for the `node-explorer` pod with the hostPath volume type set to Directory

## Learning summary

* Pods consist of containers and volumes

  * Each volume can be mounted at the desired location in the container's filesystem

* Volumes are used to persist data across container restarts, share data between containers in the pod, and even share data between the pods

* Many volume types exist

  * Some are generic and can be used in any cluster regardless of cluster environment, while others, such as the `gcePersistentDisk`, can only be used if the cluster runs on a specific cloud provider's infrastructure

* An `emptyDir` volume is used to store data for the duration of the pod

  * It starts as an empty directory just before the pod's containers are started and is deleted when the pod terminates

* The `gitRepo` volume is a deprecated volume type that is initialized by cloning a Git repository

  * Alternatively, an `emptyDir` volume can be used in combination w/ an init container that initializes the volume from Git or any other source

* Network volumes are typically mounted by the host node and then exposed to the pod(s) on that node

* Depending on the underlying storage technology, you may or may not be able to mount a network storage volume in read/write mode on multiple nodes simultaneously

* By using a proprietary volume type in a pod manifest, the pod manifest is tied to a specific Kubernetes cluster

  * The manifest must be modified before it can be used in another cluster

  * Chapter 8 explains how to avoid this issue

* The `hostPath` volume allows a pod to access any path in a filesystem of the worker node

  * This volume type is dangerous b/c it allows users to make changes to the configuration of the worker node and run any process they want on the node

* In the next chapter, you'll learn how to abstract the underlying storage technology away from the pod manifest and make the manifest portable to any other Kubernetes cluster
