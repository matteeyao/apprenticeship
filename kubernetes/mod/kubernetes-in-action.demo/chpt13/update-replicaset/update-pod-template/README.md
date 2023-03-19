# 13.2.2 Updating the Pod template

* In the next chapter, you'll learn about the Deployment object, which differs from ReplicaSets in how it handles Pod template updates

  * This difference is why you usually manage Pods w/ Deployments and not ReplicaSets

  * Therefore, it's important to see what ReplicaSets don't do

## Editing a ReplicaSet's Pod template

* The kiada Pods currently have labels that indicate the name of the application and the release type (whether it's stable release or something else)

  * It would be great if a label indicated the exact version number, so you can easily distinguish between them when you run different versions simultaneously

* To add a label to the Pods that the ReplicaSet creates, you must add the label to its Pod template

  * You can't add the label w/ the `kubectl label` command, b/c then it would be added to the ReplicaSet itself and not to the Pod template

  * There's no `kubectl` command that does this, so you must edit the manifest w/ `kubectl edit` as you did before

  * Find the `template` field and add the label key `ver` w/ value `0.5` to the `metadata.labels` field in the template, as shown in the following listing, adding a label to the Pod template:

```yaml
apiVersion: apps/v1
kind: ReplicaSet
metadata:
  ...
spec:
  replicas: 2
  selector:           # ← A
    matchLabels:      # ← A
      app: kiada      # ← A 
      rel: stable     # ← A
  template:
    metadata:
      labels:
        app: kiada
        rel: stable
        ver: '0.5'    # ← B
    spec:
      ...

# ← A ▶︎ Do not add the label to the selector.
# ← B ▶︎ Add the label here. Label values must be strings, so you must enclose the version number in quotes.
```

* Make sure you add the label in the right place

  * Don't add it to the selector, as this would cause the Kubernetes API to reject your update, since the selector is immutable

  * The version number must be enclosed in quotes, otherwise the YAML parser will interpret it as a decimal number and the update will fail, since label values must be strings

  * Save the file and close the editor so that `kubectl` can post the updated manifest to the API server

> [!NOTE]
> 
> Did you notice that the labels in the Pod template and those in the selector aren't identical? They don't have to be identical, but the labels in the selector must be a subset of the labels in the template.

## Understanding how the ReplicaSet's Pod template is used

* You updated the Pod template, now check if the change is reflected in the Pods

  * List the Pods and their labels as follows:

```zsh
$ kubectl get pods -l app=kiada --show-labels
NAME          READY   STATUS    RESTARTS  AGE   LABELS
kiada-dl7vz   2/2     Running   0         10m   app=kiada,rel=stable
kiada-dn9fb   2/2     Running   0         10m   app=kiada,rel=stable
```

* Since the Pods still only have the two labels from the original Pod template, it's clear that K8s did not update the Pods

  * However, if you now scale the ReplicaSet up by one, the new Pod should contain the label you added, as shown here:

```zsh
$ kubectl scale rs kiada --replicas 3
replicaset.apps/kiada scaled

$ kubectl get pods -l app=kiada --show-labels
NAME          READY   STATUS    RESTARTS    AGE     LABELS
kiada-dl7vz   2/2     Running   0           14m     app=kiada,rel=stable
kiada-dn9fb   2/2     Running   0           14m     app=kiada,rel=stable
kiada-z9dp2   2/2     Running   0           47s     app=kiada,rel=stable,ver=0.5  # ← A

# ← A ▶︎ The newly created Pod has the additional label.
```

* You should think of the Pod template as a cookie cutter that K8s uses to cut out new Pods

  * When you change the Pod template, only the cookie cutter changes and that only affects the Pods that are created afterwards
