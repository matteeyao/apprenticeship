# Creating Your Own Docker Image

## Introduction

Docker Hub provides many useful, pre-made images which you can use for a variety of applications. However, if you want to use Docker in the real world, you will likely be required to design and build your own Docker images, either to customize existing images or to run your own software.

In this lab, you will have the opportunity to work with Docker images by designing your own image to a set of specifications using a Dockerfile. You will then be able to run a container using your image to verify that it works.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

### Create a Dockerfile to define the image and build it

1. Change to the project directory and create a Dockerfile.

```
cd ~/fruit-list
vi Dockerfile
```

2. Build a Dockerfile that meets the provided specifications.

```Dockerfile
FROM nginx:1.15.8

ADD static/fruit.json /usr/share/nginx/html/fruit.json
ADD nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
```

3. Build the image.

```
docker build -t fruit-list:1.0.0 .
```

### Run a container with the image in detached mode and verify that it works

1. Run a container in detached mode using the newly-created image.

```
docker run --name fruit-list -d -p 8080:80 fruit-list:1.0.0
```

2. Make a request to the container and verify that you get some JSON with a list of fruits.

```
curl localhost:8080
```
