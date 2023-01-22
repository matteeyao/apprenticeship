# 5.2 Creating pods from YAML or JSON files

* In chapter 3, you created pods using the imperative command `kubectl create`, but pods and other K8s objects are usually created by creating a JSON or YAML manifest file and posting it to the K8s API

* By using YAML files to define the structure of your application, you don't need shell scripts to make the proess of deploying your applications repeatable, and you can keep a history of all changes by storing these files in a version control system

## 5.2.1 Creating a YAML manifest for a pod

`pod.kiada.yaml` ▶︎ a basic pod manifest file:

```yaml
apiVersion: v1                  # ← This manifest uses the v1 API version to define the object
kind: Pod                       # ← The object specified in this manifest is a pod
metadata:
  name: kiada                   # ← The name of the pod
spec:
  containers:
    - name: kiada               # ← The name of the container
      image: luksa/kiada:0.1    # ← The name of the container
      ports:
      - containerPort: 8080     # ← The port the app is listening on
```

* The manifest is short only b/c it does not yet contain all the fields that a pod object gets after it is created through the API

  * For example, you'll notice that the `metadata` section contains only a single field and that the `status` section is completely missing

  * Once you create the object from this manifest, this will no longer be the case

* Before you create the object, let's examine the manifest in detail

  * It uses version `v1` of the K8s API to describe the object

  * The object kind id `Pod` and the name of the object is `kiada`

  * The pod consists of a single container also called `kiada`, based on the `luksa/kiada:0.1` image

  * The pod definition also specifies that the application in the container listens on port `8080`

> [!TIP]
> 
> Whenever you want to create a pod manifest from scratch, you can also use the following command to create the file and then edit it to add more fields: `kubectl run kiada --image=luksa/kiada:0.1 --dry-run=client -o yaml > mypod.yaml`. The `--dry-run=client` flag tells kubectl to output the definition instead of actually creating the object via the API.

* The fields in the YAML file are self-explanatory, but if you want more information about each field or want to know what additional fields you can add, remember to use the `kubectl explain pods` command

## 5.2.2 Creating the Pod object from the YAML file

* After you've prepared the manifest file for your pod, you can now create the object by posting the file to the Kubernetes API

### Creating objects by applying the manifest file and re-applying it

* When you post the manifest to the API, you are directing Kubernetes to _apply_ the manifest to the cluster

```zsh
$ kubectl apply -f pod.kiada.yaml
pod "kiada" created
```

### Updating objects by modifying the manifest file and re-applying it

* The `kubectl apply` command is used for creating objects as well as for making changes to existing objects

  * If you later decide to make changes to your pod object, you can simply edit the `pod.kiada.yaml` file and run the `apply` command again

  * Some of the pod's fields aren't mutable, so the update may fail, but you can always delete the pod and re-create it

### Retrieving the full manifest of a running pod

* The pod object is now part of the cluster configuration

  * You can now read it back from the API to see the full object manifest w/ the following command:

```zsh
$ kubectl get po kiada -o yaml
```

* If you run this command, you'll notice that the manifest has grown considerably compared to the one in the `pod.kiada.yaml` file

  * You'll see that the `metadata` section is now much bigger, and the object now has a `status` section

  * The `spec` section has also grown by several fields

  * You can use `kubectl explain` to learn more about these new fields, but most of them will be explained in this and the following chapters

## 5.2.3 Checking the newly created pod

* Let's use the basic `kubectl` commands to see how the pod is doing before we start interacting w/ the application running inside it

### Quickly checking the status of a pod

* Your Pod object has been created, but how you know if the container in the pod is actually running?

  * You can use the `kubectl get` command to see a summary of the pod:

```zsh
$ kubectl get pod kiada
NAME    READY     STATUS      RESTARTS    AGE
kiada   1/1       Running     0           32s
```

* You can see that the pod is running, but not much else

  * To see more, you can try the `kubectl get pod -o wide` or the `kubectl describe` command

### Using Kubectl describe to see pod details

* To display a more detailed view of the pod, use the `kubectl describe` command:

```zsh
$ kubectl describe pod kiada
Name: kiada
Namespace: default
Priority: 0
Node: worker2/172.18.0.4
Start Time: Mon, 27 Jan 2020 12:53:28 +0100
...
```

* The listing doesn't show the entire output, but if you run the command yourself, you'll see virtually all information that you'd see if you print the complete object manifest using the `kubectl get -o yaml` command

### Inspecting events to see what happens beneath the surface

* As in the previous chapter where you used the `describe node` command to inspect a Node object, the `describe pod` command should display several events related to the pod at the bottom of the output

  * Recall, these events aren't part of the object itself, but are separate objects

  * Let's print them to learn more about what happens when you create the pod object

  * These are the events that were logged after the pod was created:

```zsh
$ kubectl get events
LAST SEEN   TYPE      REASON      OBJECT        MESSAGE
<unknown>   Normal    Scheduled   pod/kiada     Successfully assigned default/
                                                kiada to kind-worker2
5m          Normal    Pulling     pod/kiada     Pulling image luksa/kiada:0.1
5m          Normal    Pulled      pod/kiada     Successfully pulled image
5m          Normal    Created     pod/kiada     Created container kiada
5m          Normal    Started     pod/kiada     Started container kiada
```

* These events are printed in chronological order

  * The most recent event is at the bottom

  * You see that the pod was first assigned to oe of the worker nodes, then the container image was pulled, then the container was created and finally started

  * No warning events are displayed, so everything seems to be fine
