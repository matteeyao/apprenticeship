# Multi-Container Pods

## Learning objectives

- [ ] Ambassador

- [ ] Adapter

- [ ] Sidecar

## Overview

* Multi-container pods share the same network space, which means they can refer to each other as localhost

  * They have access to the same storage volumes; consequently, you do not have to establish volume sharing or services between the Pods to enable communication between them

## Create multi-container pod

* To create a multi-container pod, add the new container information to the pod-definition file

  * Recall that the containers section under the spec section in a pod definition file is an array, which allows multiple containers in a single pod

  * In this case, we add a new container named `log-agent` to our existing pod

* `cat pod-definition.yaml`

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: simple-webapp
  labels:
    name: simple-webapp
spec:
  containers:
  - name: simple-webapp
    image: simple-webapp
    ports:
      - containerPort: 8080

  - name: log-agent
    image: log-agent
```

## Design patterns

* There are 3 common patterns when it comes to designing multi-container pods

  1. Sidecar

  2. Adapter

  3. Ambassador

* `pod-definition.yaml` is an example of the sidecar pattern

  * A good example of a sidecar pattern is the deployment of a logging agent alongside a web server to collect logs and forward them to a central log server

## Adapter pattern use case

* Say we have multiple applications generating logs in different formats

  * It would be hard to process the various formats on the central logging server

* So, before sending the logs to the central server, we would like to convert the logs to a common format

  * For this, we deploy an adapter container

  * The adapter container processes the logs, before sending them to the central server

## Ambassador pattern use case

* Your application communicates to different database instances at different stages of development

  * A local database for development, one for testing, and another for production

* You must ensure to modify this connectivity depending on the environment you are deploying your application to

* You must choose to outsource such logic to a separate container within your pod, so that your application can always refer to a database at localhost, and the new container will proxy that request to the right database
