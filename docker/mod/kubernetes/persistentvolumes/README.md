# Storage with PersistentVolumes

`PersistentVolumes` allow you to manage storage resources in an abstract fashion. They let you define a set of available storage resources as a Kubernetes object, then later claim those storage resources for use in your pods.

You can think of `PersistentVolumes` as doing something w/ storage that is similar to what Kubernetes already does w/ compute resources like CPU and memory. It allows you to define the storage resources you have available, and then abstractly consume those resources, using your pods.

A `PersistentVolumeClaim` defines a request for storage resources that can be mounted to a pod's containers.

## Reclaim Policy

A PersistentVolume has a reclaim policy which determines what happens when the associated claims are detected.

* **Retain** ▶︎ Keeps the volume and its data and allows manual reclaiming. Admin is responsible for cleaning up existing data.

* **Delete** ▶︎ Deletes both the PersistentVolume and its underlying storage infrastructure (such as a cloud storage object).

* **Recycle** ▶︎ Deletes the data contained in the volume and allows it to be used again.

## Expanding a PVC

You can expand a **PersistentVolumeClaim** by simply editing the object to have a larger size.

For this to work, the storage class must support volume expansion. If this is the vase, the storage class's `allowVolumeExpansion` field will be set to true.

**StorageClass**:

```yaml
apiVersion: storage.k8s.io/v1
kind: StorageClass
metadata:
  name: localdisk
provisioner: kubernetes.io/no-provisioner
allowVolumeExpansion: true
```

**PersistentVolume**:

```yaml
kind: PersistentVolume
apiVersion: v1
metadata:
  name: my-pv
spec:
  storageClassName: localdisk
  persistentVolumeReclaimPolicy: Recycle
  capacity:
    storage: 1Gi
  accessModes:
    - ReadWriteOnce
  hostPath:
    path: /tmp/pvoutput
```

**PersistentVolumeClaim**:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: my-pvc
spec:
  storageClassName: localdisk
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 100Mi
```

**Pod**:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: pvc-pod
spec:
  containers:
    - name: busybox
      image: busybox
      command: ['sh', '-c', 'while true; do echo "Successfully written to log." >> /output/output.log; sleep 10; done']
      volumeMounts:
      - name: pv-storage
      - mountPath: /output
  volumes:
    - name: pv-storage
      persistentVolumeClaim:
        claimName: my-pvc
```
