# Chapter 8. Persisting data in PersistentVolumes

## Learning objectives

- [ ] Using PersistentVolume objects to represent persistent storage

- [ ] Claiming persistent volumes w/ PersistentVolumeClaim objects

- [ ] Dynamic provisioning of persistent volumes

- [ ] Using node-local persistent storage

* The previous chapter taught you how to mount a network storage volume into your pods

  * However, the experience was not ideal b/c you needed to understand the environment your cluster was running in to know what type of volume to add to your pod

  * For example, if your cluster runs on Google's infrastructure, you mst define a `gcePersistentDisk` volume in your pod manifest

  * You can't use the same manifest to run your application on Amazon b/c GCE Persistent Disks aren't supported in their environment

  * To make the manifest compatible w/ Amazon, one must modify the volume definition in the manifest before deploying the pod

* You may remember from chapter 1 that Kubernetes is supposed to standardize application deployment between cloud providers

  * Using proprietary storage volume types in pod manifests goes against this premise

* Fortunately, there is a better way to add persistent storage to your pods-one where you don't refer to a specific storage technology

  * This chapter explains this improved approach

## Sections

* 8.1 [Decoupling pods from the underlying storage technology](section01/decoupling-pods-from-underlying-storage/README.md)

* 7.2 [Using an emptyDir volume](section02/emptydir-volume/README.md)

* 7.3 [Using external storage in pods](section03/external-storage/README.md)

* 7.4 [Accessing files on the worker node's filesystem](section04/access-files-on-worker-nodes-filesystem/README.md)

## Learning summary
