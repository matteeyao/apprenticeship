# Organizing pods w/ labels

* Stuff we've built so far:

| **K8s Object**   | **Service**   |
|------------------|---------------|
| pod              | Kiada         |
| pod              | Quote service |
| pod              | Quiz service  |
| configMap        | -             |
| secret           | -             |
| persistentVolume | -             |
| claim            | -             |

> [!DEFINITION]
> 
> A canary release is a deployment pattern where you deploy a new version of an application alongside the stable version, and direct only a small portion of requests to the new version to see how it behaves before rolling it out to all users. This prevents a bad release from being available to too many users.

## Introducing labels

* Labels are allow us to organize K8s API objects

  * A label is a key-value pair you attach to an object that allows any user of the cluster to identify the object's role in the system

  * An object can have more than one label, but the label keys must be unique within that object

* For instance...

  * An `app` label indicates to which application the pod belongs

  * A `rel` label indicates whether the pod is running the stable or canary release of the application

## Attach labels to pods

### Exercise setup

* Create a new namespace called `kiada` as follows:

```zsh
$ kubectl create namespace kiada
namespace/kiada created
```

* Configure kubectl to default to this new namespace:

```zsh
$ kubectl config set-context --current --namespace kiada
Context "kind-kind" modified.
```

* Apply each manifest files within [`kiada-suite`](kiada-suite):

```zsh
$ kubectl apply -f kiada-suite/ --recursive                                     # ← A
configmap/kiada-envoy-config created pod/kiada-001 created
pod/kiada-002 created
pod/kiada-003 created pod/kiada-canary created secret/kiada-tls created
pod/quiz created persistentvolumeclaim/quiz-data created pod/quote-001 created
pod/quote-002 created
pod/quote-003 created
pod/quote-canary created

# ← A ▶︎ This applies all manifests in the kiada-suite/ directory and its subdirectories.
```

* The `--recursuve` option causes kubectl to look for manifests in all subdirectories instead of just the specified directory

### Display object labels

* Display labels w/ the `--show-labels` option:

```zsh
$ kubectl get pods --show-labels
```

* Instead of showing all labels w/ `--show-labels`, you can also show specific labels w/ the `--label--columns` option (or `-L`)

  * Each label is displayed in its own column

  * List all pods along w/ their `app` and `rel` labels as follows:

```zsh
$ kubectl get pods -L app,rel
NAME            READY   STATUS    RESTARTS    AGE   APP   REL
kiada-canary    2/2     Running   0           14m
kiada-001       2/2     Running   0           14m   kiada stable
kiada-002       2/2     Running   0           14m   kiada stable
kiada-003       2/2     Running   0           14m   kiada stable
quiz            2/2     Running   0           14m   quiz  stable
quote-canary    2/2     Running   0           14m
quote-001       2/2     Running   0           14m   quote stable
quote-002       2/2     Running   0           14m   quote stable
quote-003       2/2     Running   0           14m   quote stable
```

### Add labels to an existing object

* To add labels to an existing object...

  * You can edit the object's manifest file, add labels to the `metadata` section, and reapply the manifest using `kubectl apply`

  * You can also edit the object definition directly in the API using `kubectl edit`

  * Use the `kubectl label` command

* Add the labels `app` and `rel` to the `kiada-canary` pod using the folowing command:

```zsh
$ kubectl label pod kiada-canary app=kiada rel=canary
pod/kiada-canary labeled
```

### Change labels of an existing object

* To change the label you set incorrectly, run the following command:

```zsh
$ kubectl label pod quote-canary app=quote
error: 'app' already has a value (kiada), and --overwrite is false
```

* To prevent accidentally changing the value of an existing label, you must explicitly tell kubectl to overwrite the label w/ `--overwrite`, as such:

```zsh
$ kubectl label pod quote-canary app=quote --overwrite
pod/quote-canary labeled
```

## Label all objects of a kind

* Rune the following command to add the label to all pods in the namespace:

```zsh
$ kubectl label pod --all suite=kiada-suite
pod/kiada-canary labeled
pod/kiada-001 labeled
...
pod/quote-003 labeled
```

* List the pods again w/ the `--show-labels` or the `-L suite` option to confirm that all pods now contain this new label

## Remove a label from an object

