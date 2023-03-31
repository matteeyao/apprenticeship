# Init Containers

* In a multi-container pod, each container is expected to run a process that stays alive as long as the pod's lifecycle

  * For example, in the multi-container pod that has a web application and logging agent, both the containers are expected to stay alive at all times

  * The process running in the log agent container is expected to stay alive as long as the web application is running

  * If any of them fails, the pod restarts

* There are times you may want to run a process that runs to completion within a container

  * For example, a process that pulls code or a binary from a repository that will be used by the main web application

  * That is a task that will be run only one time when the pod is first created

  * Or a process that waits for an external service or database to be up before the actual application starts-this is where **initContainers** come in

* An **initContainer** is configured in a pod like all other containers, except that it is specified inside a `initContainers` section, like so:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: myapp-pod
  labels:
    app: myapp
spec:
  containers:
  - name: myapp-container
    image: busybox:1.28
    command: ['sh', '-c', 'echo The app is running! && sleep 3600']
  initContainers:
  - name: init-myservice
    image: busybox
    command: ['sh', '-c', 'git clone <some-repository-that-will-be-used-by-application> ;']
```

* When a pod is first created, the initContainer is run, and the process within the initContainer must run to completion before the real container hosting the application starts

* You can configure multiple initContainers, similar to configuring multi-pod containers

  * In this case, each init container is run **one at a time in sequential order**

* If any of the initContainers fail to complete, K8s restarts the pod repeatedly until the Init Container succeeds

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: myapp-pod
  labels:
    app: myapp
spec:
  containers:
  - name: myapp-container
    image: busybox:1.28
    command: ['sh', '-c', 'echo The app is running! && sleep 3600']
  initContainers:
  - name: init-myservice
    image: busybox:1.28
    command: ['sh', '-c', 'until nslookup myservice; do echo waiting for myservice; sleep 2; done;']
  - name: init-mydb
    image: busybox:1.28
    command: ['sh', '-c', 'until nslookup mydb; do echo waiting for mydb; sleep 2; done;']
```

* Read more about initContainers [here](https://kubernetes.io/docs/concepts/workloads/pods/init-containers/)

## Learning recap

1. Update the pod `red` to use an `initContainer` that uses the `busybox` image and `sleeps for 20` seconds. Delete and re-create the pod if necessary, but make sure no other configurations change.

```zsh
kubectl get pod red -o yaml > red.yaml
```

* Delete current instance:

```zsh
kubectl delete pod red
```

```yaml
apiVersion: v1
kind: Pod
metadata:
...
spec:
  initContainers:
    - image: busybox
      name: red-initcontainer
      command:
        - "sleep"
        - "20"
  containers:
  - command:
    - sh
    - -c
    - echo The app is running! && sleep 3600
  ...
```
