# ReplicaSets

![Fig. 1 Replicas](../../../../../img/automation-orchestration-tools/kubernetes/orchestration/replica-sets/diag01.png)

A ReplicaSet is a set of pods that all share the same labels.

When we invoke this ReplicaSet, those Pods w/ those labels are going to be controlled by the definition (Replica Set Definition) we provide. The Replica Set definition tells us how many Pods we should set up for this set. This allows us to declare that we need 3 pods so it will match that declaration.

Under the Container Definition, we define which containers should be in those Pods.

In the Resource Limits & Environment Definition, we can limit the amount of CPU or memory that those Pods get, exposed container ports, etc.

```
cat ./replicas-example.yaml
```

```yml
apiVersion: apps/v1
kind: ReplicaSet
metadata:
  name: frontend
  labels:
    app: nginx
    tier: frontend
spec:
  # modify replicas according to your case
  replicas: 2
  selector:
    matchLabels:
      tier: frontend
    matchExpressions:
      - {key: tier, operator: In, values: [frontend]}
  template:
    metadata:
      labels:
        app: nginx
        tier: frontend
    spec:
      containers:
      - name: nginx
        image: darealmc/nginx-k8s:v1
        ports:
        - containerPort: 80
```

Create the Replica Set:

```
kubectl create -f ./replicas-example.yaml
```

```
kubectl get pods
```

```
kubectl describe rs/frontend
```

```
kubectl get pods
```

```
kubectl describe pods <POD_NAME>
```

```
curl <POD_IP_ADDRESS>
```

Scale to 4 pods:

```
kubectl scale rs/frontend --replicas=4
```

```
kubectl get pods
```

De-scale to 1 pod:

```
kubectl scale rs/frontend --replicas=1
```

```
kubectl get pods
```

Clean up:

```
kubectl delete rs/frontend
```

```
kubectl get pods
```
