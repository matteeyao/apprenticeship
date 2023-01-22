# Using other persistent volume types

* In the previous exercise, we explained how to add persistent storage to a pod running in Google Kubernetes Engine

  * If you run your cluster elsewhere, you should use whatever volume type is supported by the underlying infrastructure

* For example, if your Kubernetes cluster runs on Amazon's AWS EC2, you can use an `awsElasticBlockStore` volume

  * If your cluster runs on Microsoft Azure, you can use the `azureFile` or the `azureDisk` volume

  * Similar to the previous [section](../google-compute-engine-persistent-disk), you first need the actual underlying storage and then set the right fields in the volume definition

## Using an AWS Elastic Block Store volume

* For example, if you want to use an AWS Elastic Block Store volume instead of the GCE Persistent Disk, you only need to change the volume definition as shown in the following listing (file [`pod.quiz.aws.yaml`](./pod.quiz.aws.yaml)) ▶︎ Using an awsElasticBlockStore volume in the quiz pod:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: quiz
spec:
  volumes:
    - name: quiz-data
      awsElasticBlockStore: # ← This volume refers to an awsElasticBlockStore.
        volumeID: quiz-data # ← The ID of the EBS volume
        fsType: ext4        # ← The filesystem type
  containers:
  - ...
```

## Using an NFS volume

* If your cluster runs on your own servers, you have a range of other supported options for adding external storage to your pods

  * For example, to mount an NFS share, you specify the NFS server address and the exported path, as shown in the following listing (file [`pod.quiz.nfs.yaml`](./pod.quiz.nfs.yaml)) ▶︎ Using an nfs volume in the quiz pod:

```yaml
...
  volumes:
  - name: quiz-data
    nfs:                # ← This volume refers to an NFS share.
      server: 1.2.3.4   # ← IP address of the NFS server
      path: /some/path  # ← File path exported by the server
...
```

> [!NOTE]
> 
> Although Kubernetes supports nfs volumes, the operating system running on the worker nodes provisioned by Minikube or kind might not support mounting nfs volumes.

## Using other storage technologies

* Other supported options are `iscsi` for mounting an iSCSI disk resource, `glusterfs` for a GlusterFS mount, `rbd` for a RADOS Block Device, `flexVolume`, `cinder`, `cephfs`, `flocker`, `fc` (Fibre Channel), and others

* For default on the properties that you need to set for each of these volume types, you can either refer to the Kubernetes API definitions in the Kubernetes API reference or look up the information by running `kubectl explain pod.spec.volumes`

  * If you're already familiar w/ a particular storage technology, you should be able to use the `explain` command to easily figure out how to configure the correct volume type (for example, for iSCSI you can see the configuration options by running `kubectl explain pod.spec.volumes.iscsi`)

## Why does Kubernetes force software developers to understand low-level storage?

* If you're a software developer and not a system administrator, you might wonder if you really need to know all this low-level information about storage volumes?

  * As a developer, should you have to deal w/ infrastructure-related storage details when writing the pod definition, or should this be left to the cluster administrator?

* At the beginning of this book, we discovered that Kubernetes abstracts away the underlying infrastructure

  * The configuration of storage volumes explained earlier clearly contradicts this

  * Furthermore, including infrastructure-related information, such as the NFS server hostname directly in a pod manifest means that this manifest is tied to this specific Kubernetes cluster

  * You can't use the same manifest w/o modification to deploy the pod in another cluster

* Fortunately, Kubernetes offers another way to add external storage to your pods-one that divides the responsibility for configuring and using the external storage volume into two parts

  * The low-level part is managed by cluster administrators, while software developers only specify the high-level storage requirements for their applications

  * Kubernetes then connects the two parts

* You'll learn about this in the next chapter, but first you need a basic understanding of pod volumes
