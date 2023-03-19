# Injecting config map values into environment variables

* In the previous section, you created the `kiada-config` config map

  * Let's use it in the kiada pod

## Injecting a single config map entry

* To inject the single config map entry into an environment variable, you just need to replace the `value` field in the environment definition w/ the `valueFrom` field and refer to the config map entry

* The following listing shows the relevant part of the pod manifest

  * The full manifest can be found in the file [`pod.kiada.env-valueFrom.yaml`](pod.kiada.env-valueFrom.yaml)

  * Setting an environment variable from a config map entry:

```yaml
kind: Pod
...
spec:
  containers:
  - name: kiada
    env:                            # ← A
    - name: INITIAL_STATUS_MESSAGE  # ← A
      valueFrom:                    # ← B
        configMapKeyRef:            # ← B
          name: kiada-config        # ← C
          key: status-message       # ← D
          optional: true            # ← E
    volumeMounts:
    - ...
  
# ← A ▶︎ You’re setting the environment variable `INITIAL_STATUS_MESSAGE`.
# ← B ▶︎ Instead of using a fixed value, the value is obtained from a config map key
# ← C ▶︎ The name of the config map that contains the value
# ← D ▶︎ The config map key you’re referencing
# ← E ▶︎ The container may run even if the config map or key is not found
```

* Let's break down the definition of the environment variable that you see in the listing

  * Instead of specifying a fixed value for the variable, you declare that the value should be obtained from a config map

  * The name of the config map is specified using the `name` field, whereas the `key` field specifies the key within that map

* Create the pod from this manifest and inspect its environment variables using the following command:

```zsh
$ kubectl exec kiada -- env
...
INITIAL_STATUS_MESSAGE=This status message is set in the kiada-config config map
...
```

* The status message should also appear in the pod's response when you access it via `curl` or your browser

## Marking a reference optional

* In the previous listing, the reference to the config map key is marked as `optional` so the container can be executed even if the config map or key is missing

  * If that's the case, the environment variable isn't set

  * You can mark the reference as optional b/c the Kiada application will run fine w/o it

  * You can delete the config map and deploy the pod again to confirm this

> [!NOTE]
> 
> If a config map or key referenced in the container definition is missing and not marked as optional, the pod will still be scheduled normally. The other containers in the pod are started normally. The container that references the missing config map key is started as soon as you create the config map w/the referenced key.

## Injecting the entire config map

* The `env` field in a container definition takes an array of values, so you can set as many environment variables as you need

  * However, if you want to set more than a few environment variables, it can become tedious and error-prone to specify them one at a time

  * Fortunately, by using the `envFrom` instead of the `env` field, you can inject all the entries that are in the config map w/o having to specify each key individually

* The downside to this approach is that you lose the ability to transform the key to the environment variable name, so the keys must already have the proper form

  * The only transformation that you can do is to prepend a prefix to each key

* For example, the Kiada application reads the environment variable `INITIAL_STATUS_MESSAGE`, but the key you used in the config map is `status-message`

  * You must change the config map key to match the expected environment variable name if you want it to be read by the application when you use the `envFrom` field to inject the entire config map into the pod

  * I've already done this in the `cm.kiada-config.envFrom.yaml` file

  * In addition to the `INITIAL_STATUS_MESSAGE` key, it contains two other keys to demonstrate that they will all be injected into the container's environment

* Replace the config map w/ the one in the file by running the following command:

```zsh
$ kubectl replace -f cm.kiada-config.envFrom.yaml
```

* The pod manifest in the [`pod.kiada.envFrom.yaml`](pod.kiada.envFrom.yaml) file uses the `envFrom` field to inject the entire config map into the pod

  * The following listing shows the relevant part of the manifest ▶︎ Using envFrom to inject the entire config map into environment variables:

```yaml
kind: Pod
...
spec:
  containers:
  - name: kiada
    envFrom:                # ← A
    - configMapRef:         # ← B
        name: kiada-config  # ← B
        optional: true      # ← C

# ← A ▶︎ Using envFrom instead of env to inject the entire config map
# ← B ▶︎ The name of the config map to inject. Unlike before, no key is specified.
# ← C ▶︎ The container should run even if the config map does not exist
```

* Instead of specifying both the config map name and the key as in the previous example, only the config map name is specified

  * If you create the pod from this manifest and inspect its environment variables, you'll see that it contains the environment variable `INITIAL_STATUS_MESSAGE` as well as the other two keys defined in the config map

* As before, you can mark the config map reference as `optional`, allowing the container to run even if the config map deosn't exist

  * By default, this isn't the case

  * Containers that reference config maps are prevented from starting until the referenced config maps exist

## Injecting multiple config maps

* The listing above shows that the `envFrom` field takes an array of values, which means you can combine entries from multiple config maps

  * If two config maps contain the same key, the last one takes precedence

  * You can also combine the `envFrom` field w/ the `env` field if you wish to inject all entries of one config map and particular entries of another

> [!NOTE]
> 
> When an environment variable is configured in the `env` field, it takes precedence over environment variables set in the `envFrom` field

## Prefixing keys

* Regardless of whether you inject a single config map or multiple config maps, you can set an optional `prefix` for each config map

  * When their entries are injected into the container's environment, the prefix is prepended to each key to yield the environment variable name
