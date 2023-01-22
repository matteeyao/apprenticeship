# Deleting a pod by name

* The easiest way to delete an object is to delete it by name

## Deleting a single pod

* Use the following command to remove the `kiada` pod from your cluster:

```zsh
$ kubectl delete po kiada
pod "kiada" deleted
```

* By deleting a pod, you state that you no longer want the pod or its containers to exist

  * The Kubelet shuts down the pod's containers, removes all associated resources, such as log files, and notifies the API server after this process is complete

  * The Pod object is then removed

> [!TIP]
> 
> By default, the `kubectl delete` command waits until the object no longer exists. To skip the wait, run the command w/ the `--wait=false` option.

* While the pod is in the process of shutting down, its status changes to `Terminating`:

```zsh
$ kubectl get po kiada
NAME    READY   STATUS      RESTARTS    AGE
kiada   1/1     Terminating 0           35m
```

* Knowing exactly how containers are shut down is important if you want your application to provide a good experience for its clients

> [!NOTE]
> 
> If you're familiar w/ Docker, you may wonder if you can stop a pod and start it again later, as you can w/ Docker containers. The answer is no. W/ K8s, you can only remove a pod completely and create it again later.

## Deleting multiple pods w/ a single command

* You can also delete multiple pods w/ a single command

  * If you ran the `kiada-init` and the `kiada-init-slow` pods, you can delete them both by specifying their names separated by a space, as follows:

```zsh
$ kubectl delete po kiada-init kiada-init-slow
pod "kiada-init" deleted
pod "kiada-init-slow" deleted
```
