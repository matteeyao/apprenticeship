# Building a Private Docker Registry

Docker registries provide a powerful way to manage and distribute your Docker images. Docker offers a free registry at Docker Hub, but in many scenarios, you may want greater control of your images, not to mention that it is not free to have more than one private repository on Docker Hub. Fortunately, you can build and manage your own private registries, allowing you full control over where your images are housed and how they can be accessed.

In this lab, you will have the opportunity to work with a private registry. You will build your own private registry, and you will have a chance to practice some advanced setup to ensure that your private registry is secure. After completing this lab, you will know how to set up a simple but secure private Docker registry.

## Solution

Begin by logging in to the lab servers using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

It is a good idea to log in to both servers at the same tab in separate tabs in your terminal application.

### Set up the private registry

1. In the **Registry** server, create an htpasswd file containing the login credentials for the initial account.

```
mkdir -p ~/registry/auth

docker run --entrypoint htpasswd \
  registry:2.7.0 -Bbn docker d0ck3rrU73z > ~/registry/auth/htpasswd
```

2. Create a directory to hold the certs for the registry server

```
mkdir -p ~/registry/certs
```

3. Create a self-signed certificate for the registry.

> [!NOTE]
> 
> For the _Common Name_ field, enter the hostname of the registry server, which is `ip-10-0-1-101`. For the other prompts, just hit **enter** to accept the default value.

```
openssl req \
  -newkey rsa:4096 -nodes -sha256 -keyout ~/registry/certs/domain.key \
  -x509 -days 365 -out ~/registry/certs/domain.crt
```

4. Create a container to run the registry.

```
docker run -d -p 443:443 --restart=always --name registry \
  -v /home/cloud_user/registry/certs:/certs \
  -v /home/cloud_user/registry/auth:/auth \
  -e REGISTRY_HTTP_ADDR=0.0.0.0:443 \
  -e REGISTRY_HTTP_TLS_CERTIFICATE=/certs/domain.crt \
  -e REGISTRY_HTTP_TLS_KEY=/certs/domain.key \
  -e REGISTRY_AUTH=htpasswd \
  -e "REGISTRY_AUTH_HTPASSWD_REALM=Registry Realm" \
  -e REGISTRY_AUTH_HTPASSWD_PATH=/auth/htpasswd \
  registry:2.7.0
```

5. Once the registry starts up, verify that it is responsive. It's OK if this command returns nothing, just make sure it does not fail.

```
curl -k https://localhost:443
```

### Test the registry from the Docker workstation server

1. Get the public hostname from the registry server. It should be `ip-10-0-1-101`.

```
echo $HOSTNAME
```

2. On the **Workstation** server, add the registry's public self-signed certificate to `/etc/docker/certs.d`. The `scp` command is copying the file from the registry server to the workstation. The password is the normal `cloud_user` password provided by the lab.

> [!NOTE]
>
> The following steps should be completed from the Workstation server.

```
sudo mkdir -p /etc/docker/certs.d/ip-10-0-1-101:443
sudo scp cloud_user@ip-10-0-1-101:/home/cloud_user/registry/certs/domain.crt /etc/docker/certs.d/ip-10-0-1-101:443
```

3. Log in to the private registry from the workstation. The credentials should be username `docker` and password `d0ck3rrU73z`.

```
docker login ip-10-0-1-101:443
```

4. Test the registry by pushing an image to it. You can pull any image from Docker hub and tag it appropriately to push it to the registry as a test image.

```
docker pull ubuntu
docker tag ubuntu ip-10-0-1-101:443/test-image:1
docker push ip-10-0-1-101:443/test-image:1
```

5. Verify image pulling by deleting the image locally and re-pulling it from the private repository.

```
docker image rm ip-10-0-1-101:443/test-image:1
docker image rm ubuntu:latest
docker pull ip-10-0-1-101:443/test-image:1
```
