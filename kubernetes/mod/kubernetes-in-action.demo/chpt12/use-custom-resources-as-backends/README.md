# Using custom resources instead of services as backends

* In this chpt, the backends referenced in the Ingress have always been Service objects

  * However, some ingress controllers allow you to use other resources as backends

* Theoretically, an ingress controller could allow using an Ingress object to expose the contents of a ConfigMap or PersistentVolume, but it's more typical for controllers to use resource backends to provide an option for configuring advanced Ingress routing rules through a custom resource

## 12.6.1 [Using a custom object to configure Ingress routing](use-custom-object-to-configure-ingress-routing/README.md)