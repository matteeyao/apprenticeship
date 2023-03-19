# Creating local persistent volumes

* Imagine you are a cluster administrator and you have just connected a fast SSD directly to one of the worker nodes

  * B/c this is a new class of storage in the cluster, it makes sense to create a new StorageClass object that represents it

## Creating a storage class to represent local storage

* Create a new storage class manifest as shown in the following listing ▶︎ Defining the local storage class:

```yaml
apiVersion: storage.k8s.io/v1
kind: StorageClass
  metadata:
    name: local                           # ← A
provisioner: kubernetes.io/no-provisioner # ← B
volumeBindingMode: WaitForFirstConsumer   # ← C

# ← A ▶︎ Let’s call this storage class local
# ← B ▶︎ Persistent volumes of this class are provisioned manually
# ← C ▶︎ The persistent volume claim should be bound only when the first pod that uses the claim is deployed.
```

* As I write this, locally attached persistent volumes need to be provisioned manually, so you need to set the provisioner as shown in the listing

  * B/c this storage class represents locally attached volumes that can only be accessed within the nodes to which they are physically connected, the `volumeBindingMode` is set to `WaitForFirstConsumer`, so the binding of the claim is delayed until the pod is scheduled

## Attaching a disk to a cluster node

* I assume that you're using a Kubernetes cluster created w/ the kind tool to run this exercise

  * Let's emulate the installation of the SSD in the node called `kind-worker`

  * Run the following command to create an empty directory at the location `/mnt/ssd1` in the node's filesystem:

```zsh
$ docker exec kind-worker mkdir /mnt/ssd1
```

## Creating a PersistentVolume object for the new disk

* After attaching the disk to one of the nodes, you must tell Kubernetes that this node now provides a local persistent volume by creating a PersistentVolume object

  * The manifest for the persistent volume is shown in the following listing ▶︎ Defining a local persistent volume:

```yaml
kind: PersistentVolume
apiVersion: v1
metadata:
  name: local-ssd-on-kind-worker        # ← A
spec:
  accessModes:
  - ReadWriteOnce
  storageClassName: local               # ← B
  capacity:
    storage: 10Gi
  local:                                # ← C
    path: /mnt/ssd1                     # ← C
  nodeAffinity:                         # ← D
    required:                           # ← D
      nodeSelectorTerms:                # ← D
      - matchExpressions:               # ← D
        - key: kubernetes.io/hostname   # ← D
          operator: In                  # ← D
          values:                       # ← D
          - kind-worker                 # ← D
  
# ← A ▶︎ This persistent volume represents the local SSD installed in the kind-worker node, hence the name.
# ← B ▶︎ This volume belongs to the local storage class.
# ← C ▶︎ This volume is mounted in the node's filesystem at the specified path.
# ← D ▶︎ This section tells Kubernetes which nodes can access this volume. Since the SSD is attached only to the node kind-worker, it is only accessible on this node.
```

* B/c this persistent volume represents a local disk attached to the `kind-worker` node, you give it a name that conveys this information

  * It refers to the `local` storage class that you created previously

  * Unlike previous persistent volumes, this volume represents storage space that is directly attached to the node

  * You therefore specify that it is a `local` volume

* Within the `local` volume configuration, you also specify the path where the SSD is mounted (`/mnt/ssd1`)

* At the bottom of the manifest, you'll find several lines that indicate the volume's node affinity

  * A volume's node affinity defines which node can access this volume

> [!NOTE]
> 
> You'll learn more about node affinity and selectors in later chapters. Although it looks complicated, the node affinity definition in the listing simply defines that the volume is accessible from nodes whose `hostname` is `kind-worker`. This is obviously exactly one node.

* As cluster administrator, you've now done everything you needed to do to enable cluster users to deploy applications that use locally attached persistent volumes

  * Now, it's time to put your application developer hat back on again
