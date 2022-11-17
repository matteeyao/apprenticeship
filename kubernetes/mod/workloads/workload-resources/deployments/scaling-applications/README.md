# Scaling Applications with Deployments

## What is scaling?

**Scaling** refers to dedicating more (or fewer) resources to an application in order to meet changing needs.

K8s Deployments are very useful in **horizontal scaling**, which involves changing the number of containers running an application.

## Deployments scaling

The Deployment's **replicas** setting determines how many replicas are desired in its desired state. If the **replicas** number is changed, replica Pods will be created or deleted to satisfy the new number.

## How to scale a deployment

1. **Edit YAML** ▶︎ You can scale a deployment simply by changing the number of **replicas** in the YAML descriptor w/ `kubectl apply` or `kubectl edit`

```yaml
...
spec:
  replicas: 5
...
```

2. `kubectl scale` ▶︎ Or use the `kubectl scale` command:

```yaml
kubectl scale deployment.v1.apps/my-deployment --replicas=5
```

## Hands-on demonstration

Log in to the control plane node/server.

From the previous deployment:

```zsh
kubectl get deployments
```

Change the number of replicas under `spec`:

```zsh
kubectl edit deployment my-deployment
```

...or...

```zsh
kubectl scale deployment.v1.apps/my-deployment --replicas=3
```
