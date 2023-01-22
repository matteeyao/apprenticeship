# 3.2.3 Using kubectl

## A Note on `--all-namespaces`

Appending `--all-namespaces` happens frequently enough where you should be aware of the shorthand for `--all-namespaces`:

```zsh
kubectl -A
```

## Verifying if the cluster is up and kubectl can talk to it

* To verify that your cluster is working, use the `kubectl cluster-info` command:

```zsh
$ kubectl cluster-info
Kubernetes master is running at https://192.168.99.101:8443
KubeDNS is running at https://192.168.99.101:8443/api/v1/namespaces/...
```

* This indicates that the API server is active and responding to requests

  * The output lists the URLs of the various Kubernetes cluster services running in your cluster

  * The above example shows that besides the API server, the KubeDNS service, which provides domain-name services within the cluster, is another service that runs in the cluster

## List cluster nodes

* Now use the `kubectl get nodes` command to list all nodes in your cluster

  * Here’s the output that is generated when you run the command in a cluster provisioned by kind

```zsh
$ kubectl get nodes
NAME          STATUS    ROLES   AGE   VERSION
control-plane Ready     <none>  12m   v1.18.2
kind-worker   Ready     <none>  12m   v1.18.2
kind-worker2  Ready     <none>  12m   v1.18.2
```

* Everything in Kubernetes is represented by an object and can be retrieved and manipulated via the RESTful API

  * The `kubectl get` command retrieves a list of objects of the specified type from the API

  * You’ll use this command all the time, but it only displays summary information about the listed objects

## Retrieve additional details of an object

* To see more detailed information about an object, you use the `kubectl describe` command, which shows much more:

```zsh
$ kubectl describe node gke-kiada-85f6-node-0rrx
```

* If you run the command yourself, you’ll see that it displays the status of the node, information about its CPU and memory usage, system information, containers running on the node, and much more

  *  If you run the `kubectl describe` command without specifying the resource name, information of all nodes will be printed

> [!TIP]
> 
> Executing the `describe` command without specifying the object name is useful when only one object of a certain type exists. You don’t have to type or copy/paste the object name.

