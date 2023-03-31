# ENV Variables in Kubernetes

```zsh
docker run -e APP_COLOR=pink simple-webapp-color
```

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
        value: pink
```

## ConfigMap

```yaml
    env:
      - name: APP_COLOR
        valueFrom:
          configMapKeyRef:
```

## Secrets

```yaml
    env:
      - name: APP_COLOR
        valueFrom:
          secretKeyRef:
```
