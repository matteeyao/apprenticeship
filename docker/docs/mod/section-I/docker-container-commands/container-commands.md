# Common Docker Container Commands

Docker CLI commands usually follow the format of `docker <COMMAND> <SUBCOMMAND>`

A perfect example of this is `docker container(COMMAND) run(SUBCOMMAND)`

* `Docker --help`

    * Lists out all the options available to you

* `Docker run [OPTIONS] IMAGE[:TAGNUMBER] [COMMAND]`

    * Check out the Docker [run](https://docs.docker.com/engine/reference/run/) documentation for a list of options and flags 

* `Docker container ls`

    * Lists all your running containers 

* `Docker container ls -a`

    * Lists all your containers, running or stopped

* `docker container stats`

    * W/ no provided container name to get live performance data

* `docker container inspect <CONTAINERNAME>`

    * Will return json with the metadata about that specific container

* `docker container top <CONTAINERNAME>`

    * Display the running processes of a container

* `docker container rm <CONTAINERNAME>`

    * Remove one or more stopped containers

* `docker container rm -f <CONTAINERNAME>`

    * Stop and remove a running container

* `docker container run -it <IMAGENAME> bash`

    * For running interactive processes (like a shell in this instance)

* `docker container run exec`

    * Run a command in a running container
