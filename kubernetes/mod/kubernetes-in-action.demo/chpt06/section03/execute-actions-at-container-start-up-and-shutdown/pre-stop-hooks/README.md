# Using pre-stop hooks to run a process just before the container terminates

* Besides executing a command or sending an HTTP request at container startup, K8s also allows the definition of a pre-stop hook in your containers

* A pre-stop hook is executed immediately before a container is terminated

  * To terminate a process, the `TERM` signal is usually sent to it

  * This tells the application to finish what it's doing and shut down

  * The same happens w/ containers

  * Whenever a container needs to be stopped or restarted, the `TERM` signal is sent to the main process in the container

  * Before this happens, however, K8s first executes the pre-stop hook, if one is configured for the container

  * The `TERM` signal is not sent until the pre-stop hook completes unless the process has already terminated due to the invocation of the pre-stop hook handler itself

> [!NOTE]
> 
> When container termination is initiated, the liveness and other probes are no longer invoked.

* A pre-stop hook can be used to initiate a graceful shutdown of the container or to perform additional operations w/o having to implement them in the application itself

  * As w/ post-start hooks, you can either execute a command within the container or send an HTTP request to the application running in it

## Using a pre-stop lifecycle hook to shut down a container gracefully

* The Nginx web server used in the quote pod responds to the `TERM` signal by immediately closing all open connections and terminating the process

  * This is not ideal, as the client requests that are being processed at this time aren't allowed to complete

* Fortunately, you can instruct Nginx to shut down gracefully by running the command `nginx -s quit`

  * When you run this command, the server stops accepting new connections, waits until all in-flight requests have been processed, and then quits

* When you run Nginx in a Kubernetes pod, you can use a pre-stop lifecycle hook to run this command and ensure that the pod shuts down gracefully

  * The following listing shows the definition of this pre-stop hook (you'll find it in the file `pod.quite-prestop.yaml`) ▶︎ defining a pre-stop hook for Nginx:

```yaml
  lifecycle:      # ← This is a pre-stop lifecycle hook
    preStop:      # ← This is a pre-stop lifecycle hook
      exec:       # ← It executes a command
        command:  # ← It executes a command
        - nginx   # ← This is the command that gets executed
        - -s      # ← This is the command that gets executed
        - quit    # ← This is the command that gets executed
```
  
* Whenever a container using this pre-stop hook is terminated, the command `nginx -s quit` is executed in the container before the main process of the container receives the `TERM` signal

* Unlike the post-start hook, the container is terminated regardless of the result of the pre-stop hook-a failure to execute the command or a non-zero exit code does not prevent the container from being terminated

  * If the pre-stop hook fails, you'll see a `FailedPreStopHook` warning event among the pod events, but you might not see any indication of the failure if you are only monitoring the status of the pod

> [!TIP]
> 
> If successful completion of the pre-stop hook is critical to the proper operation of your system, make sure that it runs successfully. I've experienced situations where the pre-stop hook did not run at all, but the engineers weren't even aware of it.

* Like post-start hooks, you can also configure the pre-stop hook to send an HTTP GET request to your application instead of executing commands

  * The configuration of the HTTP GET pre-stop hook is the same as for a post-start hook

* Pre-stop hooks are only invoked when the container is requested to terminate, either b/c it has failed its liveness probe or b/c the pod has to shut down

  * They are not called when the process running in the container terminates by itself

## Why doesn't my application receive the TERM signal?

* Many developers make the mistake of defining a pre-stop hook just to send a `TERM` signal to their applications in the pre-stop hook

  * They do this when they find that their application never receives the `TERM` signal

  * The root cause is usually not that the signal is never sent, but that it is swallowed by something inside the container

  * This typically happens when you use the _shell_ form of the `ENTRYPOINT` or the `CMD` directive in your Dockerfile

  * Two forms of these directives exist:

    * The _exec_ form is: `ENTRYPOINT ["/myexecutable", "1st-arg", "2nd-arg"]`

    * The _shell_ form is: `ENTRYPOINT /myexecutable 1st-arg 2nd-arg`

* When you use the exec form, the executable file is called directly

  * The process it starts becomes the root process of the container

  * When you use the shell form, a shell runs as the root process, and the shell runs the executable as its child process

  * In this case, the shell process is the one that receives the `TERM` signal

  * Unfortunately, it doesn't pass this signal to the child process

* In such cases, instead of adding a pre-stop hook to send the `TERM` signal to your app, the correct solution is to use the exec form of `ENTRYPOINT` or `CMD`

* Note that the same problem occurs if you use a shell script in your container to run the application

  * In this case, you must either intercept and pass signals to the application or use the `exec` shell command to run the application in your script

## Understanding that lifecycle hooks target containers, not pods

* As a final consideration on the post-start and pre-stop hooks, I would like to emphasize that these lifecycle hooks apply to containers and not to pods

  * You shouldn't use a pre-stop hook to perform an action that needs to be performed when the entire pod is shut down, b/c pre-stop hooks run every time the container needs to terminate

  * This can happen several times during the pod's lifetime, not just when the pod shuts down
