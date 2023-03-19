# Dynamic provisioning using the default storage class

* You've previously used a statically provisioned persistent volume for the quiz pod

  * Now you'll use dynamic provisioning to achieve the same result, but w/ much less manual work

  * And most importantly, you can use the same pod manifest, regardless of whether you use GKE, Minikube, kind, or any other tool to run your cluster, assuming that a default storage class exists in the cluster

## Creating a claim w/ dynamic provisioning

* To dynamically provision a persistent volume using the storage class from the previous section, you can create a PersistentVolumeClaim object w/ the `storageClassName` field set to `standard` or w/ the field omitted altogether

* Let's use the latter approach, as this makes the manifest as minimal as possible

  * You can find the manifest in the [`pvc.quiz-data-default.yaml`](pvc.quiz-data-default.yaml) file

  * Its contents are shown in the following listing ▶︎ A minimal PVC definition that uses the default storage class:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: quiz-data-default
spec:                       # ← A
  resources:                # ← B
    requests:               # ← B
      storage: 1Gi          # ← B
  accessModes:              # ← C
  - ReadWriteOnce           # ← C
  
# ← A ▶︎ The default storage class is used for this claim because the storageClassName field isn’t set.
# ← B ▶︎ The minimum size of the volume
# ← C ▶︎ The desired access mode
```

* This PersistentVolumeClaim manifest contains only the storage size request and the desired access mode, but no `storageClassName` field, so the default storage class is used

* After you create the claim w/ `kubectl apply`, you can see which storage class it's using by inspecting the claim w/ `kubectl get`

  * This is what you'll see if you use GKE:

```zsh
$ kubectl get pvc quiz-data-default
NAME                STATUS  VOLUME            CAPACITY    ACCESS MODES    STORAGECLASS  AGE
quiz-data-default   Bound   pvc-ab623265-...  1Gi         RWO             standard      3m
```

* As expected, and as indicated in the `STORAGECLASS` column, the claim you just created uses the `standard` storage class

* In GKE and Minikube, the persistent volume is created immediately and bound to the claim

  * However, if you create the same claim in a kind-provisioned cluster, that's not the case:

```zsh
$ kubectl get pvc quiz-data-default
NAME                STATUS    VOLUME    CAPACITY    ACCESS MODES    STORAGECLASS   AGE
quiz-data-default   Pending                                         standard       3m
```

* In a kind-provisioned cluster, and possibly other clusters, too, the persistent volume claim you just created is not bound immediately and its status is `Pending`

* In one of the previous sections, you learned that this happens when no persistent volume matches the claim, either b/c it doesn't exist or b/c it's not available for binding

  * However, you are now using dynamic provisioning, where the volume should be created after you create the claim, and specifically for this claim

  * Is your claim pending b/c the cluster needs more time to provision the volume?

* No, the reason for the pending status lies elsewhere

  * Your claim will remain in the `Pending` state until you create a pod that uses this claim

  * We'll examine why later; for now, let's just create the pod

## Using the persistent volume claim in a pod

* Create a new pod manifest file from the [`pod.quiz.pvc.yaml`](../../../section02/create-persistent-volumes-and-claims/use-claim-and-volume-in-one-pod/pod.quiz.pvc.yaml) file that you created earlier

  * Change the name of the pod to `quiz-default` and the value of the `claimName` field to `quiz-data-default`

  * You can find the resulting manifest in the file [`pod.quiz-default.yaml`](pod.quiz-default.yaml)

  * Use it to create the pod

* If you use a kind-provisioned cluster, the status of the persistent volume claim should change to `Bound` within moments of creating the pod:

```zsh
$ kubectl get pvc quiz-data-default
NAME                STATUS    VOLUME            CAPACITY  ACCESS  ...
quiz-data-default   Bound     pvc-c71fb2c2-...  1Gi       RWO     ...
```

* This implies that the persistent volume has been created

  * List persistent volumes to confirm (the following output has been reformatted to make it easier to read):

```zsh
NAME              CAPACITY    ACCESS MODES    RECLAIM POLICY    STATUS    ...
pvc-c71fb2c2...   1Gi         RWO             Delete            Bound     ...
...   STATUS    CLAIM                       STORAGE CLASS   REASON  AGE
...   Bound     default/quiz-data-default   standard                3s  
```

* As you can see, b/c the volume was created on demand, its properties perfectly match the requirements specified in the claim and the storage class it references

  * The volume capacity is `1Gi` and the access mode is `RWO`

## Understanding when a dynamically provisioned volume is actually provisioned

* Why is the volume in a kind-provisioned cluster created and bound to the claim only after you deploy the pod?

  * In an earlier example that used a manually pre-provisioned persistent volume, the volume was bound to the claim as soon as you created the claim

  * Is this a difference between static and dynamic provisioning?

  * B/c in both GKE and Minikube, the volume was dynamically provisioned and bound to the claim immediately, it's clear that dynamic provisioning alone is not responsible for this behavior

* The system behaves this way b/c of how the storage class in a kind-provisioned cluster is configured

  * You may remember that this storage class was the only one that has `volumeBindingMode` set to `WaitForFirstConsumer`

  * This causes the system to wait until the first pod, or the `consumer` of the claim, exists before the claim is bound

  * The persistent volume is also not provisioned before that

* Some types of volumes require this type of behavior, b/c the system needs to know _where_ the pod is scheduled _before_ it can provision the volume

  * This is the case w/ provisioners that create node-local volumes, such as the one you find in clusters created w/ the kind tool

  * You may remember that the provisioner referenced in the storage class had the word "local" in its name (`rancher.io/local-path`)

  * Minikube also provisions a local volume (the provisioner it uses is called `k8s.io/minikube-hostpath`), but b/c there's only one node in the cluster, there's no need to wait for the pod to be created in order to know which node the persistent volume needs to be created on

> [!NOTE]
> 
> Refer to the documentation of your chosen provisioner to determine whether it requires the volume binding mode to be set to `WaitForFirstConsumer`.

* The alternative to `WaitForFirstConsumer` is the `Immediate` volume binding mode

  * The two modes are explained in the following table ▶︎ Supported volume binding modes:

| **Volume binding mode** | **Description**                                                                                                                                                                                                                                    |
|-------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Immediate`             | The provision and binding of the persistent volume takes place immediately after the claim is created. B/c the consumer of the claim is unknown at this point, this mode is only applicable to volumes that can be accessed from any cluster node. |
| `WaitForFirstConsumer`  | The volume is provisioned and bound to the claim when the first pod that uses this claim is created. This mode is used for topology-constrained volume types.                                                                                      |
