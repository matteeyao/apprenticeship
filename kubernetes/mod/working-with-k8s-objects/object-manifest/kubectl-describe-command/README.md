# 4.2.4 Inspecting objects using the kubectl describe command

* A more user-friendly way to inspect an object is the `kubectl describe` command, which typically displays the same information or sometimes even more

## Understanding the Kubectl describe output for a Node object

* Let’s try running the kubectl describe command on a Node object

  * To keep things interesting, let’s use it to describe one of the worker nodes instead of the master

  * This is the command and its output:

```zsh
$ kubectl describe node kind-worker-2
...
```

* As you can see, the `kubectl describe` command displays all the information you previously found in the YAML manifest of the Node object, but in a more readable form

  * You can see the name, IP address, and hostname, as well as the conditions and available capacity of the node

## Inspecting other objects related to the Node

* In addition to the information stored in the Node object itself, the `kubectl describe` command also displays the pods running on the node and the total amount of compute resources allocated to them

  * Below is also a list of events related to the node

* This additional information isn’t found in the Node object itself but is collected by the kubectl tool from other API objects

  * For example, the list of pods running on the node is obtained by retrieving Pod objects via the `pods` resource

* If you run the `describe` command yourself, no events may be displayed

  * This is b/c only events that have occurred recently are shown

  * For Node objects, unless the node has resource capacity issues, you'll only see events if you've recently (re)started the node

* Virtually every API object kind has events associated w/ it

  * Since they are crucial for debugging a cluster, they warrant a closer look before you start exploring other objects
