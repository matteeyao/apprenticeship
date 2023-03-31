# Namespaces

* kube-system

* Default

* kube-public

* Resources within a namespace can refer to each other simply by their names

  * If `web-app` pod and `db-service` exist in the same namespace, `web-app` can reach `db-service` simply by using the host name `db-service`

* `web-app` pod can also reach to services in other namespaces as well

  * To do so, append the name of the namespace to the name of the `service`

  * For `web-pod` in the default namespace to connect to the `db-service` in the `dev` namespace, use `<SERVICE_NAME>.<NAMESPACE>.svc.cluster.local` or `db-service.dev.svc.cluster.local`

  * When the service is created, a DNS entry is added automatically in this format

    * In `<SERVICE_NAME>.<NAMESPACE>.svc.cluster.local`, `cluster.local` is the default domain name of the K8s cluster

    * `SVC` is the subdomain for service, followed by the namespace, and then the name of the service itself

## Commands

```zsh
kubectl create -f pod-definition.yml --namespace dev
```

* ...or...

```zsh
kubectl create -f pod-definition.yml
```

* ... and defining the namespace in your metadata section 

```yaml
apiVersion: v1
kind: Pod

metadata:
  name: myapp-pod
  namespace: dev
  labels:
    app: myapp
    type: front-end
spec:
  containers:
  - name: nginx-container
    image: nginx
```

* Create a namespace by...

  1. using a namespace definition file:

```yaml
apiVersion: v1
kind: Namespace
metadata:
  name: dev
```

  2. Running the command:

```zsh
kubectl create namespace <NAMESPACE_NAME>
```

* Set namespace to current context:

```zsh
kubectl config set-context $(kubectl config current-context) --namespace=<NAMESPACE_NAME>
```

* View pods in all namespaces:

```zsh
kubectl get pods -A
```

## Resource Quota

* `Compute-quota.yaml`:

```yaml
apiVersion: v1
kind: ResourceQuota
metadata:
  name: compute-quota
  namespace: dev
spec:
  hard:
    pods: "10"
    requests.cpu: "4"
    requests.memory: 5Gi
    limits.cpu: "10"
    limits.memory: 10Gi
```
