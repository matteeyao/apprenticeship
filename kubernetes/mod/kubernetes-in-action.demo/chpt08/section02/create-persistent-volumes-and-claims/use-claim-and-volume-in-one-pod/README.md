# Using a claim and volume in a single pod

* In this section, you'll learn the ins and outs of using a persistent volume in a single pod at a time

## Using a persistent volume in pod

* To use a persistent volume in a pod, you define a volume within the pod in which you refer to the PersistentVolumeClaim object

  * To try this, modify the quiz pod from the previous chapter and make it use the `quiz-data` claim

  * The changes to the pod manifest are highlighted in the next listing ([`pod.quiz.pvc.yaml`]()) ▶︎ A pod using a persistentVolumeClaim volume:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: quiz
spec:
  volumes:
  - name: quiz-data
    persistentVolumeClaim:      # ← The volume refers to a persistent volume claim with the name quiz-data.
      claimName: quiz-data      # ← The volume refers to a persistent volume claim with the name quiz-data.
  containers:
  - name: quiz-api
    image: luksa/quiz-api:0.1
    ports:
    - name: http
      containerPort: 8080
  - name: mongo
    image: mongo
    volumeMounts:               # ← The volume is mounted the same way that other volumes types are mounted.
    - name: quiz-data           # ← The volume is mounted the same way that other volumes types are mounted.
      mountPath: /data/db       # ← The volume is mounted the same way that other volumes types are mounted.
```

* As you can see in the listing, you don't define the volume as a `gcePersistentDisk`, `awsElasticBlockStore`, `nfs` or `hostPath` volume, but as a `persistentVolumeClaim` volume

  * The pod will use whatever persistent volume is bound to the `quiz-data` claim

  * In your case, that should be the `quiz-data` persistent volume

* Create and test this pod now

  * Before the pod starts, the GCE PD volume is attached to the node and mounted into the pod's container(s)

  * If you use GKE and have configured the persistent volume to use the GCE Persistent Disk from the previous chapter, which already contains data, you should be able to retrieve the quiz questions you stored earlier by running the following command:

```zsh
$ kubectl exec -it quiz -c mongo -- mongo kiada --quiet --eval "db.questions.find()"
{ "_id" : ObjectId("5fc3a4890bc9170520b22452"), "id" : 1, "text" : "What does k8s mean?",
"answers" : [ "Kates", "Kubernetes", "Kooba Dooba Doo!" ], "correctAnswerIndex" : 1 }
```

* If your GCE PD has no data, add it now by running the shell script [`insert-question.sh`](insert-question.sh)

## Re-using the claim in a new pod instance

* When you delete a pod that uses a persistent volume via a persistent volume claim, the underlying storage volume is detached from the worker node (assuming that it was the only pod that was using it on that node)

  * The persistent volume object remains bound to the claim

  * If you create another pod that refers to this claim, this new pod gets access to the volume and its files

* Try deleting the quiz pod and recreating it

  * If you run the `db.questions.find()` query in this new pod instance, you'll see that it returns the same data as the previous one

  * If the persistent volume uses network-attached storage such as GCE Persistent Disks, the pod sees the same data regardless of what node it's scheduled to

  * If you use a kind-provisioned cluster and had to resort to using a hostPath-based persistent volume, that isn't the case

    * To access the same data, you must ensure that the new pod instance is scheduled to the node to which the original instance was scheduled, as the data is stored in that node's filesystem

## Releasing a persistent volume

* When you no longer plan to deploy pods that will use this claim, you can delete it

  * This releases the persistent volume

  * You might wonder if you can then recreate the claim and access the same volume and data

  * Let's find out

  * Delete the pod and the claim as follows to see what happens:

```zsh
$ kubectl delete pod quiz
pod "quiz" deleted

