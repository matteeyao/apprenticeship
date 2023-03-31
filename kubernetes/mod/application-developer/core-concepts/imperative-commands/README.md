# Imperative commands

* While you would be working mostly the declarative way-using definition files, imperative commands can help in getting one time tasks done quickly, as well as generate a definition template easily

  * This would help save considerable amount of time during your exams

* Before we begin, familiarize yourself w/ the two options that can come in handy while working w/ the below commands:

  * `--dry-run=client` ▶︎ by default as soon as the command is run, the resource will be created. If you simply want to test your command, use the `--dry-run=client` option. This will not create the resource, instead, tell you whteher the resource can be created and if your command is right

  * `-o yaml` ▶︎ this will output the resource definition in YAML format on screen

* Use the above two in combination to generate a resource definition file quickly, that you can then modify and create resources as required, instead of creating the files from scratch

## Pod

* Deploy a pod named `nginx-pod` using the `nginx:alpine` image using only imperative commands:

```zsh
kubectl run nginx --image=nginx
```

* Deploy a `redis` pod using the `redis:alpine` image w/ the labels set to `tier=db`:

```zsh
kubectl run redis --image=redis:alpine -l tier=db
```

* Generate pod manifest yaml file (`-o yaml`), but don't create it (`--dry-run=client`)

```zsh
kubectl run nginx --image=nginx --dry-run=client -o yaml
```

## Deployment

* Create a deployment:

```zsh
kubectl create deployment --image=nginx nginx
```

* Generate Deployment YAML file (`-o yaml`), but don't create it (`--dry-run`):

```zsh
kubectl create deployment --image=nginx nginx --dry-run -o yaml
```

* Generate deployment w/ 4 replicas:

```zsh
kubectl create deployment nginx --image=nginx --replicas=4
```

  * You can also scale a deployment using the `kubectl scale` command:

```zsh
kubectl scale deployment nginx --replicas=4
```

  * Another way to do this is to save the YAML definition to a file and modify:

```zsh
kubectl create deployment nginx --image=nginx --dry-run=client -o yaml > nginx-deployment.yaml
```

  * You can then update the YAML file w/ the replicas or any other field before creating the deployment

## Service

### Create a Service named `redis-service` of type `ClusterIP` to expose pod redis on port `6379`

* Create a Service named `redis-service` of type `ClusterIP` to expose pod redis on port `6379` by running:

```zsh
kubectl expose pod redis --port=6379 --name redis-service --dry-run=client -o yaml
```

> [!NOTE]
> 
> This will automatically use the pod's labels as selectors.

* Or, alternatively, run:

```zsh
kubectl create service clusterip redis --tcp=6379:6379 --dry-run=client -o yaml 
```

> [!NOTE]
> 
> * This will not use the pods labels as selectors; instead, it will assume selectors as `app=redis`
>
>   * You cannot pass in selectors as an option
> 
>   * So it does not work very well if your pod has a different label set
> 
>   * So generate the file and modify the selectors before creating the service

## Create a Service named nginx of type `NodePort` to expose pod nginx's port 80 on port 30080 on the nodes:

* Create a Service named nginx of type `NodePort` to expose pod nginx's port 80 on port 30080 on the nodes by running:

```zsh
kubectl expose pod nginx --port=80 --name nginx-service --type=NodePort --dry-run=client -o yaml
```

> [!NOTE]
> 
> * This will automatically use the pod's labels as selectors, but you cannot specify the node port
> 
>   * You have to generate a definition file and then add the node port in manually before creating the service w/ the pod

* Or, alternatively, run:

```zsh
kubectl create service nodeport nginx --tcp=80:80 --node-port=30080 --dry-run=client -o yaml
```

> [!NOTE]
> 
> This will not use the pods labels as selectors.

* Both the above commands have their own challenges. While one of it cannot accept a selector, the other cannot accept a node port

  * We recommend going w/ the `kubectl expose` command

  * If you need to specify a node port, generate a definition file using the same command and manually input the nodeport before creating the service

## Learning summary

1. Create a service `redis-service` to expose the `redis` application within the cluster on port `6379` using imperative commands:

```zsh
kubectl expose pod redis --port 6379 --name redis-service
kubectl get svc   # ← verify that your service exists
```

2. Create a new pod called `custom-nginx` using the `nginx` image and expose it on `container port 8080`

```zsh
kubectl run custom-nginx --image=nginx --port=8080
```

3. Create a new deployment called `redis-deploy` in the `dev-ns` namespace w/ the `redis` image. It should have `2` replicas. Use imperative commands:

```zsh
kubectl create deploy redis-deploy -n dev-ns --image=redis --replicas=2
```

4. Create a pod called `httpd` using the image `httpd:alpine` in the default namespace. Next, create a service of type `ClusterIP` by the same name `(httpd)`. The target port for the service should be `80`:

```zsh
kubectl run httpd --image httpd:alpine --expose=true --port=80
kubectl get svc
kubectl describe svc httpd
```
