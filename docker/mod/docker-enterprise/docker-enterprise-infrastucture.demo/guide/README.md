# Building a Docker Enterprise Infrastructure with Mirantis Launchpad

## Introduction

Docker Enterprise is a complex tool with many components available for use. Luckily, Mirantis Launchpad makes the installation process for Docker EE clusters easier. In this lab, you will have the opportunity to build your own multi-server Docker EE cluster using Launchpad.

## Solution

### Install Mirantis Launchpad

1. Download and install Mirantis Launchpad on the UCP Manager server using launchpad version 1.14.0:

```zsh
wget https://github.com/Mirantis/launchpad/releases/download/0.14.0/launchpad-linux-x64
```

2. Rename the Launchpad Linux x64 file to "launchpad":

```zsh
mv launchpad-linux-x64 launchpad
```

3. Make the `launchpad` file executable:

```zsh
chmod +x launchpad
```

4. View the Launchpad version number:

```zsh
./launchpad version
```

5. Register the cluster:

```zsh
./launchpad register
```

6. When prompted, enter in your name, email, and company name.

7. Type "Y" to accept the license agreement.

### Generate Certificates for DTR

1. Generate a certificate using open SSL:

```zsh
openssl genrsa -out ca.key 4096
```

2. Generate a public key for Docker Trusted Registry by including a simple subject `"/OU=dtr/CN=DTR CA"` and passing in the private key file `ca.key`:

```zsh
openssl req -x509 -new -nodes -key ca.key -sha256 -days 1024 -subj "/OU=dtr/CN=DTR CA" -out ca.crt
```

3. Disregard any warning message that may occur.

4. Generate a server private key for the certificate:

```zsh
openssl genrsa -out dtr.key 2048
```

5. Generate a public certificate for the server by creating a certificate signing request, passing in the private key `dtr.key`, and including a simple subject `"/OU=dtr/CN=system:dtr"` and output `dtr.csr`:

```zsh
openssl req -new -sha256 -key dtr.key -subj "/OU=dtr/CN=system:dtr" -out dtr.csr
```

6. Add some certificate extension values by creating the file:

```zsh
vi extfile.cnf
```

7. Specify the certificate extension values, substituting your provided public IP address for `<DTR_PUBLIC_IP_SERVER>`:

```zsh
keyUsage = critical, digitalSignature, keyEncipherment
basicConstraints = critical, CA:FALSE
extendedKeyUsage = serverAuth, clientAuth
subjectAltName = IP:<DTR_PUBLIC_IP_SERVER>, IP:10.0.1.103,IP:127.0.0.1
```

8. Save and exit the certificate extension file:

```zsh
wq
```

9. Generate the public certificate for the server, passing in the certificate signing request `dtr.csr`, the `ca.crt` certificate and `ca.key` key, the certificate extension configuration `extfile.cnf`, and output `dtr.crt`:

```zsh
openssl x509 -req -in dtr.csr -CA ca.crt -CAkey ca.key -CAcreateserial -out dtr.crt -days 365 -sha256 -extfile extfile.cnf
```

### Create the Cluster

1. Create a cluster configuration file:

```zsh
vi cluster.yaml
```

2. For the DTR certificate data, copy in the contents of each respective certificate file. Ensure that the indentation is correct for the yaml format. Each new line of the certificate data should be indented equal to the `|-` at the top of each certificate section.

```yaml
apiVersion: launchpad.mirantis.com/v1beta3
kind: DockerEnterprise
metadata:
  name: launchpad-ucp
spec:
  ucp:
    version: 3.3.2
    installFlags:
    - --admin-username=admin
    - --admin-password=secur1ty!
    - --default-node-orchestrator=kubernetes
    - --force-minimums
  dtr:
    version: 2.8.2
    installFlags:
    - --ucp-insecure-tls
    - |-
      --dtr-cert "<contents of dtr.crt>"
    - |-
      --dtr-key "<contents of dtr.key>"
    - |-
      --dtr-ca "<contents of ca.crt>"
  hosts:
  - address: 10.0.1.101
    privateInterface: ens5
    role: manager
    ssh:
      user: launchpad
      keyPath: ~/launchpad_id
  - address: 10.0.1.102
    privateInterface: ens5
    role: worker
    ssh:
      user: launchpad
      keyPath: ~/launchpad_id
  - address: 10.0.1.103
    privateInterface: ens5
    role: dtr
    ssh:
      user: launchpad
      keyPath: ~/launchpad_id
```

3. Save and exit this file:

```zsh
wq
```

4. Then look at the certificate file:

```zsh
cat dtr.crt
```

5. Copy the entire contents of this file, including the "BEGIN CERTIFICATE" and "END CERTIFICATE" lines.

6. Edit the `cluster.yaml` file:

```zsh
vi cluster.yaml
```

7. Paste in the entire contents into the `dtr-cert "<CONTENTS_OF_DTR.CRT>"` line, replacing the "CONTENTS_OF_DTR.CRT" placeholder between the quotes. Because this is a yaml file, the indentation will have to be fixed so that every line lines up with the `--dtr-cert` line.

8. Insert the data for the `dtr-key` and `dtr-ca` in the same way, using the `cat` command and copy-pasting the data into the `cluster.yaml` file:

```zsh
vi cluster.yaml
```

9. Save and exit the `cluster.yaml` file:

```zsh
wq
```

10. Then create the cluster itself using Launchpad:

```zsh
./launchpad apply
```

11. To access your UCP instance, open a web browser and navigate to `https://` followed by the public IP address of the UCP Manager server. You may have to manually allow the self-signed certificate on your browser.

12. Log in to the UCP login page using the username and password supplied in your `cluster.yaml` file.

13. When asked to provide a license, select **Skip For Now**. From here, check that the UCP Manager interface appears and UCP is running.

14. Ensure that Docker Trusted Registry is also working by navigating to `https://` followed by the public IP address of your DTR server. You may have to accept the self-signed certificate again.

15. On the Docker Trusted Registry login page, use the same credentials that you used to log in to UCP.

16. Select **Skip For Now** for the license.

17. Check the Docker Trusted Registry interface and see if your Docker EE cluster setup was successful.
