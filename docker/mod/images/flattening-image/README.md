# Flattening an Image

Sometimes, images w/ fewer layers can perform better. In a few cases, you may want to take an image w/ many layers and flatten them into a single layer.

Docker does not provide an official method for turning a multi-layered image into a single layer. We can work around this by running a container from the image, exporting the container's file system, and then importing that data as a new image.

1. Set up a new project directory to create a basic image:

```
cd ~/
mkdir alpine-hello
cd alpine-hello
vi Dockerfile
```

2. Create a Dockerfile that will result in a multi-layered image:

```Dockerfile
FROM alpine:3.9.3
RUN echo "Hello, World!" > message.txt
CMD cat message.txt
```

3. Build the image and check how many layers it has:

```
docker build -t nonflat .
docker image history nonflat
```

4. Run a container from the image and export its file system to an archive:

```
docker run -d --name flat_container nonflat
docker export flat_container > flat.tar
```

5. Import the archive to a new image and check how many layers the new image has:

```
cat flat.tar | docker import - flat:latest
docker image history flat
```

## Learning summary

Docker does not provide an official method for doing this, but you can accomplish it by doing the following:

* Run a container from the image.

* Export the container to an archive using `docker export`.

* Import the archive as a new image using `docker import`.

The resulting image will have only one layer.
