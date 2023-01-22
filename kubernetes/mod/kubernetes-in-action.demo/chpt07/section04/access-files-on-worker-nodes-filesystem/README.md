# Accessing files on the worker node's filesystem

* Most pods shouldn't care which host node they are running on, and they shouldn't access any files on the node's filesystem

  * System-level pods are the exception

  * They may need to read the node's files or use the node's filesystem to access the node's devices or other components via the filesystem

  * Kubernetes makes this possible through the `hostPath` volume type

  * We already mentioned it in the previous section, but this is where you'll learn when to actually use it

## Introducing the hostPath volume

▶︎ See [7.4.1](./hostpath-volume/README.md)

## Using a hostPath volume

▶︎ See [7.4.2](./use-hostpath-volume/README.md)
