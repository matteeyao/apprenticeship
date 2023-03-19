# Claiming and using local persistent volumes

* As an application developer, you can now deploy your pod and its associated persistent volume claim

## Creating the pod

* The pod definition is shown in the following listing ▶︎ Pod using a locally attached persistent volume:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: mongodb-local
spec:
  volumes:
  - name: mongodb-data
    persistentVolumeClaim:
      claimName: quiz-data-local # ← A
  containers:
  - image: mongo
    name: mongodb
    volumeMounts:
    - name: mongodb-data
      mountPath: /data/db

# ← A ▶︎ The pod uses the quiz-data-local claim
```

* There should be no surprises in the pod manifest

  * You already know all this

## Creating the persistent volume claim for a local volume

* As w/ the pod, creating the claim for a local persistent volume is no different from creating any other persistent volume claim

  * The manifest is shown in the next listing ▶︎ Persistent volume claim using the local storage class:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: quiz-data-local
spec:
  storageClassName: local   # ← A
  resources:
    requests:
      storage: 1Gi
  accessModes:
    - ReadWriteOnce

# ← A ▶︎ The claim requests a persistent volume from the local storage class
```

* No surprises here either

  * Now on to creating these two objects

## Creating the pod and the claim

* After you write the pod and claim manifests, you can create the two objects by applying the manifests in any order you want

  * If you create the pod first, since the pod requires the claim to exist, it simply remains in the `Pending` state until you create the claim

* After both the pod and the claim are created, the following events take place:

  1. The claim is bound to the persistent volume

  2. The scheduler determines that the volume bound to the claim that is used in the pod can only be accessed from the kind-worker node, so it schedules the pod to this node

  3. The pod's container is started on this node, and the volume is mounted in it

* You can now use the MongoDB shell again to add documents to it

  * Then check the `/mnt/ssd1` directory on the kind-worker node to see if the files are stored there

## Recreating the pod

* If you delete and recreate the pod, you'll see that it's always scheduled on the kind-worker node

  * The same happens if multiple nodes can provide a local persistent volume when you deploy the pod for the first time

  * At this point, the scheduler selects one of them to run your MongoDB pod

  * When the pod runs, the claim is bound to the persistent volume on that particular node

  * If you then delete and recreate the pod, it is always scheduled on the same node, since that is where the volume that is bound to the claim referenced in the pod is located
