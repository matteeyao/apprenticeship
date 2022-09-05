# Image Basics

## What is an Image?

The Docker Image is the source of a Container

✓ **Comprised of layers** ▶︎ All code, runtime, libraries, variables, and configurations are supplied via a layered Dockerfile.

✓ **Layers are cached** ▶︎ This means images are more reusable, quicker to deploy, and take up less space than a standard virtual machine image.

✓ **Everything we need** ▶︎ Everything the container needs to perform its job successfully should be added to the image.

✓ **Base images on other images** ▶︎ Images can have a **parent image** on which they are based; there's no need to recreate the wheel each time. Images w/ no parent are called **base images**.

## Dockerfiles

### hello-world Line-by-Line

```dockerfile
FROM scratch
COPY hello /
CMD ["/hello"]
```

`FROM scratch` layer indicates that the container is created from a base image.

`COPY  hello` layer copies the binary to create the initial root file system `/`.

`CMD ["/hello"]` layer is set to run the `hello` binary script.

## Learning Summary

**Every container has a source image**

* A **base image** is an image based on the **scratch** template, w/ the first layer defining the file system.

* A **parent image** is the image the current Docker image is based on.

* Images are comprised of cached layers. This increases reusability, decreases disk space, and ensures fast deploy speeds.
