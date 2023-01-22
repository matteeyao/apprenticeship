# Listing Pods

* Since containers aren't a top-level K8s object, you can't list them, but you can list pods

  * As the following output of the `kubectl get pods` command shows, by creating the Deployment object, you've deployed one pod:

```zsh
$ kubectl get pods
NAME                    READY   STATUS    RESTARTS    AGE
kiada-9d785b578-p449x   0/1     PENDING   0           1m  # ← K8s created this pod from the Deployment object.
```

* This is the pod that houses the container running your application

  * To be precise, since the status is still `Pending`, the application, or rather the container, isn't running yet

  * This is also expressed in the `READY` column, which indicates that the pod has a single container that's not ready

  * The reason the pod is pending is b/c the worker node to which the pod has been assigned must first download the container image before it can run it

  * When the download is complete, the pod's container is created and the pod enters the `Running` state

* If K8s can't pull the image from the registry, the `kubectl get pods` command will indicate this in the `STATUS` column

  * If you're using your own image, ensure it's marked as public on Docker Hub

* If another issue is causing your pod not to run, or if you simply want to see more information about the pod, you can also use the `kubectl describe pod` command, as you did earlier to see the details of a worker node

  * If there are any issues w/ the pod, they should be displayed by this command

  * Look at the events shown at the bottom of a running pod's output:

```zsh
Type    Reason      Age   From                  Message
----    ------      ----  ----                  -------
Normal  Scheduled   25s   default-scheduler     Successfully assigned default/kiada-9d785b578-p449x to kind-worker2
Normal  Pulling     23s kubelet, kind-worker2   Pulling image "luksa/kiada:0.1"
Normal  Pulled      21s kubelet, kind-worker2   Successfully pulled image
Normal  Created     21s kubelet, kind-worker2   Created container kiada
Normal  Started     21s kubelet, kind-worker2   Started container kiada
```
