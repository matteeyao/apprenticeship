# 5.5 Running additional containers at pod startup

* When a pod contains more than one container, all the containers are started in parallel

  * K8s doesn't yet provide a mechanism to specify whether a container depends on another container, which would allow you to ensure that one is started before the other

  * However, K8s allows you to run a sequence of containers to initialize the pod before its main containers start
