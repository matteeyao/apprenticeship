# Using secrets to pass sensitive data to containers

* In the previous section, you learned how to store configuration data in ConfigMap objects and make it available to the application via environment variables or files

  * You may think that you can also use config maps to also store sensitive data such as credentials and encryption keys, but this isn't the best option

  * For any data that needs to be kept secure, Kubernetes provides another type of object-`Secrets`

## Introducing secrets

* Secrets are remarkably similar to config maps

  * Just like config maps, they contain key-value pairs and can be used to inject environment variables and files into containers

  * So why do we need secrets at all?

* K8s supported secrets even before config maps were added

  * Originally, secrets were not user-friendly when it came to storing plain-text data

  * For this reason, config maps were then introduced

  * Over time, both the secrets and config maps evolved to support both types of values

  * The functions provided by these two types of object converged
  
  * If they were added now, they would certainly be introduced as a single object type

  * However, b/c they each evolved gradually, there are some differences between them

### Differences in fields between Config Maps and Secrets

* The structure of a secret is slightly different from that of a config map

  * The following table shows the fields in each of the two object types ▶︎ Differences in the structure of secrets and config maps:

| **Secret**   | **ConfigMap** | **Description**                                                                                                        |
|--------------|---------------|------------------------------------------------------------------------------------------------------------------------|
| `data`       | `binaryData`  | A map of key-value pairs. The values are Base64-encoded strings.                                                       |
| `stringData` | `data`        | A map of key-value pairs. The values are plain text strings. The stringData field in secrets is write-only.            |
| `immutable`  | `immutable`   | A boolean value indicating whether the data stored in the object can be updated or not.                                |
| `type`       | `N/A`         | A string indicating the type of secret. Can be any string value, but several built-in types have special requirements. |

* As you can see in the table, the `data` field in secrets corresponds to the `binaryData` field in config maps

  * It can contain binary values as Base64-encoded strings

  * The `stringData` field in secrets is equivalent to the `data` field in config maps and is used to store plain text values

    * The `stringData` field in secrets is write-only

    * You can use it to add plaintext values to the secret w/o having to encode them manually

    * When you reade back the Secret object, any values you added to `stringData` will be included in the `data` field as Base64-encoded strings

* This is different from the behavior of the `data` and `binaryData` fields in config maps

  * Whatever key-value pair you add to one of these fields will remain in that field when you read the ConfigMap object back from the API

* Like config maps, secrets can be marked immutable by setting the `immutable` field to `true`

* Secrets have a field that config maps do not

  * The `type` field specified the type of the secret and is mainly used for programmatic handling of the secret

  * You can set the type to any value you want, but there are several built-in types w/ specific semantics

### Understanding built-in secret types

* When you create a secret and set its type to one of the built-in types, it must meet the requirements defined for that type, b/c they are used by various Kubernetes components that expect them to contain values in specific formats under specific keys

  * The following table explains the built-in secret types that exist at the time of writing this ▶︎ Types of secrets:

| **Built-in secret type**              | **Description**                                                                                                                                                                                                                                                                           |
|---------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `Opaque`                              | This type of secret can contain secret data stored under arbitrary keys. If you create a secret w/ no `type` field, an Opaque secret is created.                                                                                                                                          |
| `bootstrap.kubernetes.io/token`       | This type of secret is used for tokens that are used when bootstrapping new cluster nodes.                                                                                                                                                                                                |
| `kubernetes.io/basic-auth`            | This type of secret stores the credentials required for basic authentication. It must contain the `username` and `password` keys.                                                                                                                                                         |
| `kubernetes.io/dockercfg`             | This type of secret stores the credentials required for accessing a Docker image registry. It must contain a key called `.dockercfg`, where the value is the contents of the `~/.dockercfg` configuration file used by legacy versions of Docker.                                         |
| `kubernetes.io/dockerconfigjson`      | Like above, this type of secret stores the credentials for accessing a Docker registry, but uses the newer Docker configuration file format. The secret must contain a key called `.dockerconfigjson`. The value must be the contents of the `~/.docker/config.json` file used by Docker. |
| `kubernetes.io/service-account-token` | This type of secret stores a token that identifies a Kubernetes service account. You'll learn about service accounts and this token in chapter 23.                                                                                                                                        |
| `kubernetes.io/ssh-auth`              | This type of secret stores the private key used for SSH authentication. The private key must be stored under the key `ssh-privatekey` in the secret.                                                                                                                                      |
| `kubernetes.io/tls`                   | This type of secrets stores a TLS certificate and the associated private key. They must be stored in the secret under the key `tls.crt` and `tls.key`, respectively.                                                                                                                      |

### Understanding how K8s stores secrets and config maps

* In addition to the small differences in the names of the fields supported by config maps or secrets, Kubernetes treats them differently

  * When it comes to secrets, you need to remember that they are handled in specific ways in all Kubernetes components to ensure their security

  * For example, K8s ensures that the data in a secret is distributed only to the node that runs the pod that needs the secret

  * Also, secrets on the worker nodes themselves are always stored in memory and never written to physical storage

  * This makes it less likely that sensitive data will leak out

* For this reason, it's important that you store sensitive data only in secrets and not config maps

## Creating a secret

▶︎ See [9.3.2](create-secret/README.md)

## Using secrets in containers

▶︎ See [9.3.3](use-secrets-in-containers/README.md)
