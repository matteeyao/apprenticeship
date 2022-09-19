# Deploying to Kubernetes

Pods are a great way to organize and manage containers, but what if I want to spin up and automate multiple pods?

**Deployments** are a great way to automate the management of your pods. A deployment allows you to specify a **desired state** for a set of pods. The cluster will then constantly work to maintain that desired state.

For example:

* **Scaling** ▶︎ W/ a deployment, you can specify the number of replicas you want, and the deployment will create (or remove) pods to meet that number of replicas.

* **Rolling Updates** ▶︎ W/ a deployment, you can change the deployment container image to a new version of the image. The deployment will gradually replace existing containers w/ the new version.

* **Self-Healing** ▶︎ If one of the pods in the deployment is accidentally destroyed, the deployment will immediately spin up a new one to replace it.

Let's create a simple deployment.

We'll make a deployment that includes two replicas running basic Nginx containers.

So, create the simple deployment:

```
cat << EOF | kubectl create -f -
apiVersion: apps/v1
kind: Deployment
metadata:
  name: nginx-deployment
  labels:
    app: nginx
spec:
  replicas: 2
  selector:
    matchLabels:
      app: nginx
  template:
    metadata:
      labels:
        app: nginx
    spec:
      containers:
      - name: nginx
       image: nginx:1.15.4
       ports:
       - containerPort: 80
EOF
```

Get list of deployments, similar to getting list of pods:

```
kubectl get deployments
```

Get more information about a deployment:

```
kubectl describe deployment nginx-deployment
```

List pods that were created as past of that deployment:

```
kubectl get pods
```

## Alternative walk-through

`kubectl` get deployment command:

```
Name:   nginx-deployment
Namespace:    default
CreationTimestamp:  Thu, 30 Nov 2017 10:56:25 +0000
Labels:     app=nginx
Annotations:    deployment.kubernetes.io/revision=2
Selector:   app=nginx
Replicas    3 desired | 3 updated | 3 total | 3 available | 0 unavailable
StrategyType:   RollingUpdate
MinReadySeconds:    0
RollingUpdateStrategy: 25% max unavailable, 25% max surge
  Pod Template:
    Labels: app=nginx
    Containers:
      nginx:
        Image:    nginx:1.9.1
        Port:   80/TCP
        Environment:  <none>
        Mounts: <none>
      Volumes:  <none>
```

```
Name:   nginx-deployment
Namespace:    default
CreationTimestamp:  Thu, 30 Nov 2017 10:56:25 +0000
Labels:     app=nginx
Annotations:    deployment.kubernetes.io/revision=2
...
```

A deployment is a defined object in the Kubernetes cluster.

Deployments can be used to:

* Roll out replica sets

* Declare a new state for the pods in a replica set

* Roll back to an earlier deployment

* Scale up deployments for load

* Clean up replica sets that are no longer needed

```
...
Selector:   app=nginx
Replicas    3 desired | 3 updated | 3 total | 3 available | 0 unavailable
...
```

Deployments manage replica sets.

Deployments can be used to gradually remove and replace the pods defined in a replica set that is managed by the deployment.

When an update is required, a new replica set is created, and pods are brought up and down by the deployment controller.

The `Selector` is the label that the Pods have that allow this deployment to control those Pods. This is very much the same as replica sets. Deployments manage replica sets.

```
...
StrategyType:   RollingUpdate
MinReadySeconds:    0
RollingUpdateStrategy: 25% max unavailable, 25% max surge
...
```

Deployments define an update strategy and allow for rolling updates.

In this case, the pods will be replaced in increments of 25% of the total number of pods.

If we needed to update the pods, the update strategy is defined in the section above.

```
...
  Pod Template:
    Labels: app=nginx
    Containers:
      nginx:
        Image:    nginx:1.9.1
        Port:   80/TCP
        Environment:  <none>
        Mounts: <none>
      Volumes:  <none>
```

Deployments contain a PodSpec.

The PodSpec can be updated to increment the container version or the structure of the pods that are deployed.

If the PodSpec is updated and differs from the current spec, this triggers the rollout process.

## Further demonstration

View manifest for deployment:

```
cat ./deployexample.yaml
```

```yml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: example-deployment
  labels:
    app: nginx
spec:
  replicas: 2
  selector:
    matchLabels:
      app: nginx
  template:
    metadata:
      labels:
        app: nginx
    spec:
      containers:
      - name: nginx
        image: darealmc/nginx-k8s:v1
        ports:
        - containerPort: 80
```

```
kubectl create -f ./deployexample.yaml
```

```
kubectl get pods
```

```
kubectl create -f service-example.yaml
```

```
kubectl describe service <SERVICE_NAME>
```

```
kubectl set image deployment.v1.apps/example-deployment nginx=darealmc/nginx-k8s:v2
```

```
kubectl get deployment
```

```
kubectl describe deployment example-deployment
```

```
curl 10.106.2.26:32768
```
