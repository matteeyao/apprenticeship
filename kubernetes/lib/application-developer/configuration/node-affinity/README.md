# Node Affinity

1. Apply a label `color=blue` to `node01`

```zsh
kubectl label node node01
```

2. Which nodes can the pods for the `blue` deployment be placed on?

```zsh
kubectl describe node node01 | grep Taints
```

```zsh
kubectl describe node controlplane | grep Taints
```

3. Create a new deployment named `red` with the `nginx` image and `2` replicas, and ensure it gets plced on the `controlplane` node only.

Use the label - `node-role.kubernetes.io/master` - set on the controlplane node.

```zsh
kubectl create deployment red --image=nginx -r 2 --dry-run=client -o yaml > red-deployment-definition.yaml
```

* `vi red-deployment-definition.yaml`:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
...
spec:
  replicas: 2
  selector:
    matchLabels:
      app: red
  strategy: {}
  template:
    metadata:
      creationTimestamp: null
      labels:
        app: red
    spec:
      affinity:
    nodeAffinity:
      requiredDuringSchedulingIgnoredDuringExecution:
        nodeSelectorTerms:
        - matchExpressions:
          - key: node-role.kubernetes.io/master
            operator: Exists
      containers:
      - image: nginx
        name: nginx
        resources: {}
status: {}
```