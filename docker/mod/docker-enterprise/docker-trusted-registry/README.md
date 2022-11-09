# Docker Trusted Registry (DTR)

**Docker Trusted Registry (DTR)** is an enhanced, enterprise-ready private Docker registry.

Additional features:

* A web UI

* High availability through multiple registry nodes ▶︎ multiple DTR nodes in a cluster that can form a highly available infrastructure.

* Cache images geographically close to users

* Role-based access control, providing granular access to our Docker images

* Security vulnerability scanning for images ▶︎ automatically scan your Docker images to determine if your software contains security vulnerabilities.

## DTR high availability

Docker Trusted Repository supports **high availability** by allowing you to run multiple DTR replicas in a cluster.

A DTR cluster can tolerate failures as long as there is a **quorum**.

**Quorum**: more than half of the replicas are available.

## DTR Cache

**DTR Caches** allow you to cache images geographically close to your users, allowing for faster download speeds.

You should know:

* Each cache pulls the image from the main DTR the first time a user requests the image from that cache.

* Users still authenticate using the main DTR URL.

* Users request images from the main DTR URL, and DTR responds w/ a redirect to the cache.

* A user profile setting determines which cache a user will pull images from.

* This means authentication and image pulling are completely **transparent** to users; they do not need to think about caches.

## Setting Up Docker Trusted Registry (DTR)

You can install and configure **Docker Trusted Registry (DTR)** using Mirantis Launchpad.

Create a certificate authority (CA):

```zsh
openssl genrsa -out ca.key 4096
```

Create CA certificate:

```zsh
openssl req -x509 -new -nodes -key ca.key -sha 256 -days 1024 -subj "/OU=dtr/CN=DTR CA" -out ca.crt
```

W/ certificate authority in hand, we're ready to go ahead and create the server certificates for Docker Trusted Registry.

Create the private key for our DTR server certificate:

```zsh
openssl genrsa -out dtr.key 2048
```

Create certificate signing request:

```zsh
openssl req -new -sha256 -key dtr.key -subj "/OU/dtr/CN=system:dtr" -out dtr.csr
```

Create a file called `extfile.cnf`, a temporary configuration file that will contain certificate extension data:

```zsh
vi extfile.cnf
```

```bash
keyUsage = critical, digitalSignature, keyEncipherment
basicConstraints = critical, CA:FALSE
extendedKeyUsage = serverAuth, clientAuth
subjectAltName = DNS:<DTR_SERVER_HOSTNAME>,IP:<DTR_SERVER_PRIVATE_IP>,IP:127.0.0.1
```

Generate public key:

```zsh
sudo openssl x509 -req -in dtr.csr -CA ca.crt -CAkey ca.key -CAcreateserial -out dtr.crt -days 365 -sha256 -extfile ext file.cnf
```

Ensure file `dtr.crt` is owned by `cloud_user` appropriately.

```zsh
sudo chown cloud_user:cloud_user dtr.crt
```

Edit `cluster.yml`, adding in DTR server into the file and edit configuration for DTR.

Copy entire contents of file `dtr.crt`, pasting all the data into `--dtr-cert` in `cluster.yml`:

```zsh
cat dtr.crt
```

Same thing for files `dtr.key` and `ca.crt`.

1. To install DTR, in the Universal Control Plane interface, go to **admin**.

2. Go to **Admin Settings**, and then **Docker Trusted Registry**.

3. Under `UCP Node`, select the worker node where we want to install DTR.

4. Check the checkbox labeled `Disable TLS verification for UCP`.

5. Copy the command provided on the page, then use a text editor to change the `--ucp-url` to specify the **Private IP** of the UCP Manager server, not the public IP.

6. Run the modified command on the worker node at the desired location for installing DTR. The command should look like this:

```zsh
docker run -it --rm docker/dtr install \
  --ucp-node <DTR_NODE_HOSTNAME> \
  --ucp-username admin \
   --ucp-url https://<UCP Manager private IP> \
  --ucp-insecure-tls
```

7. When prompted for a password, enter the UCP admin password.

8. Once the installation is complete, access DTR in a browser at `https://<DTR_SERVER_PUBLIC_IP>`.

9. Log in using the UCP admin credentials.
