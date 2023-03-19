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

* 8.2 [Creating persistent volumes and claims](section02/create-persistent-volumes-and-claims/README.md)

* 8.3 [Dynamic provisioning of persistent volumes](section03/dynamic-provisioning-of-persistent-volumes/README.md)

* 8.4 [Node-local persistent volumes](section04/node-local-persistent-volumes/README.md)

## Learning summary

* This chapter explained the details of adding persistent storage for your applications

  * Infrastructure-specific information about storage volumes doesn't belong in pod manifests

    * Instead, it should be specified in the PersistentVolume object

  * A PersistentVolume object represents a portion of the disk space that is available to applications within the cluster

  * Before an application can use a PersistentVolume, the user who deploys the application must claim the PersistentVolume by creating a PersistentVolumeClaim object

  * A PersistentVolumeClaim object specifies the minimum size and other requirements that the PersistentVolume must meet

  * When using statically provisioned volumes, Kubernetes finds an existing persistent volume that meets the requirements set forth in the claim and binds it to the claim

  * When the cluster provides dynamic provisioning, a new persistent volume is created for each claim

    * The volume is created based on the requirements specified in the claim

  * A cluster administrator creates StorageClass objects to specify the storage classes that users can request in their claims

  * A user can change the size of the persistent volume used by their application by modifying the minimum volume size requested in the claim

  * Local persistent volumes are used when applications need to access disks that are directly attached to nodes

    * This affects the scheduling of the pods, since the pod must be scheduled to one of the nodes that can provide a local persistent volume

    * If the pod is subsequently deleted or recreated, it will always be scheduled to the same node

* In the next chapter, you'll learn how to pass configuration data to your applications using command-line arguments, environment variables, and files

  * You'll learn how to specify this data directly in the pod manifest and other Kubernetes API objects
