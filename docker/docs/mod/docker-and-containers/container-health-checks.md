# Container Health Checks

## Overview

Health Checks = a way of checking the health of some resource

In the case of Docker, a `HEALTHCHECK` is a cmd used to determine the health of a running container

Docker Container Health Checks are supported in a Dockerfile, the `docker container run` cmd, Docker Compose, and in Docker Swarm

The `HEALTHCHECK` instruction tells Docker how to test a container to check that it is still working

A good health check can detect cases such as a web server that is stuck in an infinite loop and unable to handle new connections, even though the server process is still running

## Why you should Health Check

