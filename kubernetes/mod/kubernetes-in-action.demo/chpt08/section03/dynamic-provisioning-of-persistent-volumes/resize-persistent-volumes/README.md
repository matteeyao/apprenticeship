# Resizing persistent volumes

* If the cluster supports dynamic provisioning, a cluster user can self-provision a storage volume w/ the properties and size specified in the claim and referenced storage class

  * If the user later needs a different storage class for their volume, they must, as you might expect, create a new persistent volume claim that references the other storage class

  * Kubernetes does not support changing the storage class name in an existing claim

  * If you try to do so, you receive the following error message:

```zsh
* spec: Forbidden: is immutable after creation except resources.requests for bound claims
```

* The error indicates that the majority of the claim's specification is immutable

  * The part that is mutable is `spec.resources.requests`, which is where you indicate the desired size of the volume

* In the previous MongoDB examples you requested 1GiB of storage space

  * Now imagine that the database grows near this size

  * Can the volume be resized w/o restarting the pod and application?

  * Let's find out

## Requesting a larger volume in an existing persistent volume claim

* If you use dynamic provisioning, you can generally change the size of a persistent volume simply by requesting a larger capacity in the associated claim

  * For the next exercise, you'll increase the size of the volume by modifying the `quiz-data-default` claim, which should still exist in your cluster

* To modify the claim, either edit the manifest file or create a copy and then edit it

  * Set the `spec.resources.requests.storage` field to `10Gi` as shown in the following listing (file [`pvc.quiz-data-default.10gib.pvc.yaml`](pvc.quiz-data-default.10gib.yaml)) ▶︎ Requesting a larger volume:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: quiz-data-default     # ← A
spec:
  resources:                  # ← B
    requests:                 # ← B
      storage: 10Gi           # ← B
  accessModes:
    - ReadWriteOnce
  
# ← A ▶︎ Ensure that the name matches the name of the existing claim.
# ← B ▶︎ Request a larger amount of storage.
```

* When you apply this file w/ the `kubectl apply` command, the existing PersistentVolumeClaim object is updated

  * Use the `kubectl get pvc` command to see if the volume's capacity has increased:

```zsh
$ kubectl get pvc quiz-data-default
NAME                STATUS    VOLUME        CAPACITY    ACCESS MODES  ...
quiz-data-default   Bound     pvc-ed36b...  1Gi         RWO           ...
```

* You may recall that when claims are listed, the `CAPACITY` column displays the size of the bound volume and not the size requirement specified in the claim

  * According to the output, this means that the size of the volume hasn't changed

  * Let's find out why

## Determining why volume hasn't been resized

* To find out why the size of the volume has remained the same regardless of the change you made to the claim, the first thing you might do is inspect the claim using `kubectl describe`

  * If this is the case, you've already got the hang of debugging objects in Kubernetes

  * You'll find that one of the claim's conditions clearly explains why the volume was not resized:

```zsh
$ kubectl describe pvc quiz-data-default ...
Conditions:
  Type                      Status ... Message
  ----                      ------ ... -------
  FileSystemResizePending   True       Waiting for user to (re-)start a
                                      pod to finish file system resize of
                                      volume on node.
```

* To resize the persistent volume, you may need to delete and recreate the pod that uses the claim

  * After you do this, the claim and the volume will display the new size:

```zsh
$ kubectl get pvc quiz-data-default
NAME                STATUS  VOLUME        CAPACITY    ACCESS MODES  ...
quiz-data-default   Bound   pvc-ed36b...  10Gi        RWO           ...
```

## Allowing and disallowing volume expansion in the storage class

* The previous example shows that cluster users can increase the size of the bound persistent volume by changing the storage requirement in the persistent volume claim

  * However, this is only possible if it's supported by the provisioner and the storage class

* When the cluster administrator creates a storage class, they can use the `spec.allowVolumeExpansion` field to indicate whether volumes of this class can be resized

  * If you attempt to expand a volume that you're not supposed to expand, the API server immediately rejects the update operation on the claim
