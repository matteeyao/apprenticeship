# Securing the Docker Daemon HTTP Socket

Docker uses a socket that is not exposed to the network by default.

However, you can configure Docker to listen on an HTTP port, which you can connect to in order to remotely manage the daemon.

In order to do this securely, we need to:

* Create a certificate authority.

* Create server and client certificates.

* Configure the daemon on the server to use `tlsverify` mode.

* Configure the client to connect securely using the client certificate.

Playground server setup:

* 2 playground servers (Client and Server)

* Image - Ubuntu 18.04 Bionic Beaver LTS

* Size - Micro

## Create a certificate authority.

Generate key for certificate authority

```zsh
openssl genrsa -aes256 -out ca-key.pem 4096
```

In order to generate the server certificate, we need to provide some additional information in the form of a certificate signing request:

```zsh
openssl req -subj "/CN=$HOSTNAME" -sha256 -new -key server-key.pem out server.csr
```

Create a configuration file `extfile.cnf`:

```zsh
# echo subjectAltName = DNS:$HOSTNAME,IP:<SERVER_PRIVATE_IP>,IP.127.0.0.1 >> extfile.cnf
echo subjectAltName = DNS:$HOSTNAME,IP:172.31.99.16,IP:127.0.0.1 >> extfile.cnf

echo extendedKeyUsage = serverAth >> extfile.cnf
```

Generate server certificate:

```zsh
openssl x509 -req -days 365 -sha256 -in server.csr -CA ca.pem -CAkey ca-key.pem \
  -CAcreateserial -out server-cert.pem -extfile extfile.cnf
```

## Create client certificates.

Create another key:

```zsh
openssl genrsa -out key.pem 4096
```

Create another certificate signing request file:

```zsh
openssl req -subj '/CN=client' -new -key key.pem -out client.csr
```

Create another config file:

```zsh
echo extendedKeyUsage = clientAuth > extfile-client.cnf
```

Generate client certificate:

```zsh
openssl x509 -req -days 365 -sha256 -in client.csr -CA ca.pem -CAkey ca-key.pem \
  -CAcreateserial -out cert.pem -extfile extfile-client.cnf
```

Set permissions on key files:

```zsh
chmod -v 0400 ca-key.pem key.pem server-key.pem
```

```zsh
chmod -v 0444 ca.pem server-cert.pem cert.pem
```

## Configure the daemon on the server to use `tlsverify` mode.

Now, we are ready to go ahead and configure our Docker host to utilize these client files and to expose that socket externally so that we can access it remotely.

```zsh
sudo vi /etc/docker/daemon.json
```

```json
{
  "tlsverify": true,
  "tlscacert": "/home/cloud_user/ca.pem",
  "tlscert": "/home/cloud_user/server-cert.pem",
  "tlskey": "/home/cloud_user/server-key.pem"
}
```

Edit docker daemon unit file:

```zsh
sudo vi /lib/systemd/system/docker.service
```

Revise line `ExecStart=/usr/bin/dockerd -H 0.0.0.0:2376 --containerd=/run/containerd/containerd.sock`

```zsh
sudo systemctl daemon-reload

sudo systemctl restart docker
```

## Configure the client to connect securely using the client certificate.

```zsh
scp ca.pem cert.pem key.pem cloud_user@<PLAYGROUND_SERVER_PRIVATE_IP_ADDRESS>:/home/cloud_user
```

Navigate to the client and you should see files `ca.pem`, `cert.pem`, `key.pem` copied over from the server.

Move those files into a home directory `/.docker`:

```zsh
mkdir -pv ~/.docker
```

Copy those files:

```zsh
cp -v {ca,cert,key}.pem ~/.docker/
```

Now, we just need to configure our local Docker client to connect to the daemon on our Docker server. To do that, we can just set some environment variables:

```zsh
export DOCKER_HOST=tcp://172.31.99.16:2376 DOCKER_TLS_VERIFY=1
```

`docker version` should work, coming from the remote Docker daemon.

## Learning Summary

1. Generate a certificate authority and server certificates for the Docker server. We must make sure that we
   replace <SERVER_PRIVATE_IP> with the actual private IP of our server:

```zsh
openssl genrsa -aes256 -out ca-key.pem 4096
openssl req -new -x509 -days 365 -key ca-key.pem -sha256 -out ca.pem -subj \
  "/C=US/ST=Texas/L=Keller/O=Linux Academy/OU=Content/CN=$HOSTNAME" \
  openssl genrsa -out server-key.pem 4096
openssl req -subj "/CN=$HOSTNAME" -sha256 -new -key server-key.pem -out server.csr \
  echo subjectAltName = DNS:$HOSTNAME,IP:<server private IP>,IP:127.0.0.1 >> extfile.cnf
echo extendedKeyUsage = serverAuth >> extfile.cnf
openssl x509 -req -days 365 -sha256 -in server.csr -CA ca.pem -CAkey ca-key.pem \
  -CAcreateserial -out server-cert.pem -extfile extfile.cnf
```

2. Generate client certificates:

```zsh
openssl genrsa -out key.pem 4096
openssl req -subj '/CN=client' -new -key key.pem -out client.csr
echo extendedKeyUsage = clientAuth > extfile-client.cnf
openssl x509 -req -days 365 -sha256 -in client.csr -CA ca.pem -CAkey ca-key.pem \
  -CAcreateserial -out cert.pem -extfile extfile-client.cnf
```

3. Set appropriate permissions on the certificate files:

```zsh
chmod -v 0400 ca-key.pem key.pem server-key.pem chmod -v 0444 ca.pem server-cert.pem cert.pem
```

4. Configure the Docker host to use `tlsverify` mode with the certificates created earlier:

```zsh
sudo vi /etc/docker/daemon.json
```

```json
{
  "tlsverify": true,
  "tlscacert": "/home/cloud_user/ca.pem",
  "tlscert": "/home/cloud_user/server-cert.pem",
  "tlskey": "/home/cloud_user/server-key.pem"
}
```

```zsh
sudo vi /lib/systemd/system/docker.service
```

5. Look for the line that begins with `ExecStart` and change the `-H` so that it looks like this:

```zsh
ExecStart=/usr/bin/dockerd -H=0.0.0.0:2376 --containerd=/run/containerd/containerd.sock
```

```zsh
sudo systemctl daemon-reload
sudo systemctl restart docker
```

6. Copy the CA cert and client certificate files to the client machine:

```zsh
scp ca.pem cert.pem key.pem cloud_user@:/home/cloud_user
```

7. On the client machine, configure the client to securely connect to the remote Docker daemon:

```zsh
mkdir -pv ~/.docker
cp -v {ca,cert,key}.pem ~/.docker
export DOCKER_HOST=tcp://<docker server private \
  IP>:2376 DOCKER_TLS_VERIFY=1
```

8. Finally, we should test the connection:

```zsh
docker version
```
