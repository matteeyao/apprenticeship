# Creating persistent volumes and claims

* Now that you have a basic understanding of persistent volumes and claims and their relationship to the pods, let's revisit the quiz pod from the previous chapter

  * You may remember that this pod contains a `gcePersistentDisk` volume

  * You'll modify that pod's manifest to make it use the GCE Persistent Disk via a PersistentVolume object

* As explained earlier, there are usually two different types of Kubernetes users involved in the provisioning and use of persistent volumes

  * In the following exercises, you will first take on the role of the cluster administrator and create some persistent volumes

  * One of them will point to the existing GCE Persistent Disk

  * Then you'll take on the role of a regular user to create a persistent volume claim to get ownership of that volume and use it in the quiz pod

## Creating a PersistentVolume object

▶︎ See [8.2.1](create-persistentvolume-object/README.md)

## Claiming a persistent volume

▶︎ See [8.2.2](claim-persistent-volume/README.md)


## Using a claim and volume in a single pod

▶︎ See [8.2.3](use-claim-and-volume-in-one-pod/README.md)

## Using a claim and volume in multiple pod

▶︎ See [8.2.4](use-claim-and-volume-in-multiple-pods/README.md)
