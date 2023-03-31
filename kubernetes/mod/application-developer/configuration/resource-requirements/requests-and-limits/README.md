# Resources

* Documentation:

```zsh
kubectl explain pod.spec.containers | grep -i resources
---
resources	<Object>
Compute Resources required by this container. Cannot be updated. More info:
https://kubernetes.io/docs/concepts/configuration/manage-compute-resources-container/
```

* As you can see, the resources are defined within the specific pod's container

* There are two components

  1. Requests

  2. Limits

* In layman's terms, the kube-scheduler is going to take these requests and limits and search for a node that has the available requested capacity, whether it be disk space or compute power

## Adding requests and limits

* Let's make a pod and add some resource requests and limits:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: p1
spec:
  containers:
  - image: busybox
    args:
    - /bin/sh
    - -c
    - sleep 3600
    name: p1
    resources:
      requests:
        memory: "64Mi"
        cpu: "100M"
      limits:
        memory: "128Mi"
        cpu: "200M"
```

* If the pod fails to be assigned to a node, it is relatively easy to debug

  * The pod will sit in the pending state forever until additional capacity is added:

```zsh
NAME   READY   STATUS    RESTARTS   AGE
p1     0/1     Pending   0          35s
```

* Take a look at the pod description to see the error:

```zsh
kubectl describe po p1 | grep -i events -A 10
---
Events:
  Type     Reason            Age                From               Message
  ----     ------            ----               ----               -------
  Warning  FailedScheduling  4s (x20 over 94s)  default-scheduler  0/2 nodes are available: 2 Insufficient cpu.
```
