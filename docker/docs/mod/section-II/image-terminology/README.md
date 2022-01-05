# Image Terminology

## Image Parent

Layers will often have a place reserved in their metadata for the `parent` field. An Image's `parent` refers to the Image from which the current image directly descends from. An image's metadata will contain a set of changes relative to the filesystem of its parent image

## Image JSON

Each layer has an associated JSON structure which describes basic information about the image such as the date the layer was created, the author, and the ID of its parent image as well as execution/runtime configuration like its networking and volumes

## Union Filesystem

Each layer has an archive of the files which have been added, changed, or deleted relative to its parent layer. Using a layer-based filesystem, or by computing the diff from filesystem snapshots, the `union Filesystem` can be used to present a series of image layers as if they were one cohesive filesystem

## Image ID

Each layer is given an ID upon its creation. It is represented as a hexadecimal encoding of 256 bits, e.g., `a956...`. Image IDs should be sufficiently random so as to be globally unique

## Tag

A tag serves to map a descriptive, user-given name to any single image ID. An image name suffix (the names that comes after the `:`) is often referred to as a tag as well, though it strictly refers to the full name of an image. (Example: in `node:8.15-alpine`, the `8.15-alpine` is the tag). Tag names should be limited to the set of alphanumeric characters `[a-z, A-Z, 0-9]` and punctuation character `[._-]`. Tag names **MUST NOT** contain a `:` character

## Versions

As an image gets updated overtime newer versions of that image will come out. The important thing to know is that when working in production you **always** want to specify which exact image version you are using (`1.11.9` vs `1.11`). You will rarely want an image to update automatically, so using a consistent image version will help keep code compatibility issues to a minimum

## Repository

A collection of tags grouped under a common prefix (the name component before `:`). For example, in an image tagged w/ the name `my-app:3.1.4`, `my-app` is the *Repository* component of the name
