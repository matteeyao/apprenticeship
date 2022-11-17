# Managing Rolling Updates w/ Deployments

## What is a rolling update?

Rolling updates allow you to make changes to a Deployment's Pods at a controlled rate, gradually replacing old Pods w/ new Pods. This allows you to update your Pods w/o incurring downtime.

## What is a rollback?

If an update to a deployment causes a problem, you can roll back the deployment to a previous working state.

## Hands-on documentation

```zsh
kubectl get deployments
```

Kickoff a rolling deployment by making a change to the deployment's pod specification or template.

```zsh
kubectl edit deployment <DEPLOYMENT_NAME>
```

Examine rolling update:

```zsh
kubectl rollout status deployment.v1.apps/my-deployment
```

```zsh
kubectl get deployment my-deployment
```

```zsh
kubectl set image deployment/<DEPLOYMENY_NAME> nginx=<CONTAINER_NAME>:<TAG> --record
```

```zsh
kubectl rollout status deplogment.v1.apps/<DEPLOYMENT_NAME>
```

```zsh
kubectl get pods
```

```zsh
kubectl rollout history deployment.v1.apps/<DEPLOYMENT_NAME>
```

B/c we used the `--record` flag above, the deployment history is kept.

Perform a rollback:

```zsh
kubectl rollout undo deployment.v1.apps/<DEPLOYMENT_NAME> --to-revision=<REVISION_NUM>
```

`--to-revision` flag will allow you to go back to a specific revision. If flag is left out, we will roll back to the previous revision.

```zsh
kubectl get pods
```

```zsh
kubectl rollut status deployment.v1.apps/<DEPLOYMENT_NAME>
```
