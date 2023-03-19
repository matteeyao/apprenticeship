# Creating a storage class and provisioning volumes of that class

* As you saw in the previous sections, most Kubernetes clusters contain a single storage class named `standard`, but use different provisioners

  * A full-blown cluster such as the one you find in GKE can surely provide more than just a single type of persistent volume

  * So how does one create other type of volumes?

## Inspecting the default storage class in GKE

* Let's look at the default storage class in GKE more closely

  * We've rearranged the fields since the original alphabetical ordering makes the YAML definition more difficult to understand

  * The storage class definition follows:

```yaml
apiVersion: storage.k8s.io/v1
kind: StorageClass
metadata:
   name: standard
   annotations:
      storageclass.kubernetes.io/is-default-class: "true"
      ...
provisioner: kubernetes.io/gce-pd                           # ← A
parameters:                                                 # ← B
  type: pd-standard                                         # ← B
volumeBindingMode: Immediate
allowVolumeExpansion: true
reclaimPolicy: Delete

# ← A ▶︎ The provisioner used to provision volumes of this storage class
# ← B ▶︎ This type parameter is passed to the provisioner
```

* If you create a persistent volume claim that references this storage class, the provisioner `kubernetes.io/gce-pd` is called to create the volume

  * In this call, the provisioner receives the parameters defined in the storage class

  * In the case of the default storage class in GKE, the parameter `type: pd-standard` is passed to the provisioner

  * This tells the provisioner what type of GCE Persistent Disk to create

* You can create additional storage class objects and specify a different value for the `type` parameter

  * You'll do this next

> [!NOTE]
> 
> The availability of GCE Persistent Disk types depends on the zone in which your cluster is deployed. To view the list of types for each availability zone, run `gcloud compute disk-types list`.

## Creating a new storage class to enable the use of SSD persistent disks in GKE

* One of the disk types supported in most GCE zones is the `pd-ssd` type, which provisions a network-attached SSD

  * Let's create a storage class called `fast` and configure it so that the provisioner creates a disk of type `pd-ssd` when you request this storage class in your claim

  * The storage class manifest is shown in the next listing (file [`sc.fast.gcepd.yaml`](sc.fast.gcepd.yaml)) ▶︎ A custom storage class definition:

```yaml
apiVersion: storage.k8s.io/v1       # ← A 
kind: StorageClass                  # ← A
metadata:
  name: fast                        # ← B
provisioner: kubernetes.io/gce-pd   # ← C
parameters:
  type: pd-ssd                      # ← D

# ← A ▶︎ This manifest defines a StorageClass object
# ← B ▶︎ The name of the storage class
# ← C ▶︎ The provisioner to use
# ← D ▶︎ Tells the provisioner to provision an SSD disk
```

> [!NOTE]
> 
> If you're using another cloud provider, check their documentation to find the name of the provisioner and the parameters you need to pass in. If you're using Minikube or kind, and you'd like to run this example, set the provisioner and parameters to the same values as in the default storage class. For this exercise, it doesn't matter if the provisioned volume doesn't actually use an SSD.

* Create the StorageClass object by applying this manifest to your cluster and list the available storage classes to confirm that more than one is now available

  * You can now use this storage class in your claims

  * Let's conclude this section on dynamic provisioning by creating a persistent volume claim that will allow your Quiz pod to use an SSD disk

## Claiming a volume of a specific storage class

* The following listing shows the updated YAML definition of the `quiz-data` claim, which requests the storage class `fast` that you've just created instead of using the default class

  * You'll find the manifest in the file [`pvc.quiz-data-fast.yaml`](pvc.quiz-data-fast.yaml) ▶︎ A persistent volume claim requesting a specific storage class:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: quiz-data-fast
spec:
  storageClassName: fast    # ← A
  resources:
    requests:
      storage: 1Gi
  accessModes:
    - ReadWriteOnce

# ← A ▶︎ This claim requests that this specific storage class be used to provision the volume.
```

* Rather than just specify the size and access modes and let the system use the default storage class to provision the persistent volume, this claim specifies that the storage class `fast` be used for the volume

  * When you create the claim, the persistent volume is created by the provisioner referenced in this storage class, using the specified parameters

* You can now use this claim in a new instance of the Quiz pod

  * Apply the file [`pod.quiz-fast.yaml`](pod.quiz-fast.yaml)

  * If you run this example on GKE, the pod will use an SSD volume

> [!NOTE]
> 
> If a persistent volume claim refers to a non-existent storage class, the claim remains `Pending` until the storage class is created. Kubernetes attempts to bind the claim at regular intervals, generating a `ProvisioningFailed` event each time. You can see the event if your execute the `kubectl describe` command on the claim.
