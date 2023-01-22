# 5.3.3 Copying files to and from containers

* Sometimes you may want to add a file to a running container or retrieve a file from it

  * Modifying files in running containers isn't something you normally do-at least not in production-but it can be useful during development

* Kubectl offers the `cp` command to copy files or directories from your local computer to a container of any  pod or from the container to your computer

  * For example, if you'd like to modify the HTML file that the `kiada` pod serves, you can use the following command to copy it to your local file system:

```zsh
$ kubectl cp kiada:html/index.html /tmp/index.html
```

* This command copies the file `/html/index.html` file from the pod named `kiada` to the `/tmp/index.html` file on our computer

  * You can not edit the file locally

  * Once you're happy w/ the changes, copy the file back to the container w/ the following command:

```zsh
$ kubectl cp /tmp/index.html kiada:html/
```

* Hitting refresh **in** your browser should now include the changes you've made.

> [!NOTE]
> 
> The `kubectl cp` command requires the `tar` binary to be present in your container, but this requirement may change in the future.
