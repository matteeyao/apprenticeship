# Safely Draining a K8s Node

## What is draining?

When performing maintenance, you may sometimes need to remove a Kubernetes node from service.

To do this, you can **drain** the node. Containers running on the node will be gracefully terminated (and potentially rescheduled on another node).

## Draining a node

To drain a node, use the `kubectl drain` command:

```zsh
kubectl drain <NODE_NAME>
```

## Ignoring DaemonSets

When draining a node, you may need to ignore DaemonSets (pods that are tied to each node). If you have any DameonSet pods running on the node, you will likely need to use the `--ignore-daemonsets` flag.

```zsh
kubectl drain <NODE_NAME> --ignore-daemonsets
```

## Uncordoning a node

If the node remains part of the cluster, you can allow pods to run on the node again when maintenance is complete using the `kubectl uncordon` command:

```zsh
kubectl uncordon <NODE_NAME>
```

## Hands-on demonstration

Log into the K8s control plane node.

Create some objects, running pods and containers in our cluster so that we can see what happens to those pods when we drain our worker node.

Create a pod definition file:

```zsh
vi pod.yml
```

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: my-pod
spec:
  containers:
  - name: nginx
    image: nginx:1.14.2
    ports:
    - containerPort: 80
  restartPolicy: onFailure
```

```zsh
kubectl apply -f pod.yml
```

Create a deployment:

```zsh
vi deployment.yml
```

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: my-deployment
  labels:
    app: my-deployment
spec:
  replicas: 2
  selector:
    matchLabels:
      app: my-deployment
  template:
    metadata:
      labels:
        app: my-deployment
    spec:
      containers:
      - name: nginx
        image: nginx:1.14.2
        ports:
        - containerPort: 80
```

```zsh
kubectl apply -f deployment.yml
```

```zsh
kubectl get pods -o wide
```

> [!NOTE]
> 
> The `-o wide` will output which node the pod is running on. 

Look for the `my-pod` and drain the node where that pod is running.

```zsh
kubectl drain <NODE_NAME>
```

To get around the errors, run:

```zsh
kubectl drain <NODE_NAME> --ignore-daemonsets --force
```

> [!NOTE]
> 
> `--force` will delete the first pod.

```zsh
kubectl get pods -o wide
```

So we've drained one of our worker nodes and essentially new pods were spun up as part of that deployment to replace the pods that were evicted from the node that was drained.

Notice for the node that we drained, `STATUS` will indicate `SchedulingDisabled`:

```zsh
kubectl get nodes
```

This means that Kubernetes is not going to run additional pods on that node b/c presumable our maintenance tasks are still underway. To change that, let's go ahead and run:

```zsh
kubectl uncordon <NODE_NAME>
```

`kubectl get nodes` will show that the node is now in ready status.

Note that uncordoning the node did not automatically re-balance our deployment pods so that pods are once again running on the node that was uncordoned.

If we schedule any new pods, they can run on that node. But the uncordoning process does not automatically cause the deployment to schedule a pod once again on the second worker node that we drained.
