# ConfigMaps

* ConfigMaps are the configuration portion of a microservice where you swap in and out environment variables

  * Want to point to a specific database, namespace, etc?

  * You're probably going to be doing that w/ ConfigMaps, unless it's more sensitive data in which you'll be using secrets

* Here's a quick Node.JS example connecting to a local MongoDB pod:

```js
const uri = 'mongodb://mongo-cluster-ip-service:27017/docker-node';
```

* Now, let's put that in terms of a ConfigMap

```js
const uri = 'mongodb://' + \
process.env.DEV_DB_ENDPOINT + ':' + \
process.env.DEV_DB_PORT + '/' + \
process.env.DEV_DB_NAME
```

## Create a ConfigMap

```zsh
kubectl create configmap
```

* As always, use the `-h` flag to see more examples

* There are a few different options you can select from, but we'll just choose `-from-literal=foo=bar`

* Your options:

  1. `-from-file`

  2. `-from-literal`

  3. `-from-env-file`

* Let's create a configmap called `cm1`:

```zsh
kubectl create cm cm1 --from-literal=name=shannon --from-literal=config=map
```

* This will create a configmap w/ two **keys**-name and config-and two **values**-shannon and map

* To confirm the `configmap` was created:

```zsh
kubectl get cm
```

* To view the yaml definition:

```zsh
kubectl get cm cm1 -o yaml
```

* To confirm the key value pairs are in the configmap:

```yaml
kubectl describe cm cm1
---
Name:         cm1
Namespace:    default
Labels:       <none>
Annotations:  <none>

Data
====
config:
----
map
name:
----
shannon
Events:  <none>
```

> [!NOTE]
> 
> The dashes separate the key from the value.

* Let's create a pod and add these `env` variables to them

  * Use `kubectl run` to generate a simple template

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
```

* First, we'll add an individual `env` variable from the cm and then we'll add all of them

  * Make sure to delete the pod before running this again unless you've changed the `name` in `metadata`

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
      - name: MY_NAME       # ← the env variable name in your pod
        valueFrom:
          configMapKeyRef:
            name: cm1       # ← the cm name you created (cm1)
            key: name       # ← the key you created (name:shannon)
```

* Run the pod to check if the environment variables was added:

```zsh
kubectl logs p1
---
PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
HOSTNAME=p1
MY_NAME=shannon
....
```

* To add all the environment variables from configmap, use `envFrom:` instead of `env:`

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
    envFrom:
    - configMapRef:
        name: cm1
```

* Check the logs again to see if both the environment variables are now logged:

```zsh
kubectl logs p1
---
PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin
HOSTNAME=p1
config=map
name=shannon
```

## Clean up

* Delete the cm:

```zsh
kubectl delete cm cm1
```

* Try to build the pod again to see what happens:

```zsh
kubectl describe po p1
---
Warning  Failed 4s   kubelet, <node>  Error: configmap "cm1" not found
```

* If a `configmap` object does not exist, the pod cannot be created. If looking at all pods, a similar error will be generated:

```zsh
kubectl get po
---
NAME   READY   STATUS                       RESTARTS   AGE
p1     0/1     CreateContainerConfigError   0          50s
```
