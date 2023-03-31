# Deployments, Rollouts, and Rollbacks

## Create a deployment

* **Deployment name**: `d1`

* **Pod name**: `d1-<SOME_UNIQUE_HASH_K8s_POPULATES>`

* **Container name**: `c1`

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: d1
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
      - image: nginx
        name: c1
```

* Check the status of the deployment, pod, and replicaset:

```zsh
kubectl get deploy
kubectl get rs
kubectl get po
```

* Or, use:

```zsh
kubectl get deploy,rs,po
```

* Confirm they're labeled properly:

```zsh
kubectl get po --show-labels
```

## Update the image

* Right now, the image is just `nginx`, which means it's `nginx:latest`

  * The `set` command allows us to update the image

  * It also has some additional features

  * Use `kubectl set -h` to see the full list

* For a specific example, take a peek at `kubectl set image -h | head -19` to get some practical examples beforehand

  * Now, let's update the `nginx` image to `nginx:1.9.1`:

```zsh
kubectl set image deploy/d1 c1=nginx:1.9.1
```

* It's important to understand the above and why we put the container's name as `c1`

  * In many situations, the container name and container image will be labeled the same thing

  * Due to this, the examples in the documentation can be a bit confusing

* Let's take a quick look at the first example from the documentation:

```zsh
# Set a deployment's nginx container image to 'nginx:1.9.1'
  kubectl set image deployment/nginx nginx=nginx:1.9.1
```

* Here, the container's name is also nginx

  * Try this in our `d1` deployment, and you'll get an error:

```zsh
kk set image deploy/d1 nginx=nginx:1.9.1
---
error: unable to find container named "nginx"
```

* Let's confirm that a new replica set was created and the new containers are deployed

  * Look in the events description to determine some whether new replica sets were scaled up:

```zsh
kubectl describe deploy d1 | grep -i events -A 10
---
Events:
  Type    Reason             Age    From                   Message
  ----    ------             ----   ----                   -------
  Normal  ScalingReplicaSet  9m52s  deployment-controller  Scaled up replica set d1-7544767566 to 2
  Normal  ScalingReplicaSet  6m35s  deployment-controller  Scaled up replica set d1-8557bf55bf to 1
  Normal  ScalingReplicaSet  6m27s  deployment-controller  Scaled down replica set d1-7544767566 to 1
  Normal  ScalingReplicaSet  6m27s  deployment-controller  Scaled up replica set d1-8557bf55bf to 2
  Normal  ScalingReplicaSet  6m23s  deployment-controller  Scaled down replica set d1-7544767566 to 0
```

* And check the replica sets too:

```zsh
kubectl get rs
---
NAME            DESIRED   CURRENT   READY   AGE
d1-7544767566   0         0         0       9m58s
d1-8557bf55bf   2         2         2       6m41s
```

## Scale the deployment

* To scale the number of pods, use `kubectl scale`

  * For examples, `kubectl scale -h`

* Let's increase the number of replicas in our deployment to 3:

```zsh
kubectl scale --replicas=3 deploy/d1
```

* Confirm an additional pod has been created:

```zsh
kubectl get pods
```

* Take a look at the replica set description:

```zsh
kubectl describe rs <D1_YOUR_UNIQUE_HASH>
```

> [!NOTE]
> 
> An additional pod was generated but a new replica set was not created, only scaled. This is b/c there is no change in the actual pod details. In our previous example, we used a new nginx image (nginx:1.9.1) and thus there was an actual change to the code.

## Rollout history

* Let's confirm the observation above

  * `kubectl rollout` is what we'll use here

  * As always, use the `-h` to get an example w/ what we're working w/

* Let's check out the history, which should only have 2 revisions:

```zsh
kubectl rollout history deploy/d1
```

* Now, check the revision for more details on what changed:

```zsh
kubectl rollout history deploy/d1 --revision=1
kubectl rollout history deploy/d1 --revision=2
```

* You'll note only one different below in the details:

```zsh
Containers:
   c1:
    Image:	nginx
---
Containers:
   c1:
    Image:	nginx:1.9.1
```

## Rollback

* Let's get this deployment back to `nginx:latest` instead of `nginx:1.9.1`

* In `rollout`, there are some other options

  * For this, we'll use `kubectl rollout undo`

* You can either use a specific revision or just rollback to the previous revision

  * Since they're both the same in our case, we'll use the standard rollout undo:

```zsh
kubectl rollout undo deploy/d1
```

* Let's check what's in our history now:

```zsh
kk rollout history deploy/d1
---
REVISION  CHANGE-CAUSE
2         <none>
3         <none>
```

* There's no longer a revision 1

  * Essentially, revision 3 is revision 1

  * Check revision 3 to confirm that the image is now `nginx`:

```zsh
kubectl rollout history deploy/d1 --revision=3
```

## Cleaning up

* For practice, let's scale the deployment back to 2 replicas and then delete the deployment:

```zsh
kk scale --replicas=2 deploy/d1

kk delete deploy d1
```

* Confirm the pods, replica set, and deployment have been deleted:

```zsh
kubectl get deploy,rs,po
```
