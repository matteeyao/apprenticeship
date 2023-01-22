# Using external storage in pods

* An `emptyDir` volume is a dedicated directory created specifically for and used exclusively by the pod in which the volume is defined

  * When the pod is deleted, the volume and its contents are deleted
  
  * However, other types of volumes don't create a new directory, but instead mount an existing external directory in the filesystem of the container

  * The contents of this volumes can survive multiple instantiations of the same pod and can even be shared by multiple pods

  * These are the types of volumes we'll explore next

* To learn how external storage is used in a pod, you'll create a pod that runs the document-oriented database MongoDB

  * To ensure that the data stored in the database is persisted, you'll add a volume to the pod and mount it in the container at the location where MongoDB writes its data files

* The tricky part of this exercise is that the type of persistent volumes available in your cluster depends on the environment in which the cluster is running

  * At the beginning of this book, you learned that Kubernetes could reschedule a pod to another node at any time

  * To ensure that the quiz pod can still access its data, it should use network-attached storage instead of the worker node's local drive

* Ideally, you should use a proper Kubernetes cluster, such as GKE, for the following exercises

  * Unfortunately, clusters provisioned w/ Minikube or kind don't provide any kind of network storage volume out of the box

  * So, if you're using either of these tools, you'll need to resort to using node-local storage provided by the so-called `hostPath` volume type, but this volume type is not explained until section 4
