# Observing the liveness probe in action

* To see Kubernetes restart a container when its liveness probe fails, create the pod from the `pod.kiada-liveness.yaml` manifest file using `kubectl apply`, and run `kubectl port-forward` to enable communication w/ te pod

  * You'll need to stop the `kubectl port-forward` command still running from the previous exercise

  * Confirm that the pod is running and is responding to HTTP requests

## Observing a successful liveness probe

* The liveness probe for the pod's containers starts firing soon after the start of each individual container

  * Since the processes in both containers are healthy, the probes continuously report success

  * As this is the normal state, the fact that the probes are successful is not explicitly indicated anywhere in the status of the pod nor in its events

* The only indication that Kubernetes is executing the probe is found in the container logs

  * The Node.js application in the `kiada` container prints a line to the standard output every time it handles an HTTP request

  * This includes the liveness probe requests, so you can display them using the following command:

```zsh
$ kubectl logs kiada-liveness -c kiada -f
```

* The liveness probe for the `envoy` container is configured to send HTTP requests to Envoy's administration interface, which doesn't log HTTP requests to the standard output, but to the file `/tmp/envoy.admin.log` in the container's filesystem

  * To display the log file, use the following command:

```zsh
$ kubectl exec kiada-liveness -c envoy -- tail -f /tmp/envoy.admin.log
```

## Observing the liveness probe fail

* A successful liveness probe isn't interesting, so let's cause Envoy's liveness probe to fail

  * To see what will happen behind the scenes, start watching events by executing the following command in a separate terminal:

```zsh
$ kubectl get events -w
```

* Using Envoy's administration interface, you can configure its health check endpoint to succeed or fail

  * To make it fail, open URL http://localhost:9901 in your browser and click the _healthcheck/fail_ button, or use the following `curl` command:

```zsh
$ curl -X POST localhost:9901/healthcheck/fail
```

  * Immediately after executing the command, observe the events that are displayed in the other terminal

    * When the probe fails, a `Warning` event is recorded, indicating the error and the HTTP status code returned:

```zsh
Warning Unhealthy Liveness probe failed: HTTP probe failed with code 503
```

  * B/c the probe's `failureThreshold` is set to three, a single failure is not enough to consider the container unhealthy, so it continues to run

    * You can make the liveness probe succeed again by clicking the _healthcheck/ok_ button in Envoy's admin interface, or by using `curl` as follows:

```zsh
$ curl -X POST localhost:9901/healthcheck/ok
```

  * If you are fast enough, the container won't be restarted

## Observing the liveness probe reach the failure threshold

* If you let the liveness probe fail multiple times, the `kubectl get events -w` command should print the following events (note that some columns are omitted due to page width constraints):

```zsh
$ kubectl get events -w
TYPE      REASON      MESSAGE
Warning   Unhealthy   Liveness probe failed: HTTP probe failed with code 503    # ← The liveness probe fails three times
Warning   Unhealthy   Liveness probe failed: HTTP probe failed with code 503    # ← The liveness probe fails three times
Warning   Unhealthy   Liveness probe failed: HTTP probe failed with code 503    # ← The liveness probe fails three times
Normal    Killing     Container envoy failed liveness probe, will be restarted  # ← When the failure threshold is reached, the container is restarted
Normal    Pulled      Container image already present on machine
Normal    Created     Created container envoy
Normal    Started     Started container envoy
```

* Remember that the probe failure threshold is set to 3, so when the probe fails three times in a row, the container is stopped and restarted

  * This is indicated by the events in the listing

  * The `kubectl get pods` command shows that the container has been restarted:

```zsh
$ kubectl get po kiada-liveness
NAME            READY   STATUS    RESTARTS    AGE
kiada-liveness  2/2     Running   1           5m
```

* The `RESTARTS` column shows that one container restart has taken place in the pod

## Understanding how a container that fails its liveness probe is restarted

* If you're wondering whether the main process in the container was gracefully stopped or killed forcibly, you can check the pod's status by retrieving the full manifest using `kubectl get` or using `kubectl describe`:

```zsh
$ kubectl describe po kiada-liveness
Name:           kiada-liveness
...
Containers:
  ...
  envoy:
    ...
    State:            Running                         # ← This is the state of the new container.
      Started:        Sun, 31 May 2020 21:33:13 +0200 # ← This is the state of the new container.
    Last State:       Terminated                      # ← The previous container terminated with exit code 0.
      Reason:         Completed                       # ← The previous container terminated with exit code 0.
      Exit Code:      0                               # ← The previous container terminated with exit code 0.
      Started:        Sun, 31 May 2020 21:16:43 +0200 # ← The previous container terminated with exit code 0.
      Finished:       Sun, 31 May 2020 21:33:13 +0200 # ← The previous container terminated with exit code 0.
    ...
```

* The exit code zero shown in the listing implies that the application process gracefully exited on its own

  * If it had been killed, the exit code would have been 137

> [!NOTE]
> 
> Exit code `128+n` indicates that the process exited due to external signal `n`. Exit code `137` is `128+9`, where `9` represents the `KILL` signal. You'll see this exit code whenever the container is killed. Exit code `143` is `128+15`, where 15 is the `TERM` signal. You'll typically see this exit code when the container runs a shell that has terminated gracefully.

* Examine Envoy's log to confirm that it caught the `TERM` signal and has terminated by itself

  * You must use the `kubectl logs` command w/ the `--container` or the shorter `-c` option to specify what container you're interested in

  * Also, b/c the container has been replaced w/ a new one due to the restart, you must request the log of the previous container using the `--previous` or `-p` flag

    * Here's the comand to use and the last four lines of its output:

```zsh
$ kubectl logs kiada-liveness -c envoy -p
...
...[warning][main] [source/server/server.cc:493] caught SIGTERM
...[info][main] [source/server/server.cc:613] shutting down server instance
...[info][main] [source/server/server.cc:560] main dispatch loop exited
...[info][main] [source/server/server.cc:606] exiting
```

* The log confirms that Kubernetes sent the `Term` signal to the process, allowing it to shut down gracefully

  * Had it not terminated by itself, Kubernetes would have killed it forcibly

* After the container is restarted, its health check endpoint responds w/ HTTP status `200 OK` again, indicating that the container is healthy
