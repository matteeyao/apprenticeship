# Claiming a persistent volume

* Your cluster now contains two persistent volumes

  * Before you can use the `quiz-data` volume in the quiz pod, you need to claim it

  * This section explains how to do this

## Creating a PersistentVolumeClaim object

* To claim a persistent volume you create a PersistentVolumeClaim object in which you specify the requirements that the persistent volume must meet

  * These include the minimum capacity of the volume and the required access modes, which are usually dictated by the application that will use the volume

  * For this reason, persistent volume claims should be created by the author of the application and not by cluster administrators, so take off your administrator hat now and put on your developer hat

> [!TIP]
> 
> As an application developer, you should never include persistent volume definitions in your application manifests. You should include persistent volume claims b/c they specify the storage requirements of your application.

* To create a PersistentVolumeClaim object, create a manifest file w/ the contents shown in the following listing ([`pvc.quiz-data.static.yaml`](pvc.quiz-data.static.yaml)) ▶︎ A PersistentVolumeClaim object manifest

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: quiz-data           # ← The name of this claim. The pod will refer to this claim using by this name.
spec:
  resources:
    requests:               # ← The volume must provide at least 1 GiB of storage space.
      storage: 1Gi          # ← The volume must provide at least 1 GiB of storage space.
  accessModes:              # ← The volume must support mounting by a single node for both reading and writing.
  - ReadWriteOnce           # ← The volume must support mounting by a single node for both reading and writing.
  storageClassName: ""      # ← This must be set to an empty string to disable dynamic provisioning.
  volumeName: quiz-data     # ← You want to claim the quiz-data persistent volume.
```

* The persistent volume claim defined in the listing requests that the volume is at least `1 GiB` in size and can be mounted on a single node in read/write mode

  * The field `storageClassName` is used for dynamic provisioning of persistent volumes, which you'll learn about later in the chapter

    * The field must be set to an empty string if you want Kubernetes to bind a pre-provisioned persistent volume to this claim instead of provisioning a new one

* In this exercise, you want to claim the `quiz-data` persistent volume, so you must indicate this w/ the `volumeName` field

  * In your cluster, two matching persistent volumes exist

  * If you don't specify this field, Kubernetes could bind your claim to the `other-data` persistent volume

* If the cluster administrator creates a bunch of persistent volumes w/ non-descript names, and you don't care which one you get, you can skip the `volumeName` field

  * In that case, Kubernetes will randomly choose one of the persistent volumes whose capacity and access modes match the claim

> [!NOTE]
> 
> Like persistent volumes, claims can also specify the required `volumeMode`. As you learned in section [8.2.1](../create-persistentvolume-object/README.md), this can be either `Filesystem` or `Block`. If left unspecified, it defaults to `Filesystem`. When Kubernetes checks whether a volume can satisfy this claim, the `volumeMode` of the claim and the volume is also considered.

* To create the PersistentVolumeClaim object, apply its manifest file w/ `kubectl apply`

  * After the object is created, Kubernetes soon binds a volume to the claim
  
  * If the claim requests a specific persistent volume by name, that's the volume that is bound, if it also matches the other requirements

  * Your claim requires 1 GiB of disk space and the `ReadWriteOnce` access mode

  * The persistent volume `quiz-data` that you created earlier meets both requirements and this allows it to be bound to the claim

## Listing persistent volume claims

* If all goes well, your claim should now be bound to the `quiz-data` persistent volume

  * Use the `kubectl get` command to see if this is the case:

```zsh
$ kubectl get pvc
NAME        STATUS  VOLUME      CAPACITY    ACCESS MODES    STORAGECLASS  AGE
quiz-data   Bound   quiz-data   10Gi        RWO,ROX                       2m    # ← The claim is bound to the quiz-data persistent volume
```

> [!TIP]
> 
> Use `pvc` as a shorthand for `persistentvolumeclaim`.

* The output of the `kubectl` command shows that the claim is now bound to your persistent volume

  * It also shows the capacity and access modes of this volume

  * Even though the claim requested only 1GiB, it has 10GiB of storage space available, b/c that's the capacity of the volume

  * Similarly, although the claim requested only the `ReadWriteOnce` access mode, it is bound to a volume that supports both the `ReadWriteOnce` (`RWO`) and the `ReadOnlyMany` (`ROX`) access modes

* If you put your cluster admin hat back on for a moment and list the persistent volumes in your cluster, you'll see that it too is now displayed as `Bound`:

```zsh
$ kubectl get pv
NAME        CAPACITY    ACCESS MODES    ...   STATUS  CLAIM               ...
quiz-data   10Gi        RWO,ROX         ...   Bound   default/quiz-data   ...
```

* Any cluster admin can see which claim each persistent volume is bound to

  * In our case, the volume is bound to the claim `default/quiz-data`

> [!NOTE]
> 
> You may wonder what the word `default` means in the claim name. This is the _namespace_ in which the PersistentVolumeClaim object is located. Namespaces allow objects to be organized into disjoint sets. You'll learn about them in chapter 10

* By claiming the persistent volume, you and your pods now have the exclusive right to use the volume

  * No one else can claim it until you release it by deleting the PersistentVolumeClaim object
