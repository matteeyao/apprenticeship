# YAML in K8s

* A K8s definition file always contains 4 top level fields:

  * `apiVersion`

  * `kind`

  * `metadata`

  * `spec`

## `apiVersion`

* The version of the K8s API we're using to create the object.

* Possible values:

  * `v1`

  * `apps/v1beta1`

  * `extensions/v1beta1`

| **Kind**   | **Version** |
|------------|-------------|
| POD        | `v1`        |
| Service    | `v1`        |
| ReplicaSet | `apps/v1`   |
| Deployment | `apps/v1`   |

## `kind`

* The `kind` refers to the type of object we seek to create

* Other possible values could be `ReplicaSet` or `Deployment` or `Service`

## `metadata`

* Data about the object like its name, labels etc.

* Unlike `apiVersion` or `kind`, `metadata` is inputted as a dictionary

## `spec`

* Depending on the object we will create, this is where additional information related to that object is provided

* Similar to `metadata`, `spec` information is inputted as a dictionary

## Edit existing pods

* If you are given a pod definition file, edit that file and use it to create a new pod

* If you are not given a pod definition file, you may extract the definition to a file using the below command:

```zsh
kubectl get pod <POD_NAME> -o yaml > pod-definition.yaml
```

  * Then edit the file to make the necessary changes, delete and re-create the pod

* Use the `kubectl edit pod <POD_NAME>` command to edit pod properties

## Formatting output w/ kubectl

* The default output format for all **kubectl** commands is the human-readable plain-text format

* The `-o` flag allows us to output the details in several different formats

```zsh
kubectl <COMMAND> <TYPE> <NAME> -o <OUTPUT_FORMAT>
```

* Some of the most commonly used formats:

  1. `-o json` ▶︎ output a JSON formatted API object

  2. `-o name` ▶︎ print only the resource name and nothing else

  3. `-o wide` ▶︎ output in the plain-text format w/ any additional information

    * Probably the most common format used to print additional details about the object

  4. `-o yaml` ▶︎ output a YAML formatted API object

## Learning summary

* To create the pod run:

```zsh
kubectl create -f <K8S_OBJECT_DEFINITION>.yml
```

* Create a pod w/ the `nginx` image:

```zsh
kubectl run nginx --image=nginx
```

* Dry run and then create pod w/ `redis123` image:

```zsh
kubectl run redis --image=redis123 --dry-run=client -o yaml
kubectl run redis --image=redis123 --dry-run=client -o yaml > redis.yaml
cat redis.yaml
kubectl create -f redis.yaml
```

* Find out which image used to create a pod:

```zsh
kubectl describe pod <POD_NAME>
```

* View which nodes each pod is hosted on:

```zsh
kubectl get pods -o wide
```

* Edit `replicaset`:

```zsh
kubectl edit rs <REPLICASET_NAME>
```

* Create a new Deployment w/ the below attributes using your own deployment definition file

  * Name: `httpd-frontend`

  * Replicas: `3`

  * Image: `httpd:2.4-alpine`

```zsh
kubectl create deployment --help
kubectl create deployment httpd-frontend --image=httpd:2.4-alpine --replicas=3
kubectl get deploy  # ← ensure deployment enters ready state
```
