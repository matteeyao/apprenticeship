# Using a hostPath volume

* To demonstrate how dangerous `hostPath` volumes are, let's deploy a pod that allows you to explore the entire filesystem of the host node from within the pod

  * The pod manifest is shown in the following listing ▶︎ Using a hostPath volume to gain access to the host node's filesystem:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: node-explorer
spec:
  volumes:
  - name: host-root                   # ← The hostPath volume points to the root directory on the node’s filesystem.
    hostPath:                         # ← The hostPath volume points to the root directory on the node’s filesystem.
      path: /                         # ← The hostPath volume points to the root directory on the node’s filesystem.
  containers:
  - name: node-explorer
    image: alpine
    command: ["sleep", "9999999999"]
    volumeMounts:                     # ← The volume is mounted in the container at /host.
    - name: host-root                 # ← The volume is mounted in the container at /host.
      mountPath: /host                # ← The volume is mounted in the container at /host.
```

* As you can see in the listing, a `hostPath` volume must specify the `path` on the host that it wants to mount

  * The volume in the listing will point to the root directory on the node's filesystem, providing access to the entire filesystem fo the node the pod is scheduled to

* After creating the pod from this manifest using `kubectl apply`, run a shell in the pod w/ the following command:

```zsh
$ kubectl exec -it node-explorer -- sh
```

* You can now navigate to the root directory of the node's filesystem by running the following command:

```zsh
/ # cd /host
```

* From here, you can explore the files on the host node

  * Since the container and the shell are running as root, you can modify any file on the worker node

  * Be careful not to break anything

> [!NOTE]
> 
> If your cluster has more than one worker node, the pod runs on a randomly selected node. If you'd like to deploy the pod on a specific node, edit the file `node-explorer.specific-node.pod.yaml`, which you'll find in the book's code archive, and set the `.spec.nodeName` field to the name of the node you'd like to run the pod on. You'll learn about scheduling pods to a specific node or a set of nodes in later chapters

* Now imagine you're an attacker that has gained access to the Kubernetes API and are able to deploy this type of pod in a production cluster

  * Unfortunately, at the time of writing, Kubernetes doesn't prevent regular users from using `hostPath` volumes in their pods and is therefore totally unsecure

  * As already mentioned, you'll learn how to secure the cluster from this type of attack in chapter 24

## Specifying the type for a hostPath volume

* In the previous example, you only specified the path for the `hostPath` volume, but you an also specify the `type` to ensure that the path represents what the process in the container expects (a file, a directory, or something else)

* The following table explains the supported `hostPath` volume types:

| **Type**            | **Description**                                                                                                                                                                                                  |
|---------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `<empty>`           | Kubernetes performs no checks before it mounts the volume.                                                                                                                                                       |
| `Directory`         | Kubernetes checks if a directory exists at the specified path. Use this type if you want to mount a pre-existing directory into the pod and want to prevent the pod from running if the directory doesn't exist. |
| `DirectoryOrCreate` | Same as `Directory`, but if nothing exists at the specified path, an empty directory is created.                                                                                                                 |
| `File`              | The specified path must be a file.                                                                                                                                                                               |
| `FileOrCreate`      | Same as `File`, but if nothing exists at the specified path, an empty file is created.                                                                                                                           |
| `BlockDevice`       | The specified path must be a block device.                                                                                                                                                                       |
| `CharDevice`        | The specified path must be a character device.                                                                                                                                                                   |
| `Socket`            | The specified path must be a UNIX socket.                                                                                                                                                                        |

* If the specified path doesn't match the type, the pod's containers don't run

  * The pod's events explain why the hostPath type check failed

> [!NOTE]
> 
> When the type is `FileOrCreate` or `DirectoryOrCreate` and Kubernetes needs to create the file/directory, its file permissions are set to `644` (`rw-r--r--`) and `755` (`rwxr-xr-x`), respectively. In either case, the file/directory is owned by the user and group used to run the Kubelet.
