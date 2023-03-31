# Commands and Arguments

1. Create a pod w/ the ubuntu image to run a container to sleep for 5000 seconds. Modify the file `ubuntu-sleeper-2.yaml`.

* `cat ubuntu-sleeper-2.yaml`:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: ubuntu-sleeper-2
spec:
  containers:
    - name: ubuntu
      image: ubuntu
      command: 
        - "sleep"
        - "5000"
      command: [ "sleep" ]
      args: [ "5000" ]
```

* Execute `k create -f ubuntu-sleeper-2.yaml`

2. Create a pod using the file named `ubuntu-sleeper-3.yaml`. There is something wrong w/ it. Try to fix it.

* `cat ubuntu-sleeper-3.yaml`:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: ubuntu-sleeper-3
spec:
  containers:
  - name: ubuntu
    image: ubuntu
    command:
      - "sleep"
      - "1200"  # ← Numbers that are part of the command have to be a string
```

3. Update pod `ubuntu-sleeper-3` to sleep for 2000 seconds

```zsh
kubectl edit pod ubuntu-sleeper-3

kubectl replace --force -f /tmp/kubectl-edit-2693604347.yaml
```

4. Create a pod w/ the given specifications. By default it displays a `blue` background. Set the given command line arguments to change it to `green`

  * Pod Name: webapp-green

  * Image: `kodekloud/webapp-color`

  * Command line arguments: --color=green

```zsh
kubectl run webapp-green --image=kodekloud/webapp-color -- --color green
```