* To remove the label from an object, run the `kubectl label` command w/ a minus sign after the label key as wollows:

```zsh
$ kubectl label pod kiada-canary suite-     # ← A
pod/kiada-canary labeled

# ← A ▶︎ The minus sign signifies the removal of a label
```

* To remove the label from all other pods, specify `--all` instead of the pod name:

```zsh
$ kubectl label pod --all suite- 
label "suite" not found.            # ← A
pod/kiada-canary not labeled        # ← A
pod/kiada-001 labeled
...
pod/quote-003 labeled

# ← A ▶︎ The kiada-canary pod doesn't have the suite label.
```

## Label syntax rules

### Valid label keys

* Common label keys that K8s itself applies or reads always start w/ a prefix

  * This also applies to labels used by K8s components outside the core, as well as other commonly accepted label keys

* An example of a prefixed label key used by K8s is `kubernetes.io/arch`

  * You can find it on Node objects to identify the architecture type used by the node

```zsh
$ kubectl get node -L kubernetes.io/arch
NAME                  STATUS  ROLES                 AGE   VERSION   ARCH
kind-control-plane    Ready   control-plane,master  31d   v1.21.1   amd64   # ← A
kind-worker           Ready   <none>                31d   v1.21.1   amd64   # ← A
kind-worker2          Ready   <none>                31d   v1.21.1   amd64   # ← A

# ← A ▶︎ The kubernetes.io/arch label is set to amd64 on all three nodes.
```

* The label prefixes `kubernetes.io/` and `k8s.io/` are reserved for K8s components

* The following syntax rules apply to the prefix:

  * Must be a DNS subdomain (must contain only lowercase alphanumeric characters, hyphens, underscores, and dots)

  * Must be no more than 253 characters long (not including the slash character)

  * Must end with a forward slash

* The prefix must be followed by the label name, which:

  * Must begin and end w/ an alphanumeric character

  * May contain hyphens, underscores, and dots

  * May contain uppercase letters

  * May not be longer than 63 characters

### Valid label values

* A label value:

  * May be empty

  * Must being w/ an alphanumeric character if not empty

  * May contain only alphanumeric characters, hyphens, underscores, and dots

  * Must not contain spaces or other whitespace

  * Must be no more than 63 characters long

* If you need to add values that don't follow these rules, add them as annotations instead of labels

## Using standard label keys

* Well-known labels on Nodes and PersistentVolumes:

| **Label key**                      | **Example value** | **Applied to**        | **Description**                                                       |
|------------------------------------|-------------------|-----------------------|-----------------------------------------------------------------------|
| `kibernetes.io/arch`               | `amd64`           | Node                  | The architecture of the node.                                         |
| `kubernetes.io/os`                 | `linux`           | Node                  | The operating system running on the node.                             |
| `kubernetes.io/hostname`           | `worker-node2`    | Node                  | THe node's hostname.                                                  |
| `topology.kubernetes.io/region`    | `eu-west3`        | Node PersistentVolume | The region in which the node or persistent volume is located.         |
| `topology.kubernetes.io/zone`      | `eu-west3-c`      | Node PersistentVolume | The zone in which the node or persistent volume is located.           |
| `node.kubernetes.io/instance-type` | `micro-1`         | Node                  | The node instance type. Set when using cloud-provided infrastructure. |

* Recommended labels used in the Kubernetes community:

| **Label**                      | **Example**       | **Description**                                                                                                                                                        |
|--------------------------------|-------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `app.kubernetes.io/name`       | `quotes`          | The name of the application. If the application consists of multiple components, this is the name of the entire application, not the individual components.            |
| `app.kubernetes.io/instance`   | `quotes-foo`      | The name of this application instance. If you create multiple instances of the same application for different purposes, this label helps you distinguish between them. |
| `app.kubernetes.io/component`  | `database`        | The role that this component plays in the application architecture                                                                                                     |
| `app.kubernetes.io/part-of`    | `kubia-demo`      | The name of the application suite to which this application belongs.                                                                                                   |
| `app.kubernetes.io/version`    | `1.0.0`           | The version of the application.                                                                                                                                        |
| `app.kubernetes.io/managed-by` | `quotes-operator` | The tool that manages the deployment and update of this application.                                                                                                   |
