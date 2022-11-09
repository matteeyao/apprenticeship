# Deleting resources

* Delete a pod using the type and name specified in `pod.json`:

```zsh
kubectl delete -f ./pod.json
```

* Delete a pod with no grace period:

```zsh
kubectl delete pod unwanted --now
```

* Delete pods and services with same names `"baz"` and `"foo"`:

```zsh
kubectl delete pod,service baz foo
```

* Delete pods and services with label `name=myLabel`:

```zsh
kubectl delete pods,services -l name=myLabel
```

* Delete all pods and services in namespace `my-ns`:

```zsh
kubectl -n my-ns delete pod,svc --all  
```

* Delete all pods matching the awk `pattern1` or `pattern2`:

```zsh
kubectl get pods  -n mynamespace --no-headers=true | awk '/pattern1|pattern2/{print $1}' | xargs  kubectl delete -n mynamespace pod
```
