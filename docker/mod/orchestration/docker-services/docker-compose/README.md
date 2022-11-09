# Docker Compose

Docker Compose is a tool that allows you to run multi-container applications defined using a declarative format. It is a tool used to manage complex, multi-container applications running on a single host.

> [!NOTE]
> 
> Docker Certified Associate primarily covers Docker compose as it relates to stacks in Docker swarm.

First, let's install Docker Compose:

```
sudo curl -L "https://github.com/docker/compose/releases/download/1.24.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

sudo chmod +x /usr/local/bin/docker-compose
```

Verify:

```
docker-compose version
```

## Getting Started

Docker Compose uses YAML files to declaratively define a set of containers and other resources that will be created as part of the larger application.

Setting up a new Docker compose project:

* Make a directory to contain your Docker Compose project.

```
cd ~/
mkdir nginx-compose
```

* Change to the project directory.

```
cd nginx-compose
```

* Add a `docker-compose.yml` file to the directory.

```
vi docker-compose.yml
```

* Define your application in `docker-compose.yml`. An example of a simple `docker-compose.yml`:

```yml
version: '3'
services:
  web:
    image: nginx
    ports:
    - "8080:80"
  redis:
    image: redis:alpine
```

* After creation, run the resources defined in `docker-compose.yml`. The `-d` works the same way it does in `docker run`, running the application in detached mode. Run an application using a compose file from the directory where the file is located:

```
docker-compose up -d
```


Lit containers/services that are currently running under Docker Compose. List compose applications:

```
docker-compose ps
```

Stop and remove all resources that were created using `docker-compose up` from the directory where the compose file is located:

```
docker-compose down
```
