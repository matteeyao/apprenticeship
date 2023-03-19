# Creating and using Ingress objects

* In this section, you'll learn how to use an Ingress to expose the services of the Kiada suite

* Before you create your first Ingress object, you must deploy the pods and services of the Kiada suite

  * If you followed the exercises in the previous chapter, they should already be there

  * If not, you can create them by creating the `kiada` namespace and then applying all manifests in the `Chapter12/SETUP` directory w/ the following command:

```zsh
$ kubectl apply -f SETUP/ --recursive
```

## 12.2.1 [Exposing a service through an ingress](expose-service-through-ingress/README.md)

## 12.2.2 [Path-based ingress traffic routing](path-based-ingress-traffic-routing/README.md)

## 12.2.3 [Using multiple rules in an Ingress object](multiple-rules-in-an-ingress-object/README.md)

## 12.2.4 [Setting the default backend](default-backend/README.md)
