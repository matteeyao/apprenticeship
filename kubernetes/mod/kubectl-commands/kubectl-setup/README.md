# 3.2.1 Setting up kubectl - the Kubernetes command-line client

* Kubectl is a single executable file that you must download to your computer and place into your path

    * It loads its configuration from a configuration file called *kubeconfig*

    * To use kubectl, you must both install it and prepare the kubeconfig file so kubectl knows what cluster to talk to

## Download nad install kubectl

* The latest stable release for Linux can be downloaded and installed w/ the following commands:

```zsh
$ curl -LO "https://dl.k8s.io/release/$(curl -L -s
      https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
$ chmod +x kubectl
$ sudo mv kubectl /usr/local/bin/
```

* To install `kubectl` on macOS, you can either run the same command, but replace `linux` in the URL w/ `darwin`, or install the tool via Homebrew by running `brew install kubectl`

* On Windows, download `kubectl.exe` from https://storage.googleapis.com/kubernetes- release/release/v1.18.2/bin/windows/amd64/kubectl.exe

    * To download the latest version, first go to https://storage.googleapis.com/kubernetes-release/release/stable.txt to see what the latest stable version is and then replace the version number in the first URL with this version
  
    *  To check if you’ve installed it correctly, run `kubectl --help`

    * Note that kubectl may or may not yet be configured to talk to your Kubernetes cluster, which means most commands may not work yet

> [!TIP]
> 
> You can always append `--help` to any `kubectl` command to get more information.

## Setting up a short alias for kubectl

* Most users of Kubernetes use `k` as the alias for kubectl

    * If you haven’t used aliases yet, here’s how to define it in Linux and macOS

    * Add the following line to your ~/.bashrc or equivalent file:

```zsh
alias k=kubectl
```

* On Windows, if you use the Command Prompt, define the alias by executing `doskey k=kubectl $*`

    * If you use PowerShell, execute `set-alias -name k -value kubectl`

> [!NOTE]
> 
> You may not need an alias if you used `gcloud` to set up the cluster. It installs the `k` binary in addition to `kubectl`.

## Configure tab completion for Kubectl

* The `kubectl` command can also output shell completion code for both the bash and the zsh shell

```zsh
$ kubectl describe node gke-kiada-default-pool-9bba9b18-4glf
```

* W/ tab completion, things are much easier. Just press TAB after typing the first few chars of each token:

```zsh
$ kubectl desc<TAB> no<TAB> gke-ku<TAB>
```

* To enable tab completion in bash, you must first install a package called `bash-completion` and then run the following command (you can also add it to `~/.bashrc` or equivalent):

```zsh
$ source <(kubectl completion bash)
```

> [!NOTE]
> 
> This enables completion in bash. You can also run this command with a different shell. At the time of writing, the available options are `bash`, `zsh`, `fish`, and `powershell`.

* However, this will only complete your commands when you use the full `kubectl` command name

  * It won’t work when you use the `k` alias

  * To enable completion for the alias, you must run the following command:

```zsh
$ complete -o default -F __start_kubectl k
```
