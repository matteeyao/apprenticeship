# Create a deployment

* In the previous chapter, you created a Node.js application called Kiada that you packaged into a container image and pushed to Docker Hub to make it easily distributable to any computer

* Command to deploy the Kiada application to the K8s cluster:

```zsh
$ kubectl create deployment kiada --image=luksa/kiada:0.1
deployment.apps/kiada created
```

* The command specifies:

    * You want to create a `deployment` object

    * You want the object to be called `kiada`

    * You want the deployment to use the container image `luksa/kiada:0.1`

* By default, the image is pulled from Docker Hub, but you can also specify the image registry in the image name (for example, `quay.io/luksa/kiada:0.1`)

> [!NOTE]
>
> Make sure that the image is stored in a public registry and can be pulled without access authorization. You’ll learn how to provide credentials for pulling private images in chapter 8.

* The Deployment object is now stored in the K8s API

    * The existence of this object tells K8s that the `luksa/kiada:0.1` container must run in your cluster

    * You've stated your _desired_ state

    * K8s must now ensure that the _actual_ state reflects your wishes
