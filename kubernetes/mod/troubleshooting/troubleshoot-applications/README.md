# Troubleshooting your Applications

## Checking pod status

You can see a Pod's status w/ `kubectl get pods`.

```zsh
kubectl get pods
```

Use `kubectl describe pod` to get more information about what may be going on w/ an unhealthy pod.

```zsh
kubectl describe pod <PODNAME>
```

## Running commands inside containers

If you need to troubleshoot what is going on inside a container, you can execute commands within the container w/ `kubectl exec`.

```zsh
kubectl exec podname -c <CONTAINER_NAME> -- <COMMAND>
```

Note that you cannot use `kubectl exec` to run any software that is not present within the container.

## Hands-on demonstration

Create a simple busybox pod:

```zsh
vi troubleshooting-pod.yml
```

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: troubleshooting-pod
spec:
  containers:
    - name: busybox
      image: busybox
      command: ['sh', '-c', 'while true; do sleep 5; done']
```

```zsh
kubectl apply -f troubleshooting-pod.yml
```

```zsh
kubectl get pods
```

If something is wrong w/ one of our pods, run:

```zsh
kubectl describe pod troubleshooting-pod
```

Run a command inside a pod:

```zsh
kubectl exec troubleshooting-pod -c busybox -- ls
```

Assuming a container actually has a shell installed on it, we can actually get an interactive shell to the container as well.

```zsh
kubectl exec troubleshooting-pod -c busybox --stdin --tty -- /bin/sh
```
