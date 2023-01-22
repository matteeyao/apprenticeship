# Deleting all pods

* You've now removed all pods except `kiada-stdin` and the pods you created in chapter 3 using the `kubectl create deployment` command

  * Depending on how you've scaled the deployment, some of these pods should still be running:

```zsh
$ kubectl get pods
NAME                  READY   STATUS    RESTARTS  AGE
kiada-stdin           1/1     Running   0         10m
kiada-9d785b578-58vhc 1/1     Running   0         1d
kiada-9d785b578-jmnj8 1/1     Running   0         1d
```

* Instead of deleting these pods by name, we can delete them all using the `--all` option:

```zsh
$ kubectl delete po --all
pod "kiada-stdin" deleted
pod "kiada-9d785b578-58vhc" deleted
pod "kiada-9d785b578-jmnj8" deleted
```

* Now confirm that no pods exist by executing the `kubectl get pods` command again:

```zsh
$ kubectl get po
NAME                    READY   STATUS    RESTARTS    AGE
kiada-9d785b578-cc6tk   1/1     Running   0           13s
kiada-9d785b578-h4gml   1/1     Running   0           13s
```

* Two pods are still running, which is expected

  * If you look closely at their names, you'll see that these aren't the two you've just deleted

  * The `AGE` column also indicates that these are _new_ pods

  * You can try to delete them as well, but you'll see that no matter how often you delete them, new pods are created to replace them

* The reason why these pods keep popping up is b/c of the Deployment object

  * The controller responsible for bringing Deployment objects to life must ensure that the number of pods always matches the desired number of replicas specified in the object

  * When you delete a pod associated w/ the Deployment, the controller immediately creates a replacement pod

* To delete these pods, you must either scale the Deployment to zero or delete the object altogether

  * This would indicate that you no longer want this deployment or its pods to exist in your cluster
