# 3.2.2 Configuring kubectl to use a specific Kubernetes cluster

* The kubeconfig configuration file is located at `~/.kube/config`

  * If you deployed your cluster using Docker Desktop, Minikube or GKE, the file was created for you

  * If you’ve been given access to an existing cluster, you should have received the file

  * Other tools, such as kind, may have written the file to a different location

  * Instead of moving the file to the default location, you can also point kubectl to it by setting the `KUBECONFIG` environment variable as follows:

```zsh
$ export KUBECONFIG=/path/to/custom/kubeconfig
```

* To learn more about how to manage kubectl’s configuration and create a config file from scratch, refer to appendix A.

> [!NOTE]
> 
> If you want to use several Kubernetes clusters (for example, both Minikube and GKE), see appendix A for information on switching between different `kubectl` contexts.
