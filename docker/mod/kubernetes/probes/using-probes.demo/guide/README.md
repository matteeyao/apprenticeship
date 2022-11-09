# Using Probes in Docker Kubernetes Service

## Introduction

Probes are a great tool for making some applications more resilient in Kubernetes. In this lab, you will have the opportunity to explore how to use probes to make Kubernetes react more quickly to application failures. This will help you design more robust applications in your Kubernetes clusters.

## Solution

1. In a web browser, navigate to the Docker Universal Control Plane (UCP):

```zsh
https://<UCP_MANAGER_PUBLIC_IP>
```

2. Click **Advanced** and proceed to the destination.

3. Login to Docker Enterprise using the following information:

* _Username_: "admin"

* _Password_: "<UCP_MANAGER_PASSWORD>"

### Get the Pod Definition

1. Click **Kubernetes**.

2. Click **Pods**.

3. Click the pod named, **webserver**.

4. Click the gear icon to edit the pod.

5. In the spec section of the _Object YAML_, make the following changes:

```yaml
...
terminationMessagePolicy: File
livenessProbe:
  httpGet:
    path: /
    port: 8080
  initialDelaySeconds: 3
  periodSeconds: 3
volumeMounts:
...
```

6. Copy the updated _Object YAML_ text and click the **Cancel**.

## Add a Probe to the Pod

1. On the right-side of the page, click the three dots, and then click **Remove**.

2. Click **Confirm**.

3. Once the pod has been terminated successfully, click **Create**.

4. For the _Namespace_, select **default**.

5. In the _Object YAML_ field, paste the updated YAML from the previous pod.

6. Once the pod is running successfully, verify that the probe was added by checking the _Containers Restart Count_ value.
