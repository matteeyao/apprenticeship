# Injecting config map entries into containers as files

* Environment variables are typically used to pass small single-line values to the application, while multiline values are usually passed as files

  * Config map entries can also contain larger blocks of data that can be projected into the container using the special `configMap` volume type

> [!NOTE]
> 
> The amount of information that can fit in a config map is dictated by etcd, the underlying data store used to store API objects. At this point, the maximum size is on the order of one megabyte.

* A `configMap` volume makes the config map entries available as individual files

  * The process running in the container gets the entry's value by reading the contents of the file

  * This mechanism is most often used to pass large config files to the container, but can also be used for smaller values, or combined w/ the `env` or `envFrom` fields to pass large entries as files and others as environment variables

## Creating config map entries from files

* In chapter 4, you deployed the Kiada pod w/ an Envoy sidecar that handles TLS traffic for the pod

  * B/c volumes weren't explained at that point, the configuration file, TLS certificate, and private key that Envoy uses were built into the container image

  * It would be more convenient if these files were stored in a config map and injected into the container

  * That way you could update them w/o having to rebuild the image

  * But since the security considerations of these files are different, we must handle them differently

  * Let's focus on the config files first

* You've already learned how to create a config map from a literal value using the `kubectl create configmap` command

  * This time, instead of creating the config map directly in the cluster, you'll create a YAML manifest for the config map so that you can store it in a version control system alongside your pod manifest

* Instead of writing the manifest file by hand, you can create it using the same `kubectl create` command that you used to create the object directly

  * The following command creates the YAML file for a config map named [`kiada-envoy-config`](create-configmap-kiada-envoy-config.sh):

```zsh

$ kubectl create configmap kiada-envoy-config \
    --from-file=envoy.yaml \
    --from-file=dummy.bin \
    --dry-run=client -o yaml > cm.kiada-envoy-config.yaml
```

* The config map will contain two entries that come from the files specified in the command

  * One is the `envoy.yaml` configuration file, while the other is just some random data to demonstrate that binary data can also be stored in a config map

* When using the `--dry-run` option, the command doesn't create the object in the Kubernetes API server, but only generates the object definition

  * The `-o yaml` option prints the YAML definition of the object to standard output, which is then redirected to the [`cm.kiada-envoy-config.yaml`](cm.kiada-envoy-config.yaml) file

  * The following listing shows the contents of this file ▶︎ A config map manifest containing a multi-line value:

```yaml
apiVersion: v1
binaryData:
  dummy.bin: n2VW39IEkyQ6Jxo+rdo5J06Vi7cz5... # ← A
data:
  envoy.yaml: |                               # ← B
    admin:                                    # ← B
      access_log_path: /tmp/envoy.admin.log   # ← B
      address:                                # ← B
        socket_address:                       # ← B
        protocol: TCP                         # ← B
        address: 0.0.0.0                      # ← B
    ...                                       # ← B
kind: ConfigMap
metadata:
  creationTimestamp: null
  name: kiada-envoy-config                    # ← C

# ← A ▶︎ Base64-encoded content of the `dummy.bin` file.
# ← B ▶︎ Contents of the `envoy.yaml` file.
# ← C ▶ The name of this config map.
```

* As you can see in the listing, the binary file ends up in the `binaryData` field, whereas the envoy config file is in the `data` field, which you know from the previous sections

  * If a config map entry contains non-UTF-8 byte sequences, it must be defined in the `binaryData` field

  * The `kubectl create configmap` command automatically determines where to put the entry

  * The values in this field are Base64 encoded, which is how binary values are represented in YAML

* In contrast, the contents of the `envoy.yaml` file are clearly visible in the `data` field

  * In YAML, you can specify multi-line values using a pipeline character and appropriate indentation

* Don't apply the config map manifest file to the Kubernetes cluster yet

  * You'll first create the pod that refers to the config map

  * This way you can see what happens when a pod points to a config map that doesn't exist

> ### Mind your whitespace hygiene when creating config maps
> 
> * When creating config maps from files, make sure that none of the lines in the file contain trailing whitespace
>
>   * If any line ends w/ whitespace, the value of the entry in the manifest is formatted as a quoted string w/ the newline character escaped
> 
>   * This makes the manifest incredibly hard to read and write
> 
>   ```yaml
>   $ kubectl create configmap whitespace-demo \
>       --from-file=envoy.yaml \
>       --from-file=envoy-trailingspace.yaml \
>       --dry-run=client -o yaml
>   ```
>
> * Notice that the `envoy-trailingspace.yaml` file contains a space at the end of the fist line
>
>   * This causes the config map entry to be presented in a not very human-friendly format
>
>   * In contrast, the `envoy.yaml` file contains no trailing whitespace and is presented as an unescaped multi-line string, which makes it easy to read and modify in place

## Using a ConfigMap volume in a pod

* To make config map entries available as files in the container's filesystem, define a `configMap` volume in the pod and mount it in the container, as in the following listing, which shows the relevant parts of the [`pod.kiada-ssl.configmap-volume.yaml`](pod.kiada-ssl.configmap-volume.yaml) file

  * Defining a configMap volume in a pod:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: kiada-ssl
