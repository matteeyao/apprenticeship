# Creating a secret

* In section 9.2, you used a config map to inject the configuration file into the Envoy sidecar container

  * In addition to the file, Envoy also requires a TLS certificate and private key

  * B/c the key represents sensitive data, it should be stored in a secret

* In this section, you'll create a secret to store the certificate and key, and project it into the container's filesystem

  * W/ the config, certificate and key files all sourced from outside the container image, you can replace the custom `kiada-ssl-proxy` image w/ the generic `envoyproxy/envoy` image

  * This is a considerable improvement, as removing custom images from the system is always a good thing, since you no longer need to maintain them

* First, you'll create the secret

## Creating a TLS secret

* Like for config maps, `kubectl` also provides a command for creating different types of secrets

  * Since you are creating a secret that will be used by your own application rather than K8s, it doesn't matter whether the secret you create is of type `Opaque` or `kubernetes.io/tls`

  * However, since you are creating a secret w/ a TLS certificate and a private key, you should use the build-in secret type `kubernetes.io/tls` to standardize things

* To create the secret, run the following command:

```zsh
$ kubectl create secret tls kiada-tls \   # ← A
  --cert example-com.crt \                # ← B
  --key example-com.key                   # ← C
  
# ← A ▶︎ Creating a TLS secret called kiada-tls
# ← B ▶︎ The path to the certificate file
# ← C ▶︎ The path to the private key
```

* The command instructs `kubectl` to create a `tls` secret named `kiada-tls`

  * The certificate and private keys are read from the file `example-com.crt` and `example.com-key`, respectively

### Creating a generic (`Opaque`) secret

* Alternatively, you could use kubectl to create a generic secret

  * The items in the resulting secret would be the same, the only difference would be its type

  * Here's the command to create the secret:

```zsh
$ kubectl create secret generic kiada-tls \   # ← A
    --from-file tls.crt=example-com.crt \     # ← B
    --from-file tls.key=example-com.key       # ← C
    
# ← A ▶︎ Creating a generic secret called kiada-tls
# ← B ▶︎ The contents of the example-com.crt file should be stored under the key tls.crt
# ← C ▶︎ The contents of the example-com.key file should be stored under the key tls.key
```

* In this case, `kubectl` creates a generic secret

  * The contents of the `example-com.crt` file are stored under the `tls.crt` key, while the contents of the `examplr-com.key` file are stored under `tls.key`

> [!NOTE]
> 
> Like config maps, the maximum size of a secret is approximately 1MB.

## Creating secrets from YAML manifests

* The kubectl create secret command creates the secret directly in the cluster

  * Previously, you learned how to create a YAML manifest for a config map

  * What about secrets?

* For obvious reasons, it's not the best idea to create YAML manifests for your secrets and store them in your version control system, as you do w/ config maps

  * However, if you need to create a YAML manifest instead of creating the secret directly, you can again use the `kubectl create --dry-run=client -o yaml` trick

* Suppose you want to create a secret YAML manifest containing user credentials under the keys `user` and `pass`

  * You can use the following command to create the YAML manifest:

```zsh
$ kubectl create secret generic my-credentials \  # ← A
    --from-literal user=my-username \             # ← B
    --from-literal pass=my-password \             # ← B
    --dry-run=client -o yaml                      # ← C
apiVersion: v1
data:
  pass: bXktcGFzc3dvcmQ=                          # ← D
  user: bXktdXNlcm5hbWU=                          # ← D
kind: Secret
metadata:
  creationTimestamp: null
  name: my-credentials
  
# ← A ▶︎ Create a generic secret
# ← B ▶︎ Store the credenitals in keys `user` and `pass`
# ← C ▶︎ Print the YAML manifest instead of posting the secret to the API server
# ← D ▶︎ Base64-encoded credentials
```

* Creating the manifest using the `kubectl create` trick as shown here is much easier than creating it from scratch and manually entering the Base64-encoded credentials

  * Alternatively, you could avoid encoding the entries by using the `stringData` field as explained next

## Using the StringData field

* Since not all sensitive data is in binary form, K8s also allows you to specify plain text values in secrets by using `stringData` instead of the `data` field

  * The following listing shows how you'd create the same secret that you created in the previous example ▶︎ Adding plain text entries to a secret using the `stringData` field:

```yaml
apiVersion: v1
kind: Secret
stringData:         # ← A
  user: my-username # ← B
  pass: my-password # ← B

# ← A ▶︎ The stringData is used to enter plain-text values w/o encoding them
# ← B ▶︎ These credentials aren't encoded using Base64 encoding
```

* The `stringData` field is write-only and can only be used to set values

  * If you create this secret and read it back w/ `kubectl get -o yaml`, the `stringData` field is no longer present

  * Instead, any entries you specified in it will be displayed in the `data` field as Base64-encoded values

> [!TIP]
> 
> Since entries in a secret are always represented as Base64-encoded values, working w/ secrets (especially reading them) is not as human-friendly as working w/ config maps, so use config maps wherever possible. But never sacrifice security for the sake of comfort.

* Let's return to the TLS secret you created earlier

  * Let's use it in a pod
