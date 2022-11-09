# Configuring Applications in Docker Kubernetes Service

## Introduction

Application configuration is a necessary skill in any technology stack. In this lab, you will be able to practice your ability to configure containerized applications in Docker Kubernetes Service. This will familiarize you with the various methods of managing application configuration in Kubernetes.

## Solution

1. In a new browser tab, navigate to the UCP instance using `https://`, followed by the public IP address of the UCP Manager server.

2. If prompted by your browser, accept the certificate and proceed. (Advance --> Proceed to...)

3. Log in with the username `admin` and paste in the randomly generated password that is associated with this server in the lab interface.

4. When prompted to provide a license, select **Skip for now**.

### Create a ConfigMap to Contain the Nginx Configuration File

1. On the UCP Manager dashboard, select the **Kubernetes** option on the left, and then select **Create**.

2. Creating an object requires selecting a namespace, so click the **Namespace** dropdown and select **default**.

3. In the _Object YAML_ box, enter the following to create a ConfigMap called `nginx-config`. Beneath that, enter the following to give it a top-level key called `nginx.conf`:

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
    name: nginx-config
data:
    nginx.conf: |
        user  nginx;
        worker_processes  1;

        error_log  /var/log/nginx/error.log warn;
        pid        /var/run/nginx.pid;

        events {
          worker_connections  1024;
        }
        http {
            server {
                listen       80;
                listen  [::]:80;
                server_name  localhost;
                location / {
                    root   /usr/share/nginx/html;
                    index  index.html index.htm;
                }
                auth_basic "Secure Site";
                auth_basic_user_file conf/.htpasswd;
            }
        }
```

4. Click **Create**.

### Create a Secret to Contain the htpasswd Data

1. Back on the dashboard, click **Kubernetes** ▶︎ **Create**.

2. Under _Namespace_, select **default** from the dropdown menu.

3. Enter the following in the _Object YAML_ box to create a Secret called `nginx-htpasswd`:

```yml
apiVersion: v1
kind: Secret
metadata:
  name: nginx-htpasswd
type: Opaque
data:
```

4. Beneath that, enter the following to store the htpasswd data in that Secret in a key called `.htpasswd`:

```yml
...

data:
  .htpasswd: dXNlcjokYXByMSRUeVZGeXByUiRCWjFEbkVCZ3YuU0poVC50b3ZzZWkwCg==
```

5. Click **Create** to create the Secret.

### Create the Nginx Pod

1. Back on the dashboard, click **Kubernetes** ▶︎ **Create**.

2. Under _Namespace_, select **default** from the dropdown menu.

3. Enter the following in the _Object YAML_ box to create a Pod with a single container using the `nginx:1.19.1` image:

```yml
apiVersion: v1
kind: Pod
metadata:
  name: nginx
  labels:
    app: nginx
spec:
  containers:
  - name: nginx
    image: nginx:1.19.1
    ports:
    - containerPort: 80
    volumeMounts:
    - name: config-volume
      mountPath: /etc/nginx
    - name: htpasswd-volume
      mountPath: /etc/nginx/conf
  volumes:
  - name: config-volume
    configMap:
      name: nginx-config
  - name: htpasswd-volume
    secret:
      secretName: nginx-htpasswd
```

4. Click **Create**.

5. In a new browser tab, navigate to the public IP of the UCP Manager server with port number `:32768` appended to the end (`http://<UCP_MANAGER_PUBLIC_IP_ADDRESS>:32768`).

6. Log in with the following to see the Nginx welcome page:

    * _Username_: **user**

    * _Password_: **docker**
