# 3.1.5 Creating a cluster using Amazon Elastic Kubernetes Service

* Install `eksctl` command-line tool by following the instructions at https://docs.aws.amazon.com/eks/latest/userguide/getting-started-eksctl.html

## Creating an EKS K8s cluster

* Creating an EKS K8s cluster using `eksctl` does not differ significantly from how you create a cluster in GKE

    * All you must do is run the following command:

```zsh
$ eksctl create cluster --name kiada --region eu-central-1 --nodes 3 --ssh-access
```

* This command creates a three-node cluster in the eu-central-1 region 

    * The regions are listed at https://aws.amazon.com/about-aws/global-infrastructure/regional-product- services/

## Inspecting an EKS worker node

* If you're interested in what's running on those nodes, you can use SSH to connect to them

    * The `--ssh-access` flag used in the command that creates the cluster ensures that your SSH public key is imported to the node

* As w/ GKE and Minikube, once you've logged into the node, you can try to list all running containers w/ `docker ps`

    * You can expect to see similar containers as in the clusters we covered earlier
