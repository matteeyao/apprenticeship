# Labels, Selectors, Annotations

## nodeSelector

* Assign a pod to a specific node w/ the **nodeSelector** resource

  * In this example, the node must have the label `nodeName=test`:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: nodetest
spec:
  containers:
  - name: n1
    image: nginx
  nodeSelector:
    nodeName: test
```

### If your node isn't labeled, you can follow these steps to test the YAML above:

* Get the node name you want to label:

```zsh
kubectl get nodes
```

* Apply the label `nodeName=test` to the node

> [!IMPORTANT]
> 
> YAML treats numbers and true/false as truthy/falsy values similar to Python. Thus, if you're labeling them w/ numbers, remember to use quotes (e.g. - `version: "2"`).

```zsh
kubectl label node <YOUR_NODE_NAME> nodeName=test
```

## Label pods

* For a more comprehensive list of examples:

```zsh
kubectl label -h | head -27
```

* Let's label the pod we created above:

```zsh
kubectl label pod nodetest name=shannon
```

* Let's confirm it's labeled correctly w/ either of the below commands:

```zsh
kubectl get pods -l name=shannon
# ...or...
kubectl get pods --selector=name=shannon
```

* Now let's remove the label:

```zsh
kubectl label pod nodetest name-
```

* Make sure that the label has been removed:

```zsh
kubectl get pods --show-labels
```

## Annotations

* Both annotations and labels are defined in the `metadata` section of a Pod

  * Labels are used to loosely couple applications

  * In contrast, **annotations are not** used to loosely define any coupling of applications or identifying and selecting objects

  * They can be used in a declarative manner, such as version control or timestamping of a Pod

  * However, you cannot use the `kubectl` CLI to find or query a Pod labeled w/ a specific annotation:

```yaml
apiVersion: v1
kind: Pod
metadata:
  annotations:
    release_version: "0.1.12"
    date: "some date here"
  labels:
    tier: backend
  name: example
spec:
  containers:
  - image: nginx
    name: n1
```

* You can, however, use `kubectl` to annotate something imperatively

  * Take a peek at `kubectl annotate -h` for some practical examples on this
