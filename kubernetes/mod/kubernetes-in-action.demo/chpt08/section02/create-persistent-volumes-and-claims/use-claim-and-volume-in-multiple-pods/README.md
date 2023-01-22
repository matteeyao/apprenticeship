# Using a claim and volume in multiple pods

* So far, you used a persistent volume in only one pod instance at a time

  * You used the persistent volume in the so-called ReadWriteOnce (`RWO`) access mode b/c it was attached to a single node and allowed both read and write operations

  * You may remember that two other modes exist, namely ReadWriteMany (`RWX`) and ReadOnlyMany (`ROX`)

  * The volume's access modes indicate whether it can concurrently be attached to one or many cluster nodes and whether it can only be read or also written to

* The ReadWriteOnce mode doesn't mean that only a single pod can use it, but that a single _node_ can attach the volume

  * As this is something that confuses a lot of users, it warrants a closer look

## Binding a claim to a randomly selected persistent volume

* This exercise requires the use of a GKE cluster

  * Make sure it has at least two nodes
  
  * First, create a persistent volume claim for the [`other-data`](../create-persistentvolume-object/pv.other-data.gcepd.yaml) persistent volume that you created earlier

  * You'll find the manifest in the file [`pvc.other-data.yaml`](pvc.other-data.yaml)

  * It's shown in the following listing ▶︎ A persistent volume claim requesting both ReadWriteOnce and ReadOnlyMany access:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: other-data
spec:
  resources:
    requests:
      storage: 1Gi
  accessModes:          # ← This claim requires the volume to support both access modes
  - ReadWriteOnce       # ← This claim requires the volume to support both access modes
  - ReadOnlyMany        # ← This claim requires the volume to support both access modes
  storageClassName: ""  # ← The storage class name is empty to force the claim to be bound to an existing persistent volume
```

* You'll notice that unlike in the previous section, this persistent volume claim does not specify the `volumeName`

  * This means that the persistent volume for this claim will be selected at random among all the volumes that can provide at least 1Gi of space and support both the `ReadWriteOnce` and the `ReadOnlyMany` access modes

* Your cluster should currently contain only the `other-data` persistent volume

  * B/c it matches the requirements in the claim, this is the volume that will be bound to it

## Using a ReadWriteOnce volume in multiple pods

* The persistent volume bound to the claim supports both `ReadWriteOne` and `ReadOnlyMany` access modes

  * First, you'll use it in `ReadWriteOnce` mode, as you'll deploy pods that write to it

* You'll create several replicas of a data-writer pod from a single pod manifest

  * The manifest is shown in the following listing [`pod.data-writer.yaml`](pod.data-writer.yaml) ▶︎ A pod that writes a file to a shared persistent volume:

```yaml
apiVersion: v1
kind: Pod
metadata:
  generateName: data-writer-                                              # ← A
spec:
  volumes:
  - name: other-data
    persistentVolumeClaim:                                                # ← B
      claimName: other-data                                               # ← B
  containers:
  - name: writer
    image: busybox
    command:
    - sh
    - -c
    - |
      echo "A writer pod wrote this." > /other-data/${HOSTNAME} && #C
      echo "I can write to /other-data/${HOSTNAME}." ; #C
      sleep 9999 #C
    volumeMounts:
    - name: other-data
      mountPath: /other-data
    resources:                                                            # ← D
      requests:                                                           # ← D
        cpu: 1m                                                           # ← D

# ← A ▶︎ This pod manifest doesn’t set a name for the pod. The generateName field allows a random name with this prefix to be generated for each pod you create from this manifest.

# ← B ▶︎ All pods created from this manifest will use the other-data persistent volume claim.

# ← C ▶︎ The pod writes a short message to a file in the persistent volume. The filename is the pod’s hostname. If the file creation succeeds, a message is printed to the standard output of the container. The container then waits for 9999 seconds.

# ← D ▶︎ Ignore these lines. You’ll learn about them in chapter 20.
```

* Use the following command to create the pod from this manifest:

```zsh
$ kubectl create -f pod.data-writer.yaml  # ← The command kubectl create is used instead of kubectl apply
pod/data-writer-6mbjg created             # ← The pod gets a randomly generated name
```

* Notice that you aren't using the `kubectl apply` this time

  * B/c the pod manifest uses the `generateName` field instead of specifying the pod name, `kubectl apply` won't work

  * You must use `kubectl create`, which is similar, but is only used to create and not update objects

* Repeat the command several times so that you create two to three times as many writer pods as there are cluster nodes to ensure that at least two pods are scheduled to each node

  * Confirm that this is the case by listing the pods w/ the `-o wide` option and inspecting the `NODE` column:

```zsh
$ kubectl get pods -o wide
NAME                READY   STATUS              RESTARTS    AGE   IP          NODE
data-writer-6mbjg   1/1     Running             0           5m    10.0.10.21  gkdp-r6j4   # ← This pod runs on the first node.
data-writer-97t9j   0/1     ContainerCreating   0           5m    <none>      gkdp-mcbg   # ← This pod is scheduled to the second node, but won't run.
data-writer-d9f2f   1/1     Running             0           5m    10.0.10.23  gkdp-r6j4   # ← This pod runs on the first node.
data-writer-dfd8h   0/1     ContainerCreating   0           5m    <none>      gkdp-mcbg   # ← This pod is scheduled to the second node, but won't run.
data-writer-f867j   1/1     Running             0           5m    10.0.10.17  gkdp-r6j4   # ← This pod runs on the first node.
```

* If all your pods are located on the same node, create a few more

  * Then look at the `STATUS` of these pods

  * You'll notice that all the pods scheduled to the first node run fine, whereas the pods on the other node are all stuck in the status `ContainerCreating`

  * Even waiting for several minutes doesn't change anything

  * Those pods will never run

* If you use `kubectl describe` to display the events related to one of these pods, you'll see that it doesn't run b/c the persistent volume can't be attached to the node that the pod is on:

```zsh
$ kubectl describe po data-writer-97t9j ...
Warning FailedAttachVolume ... attachdetach-controller AttachVolume.Attach failed
for volume "other-data" : googleapi: Error 400: RESOURCE_IN_USE_BY_ANOTHER_RESOURCE -   # ← A
The disk resource 'projects/.../disks/other-data' is already being used by              # ← A
'projects/.../instances/gkdp-r6j4'                                                      # ← A

