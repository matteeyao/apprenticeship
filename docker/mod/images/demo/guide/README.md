# Building a Docker Image via Dockerfile

When creating Docker images for websites, applications, and any service that may require any code change in the future, it's best to build in a way that can be quickly and easily rebuilt when any changes occur. Dockerfiles provide an in-platform way to do just that. In this lab, we'll be building a Dockerfile that can generate an image of our website that will make sure that when changes happen with the website code, we won't have to change the Dockerfile itself!

## Solution

Log in to the lab server using the credentials provided:

```zsh
ssh cloud_user@<PUBLIC_IP_ADDRESS>
```

## Create the Dockerfile

1. List out the information in our directory:

```zsh
ls
```

2. Change directory to `containerhub`:

```zsh
cd containerhub
```

3. List out the information in `containerhub`:

```zsh
ls
```

4. List out the information in from the `files` document:

```zsh
ls files/
```

We will see our `default.conf` file.

5. See where our information is stored:

```zsh
cat files/default.conf
```

We will find it is in `/var/www/html/`.

6. Create the Dockerfile:

```zsh
vim Dockerfile
```

7. Build the file based on the `alpine` image:

```Dockerfile
FROM alpine:3.13
RUN apk upgrade
RUN apk add nginx
COPY files/default.conf /etc/nginx/conf.d/default.conf
RUN mkdir -p /var/www/html
WORKDIR /var/www/html
COPY --chown=nginx:nginx /files/html/ .
EXPOSE 80
CMD [ "nginx", "-g", "pid /tmp/nginx.pid; daemon off;" ]
```

8. Save and exit the document by pressing **Escape** followed by `:wq`.

## Build and Test the Image

1. Build the image:

```zsh
docker build . -t web
```

2. Check that we have the `web` image:

```zsh
docker image ls
```

3. Launch Docker based on the `web` image:

```zsh
docker run -dt -p 80:80 --name web01 web
```

4. Once it is finished running, check that the website has been launched:

```zsh
curl localhost
```

We will get the HTML for the website.

5. Copy the public IP address provided on the lab page, and paste it into a new browser tab. We'll know it worked correctly when the Container Hub website appears.
