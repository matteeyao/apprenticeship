# Working with Kubernetes Namespaces

## Introduction

Namespaces are a central component of any Kubernetes infrastructure. This lab will give you the opportunity to work with namespaces in a functioning cluster. You will be able to practice the process of creating, using, and navigating Kubernetes namespaces.

## Solution

Log in to the provided control plane node server using the credentials provided:

```zsh
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

### Create the `dev` Namespace

1. Create a namespace in the cluster called `dev`:

```zsh
kubectl create namespace dev
```

### Get a List of the Current Namespaces

1. List the current namespaces:

```zsh
kubectl get namespace
```

2. Save the namespaces list to a file:

```zsh
kubectl get namespace > /home/cloud_user/namespaces.txt
```

3. Verify the list saved to the file:

```zsh
cat namespaces.txt
```

We should see the list of namespaces.

### Find the `quark` Pod's Namespace

1. Locate the `quark` pod:

```zsh
kubectl get pods --all-namespaces
```

2. Copy the name of the namespace where the `quark` pod is located.

3. Create a file in which to save its name: :

```zsh
vi /home/cloud_user/quark-namespace.txt
```

4. Paste in the name of the `quark` pod's namespace.

5. Save and exit the file by pressing **Escape** followed by `:wq`.
