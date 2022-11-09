# Creating objects 

Kubernetes manifests can be defined in YAML or JSON. The file extension `.yaml`, `.yml`, and `.json` can be used.

* Create resource(s):

```zsh
kubectl apply -f ./my-manifest.yaml
```

* Create from multiple files:

```zsh
kubectl apply -f ./my1.yaml -f ./my2.yaml 
```

* Create resource(s) in all manifest files in dir:

```zsh
kubectl apply -f ./dir
```

* Create resource(s) from url:

```zsh
kubectl apply -f https://git.io/vPieo
```

* Start a single instance of nginx:

```zsh
kubectl create deployment nginx --image=nginx
```

* Create a Job which prints "Hello World":

```zsh
kubectl create job hello --image=busybox:1.28 -- echo "Hello World"
```

* Create a CronJob that prints "Hello World" every minute:

```zsh
kubectl create cronjob hello --image=busybox:1.28   --schedule="*/1 * * * *" -- echo "Hello World"
```

* Get the documentation for pod manifests:

```zsh
kubectl explain pods
```

* Create multiple YAML objects from stdin:

```yzsh
cat <<EOF | kubectl apply -f -
apiVersion: v1
kind: Pod
metadata:
  name: busybox-sleep
spec:
  containers:
  - name: busybox
    image: busybox:1.28
    args:
    - sleep
    - "1000000"
---
apiVersion: v1
kind: Pod
metadata:
  name: busybox-sleep-less
spec:
  containers:
  - name: busybox
    image: busybox:1.28
    args:
    - sleep
    - "1000"
EOF
```

* Create a secret with several keys:

```zsh
cat <<EOF | kubectl apply -f -
apiVersion: v1
kind: Secret
metadata:
  name: mysecret
type: Opaque
data:
  password: $(echo -n "s33msi4" | base64 -w0)
  username: $(echo -n "jane" | base64 -w0)
EOF
```
