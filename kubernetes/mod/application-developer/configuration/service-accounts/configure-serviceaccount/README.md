# ServiceAccounts

* When we access the cluster (for example, using kubectl), we are authenticated by the apiserver as a particular User Account (currently, this is usually admin, unless the cluster administrator has customized our cluster)

  * Processes in containers inside pods can also contact the apiserver

  * When they do, they are authenticated as a particular Service Account (for example, default)

* Let's take a look at the yaml:

```zsh
kk create sa mysa --dry-run=client -o yaml
---
apiVersion: v1
kind: ServiceAccount
metadata:
  creationTimestamp: null
  name: mysa
```

* Let's build it now w/o the `--dry-run=client` option:

```zsh
kubectl create sa mysa
```

* Then add it to a Pod's spec

  * `pod.spec.serviceAccount` is deprecated, so we'll use `serviceAccountName` instead:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: p1
spec:
  serviceAccountName: mysa
  containers:
  - image: nginx
    name: c1
```
