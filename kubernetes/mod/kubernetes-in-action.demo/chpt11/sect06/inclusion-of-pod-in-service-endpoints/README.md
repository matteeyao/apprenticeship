# Managing the inclusion of a pod in service endpoints

* A pod is included as an endpoint of a service if its labels match the service's label selector

  * Once a new pod w/ matching labels shows up, it becomes part of the service and connections are forwarded to the pod

  * But what if the application in the pod isn't immediately ready to accept connections?

* It may be that the application needs time to load either the configuration or the data, or that it needs to warm up so that the first client connection can be processed as quickly as possible w/o unnecessary latency caused by the fact that the application has just started

  * In such cases, you don't want the pod to receive traffic immediately, especially if the existing pod instances can handle the traffic

  * It makes sense not to forward requests to a pod that's just starting up until it becomes ready

## 11.6.1 Introducing readiness probes

* See [readiness-probes](readiness-probes/README.md)

## 11.6.2 Adding a readiness probe to a pod

* See [add-readiness-probes-to-a-pod](add-readiness-probes-to-a-pod/README.md)

## 11.6.3 Implementing real-world readiness probes

* See [real-world-readiness-probes](real-world-readiness-probes/README.md)
