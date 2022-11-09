# Viewing, finding resources

## Get commands with basic output

* List all services in the namespace:

```zsh
kubectl get services
```

* List all pods in all namespaces

```zsh
kubectl get pods --all-namespaces
```

* List all pods in the current namespace, with more details

```zsh
kubectl get pods -o wide
```

* List a particular deployment:

```zsh
kubectl get deployment <DEPLOYMENT> 
```

* List all pods in the namespace

```zsh
kubectl get pods
```

* Get a pod's YAML:

```zsh
kubectl get pod my-pod -o yaml
```

## Describe commands w/ verbose output

```zsh
kubectl describe nodes <NODE_NAME>
```

```zsh
kubectl describe pods <POD_NAME>
```

## List commands

* List services sorted by name:

```zsh
kubectl get services --sort-by=.metadata.name
```

* List pods sorted by restart count:

```zsh
kubectl get pods --sort-by='.status.containerStatuses[0].restartCount'
```

* List `PersistentVolumes` sorted by capacity:

```zsh
kubectl get pv --sort-by=.spec.capacity.storage
```

* Get the version label of all pods with label `app=cassandra`:

```zsh
kubectl get pods --selector=app=cassandra -o \
  jsonpath='{.items[*].metadata.labels.version}'
```

* Retrieve the value of a key with dots, e.g. `'ca.crt'`:

```zsh
kubectl get configmap myconfig \
  -o jsonpath='{.data.ca\.crt}'
```

* Retrieve a base64 encoded value with dashes instead of underscores:

```zsh
kubectl get secret my-secret --template='{{index .data "key-name-with-dashes"}}'
```

* Get all worker nodes (use a selector to exclude results that have a label named 'node-role.kubernetes.io/control-plane'):

```zsh
kubectl get node --selector='!node-role.kubernetes.io/control-plane'
```

* Get all running pods in the namespace:

```zsh
kubectl get pods --field-selector=status.phase=Running
```

* Get `ExternalIP`s of all nodes:

```zsh
kubectl get nodes -o jsonpath='{.items[*].status.addresses[?(@.type=="ExternalIP")].address}'
```

* List Names of Pods that belong to Particular RC:

    * `"jq"` command useful for transformations that are too complex for jsonpath, it can be found at https://stedolan.github.io/jq/

```zsh
sel=${$(kubectl get rc my-rc --output=json | jq -j '.spec.selector | to_entries | .[] | "\(.key)=\(.value),"')%?}
echo $(kubectl get pods --selector=$sel --output=jsonpath={.items..metadata.name})
```

* Show labels for all pods (or any other Kubernetes object that supports labelling):

```zsh
kubectl get pods --show-labels
```

* Check which nodes are ready:

```zsh
JSONPATH='{range .items[*]}{@.metadata.name}:{range @.status.conditions[*]}{@.type}={@.status};{end}{end}' \
 && kubectl get nodes -o jsonpath="$JSONPATH" | grep "Ready=True"
```

* Output decoded secrets without external tools:

```zsh
kubectl get secret my-secret -o go-template='{{range $k,$v := .data}}{{"### "}}{{$k}}{{"\n"}}{{$v|base64decode}}{{"\n\n"}}{{end}}'
```

* List all Secrets currently in use by a pod:

```zsh
kubectl get pods -o json | jq '.items[].spec.containers[].env[]?.valueFrom.secretKeyRef.name' | grep -v null | sort | uniq
```

* List all containerIDs of initContainer of all pods

    * Helpful when cleaning up stopped containers, while avoiding removal of initContainers.

```zsh
kubectl get pods --all-namespaces -o jsonpath='{range .items[*].status.initContainerStatuses[*]}{.containerID}{"\n"}{end}' | cut -d/ -f3
```

* List Events sorted by timestamp:

```zsh
kubectl get events --sort-by=.metadata.creationTimestamp
```

* Compares the current state of the cluster against the state that the cluster would be in if the manifest was applied:

```zsh
kubectl diff -f ./my-manifest.yaml
```

* Produce a period-delimited tree of all keys returned for nodes

    * Helpful when locating a key within a complex nested JSON structure

```zsh
kubectl get nodes -o json | jq -c 'paths|join(".")'
```

* Produce a period-delimited tree of all keys returned for pods, etc

```zsh
kubectl get pods -o json | jq -c 'paths|join(".")'
```

* Produce ENV for all pods, assuming you have a default container for the pods, default namespace and the `env` command is supported.

    * Helpful when running any supported command across all pods, not just `env`

```zsh
for pod in $(kubectl get po --output=jsonpath={.items..metadata.name}); do echo $pod && kubectl exec -it $pod -- env; done
```

* Get a deployment's status subresource:

```zsh
kubectl get deployment nginx-deployment --subresource=status
```
