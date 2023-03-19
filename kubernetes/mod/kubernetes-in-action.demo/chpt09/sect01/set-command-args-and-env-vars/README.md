# Setting the command, arguments, and environment variables

* Like regular applications, containerized applications can be configured using command-line arguments, environment variables, and files

* You learned that the command that is executed when a container starts is typically defined in the container image

  * The command is configured in the container's Dockerfile using the `ENTRYPOINT` directive, while the arguments are typically specified using the `CMD` directive

  * Environment variables can als be specified using the `ENV` directive in the Dockerfile

  * If the application is configured using configuration files, these can be added to the container image using the `COPY` directive

  * You've seen several examples of this in the previous chapters

* Let's take the Kiada application and make it configurable via command-line arguments and environment variables

  * The previous versions of the application all listen on port 8080

  * This is now configurable via the `--listen-port` command line argument

  * Also, the application will read the initial status message from the environment variable `INITIAL_STATUS_MESSAGE`

  * Instead of just returning the hostname, the application now also returns the pod name and IP address, as well as the name of the cluster node on which it is running

  * The application obtains this information through environment variables

  * The container image for this new version is available at [docker.io/luksa/kiada:0.4](../../kiada-0.4/Dockerfile)

* The updated Dockerfile, which you can also find in the code repository, is shown in the following listing ▶︎ A sample Dockerfile using several application configuration methods:

```dockerfile
FROM node:12
COPY app.js /app.js
COPY html/ /html

ENV INITIAL_STATUS_MESSAGE="This is the default status message"     # ← A

ENTRYPOINT ["node", "app.js"]                                       # ← B
CMD ["--listen-port", "8080"]                                       # ← C

# ← A ▶︎ Set an environment variable
# ← B ▶︎ Set the command to run when the container is started
# ← C ▶︎ Set the default command-line arguments
```

* Hardcoding and configuration into the container image is the same as hardcoding it into the application source code

  * This is not ideal b/c you must rebuild the image every time you change the configuration

* Safer to store sensitive configuration data such as security credentials or encryption keys in a volume that you mount in the container

  * One way to do this is to store the files in a persistent volume

  * Another way is to use an `emptyDir` volume and an init container that fetches the files from secure storage and writes them to the volume

* In this chapter, you'll learn how to use special volume types to achieve the same result w/o using init containers

  * But first, let's learn how to change the command, arguments, and environment variables w/o recreating the container image

## Setting the command and arguments

▶︎ See [9.1.1](set-command-and-args/README.md)

## Setting environment variables in a container

▶︎ See [9.1.2](set-env-vars-in-container/README.md)
