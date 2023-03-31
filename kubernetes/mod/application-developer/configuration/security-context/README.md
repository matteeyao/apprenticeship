# Container Security

* Container security should be considered in any type of environment w/ sensitive data, and there are a number of considerations to be taken into account

  * Is the container running as privileged?

  * Is there a PID safety net (aka not running at PID1 - init)?

  * What is the UID the container is running as?

* When you run a Docker container, you have the option to define a set of security standards, such as the ID of the user used to run the container, the Linux capabilities that can be added or removed from the container, etc.

```zsh
docker run --user=1001 ubuntu sleep 3600
```

```zsh
docker run --cap-add MAC_ADMIN ubuntu
```

* These can be configured in K8s as well

* In K8s, containers are encapsulated in pods

  * You may choose to configure the security settings at a container level or at a Pod level

  * If you configure it at a Pod level, the settings will carry over to all the containers within the Pod

  * If you configure it at both the Pod and the container levels, the settings on the container will override the settings on the Pod

## Pod definition file

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: web-pod
spec:
  securityContext:
    runAsUser: 1000
  containers:
    - name: ubuntu
      image: ubuntu
      command: ["sleep", "3600"]
```

* According to the Pod definition file, this pod runs an ubuntu image w/ the `sleep` command

  * To configure security context on the container, add a field called `securityContext` under the `spec` section of the pod

  * Use the `runAsUser` option to set the user ID for the pod

* To set the same configuration on the container level, move the whole section under the container specification like so:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: web-pod
spec:
  containers:
    - name: ubuntu
      image: ubuntu
      command: ["sleep", "3600"]
      securityContext:
        runAsUser: 1000
        capabilities:
            add: ["MAC_ADMIN"]
```

* To add `capabilities`, use the capabilities option and specify a list of `capabilities`  to add to the Pod

## Learning summary

1. What is the user used to execute the sleep process within the `ubuntu-sleeper` pod? In the current (default) namespace.

```zsh
kubectl exec ubuntu-sleeper -- whoami
```

2. Edit the pod `ubuntu-sleeper` to run the sleep process w/ user ID `1010`

> [!NOTE]
> 
> Only make the necessary changes. Do not modify the name or image of the pod.

```zsh
kubectl edit pod ubuntu-sleeper
```

```yaml
apiVersion: v1
kind: Pod
metadata:
...
spec:
  containers:
  - command:
    - sleep
    - "4800"
    image: ubuntu
    securityContext:
      runAsUser: 1010
    ...
```

```zsh
kubectl replace --force -f /tmp/kubectl-edit-3003420587.yaml
```

```zsh
kubectl exec ubuntu-sleeper -- whoami
```

3. Update pod `ubuntu-sleeper` to run as Root user and w/ the `SYS_TIME` capability

> [!NOTE]
> 
> Only make the necessary changes. Do not modify the name of the pod.

```zsh
kubectl edit pod ubuntu-sleeper
```

```yaml
apiVersion: v1
kind: Pod
metadata:
...
spec:
  containers:
  - command:
    - sleep
    - "4800"
    image: ubuntu
    imagePullPolicy: Always
    name: ubuntu
    resources: {}
    securityContext:
      runAsUser: 1010
      capabilities:
        add: ["SYS_TIME"]
    ...
```

```zsh
k replace --force -f /tmp/kubectl-edit-270267019.yaml
```

4. Now update the pod to also make use of the `NET_ADMIN` capability

> [!NOTE]
> 
> Only make the necessary changes. Do not modify the name of the pod

```zsh
kubectl edit pod ubuntu-sleeper
```

```yaml
apiVersion: v1
kind: Pod
metadata:
...
spec:
  containers:
  - command:
    - sleep
    - "4800"
    image: ubuntu
    imagePullPolicy: Always
    name: ubuntu
    resources: {}
    securityContext:
      runAsUser: 1010
      capabilities:
        add:
        - SYS_TIME
        - NET_ADMIN
    ...
```

```zsh
kubectl replace --force -f /tmp/kubectl-edit-926008968.yaml
```
