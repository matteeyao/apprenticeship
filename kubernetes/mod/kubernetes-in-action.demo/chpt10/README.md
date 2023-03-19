# Chapter 10. Organizing objects using namespaces and labels

## Learning objectives

- [ ] Using namespaces to split a physical cluster into virtual clusters

- [ ] Organizing objects using labels

- [ ] Using label selectors to perform operations on subsets of objects

- [ ] Using label selectors to schedule pods onto specific nodes

- [ ] Using field selectors to filter objects based on their properties

- [ ] Annotating objects w/ additional non-identifying information

* **object namespaces** ▶︎ solves how teams deploy objects to the same cluster and organizes those objects so another team doesn't accidentally modify those objects 

* **object labels** ▶︎ enable us to see where each object belongs and what role that object serves in the system

  * For example, to which application does a config map or secret belong

## Sections

* 10.1 [Organizing objects into namespaces](sect01/namespaces/README.md)

* 10.2 [Organizing pods with labels](sect02/labels/README.md)

* 10.3 [Filtering objects with label selectors](sect03/label-selectors/README.md)

* 10.4 [Annotating objects](sect04/annotations/README.md)

## Learning summary

* Objects in a K8s cluster are typically divided into many namespaces

  * Within a namespace, object names must be unique, but you can give two objects the same name if you create them in different namespaces

* Namespaces allow different users and teams to use the same cluster as if they were using separate K8s clusters

* Each object can have several labels

  * Labels are key-value pairs that help identify the object

  * By adding labels to objects, you can effectively organize objects into groups

* Label selectors allow you to filter objects based on their labels

  * You can easily filter pods that belong to a specific application, or by any other criteria if you've previously added the appropriate labels to those pods

* Field selectors are like label selectors, but they allow you to filter objects based on specific fields in the object manifest

  * For example, a field selector can be used to list pods that run on a particular node

  * Unfortunately, you can't use them to filter on annotations

* Instead of performing an operation on each pod individually, you can use a label selector to perform the same operation on a set of objects that match the label selector

* Labels and selectors are also used internally by some object types

  * You can add labels to Node objects and define a node selector in a pod to schedule that pod only to those nodes that meet the specified criteria

* In addition to labels, you can also add annotations to objects

  * An annotation can contain a much larger amount of data and can include whitespace and other characters that aren't allowed in labels

  * Annotations are typically used to add additional information used by tools and cluster users

  * They are also used to defer changes to the K8s API

* In the next chapter, you'll learn how to forward traffic to a set of pods using the Service object
