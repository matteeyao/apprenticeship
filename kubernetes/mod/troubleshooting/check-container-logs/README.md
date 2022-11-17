# Checking Container Logs

## Container logging

K8s containers maintain **logs**, which you can use to gain insight into what is going on within the container.

A container's logs contains everything written to the standard output (stdout) and error (stderr) streams by the container process.

## kubectl logs

Use the `kubectl logs` command to view a container's logs.

```zsh
kubectl logs <POD_NAME> -c <CONTAINER_NAME>
```

## Hands-on demonstration

Log into the control plane node.

Create a basic busybox pod:

```zsh
vi logs-pod.yml
```

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: logs-pod
spec:
  containers:
  - name: busybox
    image: busybox
    command: ['sh', '-c', 'while true; do echo Here is my output!; sleep 5; done']
```

The string `Here is my output!` will be outputted to the stdout stream.

```zsh
kubectl apply -f logs-pod.yml
```

```zsh
kubectl logs logs-pod -c busybox
```
