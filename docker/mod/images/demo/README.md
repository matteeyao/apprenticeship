# Building a Docker Image via Dockerfile

## About this Lab

When creating Docker images for websites, applications, and any service that may require any code change in the future, it's best to build in a way that can be quickly and easily rebuilt when any changes occur. Dockerfiles provide an in-platform way to do just that. In this lab, we'll be building a Dockerfile that can generate an image of our website that will make sure that when changes happen with the website code, we won't have to change the Dockerfile itself.

## Learning Objectives

[ ] Create the Dockerfile

[ ] Test the Image

## Lab Overview

Your company's landing page needs to be Dockerized!

* Use Alpine Linux container

* Nginx web server

* Website files and Nginx configuration provided

1. **Write the Dockerfile** ▶︎ The Dockerfile should define the parent image, install Nginx, then move the configuration and website files to the desired locations.

2. **Build the Image** ▶︎ Build an image using the Dockerfile; tag the image w/ the name `web`.

3. **Launch a Container** ▶︎ Launch a `web01` container using the new image; map port 80 on the container to 80 on the host.
