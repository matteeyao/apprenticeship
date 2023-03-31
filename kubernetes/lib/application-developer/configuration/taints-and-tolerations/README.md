# Taints and Tolerations

1. Create another pod named `bee` with the `nginx` image, which has a toleration set to the taint `mortain`

```zsh
kubectl run bee --image=nginx --dry-run=client -o yaml > bee.yaml
```

* `cat bee.yaml`:

```yaml
apiVersion: v1
kind: Pod
metadata:
  creationTimestamp: null
  labels:
    run: bee
  name: bee
spec:
  containers:
  - image: nginx
    name: bee
    resources: {}
  dnsPolicy: ClusterFirst
  restartPolicy: Always
  tolerations:
    - key: "spray"
      operator: "Equal"
      value: "mortein"
      effect: "NoSchedule"
status: {}
```

2. Remove the taint on `controlplane`, which currently has the taint effect of `NoSchedule`:

```zsh
kubectl tain node controlplane node-role.kubernetes.io/master:NoSchedule-
```
