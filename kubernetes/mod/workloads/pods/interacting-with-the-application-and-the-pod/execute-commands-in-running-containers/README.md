# 5.3.4 Executing commands in running containers

* When debugging an application running in a container, it may be necessary to examine the contaienr and its environment from the inside

  * Kubectl provides this functionality, too

  * You can execute any binary file present in the container's file system using the `kubectl exec` command

## Invoking a single command in the container

* For example, you can list the processes running in the container in the `kiada` pod by running the following command:

```zsh
$ kubectl exec kiada -- ps aux
USER PID %CPU %MEM    VSZ   RSS TTY STAT START TIME COMMAND
root   1  0.0  1.3 812860 27356 ?   Ssl  11:54 0:00 node app.js # ← The Node.js server
root 120  0.0  0.1  17500  2128 ?   Rs   12:22 0:00 ps aux      # ← The command you’ve just invoked
```

* This is the K8s equivalent of the Docker command you used to explore the processes in a running container

  * It allows you to remotely run a command in any pod w/o having to log in to the node that hosts the pod

  * If you've used `ssh` to execute commands on a remote system, you'll see that `kubectl exec` is not much different

* Previously, you executed the `curl` command in a one-off client pod to send a request to your application, but you can also run the command inside the `kiada` pod itself:

```zsh
$ kubectl exec kiada -- curl -s localhost:8080
Kiada version 0.1. Request processed by "kiada". Client IP: ::1
```

## Why use a double dash in the kubectl exec command?

* The double dash `--` in the command delimits kubectl arguments from the command to be executed in the container

  * The use of the double dash isn't necessary if the command has no arguments that begin w/ a dash
  
  * If you omit the double dash in the previous example, the `-s` option is interpreted as an option for `kubectl exec` and results in the following misleading error:

```zsh
$ kubectl exec kiada curl -s localhost:8080
The connection to the server localhost:8080 was refused – did you specify the right host or port?
```

* This may look like the Node.js server is refusing to accept the connection, but the issues lies elsewhere

  * The curl command is never executed

  * The error is reported by `kubectl` itself when it tries to talk to the Kubernetes API server at `localhost:8080`, which isn't where the server is

  * If you run the `kubectl options` command, you'll see that the `-s` option can be used to specify the address and port of the K8s API server

  * Instead of passing that option to curl, kubectl adopted it as its own

  * Adding the double dash prevents this

* Fortunately, to prevent scenarios like this, newer versions of kubectl are set to return an error if you forget the double dash

## Running an interactive shell in the container

* The two previous examples showed how a single command can be executed in the container

  * When the command completes, you are returned to your shell

  * If you want to run several commands in the container, you can run a shell in the container as follows:

```zsh
$ kubectl exec -it kiada -- bash
root@kiada:/#                     # ← The command prompt of the shell running in the container
```

* The `-it` is short for two options: `-i` and `-t`, which indicate that you want to execute the `bash` command interactively by passing the standard input to the container and marking it as a terminal (TTY)

* You can now explore the inside of the container by executing commands in the shell

  * For example, you can view the files in the container by running `ls -la`, view its network interfaces w/ `ip link`, or test its connectivity w/ `ping`

  * You can now run any tool available in the container

## Not all containers allow you to run shells

* The container image of your application contains many important debugging tools, but this isn't the case w/ every container image

  * To keep images small and improve security in the container, most containers used in production don't contain any binary files other than those required for the container's primary process

    * This significantly reduces the attack surface, but also means that you can't run shells or other tools in production containers

  * Fortunately, a new Kubernetes feature called _ephemeral containers_ allows you to debug running containers by attaching a debug container to them
