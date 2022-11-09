# Building Efficient Images

When working w/ Docker in the real world, it is important to create Docker images that are as efficient as possible.

This means that they are as small as possible and result in ephemeral containers that can be started, stopped, and destroyed easily.

**General Tips**:

* Put things that are less likely to change on lower-level layers.

* Don't create unnecessary layers.

* Avoid including any unnecessary files, packages, etc. in the image.

## Multi-Stage Builds

**Multi-Stage Build**: A build from a Dockerfile with more than one `FROM` directive. It is used to selectively copy files into
the final stage, keeping the resulting image as small as possible.

Docker supports the ability to perform multi-stage builds. Multi-stage builds have more than one `FROM` directive in the Dockerfile, w/ each `FROM` directive starting a new stage.

Each stage begins a completely new set of file system layers, allowing you to selectively copy only the files you need from previous layers.

Use the `--from` flag w/ `COPY` to copy your files from a previous stage.

```
COPY --from=0 ...
```

You can also name your stages w/ `FROM...AS`, then reference the name w/ `COPY --from`:

```
FROM <image> AS stage1
...
FROM <image> AS stage2
COPY --from=stage1 ...
```

For example:

```Dockerfile
FROM golang:1.12.4
WORKDIR /helloworld
COPY helloworld.go .
RUN GOOS=linux go build -a -installsuffix cgo -o helloworld .
CMD ["./helloworld"]
```

W/ the dockerfile, we can go ahead and build:

```
docker build -t inefficient .
```

Then:

```
docker run inefficient
```

```
FROM golang:1.12.4 AS compiler
WORKDIR /helloworld
COPY helloworld.go
RUN GOOS=linux go build -a -installsuffix cgo -o helloworld .

FROM alpine:3.9.3
WORKDIR /root
COPY --from=compiler /helloworld/helloworld .
CMD ["./helloworld"]
```

```
docker build -t efficient .
```

```
docker run efficient
```

```
docker image ls
```

