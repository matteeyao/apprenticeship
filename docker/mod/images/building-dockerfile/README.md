# Building a Dockerfile

```zsh
wget --content-disposition 'https://github.com/linuxacademy/content-Introduction-to-Containers-and-Docker/raw/master/lessonfiles/demo-app.tar'
tar -xf demo-app.tar
```

## Dockerfile Breakdown

1. **Define parent image** ▶︎ Use the `FROM` instruction to set the parent image to the `node:10-alpine` Node.JS 10 Alpine image.

2. **Create directory structure** ▶︎ Use `RUN` to create the `/home/node/app/node_modules` directory and set `node` as the owner/group.

3. **Move directories** ▶︎ Use `WORKDIR` to set `/home/node/app` as the active directory.

4. **Copy files** ▶︎ Use `COPY` to move the `package*.json` metadata files to the working directory.

5. **Configure the `npm` registry** ▶︎ Use `RUN` to set the `npm` registry via the `npm config set registry` command.

6. **Install required packages** ▶︎ Use `RUN` to have `npm` install any prerequisite packages for our application.

7. **Copy more files** ▶︎ Use `COPY` to copy over th remaining files from the Dockerfile's working directory.

8. **Switch users** ▶︎ Use `USER` to switch to the `node` user.

9. **Expose port** ▶︎ Use `EXPOSE` to ensure we can access the application on container port 8080.

10. **Run the application** ▶︎ Use `CMD` to run the `node` executable against the `index.js` file.

## Dockerfile Creation

```zsh
vim Dockerfile
```

```Dockerfile
FROM node:10-alpine
RUN mkdir -p /home/node/app/node_modules && chown -R node:node /home/node/app
WORKDIR /home/node/app
COPY package*.json ./
RUN npm config set registry http://registry.npmjs.org/
RUN npm install
COPY --chown=node:node . .
USER node
EXPOSE 8080
CMD [ "node", "index.js" ]
```

## Building the Image

```zsh
docker build <DOCKERFILE_PATH>
```

e.g.

```zsh
docker build . -t appimage
```

## Run Container from Image

```zsh
docker run -dt --name app01 appimage
```

```zsh
docker inspect app01 | grep IPAddress
```

```zsh
curl 172.17.0.3:8080
```

## Learning Summary

### Provide a parent/scratch image:

```zsh
FROM <image|scratch>
```

### Run a command against the default shell:

```zsh
RUN <command>
```

### Set the working directory

```zsh
WORKDIR <directory/path>
```

### Copy files

```zsh
COPY --chown <user> <source> <destination>
```

### Copy files (can be from URLs)

```zsh
ADD <source> <destination>
```

### Switch to user

```zsh
USER <user>
```

### Expose a port

```zsh
EXPOSE <port>
```

### Single command to run when container starts

```zsh
CMD [ "<executable", "<param1>", "<param2>" ]
```