spec:
  volumes:
  - name: envoy-config                # ← A
    configMap:                        # ← A
      name: kiada-envoy-config        # ← A
  ...
  containers:
  ...
  - name: envoy
    image: luksa/kiada-ssl-proxy:0.1
    volumeMounts:                     # ← B
    - name: envoy-config              # ← B
      mountPath: /etc/envoy           # ← B
  ...

# ← A ▶︎ The definition of the configMap volume
# ← B ▶︎ The volume is mounted into the container
```

* The definitions of the `volume` and `volumeMount` should be clear

  * As you can see, the volume is a `configMap` volume that points to the `kiada-envoy-config` config map, and it's mounted in the `envoy` container under `/etc/envoy`

  * The volume contains the `envoy.yaml` and `dummy.bin` files that match the keys in the config map

* Create the pod from the manifest file and check its status

  * Here's what you'll see:

```zsh
$ kubectl get po
NAME        READY   STATUS              RESTARTS  AGE
Kiada-ssl   0/2     ContainerCreating   0         2m
```

* B/c the pod's `configMap` volume references a config map that doesn't exist, and the reference isn't marked as optional, the container can't run

## Marking a ConfigMap volume as optional

* Previously, you learned that if a container contains an environment variable definition that refers to a config map that doesn't exist, the container is prevented from starting until you create that config map

  * You also learned that this doesn't prevent the other containers from starting

  * What about the case at hand where the missing config map is referenced in a volume?

* B/c all the pod's volumes must be set up before the pod's containers can be started, referencing a missing config map in a volume prevents all the containers in the pod from starting, not just the container in which the volume is mounted

  * An event is generated indicating the problem

  * You can display it w/ the `kubectl describe pod` or `kubectl get events` command, as explained in the previous chapters

> [!NOTE]
> 
> A `configMap` volume can be marked as optional by adding the line `optional: true` to the volume definition. If a volume is optional and the config map doesn't exist, the volume is not created, and the container is started w/o mounting the volume.

* To enable the pod's containers to start, create the config map by applying the [`cm.kiada-envoy-config.yaml`](cm.kiada-envoy-config.yaml) file you created earlier

  * Use the `kubectl apply` command

  * After doing this, the pod should start, and you should be able to confirm that both config map entries are expsoed as files in the container by listing the contents of the `/etc/envoy` directory as follows:

```zsh
$ kubectl exec kiada-ssl -c envoy -- ls /etc/envoy
dummy.bin
envoy.yaml
```

## Projecting only specific config map entries

* Envoy doesn't need the `dummy.bin` file, but imagine that it's needed by another container or pod and you can't remove it from the config map

  * But having this file appear in `/etc/envoy` is not ideal, so let's do something about it

* Fortunately, configMap volumes let you specify which config map entries to project into the files

  * The following listing shows how ▶︎ Specifying which config map entries to include into a configMap volume:

```yaml
volumes:
- name: envoy-config
  configMap:
    name: kiada-envoy-config
    items:                    # ← A
    - key: envoy.yaml         # ← B
      path: envoy.yaml        # ← B

# ← A ▶︎ Only the following config map entry should be included in the volume.
# ← B ▶︎ The config map entry value stored under the key `envoy.yaml` should be included in the volume as file `envoy.yaml`
```

* The `items` field specifies the list of config map entries to include in the volume

  * Each item must specify the `key` and the file name in the `path` field

  * Entries not listed here aren't included in the volume

  * In this way, you can have a single config map for a pod w/ some entries showing up as environment variables and others as files

## Setting file permissions in a ConfigMap volume

* By default, the file permissions in a `configMap` volume are set to `rw-r--r--` or `0644` in octal form

> [!NOTE]
> 
> If you aren't familiar w/ Unix file permissions, `0644` in the octal number system is equivalent to `110`,`100`,`100` in the binary system, which maps to the permission triplet `rw-`, `r--`, `r--`. The first element refers to the file owner's permissions, the second to the owning group, and the third to all other users. The owner car read (`r`) and write (`w`) the file but can't execute it (`-` instead of `x`), while the owning group and other users can read, but not write or execute the file (`r--`).

* You can set the default permissions for the files in a `configMap` volume by setting the `defaultMode` field in the volume definition

  * In YAML, the field takes either an octal or decimal value

  * For example, to set permissions to `rwxr-----`, add `defaultMode: 0740` to the `configMap` volume definition

  * To set permissions for individual files, set the `mode` field next to the item's `key` and `path`

* When specifying file permissions in YAML manifests, make sure you never forget the leading zero, which indicates that the value is in octal form

  * If you omit the zero, the value will be treated as decimal, which may cause the file to have permissions that you didn't intend

> [!IMPORTANT]
> 
> When you use `kubectl get -o yaml` to display the YAML definition of a pod, note that the file permissions are represented as decimal values. For example, you'll regularly see the value `420`. This is the decimal equivalent of the octal value `0644`, which is the default file permissions.

* Before you move on to setting file permissions and checking them in the container, you should know that the files you find in the `configMap` volume are symbolic links (section [9.2.6]() explains why)

  * To see the permissions of the actual file, you must follow these links, b/c they themselves have no permissions and are always shown as `rwxrwxrwx`
