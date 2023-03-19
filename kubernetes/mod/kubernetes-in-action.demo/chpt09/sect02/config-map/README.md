# Using a config map to decouple configuration from the pod

* In the previous section, you learned how to hardcode configuration directly into your pod manifests

  * While this is much better than hard-coding in the container image, it's still not ideal b/c it means you might need a separate version of the pod manifest for each environment you deploy the pod to, such as your development, staging, or production cluster

* To reuse the same pod definition in multiple environments, it's better to decouple the configuration from the pod manifest

  * One way to do this is to move the configuration into a ConfigMap object, which you then reference in the pod manifest

  * This is what you'll do next

## Setting the command and arguments

▶︎ See [9.2.1](configmaps/README.md)

## Setting the command and arguments

▶︎ See [9.2.2](create-configmap-object/README.md)

## Injecting config map values into environment variables

▶︎ See [9.2.3](inject-config-map-values-into-env-vars/README.md)

## Injecting config map entries into containers as files

▶︎ See [9.2.4](inject-config-map-entries/README.md)

## Updating and deleting config maps

▶︎ See [9.2.5](update-and-delete-config-maps/README.md)

## Understanding how configMap volumes work

▶︎ See [9.2.6](configmap-volumes/README.md)
