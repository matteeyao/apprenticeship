# List deployments

* The interaction w/ K8s consists mainly of the creation and manipulation of objects via its API

    * K8s stores these objects and then performs operations to bring them to life

    * For example, when you create a Deployment object, K8s runs an application
    
    * K8s then keeps you informed about the current state of the application by writing the status to the same Deployment object

    * You can view the status by reading back the object

    * One way to do this is to list all Deployment objects as follows:

```zsh
$ kubectl get deployments
NAME  READY   UP-TO-DATE    AVAILABLE   AGE
kiada 0/1     1             0           6s
```

* The `kubectl get deployments` command lists all Deployment objects that currently exit in the cluster

  * You have only one Deployment in your cluster

  * It runs one instance of your application as shown in the `UP-TO-DATE` column, but the `AVAILABLE` column indicates that the application is not yet available

  * That's b/c the container isn't ready, as shown in the `READY` column

  * You can see that zero of a total of one container are ready

* You may wonder if you can ask K8s to list all the running containers by running `kubectl get containers`. Let's try this:

```zsh
$ kubectl get containers
error: the server doesn't have a resource type "containers"
```

* The command fails b/c K8s doesn't have a "Container" object type

    * This may seem odd, since K8s is all about running containers, but there's a twist: A container is not the smallest unit of deployment in Kubernetes. So, what is?
