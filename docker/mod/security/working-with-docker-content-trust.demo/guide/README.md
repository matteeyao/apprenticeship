# Working with Docker Content Trust

## Introduction

In this lab, we will work with Docker Content Trust (DCT) by signing a previously unsigned image and running it on a system that has DCT enabled.

## Solution

Log in to the Docker server using the credentials provided on the hands-on lab page:

```zsh
ssh cloud_user@<DOCKER_SERVER_PUBLIC_IP_ADDRESS>
```

### Generate a trust key and add yourself as a signer to the new repository

1. Generate a trust key:

```zsh
docker trust key generate docker
```

2. Create a new passphrase for your key when prompted. (Make sure it's easy to remember, as we'll need it later.)

3. Add yourself as a signer to the `ip-10-0-1-102:443/content-dca-tea` repository:

```zsh
docker trust signer add --key docker.pub docker ip-10-0-1-102:443/content-dca-tea
```

4. Create passphrases for the new root key and new repository key when prompted.

### Create a new tag for the image, sign it, and push it to the registry

1. Create a new tag for the image:

```zsh
docker tag linuxacademycontent/content-dca-tea:1 ip-10-0-1-102:443/content-dca-tea:1
```

2. Sign the image and push it to the registry:

```zsh
docker trust sign ip-10-0-1-102:443/content-dca-tea:1
```

3. When prompted, enter the first passphrase you created earlier (the one for the trust key).

4. Verify you can run the signed image:

```zsh
docker run -d -p 8080:80 ip-10-0-1-102:443/content-dca-tea:1
```

5. To test the image further, query the tea list web service:

```zsh
curl localhost:8080
```

We should see generated JSON data that contains a list of the various kinds of tea.
