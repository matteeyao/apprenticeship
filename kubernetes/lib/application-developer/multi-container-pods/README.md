# Multi-Container Pods

1. The application outputs logs to the file `/log/app.log`. View the logs and try to identify the user having issues w/ Login. Inspect the log file inside the pod.

```zsh
kubectl exec -it app -- cat /log/app.log
```

2. The application outputs logs to the file `/log/app.log`. View the logs and try to identify the user having issues / Login. Inspect the log file inside the pod.

```zsh
kubectl edit pod app -n elastic-stack
```

```yaml
apiVersion: v1
kind: Pod
metadata:
...
spec:
  containers:
    - image: kodekloud/filebeat-configured
      name: sidecar
      volumeMounts:
        - mountPath: /var/log/event-simulator/
          name: log-volume
    - image: kodekloud/event-simulator
      imagePullPolicy: Always
      name: app
      resources: {}
      terminationMessagePath: /dev/termination-log
      terminationMessagePolicy: File
      volumeMounts:
      - mountPath: /log
        name: log-volume
      - mountPath: /var/run/secrets/kubernetes.io/serviceaccount
        name: kube-api-access-wnzb6
        readOnly: true
  ...
```