$ kubectl delete pvc quiz-data
persistentvolumeclaim "quiz-data" deleted
```

* Now check the status of the persistent volume:

```zsh
$ kubectl get pv quiz-data
NAME        ...   RECLAIM POLICY    STATUS      CLAIM               ...
quiz-data   ...   Retain            Released    default/quiz-data   ...
```

* The `STATUS` column shows the volume as `Released` rather than `Available`, as was the case initially

  * The `CLAIM` column still shows the `quiz-data` claim to which it was previously bound, even if the claim no longer exists

  * You'll understand why in a minute

## Binding a released persistent volume

* What happens if you create the claim again?

  * Is the persistent volume bound to the claim so that it cna be reused in a pod?

  * Run the following commands to see if this is the case:

```zsh
$ kubectl apply -f pvc.quiz-data.static.yaml
persistentvolumeclaim/quiz-data created

$ kubectl get pvc
NAME      STATUS    VOLUME    CAPACITY    ACCESSMODES   STORAGECLASS    AGE 
quiz-data Pending                                                       13s # ← The claim’s status is Pending.
```

* The claim isn't bound to the volume and its status is `Pending`

  * When you created the claim earlier, it was immediately bound to the persistent volume, so why not now?

* The reason behind this is that the volume has already been used and might contain data that should be erased before another user claims the volume

  * This is also the reason why the status of the volume is `Released` instead of `Available` and why the claim name is still shown on the persistent volume, as this helps the cluster administrator to know if the data can be safely deleted

## Making a released persistent volume available for re-use

* To make the volume available again, you must delete and recreate the PersistentVolume object

  * But will this cause the data stored in the volume to be lost?

* Imagine if you had accidentally deleted the pod and the claim and caused a loss of service to the Kiada application

  * You need to restore the service as soon as possible, w/ all data intact

  * If you think that deleting the PersistentVolume object would delete the data, that sounds like the last thing you should do but is actually completely safe

* W/ a pre-provisioned persistent volume like the one at hand, deleting the object is equivalent to deleting a data pointer

  * The PersistentVolume object merely _points_ to a GCE Persistent Disk

  * It doesn't store the data

  * If you delete and recreate the object, you end up w/ a new pointer to the same GCE PD and thus the same data

  * You'll confirm this is the case in the next exercise

```zsh
$ kubectl delete pv quiz-data
persistentvolume "quiz-data" deleted

$ kubectl apply -f pv.quiz-data.gcepd.yaml
persistentvolume/quiz-data created

$ kubectl get pv quiz-data
NAME        ...   RECLAIM POLICY    STATUS      CLAIM   ... 
quiz-data   ...   Retain            Available           ...
```

> [!NOTE]
> 
> An alternative way of making a persistent volume available again is to edit the PersistentVolume object and remove the `claimRef` from the `spec` section.

* The persistent volume is displayed as `Available` again

  * Recall that you created a claim for the volume earlier

  * Kubernetes has been waiting for a volume to bind to the claim

  * As you might expect, the volume you've just created will be bound to this claim in a few seconds

  * List the volumes again to confirm:

```zsh
$ kubectl get pv quiz-data
NAME        ...   RECLAIM POLICY  STATUS    CLAIM               ...
quiz-data   ...   Retain          Bound     default/quiz-data   ... # ← The persistent volume is again bound to the claim.
```

* The output shows that the persistent volume is again bound to the claim

  * If you now deploy the quiz pod and query the database again w/ the following command, you'll see that the data in underlying GCE Persistent Disk has not been lost:

```zsh
$ kubectl exec -it quiz -c mongo -- mongo kiada --quiet --eval "db.questions.find()"
{ "_id" : ObjectId("5fc3a4890bc9170520b22452"), "id" : 1, "text" : "What does k8s mean?",
"answers" : [ "Kates", "Kubernetes", "Kooba Dooba Doo!" ], "correctAnswerIndex" : 1 }
```

## Configuring the reclaim policy on persistent volumes

* What happens to a persistent volume when it is released is determined by the volume's reclaim policy

  * When you used the `kubectl get pv` command to list persistent volumes, you may have noticed that the `quiz-data` volume's policy is `Retain`

  * This policy is configured using the field `.spec.persistentVolumeReclaimPolicy` in the PersistentVolume object

* The field can have one of three values explained in the following table ▶︎ Persistent volume reclaim policies:

| **Reclaim policy** | **Description**                                                                                                                                                                                                                                                        |
|--------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Retain`           | When the persistent volume is released (this happens when you delete the claim that's bound to it), Kubernetes _retains_ the volume. The cluster administrator must manually reclaim the volume. This is the default policy for manually created persistent volumes.   |
| `Delete`           | The PersistentVolume object and the underlying storage are automatically deleted upon release. This is the default policy for dynamically provisioned persistent volumes, which are discussed in the next section.                                                     |
| `Recycle`          | This option is deprecated and shouldn't be used as it may not be supported by the underlying volume plugin. This policy typically causes all files on the volume to be deleted and makes the persistent volume available again w/o the need to delete and recreate it. |

> [!TIP]
> 
> You can change the reclaim policy of an existing PersistentVolume at any time. If it's initially set to `Delete`, but you don't want to lose your data when deleting the claim, change the volume's policy to `Retain` before doing so.

> [!WARNING]
> 
> If a persistent volume is `Released` and you subsequently change its reclaim policy from `Retain` to `Delete`, the PersistentVolume object and the underlying storage will be deleted immediately. However, if you instead delete the object manually, the underlying storage remains intact.

## Deleting a persistent volume while it's bound

* You're done playing w/ the `quiz` pod, the `quiz-data` persistent volume claim, and the `quiz-data` persistent volume, so you'll now delete them

  * You'll learn one more thing in the process

* Have you wondered what happens if a cluster administrator deletes a persistent volume while it's in use (while it's bound to a claim)?

  * Let's find out

  * Delete the persistent volume like so:

