# Encrypting Secret Data at Rest

* Let's create a Secret object:

```zsh
kubectl create secret generic my-secret --from-literal=key1=supersecret

kubectl get secret

kubectl get secret  my-secret -o yaml
```

* Decode the secret:

```zsh
ehco "<SECRET>" | base64 --decrypt
```

* Install etcd client utility:

```zsh
apt-get install etcd-client
```

```zsh
ETCDCTL_API=3 etcdctl \
   --cacert=/etc/kubernetes/pki/etcd/ca.crt   \
   --cert=/etc/kubernetes/pki/etcd/server.crt \
   --key=/etc/kubernetes/pki/etcd/server.key  \
   get /registry/secrets/default/my-secret | hexdump -C
```

* Check to see if encryption at rest is already enabled:

```zsh
ps -aux | grep kube-api | grep "encryption-provider-config"
```

* Generate a 32-byte random key and base64 encode it. If you're on Linux or macOS, run the following command:

```zsh
head -c 32 /dev/urandom | base64
```

* Create a configuration file and pass that file as an option through `--encryption-provider-config`

  * `vi enc.yaml`:

```yaml
apiVersion: apiserver.config.k8s.io/v1
kind: EncryptionConfiguration
resources:
  - resources:
      - secrets
    providers:
      - aescbc:
          keys:
            - name: key1
              secret: <BASE_64_ENCODED_SECRET>
      - identity: {}
```

* Move `enc.yaml`:

```zsh
mkdir /etc/kubernetes/enc

mv enc.yaml /etc/kubernetes/enc/

ls /etc/kubernetes/enc
```

* Edit kube-apiserver manifest:

```zsh
vi /etc/kubernetes/manifests/kube-apiserver.yaml
```

* Add `--encryption-provider-config=/etc/kubernetes/enc/enc.yaml` to 

* Add `volumeMount`:

```yaml
  volumeMounts:
  - mountPath: /etc/kubernetes/enc
  - name: enc
  - readonly: true
...
volumes:
- name: env
  hostPath:
    path: /etc/kubernetes/enc
    type: DirectoryOrCreate
```

* View status of the Kube API server (since we're using ContainerD):

```zsh
crictl pods
```

* Verify that it has the encryption provider configured:

```zsh
ps aux | grep kube-api | grep encry
```

* Create secret:

```zsh
kubectl create secret generic my-secret-2 --from-literal=key2=topsecret
```

```zsh
ETCDCTL_API=3 etcdctl \
   --cacert=/etc/kubernetes/pki/etcd/ca.crt   \
   --cert=/etc/kubernetes/pki/etcd/server.crt \
   --key=/etc/kubernetes/pki/etcd/server.key  \
   get /registry/secrets/default/my-secret-2 | hexdump -C
```

* Ensure all secrets are encrypted:

```zsh
kubectl get secrets --all-namespaces -o json | kubectl replace -f -
```
