# K8s Secrets

* Recall that the configMap stores configuration data in plain text, so while it would be okay to move the hostname and username into a configMap, it is definitely not the right place to store a password

* Secrets are stored in an encoded or hashed format

* As w/ configMaps, there are two steps involved in working w/ Secrets:

  * First, create the secret

  * Second, inject it into Pod

* Imperative approach:

```zsh
kubectl create secret generic
```

* Declarative approach:

```zsh
kubectl create -f <SECRET_DEFINITION>.yaml
```

## Imperative approach

```zsh
kubectl create secret generic \
  <SECRET_NAME> --from-literal=<KEY>=<VALUE>
```

```zsh
kubectl create secret generic \
  app-secret --from-literal=DB_HOST=mysql
             --from-literal=DB_User=root
             --from-literal=DB_Password=passwrd
```

* ...or...

```zsh
kubectl create secret generic
  <SECRET_NAME> --from-file=<PATH_TO_FILE>
```

```zsh
kubectl create secret generic
  app-secret --from-file=app_secret.properties
```

## Declarative approach

* `cat secret-data.yaml`

```yaml
apiVersion: v1
kind: Secret
metadata:
  name: app-secret
data:
  DB_Host: mysql
  DB_User: root
  DB_Password: passwrd
```

```zsh
kubectl create -f secret-data.yaml
```

## Secrets in Pods

* `cat secret-data.yaml`:

```yaml
apiVersion: v1
kind: Secret
metadata:
  name: app-secret
data:
  DB_Host: bXlzcWw=
  DB_User: cm9vdA==
  DB_Password: cGFzd3Jk
```

* `cat pod-definition.yaml`:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: simple-webapp-color
  labels:
    name: simple-webapp-color
spec:
  containers:
  - name: simple-webapp-color
    image: simple-webapp-color
    ports:
      - containerPort: 8080
    envFrom:
      - secretRef:
            name:
```

```zsh
kubectl create -f pod-definition.yaml
```

* Env:

```yaml
envFrom:
  - secretRef:
        name: app-config
```

* Single Env:

```yaml
env:
  - name: DB_Password
    valueFrom:
      secretKeyRef:
        name: app-secret
        key: DB_Password
```

* Volume:

```yaml
volumes:
- name: app-secret-volume
  secret:
    secretName: app-secret
```

## Safety of the Secrets

* Remember that secrets encode data in base64 format

  * Anyone w/ the base64 encoded secret can easily decode it

  * As such the secrets can be considered as not very safe

* Secrets are not encrypted, so it is not safer in that sense

  * However, some best practices around using secrets make it safer

  * As in best practices like:

    * Not checking-in secret object definition files to source code repositories

    * Enabling Encryption at Rest for Secrets so they are stored encrypted in ETCD

* Also the way K8s handles secrets, such as:

  * A secret is only sent to a node if a pod on that node requires it

  * Kubelet stores the secret into a tmpfs so that the secret is not written to disk storage

  * Once the Pod that depends on the secret is deleted, Kubelet will delete its local copy of the secret data as well

## Learning summary

* Secrets are not Encrypted. Only encoded.

  * Do not check in Secret objects to SCM along w/ code.

* Secrets are not encrypted in ETCD

* Anyone able to create pods/deployments in the same namespace can accesss the secrets

  * Configure least-privilege access to Secrets - RBAC

* Consider third-party secrets store providers

  * AWS Provider, Azure Provider, GCP Provider, Vault Provider

1. Configure `webapp-pod` to load environment variables from the newly created secret. Delete and recreate the pod if required.

  * Pod name: webapp-pod

  * Image name: kodekloud/simple-webapp-mysql

  * Env From: Secret=db-secret

```zsh
kubectl edit pod webapp-pod
```

```yaml
    envFrom:
      - secretRef:
          name: db-secret
```

```zsh
kubectl replace --force -f /tmp/kubectl-edit-123695794.yaml
```
