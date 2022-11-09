# Using Storage in Docker Kubernetes Service

## Introduction

Kubernetes has the ability to provide external storage for your containerized applications. In this lab, you will be able to work with Kubernetes storage hands-on, designing your own solution to a real-world problem using Kubernetes features. This will help you familiarize yourself with Kubernetes features, such as volumes, and PersistentVolumes.

## Solution

1. In a web browser, navigate to the Docker Universal Control Plane (UCP):

```
https://<UCP_MANAGER_PUBLIC_IP>
```

2. Click **Advanced** and proceed to the destination.

3. Login to Docker Enterprise using the following information:

* _Username_: "admin"

* _Password_: "<UCP_MANAGER_PASSWORD>"

### Create a Persistent Volume

1. Click **Kubernetes**.

2. Click **Create**.

3. For the _Namespace_, select **default**.

4. In the _Object YAML_ field, paste the following:

```yaml
apiVersion: storage.k8s.io/v1
kind: StorageClass
metadata:
  name: localdisk
provisioner: kubernetes.io/no-provisioner
allowVolumeExpansion: true
```

> [!NOTE]
> 
> You will need to create and use a custom StorageClass that supports volume resizing since you will need to resize the `PersistentVolumeClaim` later. For this exercise, you can use the provisioner `kubernetes.io/no-provisioner`.

5. Click **Create**.

6. Click **Kubernetes** a second time, and click **Create**.

7. For the _Namespace_, select **default**.

8. In the _Object YAML_ field, paste the following to create the `PersistentVolume`:

```yaml
kind: PersistentVolume
apiVersion: v1
metadata:
  name: test-pv
spec:
  storageClassName: localdisk
  capacity:
    storage: 1Gi
  accessModes:
    - ReadWriteOnce
  hostPath:
    path: /etc/output
```

9. Click **Create**.

### Create a Persistent Volume Claim

1. Click **Kubernetes**.

2. Click **Create**.

3. For the _Namespace_, select **default**.

4. In the Object YAML field, paste the following:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata: 
  name: test-pvc
spec:
  storageClassName: localdisk
  accessModes:
    - ReadWriteOnce
  resources: 
    requests: 
      storage: 100Mi
```

5. Click **Create**.

### Create a Pod That Will Output Data to the Persistent Volume

1. Click **Kubernetes**.

2. Click **Create**.

3. For the _Namespace_, select **default**.

4. In the _Object YAML_ field, paste the following:

```yaml
apiVersion: v1
kind: Pod
metadata: 
  name: test-pod
spec: 
  containers: 
  - name: busybox
    image: busybox
    command: ['sh', '-c', 'while true; do echo "Successfully written to log." >> /output/output.log; sleep 10; done']
    volumeMounts:
    - name: pv-storage
      mountPath: /output
  volumes:
  - name: pv-storage
    persistentVolumeClaim:
      claimName: test-pvc
```

5. Click **Create**.

### Resize the Persistent Volume Claim

1. Under _Storage_, select `test-pvc`.

2. In the upper right-hand corner of the page, click **Edit**.

3. Increase the `storage` size of the `PersistentVolumeClaim` to `200Mi`.

4. Click **Save**.
