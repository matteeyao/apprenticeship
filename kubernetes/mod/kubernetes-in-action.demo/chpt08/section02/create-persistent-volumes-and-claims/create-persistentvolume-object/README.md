# Creating a PersistentVolume object

* Imagine being the cluster administrator

  * The development team has asked you to provide two persistent volumes for their applications

  * One will be used to store the data files used by MongoDB in the quiz pod, and the other will be used for something else

* If you use Google Kubernetes Engine to run these examples, you'll create persistent volumes that point to GCE Persistent Disks (GCE PD

  * For the quiz data files, you can use the GCE PD that you provisioned in the previous chapter

> [!NOTE]
> 
> If you use a different cloud provider, consult the provider's documentation to learn how to create the physical volume in their environment. If you use Minikube, kind, or any other type of cluster, you don't need to create volumes b/c you'll use a persistent volume that refers to a local directory on the worker node.

## Creating a persistent volume w/ GCE Persistent Disk as the underlying storage

* If you don't have the `quiz-data` GCE Persistent Disk set up from the previous chapter, create it again using the `gcloud compute disks create quiz-data` command

  * After the disk is created, you must create a manifest file for the PersistentVolume object, as shown in the following listing ▶︎ [`pv.quiz-data.gcepd.yaml`](pv.quiz-data.gcepd.yaml)) A persistent volume manifest referring to a GCE Persistent Disk:

```yaml
apiVersion: v1
kind: PersistentVolume
metadata:
  name: quiz-data       # ← The name of this persistent volume
spec:
  capacity:             # ← The storage capacity of this volume
    storage: 1Gi        # ← The storage capacity of this volume
  accessModes:          # ← Whether a single node or many nodes can access this volume in read/write or read-only mode.
  - ReadWriteOnce       # ← Whether a single node or many nodes can access this volume in read/write or read-only mode.
  - ReadOnlyMany        # ← Whether a single node or many nodes can access this volume in read/write or read-only mode.
  gcePersistentDisk:    # ← This persistent volume uses the GCE Persistent Disk created in the previous chapter
    pdName: quiz-data   # ← This persistent volume uses the GCE Persistent Disk created in the previous chapter
    fsType: ext4        # ← This persistent volume uses the GCE Persistent Disk created in the previous chapter
```

* The `spec` section in a PersistentVolume object specifies the storage capacity of the volume, the access modes it supports, and the underlying storage technology it uses, along w/ all the information required to use the underlying storage

  * In the case of GCE Persistent Disks, this includes the name of the PD resource in Google Compute Engine, the filesystem type, the name of the partition in the volume, and more

* Now create another GCE Persistent Disk named `other-data` and an accompanying PersistentVolume object

  * Create a new file from the manifest in listing above and make the necessary changes ▶︎ [`pv.other-data.gcepd.yaml`](pv.other-data.gcepd.yaml)

## Creating persistent volumes backed by other storage technologies

* If your Kubernetes cluster runs on a different cloud provider, you should be able to easily change the persistent volume manifest to use something other than a GCE Persistent Disk, as you did in the previous chapter when you directly referenced the volume within the pod manifest

* If you used Minikube or the kind tool to provision your cluster, you can create a persistent volume that uses a local directory on the worker node instead of network storage by using the `hostPath` field in the PersistentVolume manifest

  * The manifest for the `quiz-data` persistent volume is shown in the next listing ([`pv.quiz-data.hostpath.yaml`](pv.quiz-data.hostpath.yaml))

  * The manifest for the `other-data` persistent volume is in [`pv.other-data.hostpath.yaml`](pv.other-data.hostpath.yaml)

  * A persistent volume using a local directory:

```yaml
apiVersion: v1
kind: PersistentVolume
metadata:
  name: quiz-data
spec:
  capacity:
    storage: 1Gi
  accessModes:
  - ReadWriteOnce
  - ReadOnlyMany
  hostPath:               # ← Instead of a GCE Persistent Disk, this persistent volume refers to a local directory on the host node
    path: /var/quiz-data  # ← Instead of a GCE Persistent Disk, this persistent volume refers to a local directory on the host node
```

* You'll notice that the two persistent volume manifests in this and the previous listing differ only in the part that specifies which underlying storage method to use

  * The hostPath-backed persistent volume stores data in the `/var/quiz-data` directory in the worker node's filesystem

> [!NOTE]
> 
> To list all other supported technologies that you can use in a persistent volume, run `kubectl explain pv.spec`. You can then drill further down to see the individual configuration options for each technology. For example, for GCE Persistent Disks, run `kubectl explain pv.spec.gcePersistentDisk`.

