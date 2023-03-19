# 14.1.3 Deleting a Deployment

* Before we get to Deployment updates, which are the most important aspect of Deployments, let's take a quick look at what happens when you delete a Deployment

  * After learning what happens when you delete a ReplicaSet, you probable already know that when you delete Deployment object, the underlying ReplicaSet and Pods are also deleted

## Preserving the ReplicaSet and Pods when deleting a Deployment

* If you want to keep the Pods, you can run the `kubectl delete` command w/ the `--cascade=orphan` option, as you can w/ a ReplicaSet

  * If you use this approach w/ a Deployment, you'll find that this not only preserves the Pods, but also the ReplicaSets

  * The Pods still belong to and are controlled by that ReplicaSet

## Adopting an existing ReplicaSet and Pods

* If you recreate the Deployment, it picks up the existing ReplicaSet, assuming you haven't changed the Deployment's Pod template in the meantime

  * This happens b/c the Deployment controller finds an existing ReplicaSet w/ a name that matches the ReplicaSet that the controller would otherwise create
