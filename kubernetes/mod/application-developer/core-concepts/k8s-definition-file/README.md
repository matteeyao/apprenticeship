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

## Learning summary

* To create the pod run:

```zsh
kubectl create -f <K8S_OBJECT_DEFINITION>.yml
```