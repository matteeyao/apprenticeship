# Services and Deployments in Docker Kubernetes Service

## Introduction

Services and Deployments, when used together, are a great way to build and manage applications in Kubernetes. In this lab, you will have the opportunity to build an application around deployments and services in a Docker Kubernetes Service cluster. This will provide hands-on experience in architecting robust Kubernetes applications.

Your company recently purchased Docker Enterprise and is experimenting with using Docker Kubernetes Service to orchestrate some applications. They would like to create a simple Nginx server with three replica instances and expose it externally.

Your task is to build a deployment with three replicas in the Docker Enterprise Kubernetes cluster. You will then need to expose these containers externally on port `32768` using a service.

## Solution

1. Access UCP Manager at `https://<UCP_MANAGER_PUBLIC_IP>`. Since we use a self-signed certificate, choose to proceed to the site.

2. Log in to the UCP using these credentials:

    * Username: **admin**

    * Password: randomly-generated password associated with any of the lab servers

> [!NOTE]
> 
> Create Kubernetes objects by clicking **Kubernetes** ▶︎ **+ Create** in UCP.

## Create a Deployment to Manage the Application

1. Click **Kubernetes** ▶︎ **+ Create** in UCP.

2. Select the `default` namespace from the dropdown list.

3. Create the deployment using YAML. It should spin up three replica pods running the `nginx:1.19.1` image. The pods will need to publish a `containerPort` of `80` to expose Nginx externally:

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: nginx-deployment
spec:
  replicas: 3
  selector:
    matchLabels:
      app: nginx
  template:
    metadata:
      labels:
        app: nginx
    spec:
      containers:
      - name: nginx
        image: nginx:1.19.1
        ports:
        - containerPort: 80
```

4. Click create.

## Create a Service to Expose the Application Externally

1. Click **Kubernetes** ▶︎ **+ Create** in UCP.

2. Select the `default` namespace from the dropdown list.

3. Create the service using YAML. The service should map to a `targetPort` of `80`. Use a `nodePort` of `32768` to expose the service externally:

```yaml
apiVersion: v1
kind: Service
metadata:
  name: nginx-service
spec:
  type: NodePort
  selector:
    app: nginx
  ports:
    - protocol: TCP
      port: 80
      targetPort: 80
      nodePort: 32768
```

4. Click create.

5. To test, navigate to UCP:

```zsh
https://<UCP_MANAGER_PUBLIC_IP>:32768
```
