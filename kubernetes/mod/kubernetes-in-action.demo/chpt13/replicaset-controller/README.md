# 13.3 Understanding the operation of the ReplicaSet controller

* In the previous sections, you saw how changing the `replicas` and `template` within the ReplicaSet object causes K8s to do something w/ the Pods that belong to the ReplicaSet

  * The K8s component that performs these actions is called the controller

  * Most of the object types you create through your cluster's API have an associated controller

  * For example, in the previous chapter you learned about the Ingress controller, which manages Ingress objects

  * There's also the Endpoints controller for the Endpoints objects, the Namespace controller for the Namespace objects, and so on

* Not surprisingly, ReplicaSets are managed by the ReplicaSet controller

  * Any change you make to a ReplicaSet object is detected and processed by this controller

  * When you scale the ReplicaSet, the controller is the one that creates or deletes the Pods

  * Each time it does this, it also creates an Event object that informs you of what it's done

  * As you learned in chapter 4, you can see the events associated w/ an object at the bottom of the output of the `kubectl describe` command as shown in the next snippet, or by using the `kubectl get events` command to specifically list the Event objects

```zsh
$ kubectl describe rs kiada
...
Events:
Type    Reason             Age    From                    Message
----    ------             ----   ----                    -------
Normal  SuccessfulDelete   34m    replicaset-controller   Deleted   pod: kiada-k9hn2  # ← A
Normal  SuccessfulCreate   30m    replicaset-controller   Created   pod: kiada-dl7vz  # ← B
Normal  SuccessfulCreate   30m    replicaset-controller   Created   pod: kiada-dn9fb  # ← B
Normal  SuccessfulCreate   16m    replicaset-controller   Created   pod: kiada-z9dp2  # ← B

# ← A ▶︎ This event indicates that the controller deleted a Pod.
# ← B ▶︎ These events show that the ReplicaSet controller created three Pods.
```

* To understand ReplicaSets, you must understand the operation of their controller

## 13.3.1 [Introducing the reconciliation control loop](reconciliation-control-loop/README.md)

## 13.3.2 [Understanding how the ReplicaSet controller reacts to Pod changes](pod-changes/README.md)

## 13.3.3 [Removing a Pod from the ReplicaSet's control](remove-pod-from-replicaset-control/README.md)
