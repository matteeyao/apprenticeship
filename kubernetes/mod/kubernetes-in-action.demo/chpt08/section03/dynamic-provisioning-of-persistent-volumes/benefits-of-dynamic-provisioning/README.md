# Understanding the benefits of dynamic provisioning

* This section on dynamic provisioning should convince you that automating the provisioning of persistent volumes benefits both the cluster administrator and anyone who uses the cluster to deploy applications

  * By setting up the dynamic volume provisioner and configuring several storage classes w/ different performance or other features, the administrator gives cluster users the ability to provision as many persistent volumes of any type as they want

  * Each developer decides which storage class is best suited for each claim they create

## Understanding how storage classes allow claims to be portable

* Another great thing about storage classes is that claims refer to them by name

  * If the storage classes are named appropriately, such as `standard`, `fast`, and so on, the persistent volume claim manifests are portable across different clusters

> [!NOTE]
> 
> Remember that persistent volume claims are usually part of the application manifest and are written by application developers.

* If you used GKE to run the previous examples, you can now try to deploy the same claim and pod manifests in a non-GKE cluster, such as a cluster created w/ Minikube or kind

  * In this way, you can see this portability for yourself

  * The only thing you need to ensure is that all your clusters use the storage class names
