# 5.3.2 Viewing application logs

* Your Node.js application writes its log to the standard output stream

  * Instead of writing the log to a file, containerized applications usually log to the standard output (_stdout_) and standard error streams (_stderr_)

  * This allows the container runtime to intercept the output, store it in a consistent location (usually `/var/log/containers`) and provide access to the log w/o having to know where each application stores its log files

* When you run an application in a container using Docker, you can display its log w/ `docker logs <CONTAINER_ID>`

  * When you run your application in K8s, you could log into the node that hosts the pod and display its log uding `docker logs`, but K8s provides an easier way to do this w/ the `kubectl logs` command

## Retrieving a pod's log w/ kubectl logs

* To view the log of your pod (more specifically, the container's log), run the following command:

```zsh
$ kubectl logs kiada
Kiada - Kubernetes in Action Demo Application
---------------------------------------------
Kiada 0.1 starting...
Local hostname is kiada
Listening on port 8080
Received request for / from ::ffff:10.244.2.1   # ← Request you sent from within the node
Received request for / from ::ffff:10.244.2.5   # ← Request from the one-off client pod
Received request for / from ::ffff:127.0.0.1    # ← Request sent through port forwarding
```

## Streaming logs using kubectl logs -f

* If you want to stream the application log in real-time to see each request as it comes in, you can run the command w/ the `--follow` option (or the shorter version `-f`):

```zsh
$ kubectl logs kiada -f
```

* Now send some additional requests to the application and have a look at the log

  * Press `ctrl-C` to stop streaming the log when you're done

## Displaying the timestamp of each logged line

* You may have noticed that we forgot to include the timestamp in the log statement

  * Logs w/o timestamps have limited usability

  * Fortunately, the container runtime attaches the current timestamp to every line produced by the application

  * You can display these timestamps by using the `--timestamps=true` option as follows:

```zsh
$ kubectl logs kiada --timestamps=true
2020-02-01T09:44:40.954641934Z Kiada - Kubernetes in Action Demo Application
2020-02-01T09:44:40.954843234Z ---------------------------------------------
2020-02-01T09:44:40.955032432Z Kiada 0.1 starting...
2020-02-01T09:44:40.955123432Z Local hostname is kiada
2020-02-01T09:44:40.956435431Z Listening on port 8080
2020-02-01T09:50:04.978043089Z Received request for / from ...
2020-02-01T09:50:33.640897378Z Received request for / from ...
2020-02-01T09:50:44.781473256Z Received request for / from ...
```

> [!TIP]
> 
> You can display timestamps by only typing `--timestamps` w/o the value. For boolean options, merely specifying the option name sets the option to `true`. This applies to all kubectl options that take a Boolean value and default to `false`.

## Displaying recent logs

* The previous feature is great if you run third-party applications that don't include the timestamp in their log output, but the fact that each line is timestamped brings us another benefit: filtering log lines by time

* Kubectl provides two ways of filtering the logs by time

  * The first option is when you want to only display logs from the past several seconds, minutes, or hours

    * For example, to see the logs produced in the last two minutes, run:

```zsh
$ kubectl logs kiada --since=2m
```

  * The other option is to display logs produced after a specific date and time using the `--since-time` option

    * The time format to be used is RFC3339

    * For example, the following command is used to print logs produced after February 1st, 2020 at 9:50 a.m.:

```zsh
$ kubectl logs kiada --since-time=2020-02-01T09:50:00Z
```

## Displaying the last several lines of the log

* Instead of using time to constrain the output, you can also specify how many lines from the end of the log you want to display

  * To display the last ten lines, try:

```zsh
$ kubectl logs kiada --tail=10
```

> [!NOTE]
> 
> Kubectl options that take a value can be specified w/ an equal sign or w/ a space. Instead of `--tail=10`, you can also type `--tail 10`.

## Understanding the availability of the pod's logs

* K8s keeps a separate log file for each container

  * They are usually stored in `/var/log/containers` on the node that runs the container

  * A separate file is created for each container

  * If the container is restarted, its logs are written to a new file

  * B/c of this, if the container is restarted while you're following its log w/ `kubectl logs -f`, the command will terminate, and you'll need to run it again to stream the new container's logs

* The `kubectl logs` command displays only the logs of the current container

  * To view the logs from the previous container, use the `--previous` (or `-p`) option

> [!NOTE]
> 
> Depending on your cluster configuration, the log files are also deleted. To make pods' logs available permanently, you need to set up a central, cluster-wide logging system.

## What about applications that write their logs to files?


* If your application writes its logs to a file instead of stdout, you may be wondering how to access that file

  * Ideally, you'd configure the centralized logging system to collect the logs so you can view them in a central location, but sometimes you just want to keep things simple and don't mind accessing the logs manually

  * In the next sections, you'll learn how to copy log and other files from the container to your computer and in the opposite direction, and how to run commands in containers

  * You can use either method to display the log files or any other file inside the container