# ← A ▶︎ The disk is being used by node gkdp-r6j4
```

* The reason the volume can't be attached is b/c it's already attached to the first node in read--write mode

  * The volume supports ReadWriteOnce and ReadOnlyMany but doesn't support ReadWriteMany

  * This means that only a single node can attach the volume in read-write mode

  * When the second node tries to do the same, the operation fails

* All the pods on the first node run fine

  * Check their logs to confirm that they were all able to write a file to the volume

  * Here's the log of one of them:

```zsh
$ kubectl logs other-data-writer-6mbjg
I can write to /other-data/other-data-writer-6mbjg.
```

* You'll find that all the pods on the first node successfully wrote their files to the volume

  * You don't need ReadWriteMany for multiple pods to write to the volume if they are on the same node

  * As explained before, the word "Once" in ReadWriteOnce refers to nodes, not pods

## Using a combination of read-write and read-only pods w/ a ReadWriteOnce and ReadOnlyMany volume

* You'll now deploy a group of reader pods alongside the data-writer pods

  * They will use the persistent volume in read-only mode

  * The following listing shows the pod manifest for these data-reader pods

  * You'll find it in [`pod.data-reader.yaml`](pod.data-reader.yaml) ▶︎ A pod that mounts a shared persistent volume in read-only mode:

```yaml
apiVersion: v1
kind: Pod
metadata:
  generateName: data-reader-
spec:
  volumes:
  - name: other-data
    persistentVolumeClaim:
      claimName: other-data                                           # ← A
      readOnly: true                                                  # ← B
  containers:
  - name: reader
    image: busybox
    imagePullPolicy: Always
    command:
    - sh
    - -c
    -|
      echo "The files in the persistent volume and their contents:" ; # ← C
      grep ^ /other-data/* ;                                          # ← C
      sleep 9999                                                      # ← C
    volumeMounts:
    - name: other-data
      mountPath: /other-data
    ...

# ← A ▶︎ This pod also uses the other-data persistent volume claim.
# ← B ▶︎ Unlike the writer pod, this pod uses the persistent volume in read-only mode.
# ← C ▶︎ When it runs, it prints the name and contents of each file stored in the persistent volume.
```

* Use the `kubectl create` command to create as many of these reader pods as necessary to ensure that each node runs at least two instances

  * Use the `kubectl get po -o wide` command to see how many pods are on each node

* As before, you'll notice that only those reader pods that are scheduled to the first node are running

  * The pods on the second node are stuck in `ContainerCreating`, just like the writer pods

  * Here's a list of just the reader pods (the writer pods are still there, but aren't shown):

```zsh
$ kubectl get pods -o wide | grep reader
NAME                READY   STATUS              RESTARTS   AGE  IP          NODE
data-reader-6594s   1/1     Running             0          2m   10.0.10.25  gkdp-r6j4   # ← A
data-reader-lqwkv   1/1     Running             0          2m   10.0.10.24  gkdp-r6j4   # ← A
data-reader-mr5mk   0/1     ContainerCreating   0          2m   <none>      gkdp-mcbg   # ← B
data-reader-npk24   1/1     Running             0          2m   10.0.10.27  gkdp-r6j4   # ← A
data-reader-qbpt5   0/1     ContainerCreating   0          2m   <none>      gkdp-mcbg   # ← B
 
# ← A ▶︎ These run on the first node
# ← B ▶︎ These are scheduled to the second node, but don't run
```

* These pods use the volume in read-only mode

  * The claim's (and volume's) access modes are both ReadWriteOnce (`RWO`) and ReadOnlyMany (`ROX`), as you can see by running `kubectl get pvc`:

```zsh
$ kubectl get pvc other-data
NAME        STATUS    VOLUME      CAPACITY  ACCESS MODES    STORAGECLASS    AGE
other-data  Bound     other-data  10Gi      RWO,ROX                         23h 
```

* If the claim supports access mode ReadOnlyMany, why can't both nodes attach the volume and run the reader pods?

  * This is caused by the writer pods

  * The first node attached the persistent volume in read-write mode

  * This prevents other nodes from attaching the volume, even in read-only mode

  *