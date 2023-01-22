# Inspecting init containers

* As w/ regular containers, you can run additional commands in a running init container using `kubectl exec` and display the logs using `kubectl logs`

## Displaying the logs of an init container

* The standard and error output, into which each init container can write, are captured exactly as they are for regular containers

  * The logs of an init container can be displayed using the `kubectl logs` command by specifying the name of the container w/ the `-c` option either while the container runs or after it has completed

  * To display the logs of the `network-check` container in the `kiada-init` pod, run the next command:

```zsh
$ kubectl logs kiada-init -c network-check
Checking network connectivity to 1.1.1.1 ...
Host appears to be reachable
```

* The logs show that the `network-check` init container ran w/o errors

## Entering a running init container

* You can use the `kubectl exec` command to run a shell or a different command inside an init container the same way you can w/ regular containers, but you can only do this before the init container terminates

  * If you'd like to try this yourself, create a pod from the `[pod.kiada-init-slow.yaml](../../../../kubernetes-in-action.demo/chapter05/pod.kiada-init-slow.yaml)` file, which makes the init-demo container run for 60 seconds

    * When the pod starts, run a shell in the container w/ the following command:

```zsh
$ kubectl exec -it kiada-init-slow -c init-demo -- sh
```

  * You can use the shell to explore the container from the inside, but only for a short time

    * When the container's main process exits after 60 seconds, the shell process is also terminated

* You typically enter a running init container only when it fails to complete in time, and you want to find the cause

  * During normal operation, the init container terminates before you can run the `kubectl exec` command
