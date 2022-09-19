# Deploying a Simple Service to Kubernetes

## Introduction

Deployments and services are at the core of what makes Kubernetes a great way to manage complex application infrastructures. In this hands-on lab, you will have an opportunity to get hands-on with a Kubernetes cluster and build a simple deployment, coupled with a service providing access to it. You will create a deployment and a service which can be accessed by other pods in the cluster.

## Solution

1. Begin by logging in to the **Kubernetes Master** server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

## Create a deployment for the store-products service with four replicas

1. Log in to the Kube master node.

2. Create the deployment with four replicas:

```
cat << EOF | kubectl apply -f -
apiVersion: apps/v1
kind: Deployment
metadata:
  name: store-products
  labels:
    app: store-products
spec:
  replicas: 4
  selector:
    matchLabels:
      app: store-products
  template:
    metadata:
      labels:
        app: store-products
    spec:
      containers:
      - name: store-products
        image: linuxacademycontent/store-products:1.0.0
        ports:
        - containerPort: 80
EOF
```

## Create a store-products service and verify that you can access it from the busybox testing pod

1. Create a service for the store-products pods:

```
cat << EOF | kubectl apply -f -
kind: Service
apiVersion: v1
metadata:
  name: store-products
spec:
  selector:
    app: store-products
  ports:
  - protocol: TCP
    port: 80
    targetPort: 80
EOF
```

2. Make sure the service is up in the cluster:

```
kubectl get svc store-products
```

The output will look something like this:

```
NAME             TYPE        CLUSTER-IP      EXTERNAL-IP   PORT(S)   AGE
store-products   ClusterIP   10.104.11.230   <none>        80/TCP    59s
```

3. Use `kubectl exec` to query the store-products service from the busybox testing pod.

```
kubectl exec busybox -- curl -s store-products
```
