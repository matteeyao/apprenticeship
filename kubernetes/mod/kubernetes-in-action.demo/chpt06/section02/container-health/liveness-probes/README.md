# Checking the container’s health using liveness probes

* In the previous section, you learned that Kubernetes keeps your application healthy by restarting it when its process terminates

  * But applications can also become unresponsive w/o terminating

  * For example, a Java application w/ a memory leak eventually start spewing out OutOfMemoryErrors, but its JVM process continues to run

  * Ideally, Kubernetes should detect this kind of error and restart the container

* The application could catch these errors by itself and immediately terminate, but what about the situations where your application stops responding b/c it gets into an infinite loop or deadlock?

  * What if the application can't detect this?

  * To ensure that the application is restarted in such cases, it may be necessary to check its state from the outside

## Introducing liveness probes

* Kubernetes can be configured to check whether an application is still alive by defining a _liveness_ probe

  * You can specify a liveness probe for each container in the pod

  * Kubernetes runs the probe periodically to ask the application if it's still alive and well

  * If the application doesn't respond, an error occurs, or the response is negative, the container is considered unhealthy and is terminated

  * The container is then restarted if the restart policy allows it

> [!NOTE]
> 
> Liveness probes can only be used in the pod's regular containers. They can't be defined in init containers.

## Types of liveness probes

* Kubernetes can probe a container w/ one of the following three mechanisms:

  * An _HTTP GET_ probe sends a GET request to the container's IP address, on the network port and path you specify

    * If the probe receives a response, and the response code doesn't represent an error (in other words, if the HTTP response code is `2xx` or `3xx`), the probe is considered successful

    * If the server returns an error response code, or if it doesn't respond in time, the probe is considered to have failed

  * A _TCP Socket_ probe attempts to open a TCP connection to the specified port of the container

    * If the connection is successfully established, the probe is considered successful

    * If the connection can't be established in time, the probe is considered failed

  * An _Exec_ probe executes a command inside the container and checks the exit code it terminates with

    * If the exit code is zero, the probe is successful

    * A non-zero exit code is considered a failure

    * The probe is also considered to have failed if the command fails to terminate in time

> [!NOTE]
> 
> In addition to a liveness probe, a container may also have a _startup_ probe, which is discussed in section 6.2.6, and a _readiness_ probe, which is explained in chapter 10.
