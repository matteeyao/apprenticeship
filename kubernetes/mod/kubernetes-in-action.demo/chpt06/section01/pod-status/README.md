# Understanding the pod's status

* After you create a pod object and it runs, you can see what's going on w/ the pod by reading the pod object back from the API

  * Recall from chapter 4, the pod object manifest, as well as the manifests of most other kinds of objects, contain a section, which provides the status of the object

  * A pod's `status` section contains the following information:

    * the IP addresses of the pod and the worker node that hosts it

    * when the pod was started

    * the pod's quality-of-service (QoS) class

    * what phase the pod is in

    * the conditions of the pod, and

    * the state of its individual containers

* The phase and conditions of the pod, as well as the states of its containers are important un understanding the pod lifecycle
