# Secrets

* Similar to ConfigMaps, generic secrets-a local file, directory, or literal value-can be declared in the following ways:

  1. `--from-file`

  2. `--from-literal`

  3. `--from-env-file`

* Use the `-h` flag to get some practical examples, as always:

```zsh
kubectl create secret generic -h
```

## Create a secret

* Let's create a secret, similar to ConfigMap:

```zsh
kubectl create secret generic mysecret --from-literal=name=shannon
```

* Confirm the secret was created:

```zsh
kubectl get secret
```

* Take a look at the yaml:

```zsh
kubectl get secret mysecret -o yaml
```

* Here's the important part:

```yaml
- apiVersion: v1
  data:
    name: c2hhbm5vbg==
```

* Let's throw this in a pod as an environment variable and confirm:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: p1
spec:
  containers:
  - command:
    - printenv
    image: busybox
    name: bb1
    env:
      - name: MY_SECRET_NAME    # ← The env variable name in the pod
        valueFrom:
          secretKeyRef:
            name: mysecret      # ← The secret name you created
            key: name           # ← The key you created (name:shannon)
```

* Let's check the logs and make sure the secret was added:

```zsh
kubectl logs p1
---
PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
HOSTNAME=p1
MY_SECRET_NAME=shannon
...
```
