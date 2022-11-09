# Building a Private Docker Registry

## About this Lab 

Docker registries provide a powerful way to manage and distribute your Docker images. Docker offers a free registry at Docker Hub, but in many scenarios, you may want greater control of your images, not to mention that it is not free to have more than one private repository on Docker Hub. Fortunately, you can build and manage your own private registries, allowing you full control over where your images are housed and how they can be accessed.

In this lab, you will have the opportunity to work with a private registry. You will build your own private registry, and you will have a chance to practice some advanced setup to ensure that your private registry is secure. After completing this lab, you will know how to set up a simple but secure private Docker registry.

## Learning Objectives

[ ] Set up the private registry.

[ ] Test the registry from the Docker workstation server.

## Additional Resources

Your company has recently decided to use Docker to run containers in production. They have built some Docker images to run their own proprietary software and need a place to store and manage these images. You have been asked to build a secure, private Docker registry for use by the company. In order to verify that everything works, you have also been asked to configure a Docker workstation server to push to and pull from the registry.

To complete this lab, ensure that the following requirements are met for the registry:

* A private Docker registry is running on the Docker registry server using version `2.7` of the registry image.

* The container name for the registry should be `registry`.

* The registry should always automatically restart if it stops or the Docker daemon or server restarts.

* The registry should require authentication. Set up an initial account with the username `docker` and the password `d0ck3rrU73z`.

* The registry should use TLS with a self-signed certificate.

* The registry should listen on port `443`.

Set up the Docker workstation server to meet the following requirements:

* Docker is logged in to the private registry.

* Docker is configured to accept the self-signed cert. Do not turn off certificate verification using the `insecure-registries` setting.

* To confirm that everything is working, push a test image called `ip-10-0-1-101:443/test-image:1` to the private registry. You can pull any image from Docker hub and tag it with `ip-10-0-1-101:443/test-image:1` as a test.

* Delete the test image locally and pull it from the registry.
