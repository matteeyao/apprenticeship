# Image Composition

## Docker Images

The official Docker definition of an image is: "an ordered collection of root filesystem changes and the corresponding execution parameters for use within a container runtime"

Just like a class is used to create objects - you can think of an image as the template that Docker uses to spawn containers. Images are created by using a Dockerfile and are built up using a series of layers. Each layer represents an instruction in the image's Dockerfile. Each layer except for the very last layer is read-only

A typical image contains:

1. **Files**: hold data like application binaries, dependencies, libraries, and kernel modules

2. **Metadata**: instructions hot how the container will behave. For example, which processes it will run, which network ports it will expose, which volumes it will use for persistent storage, among other settings

What is **NOT** an image?

An image is not a complete Virtual Machine, it does not have a complete OS or it's own Kernel. An image can be as small as one file, or as large as a Linux distribution like Ubuntu

## Image Composition

Images are composed of metadata and **layers**. Each layer is only a set of differences from the layer before it. Layers are stacked on top of each other - similar to `git commits`. An Image layer is a general term which may be used to refer to one or both of the following:

1. Metadata formatted in JSON

2. Filesystem changes described by a particular layer

The first type of layer is known either as the `Layer JSON` or `Layer Metadata`. The latter is referred to as the `Image Diff`

## Viewing Image MetaData

You can inspect the metadata of an image using the command `docker image inspect IMAGENAME`. You can see basic info like:

1. The image's `id`

2. Default environment information

3. Exposed ports

4. Tags associated w/ that image

5. The Command the image will run by default when a container is based on that image

6. The hash values for the layers contained within the image
