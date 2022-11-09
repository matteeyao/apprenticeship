# Application Configuration

**Application Configuration** is all about managing configuration data and passing that data to applications.

Kubernetes uses ConfigMaps and Secrets to store configuration data and pass it to containers.

## ConfigMaps and Secrets

**ConfigMaps** allow you to store configuration data in a key/value format. This data can include simple values or larger blocks of data such as configuration files.

**Secrets** are similar to ConfigMaps, but their data is encrypted at rest. Use secrets to store sensitive data, such as passwords or API tokens.

## Using Config Data

Configuration data stored in ConfigMaps or Secrets can be passed to our containers in two ways:

* **Environment variables** ▶︎ Configuration values can be passed in as environment variables that will be visible to the container process at runtime.

* **Volume mounts** ▶︎ Configuration data can be mounted to the container file system, where it will appear in the form of files.