* We won't bore you w/ the details of how to configure the persistent volume for each available storage technology, but we do need to explain the `capacity` and `accessModes` fields that you must set in each persistent volume

## Specifying the volume capacity

* The `capacity` of the volume indicates the size of the underlying volume

  * Each persistent volume must specify its capacity so that Kubernetes can determine whether a particular persistent volume can meet the requirements specified in the persistent volume claim before it can bind them

## Specifying volume access modes

* Each persistent volume must specify a list of `accessModes` it supports

  * Depending on the underlying technology, a persistent volume may or may not be mounted by multiple worker nodes simultaneously in read/write or read-only mode

  * Kubernetes inspects the persistent volume's access modes to determine if it meets the requirements of the claim

> [!NOTE]
> 
> The access mode determines how many _nodes_, not pods, can attach the volume at a time. Even if a volume can only be attached to a single node, it can be mounted in many pods if they all run on that single node.

* Three access modes exist

  * They are explained in the following table along w/ their abbreviated form displayed by `kubectl` ▶︎ Persistent volume access modes:

| **Access Mode** | **Abbr.** | **Description**                                                                                                                              |
|-----------------|-----------|----------------------------------------------------------------------------------------------------------------------------------------------|
| `ReadWriteOnce` | RWO       | The volume can be mounted by a single worker node in read/write mode.<br>While it's mounted to the node, other nodes can't mount the volume. |
| `ReadOnlyMany`  | ROX       | The volume can be mounted on multiple worker nodes simultaneously in read-only mode.                                                         |
| `ReadWriteMany` | RWX       | The volume can be mounted in read/write mode on multiple worker nodes at the same time.                                                      |

> [!NOTE]
> 
> The `ReadOnlyOnce` option doesn't exist. If you use a `ReadWriteOnce` volume in a pod that doesn't need to write to it, you can mount the volume in read-only mode.

## Using persistent volumes as block devices

* A typical application uses persistent volumes w/ a formatted filesystem

  * However, a persistent volume can also be configured so that the application can directly access the underlying block device w/o using a filesystem

  * This is configured on the PersistentVolume object using the `spec.volumeMode` field

  * The supported values for the field are explained in the next table ▶︎ Configuring the volume mode for the persistent volume:

| **Volume Mode** | **Description**                                                                                                                                                                                                                                                                                                                                                                                 |
|-----------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Filesystem`    | When the persistent volume is mounted in a container, it is mounted to a directory in the file of the container. If the underlying storage is an unformatted block device, Kubernetes formats the device using the filesystem specified in the volume definition (for example, in the field `gcePersistentDisk.fsType`) before it is mounted in the container. This is the default volume mode. |
| `Block`         | When a pod uses a persistent volume w/ this mode, the volume is made available to the application in the container as a raw block device (w/o a filesystem). This allows the application to read and write data w/o any filesystem overhead. This mode is typically used by special types of applications, such as database systems.                                                            |

* The manifests for the `quiz-data` and `other-data` persistent volumes do not specify a `volumeMode` field, which means that the default mode is used, namely `Filesystem`

## Creating and inspecting the persistent volume

* You can now create the PersistentVolume objects by posting the manifests to the Kubernetes API using the now well-known command `kubectl apply`

  * Then use the `kubectl get` command to list the persistent volumes in your cluster:

```zsh
$ kubectl get pv
NAME        CAPACITY    ACCESS MODES    ...   STATUS      CLAIM   ...   AGE
other-data  10Gi        RWO,ROX         ...   Available           ...   3m
quiz-data   10Gi        RWO,ROX         ...   Available           ...   3m
```

> [!TIP]
> 
> Use `pv` as the shorthand for PersistentVolume.

* The `STATUS` column indicates that both persistent volumes are `Available`

  * This is expected b/c they aren't yet bound to any persistent volume claim, as indicated by the empty `CLAIM` column

  * Also displayed are the volume capacity and access modes, which are shown in abbreviated form, as explained in the previous table

* The underlying storage technology used by the persistent volume isn't displayed by the `kubectl get pv` command b/c it's less important

  * What is important is that each persistent volume represents a certain amount of storage space available in the cluster that applications can access w/ the specified modes

  * The technology and the other parameters configured in each persistent volume are implementation details that typically don't interest users who deploy applications

  * If someone needs to see these details, they can use `kubectl describe` or print the full definition of the PersistentVolume object as in the following command:

```zsh
$ kubectl get pv quiz-data -o yaml
```
