# Kubectl context and configuration

Set which Kubernetes cluster `kubectl` communicates with and modifies configuration information.

* Show merged kubeconfig settings:

```zsh
kubectl config view
```

* Use multiple kubeconfig files at the same time and view merged config:

```zsh
KUBECONFIG=~/.kube/config:~/.kube/kubconfig2
```

```zsh
kubectl config view
```

* Get the password for the e2e user:

```zsh
kubectl config view -o jsonpath='{.users[?(@.name == "e2e")].user.password}'
```

* Display the first user:

```zsh
kubectl config view -o jsonpath='{.users[].name}'
```

* Get a list of users:

```zsh
kubectl config view -o jsonpath='{.users[*].name}'
```

* Display list of contexts:

```zsh
kubectl config get-contexts
```

* Display the current-context:

```zsh
kubectl config current-context
```

* Set the default context to `<CLUSTER_NAME>`

```zsh
kubectl config use-context <CLUSTER_NAME>
```

* Set a cluster entry in the `kubeconfig`:

```zsh
kubectl config set-cluster <CLUSTER_NAME>
```

* Configure the URL to a proxy server to use for requests made by this client in the `kubeconfig`:

```zsh
kubectl config set-cluster my-cluster-name --proxy-url=my-proxy-url
```

* Add a new user to your kubeconf that supports basic auth:

```zsh
kubectl config set-credentials <KUBE_USER>/foo.kubernetes.com --username=<KUBE_USER> --password=<KUBE_PASSWORD>
```

* Permanently save the namespace for all subsequent kubectl commands in that context:

```zsh
kubectl config set-context --current --namespace=ggckad-s2
```

* Set a context utilizing a specific username and namespace:

```zsh
kubectl config set-context gce --user=cluster-admin --namespace=foo \
  && kubectl config use-context gce
```

* Delete user `foo`:

```zsh
kubectl config unset users.foo
```

* Short alias to set/show context/namespace (only works for bash and bash-compatible shells, current context to be set before using kn to set namespace):

```zsh
alias kx='f() { [ "$1" ] && kubectl config use-context $1 || kubectl config current-context ; } ; f'
alias kn='f() { [ "$1" ] && kubectl config set-context --current --namespace $1 || kubectl config view --minify | grep namespace | cut -d" " -f6 ; } ; f'
```
