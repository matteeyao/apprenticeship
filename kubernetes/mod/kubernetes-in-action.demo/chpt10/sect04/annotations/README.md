## Annotating objects

## Introducing object annotations

* Like labels, annotations are also key-value pairs, but they don't store identifying information and can't be used to filter objects

  * Unlike labels, an annotation value can be much longer (up to 256 KB) and can contain any character

### Understanding annotations added by K8s

* Annotations are often used when new features are introduced to K8s

  * If a feature requires a change to the Kubernetes API (for example, a new field needs to be added to an object's schema), that change is usually deferred for a few Kubernetes releases until it's clear that the change makes sense

### Adding your own annotations

* As w/ labels, you can add your own annotations to objects

  * A great use of annotations is to add a description to each pod or other object so that all users of the cluster can quickly see information about an object w/o having to look it up elsewhere

* For example, storing the name of the person who created the object and their contact information in the object's annotations can greatly facilitate collaboration between cluster users

* Similarly, you can use annotations to provide more details about the application running in a pod

  * For example, you can attach the URL of the Git repository, the Git commit hash, the build timestamp, and similar information to your pods

* You can also use annotations to add the information that certain tools need to manage or augment your objects

  * For example, a particular annotation value set to `true` could signal to the tool whether it should process and modify the object

## Adding annotations to objects

### Setting object annotations

```zsh
$ kubectl annotate pod kiada-front-end created-by='Marko Luksa <marko.luksa@xyz.com>'
pod/kiada-front-end annotated
```

* The command adds the annotation `created-by` w/ the value `Marko Luksa <marko.luksa@xyz.com>` to the `kiada-front-end` pod

### Specifying annotations in the object manifest

* You can also add annotations to your object manifest file before you create the object

  * [`pod.pod-with- annotations.yaml`](pod.pod-with- annotations.yaml) shows annotations in an object manifest:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: pod-with-annotations
  annotations:
    created-by: Marko Luksa <marko.luksa@xyz.com>   # ← A
    contact-phone: +1 234 567 890                   # ← B
    managed: 'yes'                                  # ← C
    revision: '3'                                   # ← D
spec:
  ...

# ← A ▶︎ Here's one annotation.
# ← B ▶︎ Here's another one.
# ← C ▶︎ Third annotation. Value must be quoted. See next warning for explanation.
# ← D ▶︎ Another annotation value that must be quoted or an error would occur.
```

> [!WARNING]
> 
> Make sure you enclose the annotation value in quotes if the YAML parser would otherwise treat it as something other than a string. If you don't, a cryptic error will occur when you apply the manifest. For example, if the annotation value is a number like 123 or a value that could be interpreted as a boolean (true, false, but also words like yes and no), enclose the value in quotes (examples: "123", "true", "yes") to avoid the following error: "unable to decode yaml ... ReadString: expects "or n, but found t".

* Apply the manifest from the previous listing by executing the following command:

```zsh
$ kubectl apply -f pod.pod-with-annotations.yaml
```

## Inspecting an object's annotations

* Unlike labels, the `kubectl get` command does not provide an option to display annotations in the object list

  * To see the annotations of an object, you should use `kubectl describe` or find the annotation in the object's YAML or JSON definition

### Viewing an object's annotations w/ kubectl describe

To see the annotations of the `pod-with-annotations` pod you created, use `kubectl describe`:

```zsh
$ kubectl describe pod pod-with-annotations
Name:         pod-with-annotations
Namespace:    kiada
Priority:     0
Node:         kind-worker/172.18.0.4
Start Time:   Tue, 12 Oct 2021 16:37:50 +0200
Labels:       <none>
Annotations:  contact-phone: +1 234 567 890                   # ← A
              created-by: Marko Luksa <marko.luksa@xyz.com>   # ← A
              managed: yes                                    # ← A
              revision: 3                                     # ← A
Status:       Running
...

# ← A ▶︎ These are the four annotations that were defined in the manifest file.
```

### Displaying the object's annotations in the object's JSON definition

* Alternatively, you can use the `jq` command to extract the annotations from the JSON definition of the pod:

```zsh
$ kubectl get pod pod-with-annotations -o json | jq .metadata.annotations
{
  "contact-phone": "+1 234 567 890",
  "created-by": "Marko Luksa <marko.luksa@xyz.com>",
  "kubectl.kubernetes.io/last-applied-configuration": "..."   # ← A
  "managed": "yes",
  "revision": "3"
}

# ← A ▶︎ This annotation is added by kubectl. It could be deprecated and removed in the future.
```

* You'll notice that there's an additional annotation in the object w/ the key `kubectl.kubernetes.io/last-applied-configuration`

  * It isn't shown by the `kubectl describe` command, b/c it's only used internally by kubectl and would also make the output too long

  * Don't worry if you don't see it when you run the command yourself

## Updating and removing annotations

* If you want to use the `kubectl annotate` command to change an existing annotation, you must also specify the `--overwrite` option, just as you would when changing an existing object label

  * For example, to change the annotation `created-by`, the full command is as follows:

```zsh
$ kubectl annotate pod kiada-front-end created-by='Humpty Dumpty' --overwrite
```

* To remove an annotation from an object, add the minus sign to the end of the annotation key you want to remove:

```zsh
$ kubectl annotate pod kiada-front-end created-by-
```
