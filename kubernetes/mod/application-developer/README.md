# CKAD Tips and Tricks

## Kubectl run is your friend

* Create Pod yaml:

```zsh
kubectl run p1 --image=nginx --restart=Never --dry-run=client -o yaml > p1.yaml
```

* Create Deployment yaml:

```zsh
kubectl run d1 --image=nginx --dry-run -o yaml > d1.yaml
```

* Create Job yaml:

```zsh
kubectl run j1 --image=nginx --restart=OnFailure --dry-run=client -o yaml > j1.yaml
```

* Create CronJob yaml:

```zsh
kubectl run cj1 --image=nginx --restart=OnFailure --schedule="*/1 * * * * " \
--dry-run=client -o yaml > cj1.yaml
```

## Grep for events to describe pod details

```zsh
kubectl describe pod <POD_NAME> | grep -i events -A 10
```

* The `-A` option essentially means 'after', so you're saying give me the search results that start w/ 'events' and then the next 10 lines too

* Piping through w/ **tail** works fine as well, but this option is a bit more precise

## Find all the kubectl shortcuts

```zsh
$ kubectl api-resources | grep -i persistentvolumeclaim

persistentvolumeclaims     pvc   true    PersistentVolumeClaim
```

* `api-resources` provides the supported api resources, but also provides you the shortcuts for each resource

```zsh
kubectl get persistentvolumeclaim

kubectl get pvc
```

## Find the fields for supported resources

* Need to know about the NodeSelector field in a pod? Check the specs w/ **explain**:

```zsh
kubectl explain pod.spec.nodeSelector
```

* Note that `kubectl explain` is cap-sensitive, so a suggested practice is to first pipe it through a case-insensitive grep:

```zsh
kk explain pod.spec | grep -i nodeselector
```

## Shorten --help output

* Use `head` to only see examples of the kubectl command; generally, the first 25-30 lines are strictly example-based

```zsh
kubectl annotate --help | head -30
```

## Specificity matters

* It's possible to declare details at both the pod level and the container level

  * For instance, `securityContext` can be set in both

  * More granularity will override other specifics declared in the YAML

* Here is an example w/ `securityContext`:

```zsh
kubectl explain pod.spec.securityContext

kubectl explain pod.spec.containers.securityContext
```

* In the above example, if we set `runAsUser` in both the Pod specs and container specs, the container classification would override the pod specs

## Vim

* `shift + a` ▶︎ go to the end of the current row (insert mode)

* `shift + c` ▶︎ delete everything after the cursor (insert mode)

* `shift + i` ▶︎ go to the first letter on the current row (insert mode)

* `shift + g` ▶︎ go to the last row of data in the file

* `/Pod` ▶︎ find any instances of `Pod` in the file

* `e` ▶︎ jump to the end of the next word

* `w` ▶︎ jump to the start of the next word

* `:25` ▶︎ go to the 25th row in the file
