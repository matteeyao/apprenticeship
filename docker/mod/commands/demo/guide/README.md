# Working with Docker Containers

In this lab, we create a containerized environment that runs a website on port `80`. When finished, we'll create an image of the container and ensure it will work as expected by launching a new container and mapping it to port `80` on the `localhost`.

Note that an nginx site configuration has been supplied at `/home/cloud_user/webfiles/default.conf`.

### Before we begin

To get started, we need to log in to our lab server using the provided lab credentials. Please use your preferred ssh client or the Instant Terminal to connect to the public IP of the lab provided instance using 'ssh cloud_user@[Public IP]'

## Create an Nginx Container

Use the `docker run` command to launch a new container based on the `nginx` image with the name `web`. Ensure the container is running while detached. To do so, complete the following:

1. Run docker:

```zsh
docker run --name web -dt nginx
```

2. Check that the container is running:

```zsh
docker container ls
```

3. Outside of docker, use `ls` too check that we have `webfiles`.

4. Use `ls webfiles/` and look for `default.conf`

5. Use `cat webfiles/default.conf` to check for the location of our expected webroots. In this lab, we're using `/var/www/html`.

## Configure Nginx

Create and configure the `var/www/` directory on the container:

1. Create the `/var/www/` directory on the container:

```zsh
docker exec web mkdir /var/www
```

2. Copy the default `nginx` configuration in `webfiles` to `/etc/nginx/conf.d`:

```zsh
docker cp webfiles/default.conf web:/etc/nginx/conf.d/default.conf
```

3. Move the `webfiles/html` documents to `/var/www/`:

```zsh
docker cp webfiles/html/ web:/var/www/
```

4. Check that the documentation transferred:

```zsh
docker exec web ls /var/www/html
```

5. Ensure the `nginx` user and group owns these directories:

```zsh
docker exec web chown -R nginx:nginx /var/www/html
```

6. Reload `docker`:

```zsh
docker exec web nginx -s reload
```

## Test and Publish the Website to Port 80

Test that the configuration was successful by trying to access the website on the container:

1. Get our IP address:

```zsh
docker inspect web | grep IPAddress
```

2. Copy the `IPAddress` output.

3. Use `curl <IPAddress>` and take a look at the `html` that appears.

4. Create a container for our web image:

```zsh
docker commit web web-image
```

5. Launch a new container called `web01` for the image:

```zsh
docker run -dt --name web01 -p 80:80 web-image
```

6. Check the `localhost` and make sure that we get the same information as we did when we used `curl` on the IP address:

```zsh
curl localhost
```

7. Use the IP address provided by the lab and enter it into your preferred web browser. You'll know that you've completed the lab completely when the webpage loads correctly.

8. W/ everything transferred to `web01` and our website working, we can get rid of the `web` container, as it is no longer needed. To do so, stop the container w/ `docker stop web`, and then remove it w/ `docker rm web`.
