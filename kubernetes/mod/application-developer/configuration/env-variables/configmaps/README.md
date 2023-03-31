# ConfigMaps

* From the K8s definition, "ConfigMaps allow you to decouple configuration artifacts from image content to keep containerized applications portable"

* ConfigMaps are used to pass configuration data in the form of key value pairs in Kubernetes

* When a pod is created, the ConfigMap is injected into the pod, so the key value pairs are available as environment variables for the application hosted inside the container in the pod

* `cat pod-definition.yaml`:

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: simple-webapp-color
spec:
  containers:
  - name: simple-webapp-color
    image: simple-webapp-color
    ports:
      - containerPort: 8080
    env:
      - name: APP_COLOR
        value: blue
      - name: APP_MODE
        value: prod
```

* There are two steps in configuring ConfigMaps:

  1. Create ConfigMap

  2. Inject into Pod

* To create a ConfigMap the imperative way:

```zsh
kubectl create configmap
```

* To create a ConfigMap the declarative way:

```zsh
kubectl create -f <CONFIGMAP_DEFINITION>.yaml
```

## Imperative approach

```zsh
kubectl create configmap \
  <CONFIG_NAME> --from-literal=<KEY>=<VALUE>
```

* i.e.

```zsh
kubectl create configmap \
  app-config --from-literal=APP_COLOR=blue
             --from-literal=APP_MODE=prod
```

* Another way to input the configuration data is through a file

  * Use the `--from-file` option to specify a path to the file that contains the required data

  * The data from this file is read and stored under the name of the file

```zsh
kubectl create configmap \
  <CONFIG_NAME> --from-file=<PATH_TO_FILE>
```

```zsh
kubectl create configmap \
  app-config --from-file=app_config.properties
```

## Declarative approach

* `cat config-map.yaml`:

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: app-config
data:
  APP_COLOR: blue
  APP_MODE: prod
```

```zsh
kubectl create -f config-map.yaml 
```

## ConfigMap in pods

* `cat config-map.yaml`:

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: app-config
data:
  APP_COLOR: blue
  APP_MODE: prod
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
        - configMapRef:
            name: app-config
```

## Learning summary

1. Update the environment variable on the POD to display a `green` background

> [!NOTE]
> 
> Delete and recreate the POD. Only make the necessary changes. Do not modify the name of the Pod.

```zsh
kubectl get pods

kubectl get pod web-app-color -o yaml > pod.yaml

kubectl delete pod webapp-color

vi pod.yaml

kubectl apply -f pod.yaml
```

2. Create a new ConfigMap for the `webapp-color` POD. Use the spec given below:

   * ConfigMap Name: webapp-config-map

   * Data: APP_COLOR=darkblue

```zsh
kubectl create cm webapp-config-map --from-literal=APP_COLOR=darkblue
```

* Update the environment variable on the POD to use the newly created ConfigMap

> [!NOTE]
> 
> Delete and recreate the POD. Only make the necessary changes. Do not modify the name of the Pod.

```zsh
kubectl delete pod webapp-color

vi pod.yaml
```

```yaml
spec:
  containers:
  - env:
    - name: APP_COLOR
      valueFrom:
        configMapKeyRef:
          name: webapp-config-map
          key: APP_COLOR
```

```zsh
kubectl apply -f pod.yaml
```