```zsh
$ kubectl delete pv quiz-data
persistentvolume "quiz-data" deleted # ← The command blocks after printing this message
```

* This command tells the Kubernetes API to delete the PersistentVolume object and then waits for Kubernetes controllers to complete the process

  * But this can't happen until you release the persistent volume from the claim by deleting the PersistentVolumeClaim object

* You can cancel the wait by pressing Control-C

  * However, this doesn't cancel the deletion, as its already underway

  * You can confirm this as follows:

```zsh
$ kubectl get pv quiz-data
NAME        CAPACITY    ACCESS MODES    STATUS        CLAIM               ...
quiz-data   10Gi        RWO,ROX         Terminating   default/quiz-data   ... # ← The persistent volume is terminating
```

* As you can see, the persistent volume's status shows that it's being terminated

  * But it's still bound to the persistent volume claim

  * You need to delete the claim for the volume deletion to complete

## Deleting a persistent volume claim while a pod is using it

* The claim is still being used by the quiz pod, but let's try deleting it anyway:

```zsh
$ kubectl delete pvc quiz-data
persistentvolumeclaim "quiz-data" deleted # ← The command blocks after printing this message
```

* Like the `kubectl delete pv` command, this command also doesn't complete immediately

  * As before, the command waits for the claim deletion to complete

  * You can interrupt the execution of the command, but this won't cancel the deletion, as you can see w/ the following command:

```zsh
$ kubectl get pv quiz-data
NAME        STATUS        VOLUME      CAPACITY    ACCESS MODES    STORAGECLASS   AGE
quiz-data   Terminating   quiz-data   10Gi        RWO,ROX                        15m # ← The persistent volume claim is being terminated
```

* The deletion of the claim is blocked by the pod

  * Unsurprisingly, deleting a persistent volume or a persistent volume claim has no immediate effect on the pod that's using it

  * The application running in the pod continues to run unaffected

  * Kubernetes never kills pods just b/c the cluster administrator wants their disk space back

* To allow termination of the persistent volume claim and the persistent volume to complete, delete the quiz pod w/ `kubectl delete po quiz`

## Deleting the underlying storage

* As you learned in the previous section, deleting the persistent volume does not delete the underlying storage, such as the `quiz-data` GCE Persistent Disk if you use Google Kubernetes Engine to perform these exercises, or the `/var/quiz-data` directory on the worker node if you use Minikube or kind

* You no longer need the data files and can safely delete them

  * If you use Minikube or kind, you don't need to delete the data directory, as it doesn't cost you anything

  * However, a GCE Persistent Disk does

  * You can delete it w/ the following command:

```zsh
$ gcloud compute disks delete quiz-data
```

* You might remember that you also created another GCE Persistent Disk called `other-data`

  * Don't delete that one just yet

  * You'll use it in th next section's exercise
