# Deleting objects using the “all” keyword

* You can delete everything you've created so far - including the deployment, its pods, and the service - w/ the following command:

```zsh
$ kubectl delete all --all
pod "kiada-9d785b578-cc6tk" deleted
pod "kiada-9d785b578-h4gml" deleted
service "kubernetes" deleted
service "kiada" deleted
deployment.apps "kiada" deleted
replicaset.apps "kiada-9d785b578" deleted
```

* The first `all` in the command indicates that you want to delete objects of all types

  * The `--all` option indicates that you want to delete all instances of each object type

  * You used this option in the previous section when you tried to delete all pods

* When deleting objects, `kubectl` prints the type and name of each deleted object

  * In the previous listing, you should see that it deleted the pods, the deployment, and the service, but also a so-called replica set object

* You'll notice that the delete command also deletes the built-in `kubernetes` service

  * Don't worry about this, as the service is automatically recreated after a few moments

* Certain objects aren't deleted when using this method, b/c the keyword `all` does not include object kinds

  * This is a precaution to prevent you from accidentally deleting objects that contain important information

  * The Event object kind is one example of this

> [!NOTE]
> 
> You can specify multiple object types in the `delete` command. For example, you can use `kubectl delete events, all --all` to delete events along w/ all object kinds included in `all`.
