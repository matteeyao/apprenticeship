# SecurityContext

* There are two different `securityContext` sections that can be added:

  1. Pod level ▶︎ `kubectl explain pod.spec.securityContext`

  2. Container level ▶︎ `kubectl explain pod.spec.containers.securityContext`

* Let's make a pod where both the user ID and group filesystem are different in each:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: p1
spec:
  securityContext:
    runAsUser: 1500
    fsGroup: 2000
  containers:
  - image: busybox
    args:
    - /bin/sh
    - -c
    - sleep 3600
    name: p1
    securityContext:
      runAsUser: 1000
```

* Once the pod is up and running, let's `exec` into the pod and check out the ID and group:

```zsh
kubectl exec -it p1 -- sh
```

* Type in `id` and you should get the following details:

```zsh
uid=1000 gid=0(root) groups=2000
```

* Based off this, we can see that **Container** securityContext takes precedence over **Pod** securityContext

  * By doing this, you're adding additional layers of protection between your host OS and the running container

  * This is implemented w/ the user namespace from your kernel
