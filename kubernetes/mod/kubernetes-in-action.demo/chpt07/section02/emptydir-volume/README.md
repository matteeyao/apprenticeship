# Using an emptyDir volume

* The simplest volume type is `emptyDir`

  * As its name suggests, a volume of this type starts as an empty directory

  * When this type of volume is mounted in a container, files written by the application to the path where the volume is mounted are preserved for the duration of the pod's existence

* This volume type is used in single-container pods when data must be preserved even if the container is restarted

  * It's also used when the container's filesystem is marked read-only, and you want part of it to be writable

  * In pods w/ two or more containers, an `emptyDir` volume is used to share data between them
