# Updating and deleting config maps

* As w/ most Kubernetes API objects, you can update a config map at any time by modifying the manifest file and reapplying it to the cluster using `kubectl apply`

  * There's also a quicker way, which you'll mostly use during development

## In-place editing of API objects using `kubectl edit`

* When you want to make a quick change to an API object, such as a ConfigMap, you can use the `kubectl edit` command

  * For example, to edit the `kiada-envoy-config` config map, run the following command:

```zsh
$ kubectl edit configmap kiada-envoy-config
```

* This opens the object manifest in your default text editor, allowing you to change the object directly

  * When you close the editor, kubectl posts your changes to the Kubernetes API server

> ### Configuring kubectl edit to use a different text editor
> 
> You can tell `kubectl` to use a text editor of your choice by setting the `KUBE_EDITOR` environment variable. For example, if you'd like to use `nano` for editing Kubernetes resources, execute the following command (or put it into your `~/.bashrc` or an equivalent file):
> 
>   ```zsh
>   export KUBE_EDITOR="/usr/bin/nano"
>   ```
> 
> If the `KUBE_EDITOR` environment variable isn't set, `kubectl edit` falls back to using the default editor, usually configured through the `EDITOR` environment variable.

## What happens when you modify a config map

* When you update a config map, the files in the `configMap` volume are automatically updated

> [!NOTE]
> 
> It can take up to a minute for the files in a `configMap` volume to be updated after you change the config map.

* Unlike files, environment variables can't be updated while the container is running

  * However, if the container is restarted for some reason (b/c it crashed or b/c it was terminated externally due to a failed liveness probe), K8s will use the new config map values when it sets up the environment variables for the new container

  * The question is whether you want it to do that at all

## Understanding the consequence of updating a config map

* One of the most important properties of containers is their immutability, which allows you to be sure that there are no differences btwn multiple instances of the sam container (or pod)

  * Shouldn't the config maps from which these instances get their configuration also be immutable?

* Let's think about this for a moment

  * What happens if you change a config map used to inject environment variables into an application?

  * What if the application is configured via config files, but it doesn't automatically reload them when they are modified?

  * The changes you make to the config map don't affect any of these running application instances

  * However, if some of these instances are restarted or you create additional instances, they _will_ use the new configuration

* A similar scenario occurs even w/ applications that can reload their configuration

  * Kubernetes updates `configMap` volumes asynchronously

  * Some application instances may see the changes sooner than others

  * And b/c the updates process may take dozens of seconds, the files in individual pods instances can be out of sync for a considerable amount of time

* In both scenarios, you get instances that are configured differently

  * This may cause parts of your system to behave differently than the rest

  * You need to take this into account when deciding whether to allow changes to a config map while it's being used by running pods

## Preventing a config map from being updated

* To prevent users from changing the values in a config map, you can mark the config map as immutable, as shown in the following listing

  * Creating an immutable config map:

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: my-immutable-configmap
data:
  mykey: myvalue
  another-key: another-value
immutable: true                 # ← A

# ← A ▶︎ This prevents this config map's values from being updated
```

* If someone tries to change the `data` or `binaryData` fields in an immutable config map, the API server will prevent it

  * This ensures that all pods using this config map use the same configuration values

  * If you want to run a set of pods w/ a different configuration, you typically create a new config map and point them to it

* Immutable config maps prevent users from accidentally changing the application configuration, but also help improve the performance of your K8s cluster

  * When a config map is marked as immutable, the Kubelets on the worker nodes that use it don't have to be notified of changes to the ConfigMap object

  * This reduces the load on the API server

## Deleting a config map

* ConfigMap objects can be deleted w/ the `kubectl delete` command

  * The running pods that reference the config map continue to run unaffected, but only until their containers must be restarted

  * If the config map reference in the container definition isn't marked as optional, the container will fail to run
