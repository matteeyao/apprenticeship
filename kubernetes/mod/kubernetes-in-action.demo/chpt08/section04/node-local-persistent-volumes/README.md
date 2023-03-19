# Node-local persistent volumes

* In the previous sections of this chapter, you've used persistent volumes and claims to provide network-attached storage volumes to your pods, but this type of storage is too slow for some applications

  * To run a production-grade database, you should probably use an SSD connected directly to the node where the database is running

* In the previous chapter, you learned that you can use a `hostPath` volume in a pod if you want the pod to access part of the host's filesystem

  * Now you'll learn how to do the same w/ persistent volumes

  * You might wonder why we need to each you another way to do the same thing, but it's really not the same

* You might remember that when you add a `hostPath` volume to a pod, the data that the pod sees depends on which node the pod is scheduled to

  * In other words, if the pod is deleted and recreated, it might end up on another node and no longer have access to the same data

* If you use a local persistent volume instead, the problem is resolved

  * The Kubernetes scheduler ensures that the pod is always scheduled on the node to which the local volume is attached

> [!NOTE]
> 
> Local persistent volumes are also better than `hostPath` volumes b/c they offer much better security. As explained in the previous chapter, you don't want to allow regular users to use `hostPath` volumes at all. B/c persistent volumes are managed by the cluster administrator, regular users can't use them to access arbitrary paths on the host node.

## Creating local persistent volumes

▶︎ See [8.4.1](create-local-persistent-volumes/README.md)

## Claiming and using local persistent volumes

▶︎ See [8.4.2](claim-and-use-local-persistent-volumes/README.md)
