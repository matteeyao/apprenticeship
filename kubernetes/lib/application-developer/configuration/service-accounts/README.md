# Service Account

1. You shouldn't have to copy and paste the token each time. The Dashboard application is programmed to read token from the secret mount location. However, currently, the `default` service account is mounted. Update the deployment to use the newly created `serviceAccount`. Edit the deployment to change `serviceAccount` from `default` to `dashboard-sa`

```zsh
kubectl get deployment -o yaml > dashboard.yaml
```

```zsh
kubectl edit deployment web-dashboard
```

```yaml
  ...
  spec:
    serviceAccountName: dashboard-sa
    container:
  ...
```
