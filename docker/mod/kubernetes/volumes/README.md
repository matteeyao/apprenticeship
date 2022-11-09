# Storage with Volumes

Kubernetes uses **volumes** to provide simple storage to containers. Volume storage is more persistent than the container file system and can remain even if a container is restarted or re-created.

Volumes are part of the pod spec, and the same volume can be mounted to multiple containers.

## Volume types

A volume's **type** determines how the underlying storage is handled. There are many volume types available.

Some volume types to know:

* **nfs** ▶︎ Shared network storage is provided using nfs.

* **hostPath** ▶︎ Files are stored in a directory on the worker node. The same files will not appear if the pod runs on another node.

* **emptyDir** ▶︎ Temporary storage that is deleted if the pod is deleted. Useful for sharing simple storage between containers in a pod.

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: volume-pod
spec:
  restartPolicy: Never
  containers:
    - name: busybox
      image: busybox
      command: ["sh", "-c", "echo \"Successful output!\" > /output/output.txt"]
      volumeMounts:
      - name: host-vol
        mountPath: /output
  volumes:
    - name: host-vol
      hostPath:
        path: /tmp/output
```
