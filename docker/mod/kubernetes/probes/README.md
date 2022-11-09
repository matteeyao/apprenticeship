# Probes

Probes allow you to customize how the Kubernetes cluster determines the state of each container.

**Liveness Probe** checks whether the container is healthy.

So Kubernetes is constantly monitoring your containers to determine if they're healthy and automatically restarts them if they become unhealthy.

**Readiness Probes** check whether the container is ready to service user requests.

```yml
apiVersion: v1
kind: Pod
metadata:
  name: probe-pod
spec:
  containers:
    - name: nginx
      image: nginx:1.19.1
      ports:
      - containerPort: 80
      livenessProbe:
        httpGet:
          path: /
          port: 80
        initialDelaySeconds: 3
        periodSeconds: 3
      readinessProbe:
        httpGet:
          path: /
          port: 80
        initialDelaySeconds: 3
        periodSeconds: 3
```
