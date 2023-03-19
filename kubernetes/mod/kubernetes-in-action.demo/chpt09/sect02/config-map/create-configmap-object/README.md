# Creating a ConfigMap object

* Let's create a config map and use it in a pod

  * The following is a simple example where the config map contains a single entry used to initialize the environment variable `INITIAL_STATUS_MESSAGE` for the kiada pod

## Creating a config map w/ the `kubectl create configmap` command

* As w/ pods, you can create the ConfigMap object from a YAML manifest, but a faster way is to use the `kubectl create cconfigmap` command as follows:

```zsh
$ kubectl create configmap kiada-config --from-literal status-message="This status message is set in the kiada-config config map"
configmap "kiada-config" created
```

> [!NOTE
> 
> Keys in a config map may only consist of alphanumeric characters, dashes, underscores, or dots. Other characters are not allowed.

* Running this command creates a config map called `kiada-config` w/ a single entry

  * The key and value are specified w/ the `--from-literal` argument

* In addition to `--from-literal`, the `kubectl create configmap` command also supports sourcing the key/value pairs from files

  * The following table explains the available methods ▶︎ Options for creating config map entries using `kubectl create configmap`:

| **Option**        | **Description**                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
|-------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `--from-literal`  | Inserts a key and a literal value into the config map. Example: `--from-literal mykey=myvalue`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| `--from-file`     | Inserts the contents of a file into the config map. The behavior depends on the argument that comes after `--from-file`:<br> If only the filename is specified (example: `--from-file myfile.txt`), the base of the name of the file is used as the key and the entire contents of the file are used as the value.<br> If `key-file` is specified (example: `--from-file mykey=myfile.txt`), the contents of the file are stored under the specified key.<br> If the filename represents a directory, each file contained in the directory is included as a separate entry. The base name of the file is used as the key, and the contents of the file are used as the value. Subdirectories, symbolic links, devices, pipes, and files whose base name isn't a valid config map key are ignored. |
| `--from-env-file` | Inserts each line of the specified file as a separate entry (example: `--from-env file myfile.env`). The file must contain lines w/ the following format: `key=value`                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |

* Config maps usually contain more than one entry

  * To create a config map w/ multiple entries, you an use multiple arguments `--from-literal`, `--from-file`, and `--from-env-file`, or a combination thereof

## Creating a config map from a YAML manifest

* Alternatively, you can create the config map from a YAML manifest file

  * The following listing shows the contents of an equivalent file named [`cm.kiada-config.ymal`](cm.kiada-config.yaml)

    * You can creat the config map by applying this file using `kubectl apply`

  * A config map manifest file:

```yaml
apiVersion: v1                                                                # ← A
kind: ConfigMap                                                               # ← A
metadata:
  name: kiada-config                                                          # ← B
data:                                                                         # ← C
  status-message: This status message is set in the kiada-config config map   # ← C

# ← A ▶︎ This manifest defines a ConfigMap object.
# ← B ▶︎ The name of this config map
# ← C ▶︎ Key/value pairs are specified in the data field
```

## Listing config maps and displaying their contents

* Config maps are Kubernetes API objects that live alongside pods, nodes, persistent volumes, and others you've learned so far

  * You can use various kubectl commands to perform CRUD operations on them

  * For example, you can list config maps with:

```zsh
$ kubectl get cm
```

> [!NOTE]
> 
> The shorthand for `configmaps` is `cm`.

* You can display the entries in the config map by instructing kubectl to print its YAML manifest:

```zsh
$ kubectl get cm kiada-config -o yaml
```

> [!NOTE]
> 
> B/c YAML fields are output in alphabetical order, you'll find the `data` field at the top of the output.

> [!TIP]
> 
> To display only the key/value pairs, combine `kubectl` w/ `jq`. For example `kubectl get cm kiada-config -o json | jq .data`. Display the value of a given entry as follows: `kubectl... | jq '.data["status-message"]'`
