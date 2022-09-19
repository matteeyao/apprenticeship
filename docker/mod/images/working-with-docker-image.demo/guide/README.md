# Working with Docker Images

## Introduction

Time to put your Docker image skills to the test! In this lab, you will use your newfound knowledge of Docker images to pull, build, and launch containers.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

## Pull the Latest Alpine Image

1. Pull the latest `alpine` image from Docker Hub:

```
docker image pull alpine:latest
```

2. Confirm it is there with:

```
docker images
```

## Pull the Latest `httpd` Image

1. Pull the latest `httpd` image:

```
docker pull httpd:latest
```

> [!NOTE]
>
> If you don't put the version, the latest version is assumed.

## Pull nginx 1.15

1. Pull nginx version 1.15.

```
docker pull nginx:1.15
```

2. Confirm that it is there:

```
docker images
```

## Compare the History

1. Look at the history for both the `httpd` and `nginx` images:

```
docker history httpd
```

2. Note the following command gives an error because the latest version is assumed:

```
docker history nginx
```

3. When you specify the right version, the command succeeds:

```
docker history nginx:1.15
```
