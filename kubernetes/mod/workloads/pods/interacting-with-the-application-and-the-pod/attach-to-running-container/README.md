# 5.3.5 Attaching to a running container

* The `kubectl attach` command is another way yo interact w/ a running container

  * It attaches itself to the standard input, output and error streams of the main process running in the container

  * Normally, you only use it to interact w/ applications that read from the standard input

## Using kubectl attach to see what the application prints to standard output

* If the application doesn't read from standard input, the `kubectl attach` command is no more than an alternative way to stream the application logs, as these are typically written to the standard output and error streams, and the `attach` command streams them just like the `kubectl logs -f` command does

* Attach to your `kiada` pod by running the following command:

```zsh
$ kubectl attach kiada
Defaulting container name to kiada.
Use 'kubectl describe pod/kiada -n default' to see all of the containers in this pod.
If you don't see a command prompt, try pressing enter.
```

* Now, when you send new HTTP requests to the application using `curl` in another terminal, you'll see the lines that the application logs to standard output also printed in the terminal where the `kubectl attach` command is executed.

## Using kubectl attach to write to the application's standard input

* The Kiada application version 0.1 doesn't read from the standard input stream, but you'll find the source code of version 0.2 that does this in the book's code archive

  * This version allows you to set a status message by writing it to the standard input stream of the application

  * This status message will be included in the application's response

  * Let's deploy this version of the application in a new pod and use the `kubectl attach` command to set the status message

* You can find the artifacts required to build the image in the `kiada-0.2/` directory

  * You can also use the pre-built image `docker.io/luksa/kiada:0.2`

  * The pod manifest is in the file `Chapter05/pod.kiada-stdin.yaml` and is shown in the folloing listing

    * It contains one additional line compared to the previous manifest:

**Listing 01 Enabling standard input for a container**

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: kiada-stdin         # ← This pod is named kiada-stdin
spec:
  containers:
  - name: kiada
    image: luksa/kiada:0.2  # ← It uses the 0.2 version of the Kiada application
    stdin: true             # ← The application needs to read from the standard input stream
    ports:
    - containerPort: 8080
```

* As you can see in the listing, if the application running in a pod wants to read from standard input, you must indicate this in the pod manifest by setting the `stdin` field in the container definition to `true`

  * This tells K8s to allocate a buffer for the standard input stream, otherwise the application will always receive and `EOF` when it tries to read from it

* Create the pod from this manifest file w/ the `kubectl apply` command:

```zsh
$ kubectl apply -f pod.kiada-stdin.yaml
pod/kiada-stdin created
```

* To enable communication w/ the application, use the `kubectl port-forward` command again, but b/c the local port `8080` is still being used by the previously executed `port-forward` command, you must either terminate it or choose a different local port to forward to the new pod

  * You can do this as follows:

```zsh
$ kubectl port-forward kiada-stdin 8888:8080
Forwarding from 127.0.0.1:8888 -> 8080
Forwarding from [::1]:8888 -> 8080
```

* The command-line argument `8888:8080` instructs the command to forward local port `8888` to the pod's port `8080`

  * You can now reach the application at http://localhost:8888:

```zsh
$ curl localhost:8888
Kiada version 0.2. Request processed by "kiada-stdin". Client IP: ::ffff:127.0.0.1
```

* Let's set the status message by using `kubectl attach` to write to the standard input stream of the application

  * Run the following command:

```zsh
$ kubectl attach -i kiada-stdin
```

* Note the use of the additional option `-i` in the command

  * It instructs `kubectl` to pass its standard input to the container

> [!NOTE]
> 
> Like the `kubectl exec` command, `kubectl attach` also supports the `--tty` or `-t` option, which indicates that the standard input is a terminal (TTY), but the container must be configured to allocate a terminal through the `tty` field in the container definition.

* You can now enter the status message into the terminal and press the ENTER key

  * For example, type the following message:

```zsh
This is my custom status message.
```

* The application prints the new new message to the standard output:

```zsh
Status message set to: This is my custom status message.
```

* To see if the application now includes the message in its responses to HTTP requests, re-execute the `curl` command or refresh the page in your web browser:

```zsh
$ curl localhost:8888
Kiada version 0.2. Request processed by "kiada-stdin". Client IP: ::ffff:127.0.0.1
This is my custom status message.   # ← Here’s the message you set via the kubectl attach command.
```

* You can change the status message again by typing another line in the terminal running the `kubectl attach` command

  * To exit the `attach` command, press Control-C or the equivalent key

> [!NOTE]
> 
> An additional field in the container definition, `stdinOnce`, determines whether the standard input channel is closed when the attach session ends. It's set to `false` by default, which allows you to use the standard input in every `kubectl attach` session. If you set it to `true`, standard input remains open only during the first session.
