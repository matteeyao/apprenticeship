# Executors and Images

Each separate job defined within your config will run in a unique executor. An executor can be a docker container or a virtual machine running Linux, Windows, or MacOS

```yml
version: 2.1

jobs:
  build1: # job name
    docker: # Specifies the primary container image,
      - image: buildpack-deps:trusty
        auth:
          username: mydockerhub-user
          password: $DOCKERHUB_PASSWORD  # context / project UI env-var reference
      - image: postgres:9.4.1 # Specifies the database image
        auth:
          username: mydockerhub-user
          password: $DOCKERHUB_PASSWORD  # context / project UI env-var reference
      # for the secondary or service container run in a common
      # network where ports exposed on the primary container are
      # available on localhost.
        environment: # Specifies the POSTGRES_USER authentication
          # environment variable, see circleci.com/docs/2.0/env-vars/
          # for instructions about using environment variables.
          POSTGRES_USER: root
#...
  build2:
    machine: # Specifies a machine image that uses
      # an Ubuntu version 20.04 image with Docker 19.03.13
      # and docker-compose 1.27.4, follow CircleCI Discuss Announcements
      # for new image releases.
      image: ubuntu-2004:202010-01
#...
  build3:
    macos: # Specifies a macOS virtual machine with Xcode version 11.3
      xcode: "11.3.0"
# ...
```

The Primary Container is defined by the first image listed in `.circleci/config.yml` file. This is where commands are executed. The Docker executor spins up a container w/ a Docker image. The machine executor spins up a complete Ubuntu virtual machine image

When using the docker executor and running docker commands, the `setup_remote_docker` key an be used to spin up another docker container in which to run these commands, for added security
