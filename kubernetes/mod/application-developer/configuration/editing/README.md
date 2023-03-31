# Editing Pods and Deployments

## Edit a Pod

* Remember, you cannot edit specifications of an existing Pod other than the below:

  * `spec.containers[*].image`

  * `spec.initContainers[*].image`

  * `spec.activeDeadlineSeconds`

  * `spec.tolerations`

* For example, you cannot edit the environment variables, service accounts, resource limits of a running pod

* But if you need to, you have 2 options:

1. Run the `kubectl edit pod <POD_NAME>` command

   * This will open the pod specification in an editor (vi editor). Then edit the required properties. When you try to save it, you will be denied. This is b/c you are attempting to edit a field on the pod that is not editable

   * A copy of the file w/ your changes is saved in a temporary location

   * You can then delete the existing pod by running the command:

```zsh
kubectl delete pod webapp
```

  * Then create a new pod w/ your changes using the temporary file

```zsh
kubectl create -f /tmp/kubectl-edit-ccvrq.yaml
```

2. The second option is to extract the pod definition in YAML format to a file using the command:

```zsh
kubectl get pod webapp -o yaml > my-new-pod.yaml
```

  * Then make the changes to the exported file using an editor (vi editor). Save the changes:

```zsh
vi my-new-pod.yaml
```

  * Then delete the existing pod:

```zsh
kubectl delete pod webapp
```

* Then create a new pod w/ the edited file:

```zsh
kubectl delete -f my-new-pod.yaml
```

## Edit Deployments

* W/ Deployments, you can easily edit any field/property of the Pod template

  * Since the pod template is a child of the deployment specification, w/ every change the deployment will automatically delete and create a new pod w/ the new changes

  * So if you are asked to edit a property of a Pod part of a deployment, you may do that simply by running the command:

```zsh
kubectl edit deployment my-deployment
```
