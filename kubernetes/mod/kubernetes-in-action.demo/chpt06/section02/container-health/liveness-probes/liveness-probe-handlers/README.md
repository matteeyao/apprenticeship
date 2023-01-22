# Creating effective liveness probe handlers

* You should define a liveness probe for all your pods

  * W/o one, Kubernetes has no way of knowing whether your app is still alive or not, apart from checking whether the application process has terminated

## Causing unnecessary restarts w/ badly implemented liveness probe handlers

* When you implement a handler for the liveness probe, either as an HTTP endpoint in your application or as an additional executable command, be very careful to implement it correctly

  * If a poorly implemented probe returns a negative response even though the application is healthy, the application will be restarted unnecessarily

  * Many K8s users learn this the hard way

  * If you can make sure the application process terminates by itself when it becomes unhealthy, it may be safer not to define a liveness probe

## What a liveness probe should check

* The liveness probe for the `kiada` container isn't configured to call an actual health-check endpoint, but only checks that the Node.js server responds to simple HTTP requests for the root URI

  * This may seem overly simple, but even such a liveness probe works wonders, b/c it causes a restart of the container if the server no longer responds to HTTP requests, which is its main task

  * If no liveness probe were defined, the pod would remain in an unhealthy state where it doesn't respond to any requests and would have to be restarted manually

  * A simple liveness probe like this is better than nothing

* To provide a better liveness check, web applications typically expose a specific health-check endpoint, such as `/healthz`

  * When this endpoint is called, the application performs an internal status check of all the major components running within the application to ensure that none of them have died or are no longer doing what they should

> [!TIP]
> 
> Make sure that the `/healthz` HTTP endpoint doesn't require authentication or the probe will always fail, causing your container to be restarted continuously.

* Make sure that the application checks only the operation of its internal components and nothing that is influenced by external factor

  * For example, the health-check endpoint for a frontend service should never respond w/ failure when it can't connect to a backend service
  
  * If the backend service fails, restarting the frontend will not solve the problem

  * Such a liveness probe will fail again after the restart, so the container will be restarted repeatedly until the backend is repaired

  * If many services are interdependent in this way, the failure of a single service can result in cascading failure across the entire system

## Keeping probes light

* The handler invoked by a liveness probe shouldn't use too much computing resources and shouldn't take too long to complete

  * By default, probes are executed relatively often and only given one second to complete

  * Using a handler that consumes a lot of CPU or memory can seriously affect the main process of your container

    * Later in the book you'll learn how to limit the CPU time and total memory available to a container

    * The CPU and memory consumed by the probe handler invocation count towards the resource quota of the container, so using a resource-intensive handler will reduce the CPU time available to the main process of the application

> [!TIP]
> 
> When running a Java application in your container, you may want to use an HTTP GET probe instead of an exec liveness probe that starts an entire JVM. The same applies to commands that require considerable computing resources.

## Avoiding retry loops in your probe handlers

* You've learned that the failure threshold for the probe is configurable

  * Instead of implementing a retry loop in your probe handlers, keep it simple and instead set the `failureThreshold` field to a higher value so that the probe must fail several times before the application is considered unhealthy

  * Implementing your own retry mechanism in the handler is a waste of effort and represents another potential point of failure
