# Managing Images

**Download an image from a remote registry**:

```
docker image pull IMAGE[:TAG]
```

For example:

```zsh
docker image pull nginx:1.14.0
```

...or...

```
docker pull nginx:1.14.0
```

**List images on the system**:

```
docker image ls
```

**Add the `-a` flag to include intermediate images**:

```
docker image ls -a
```

**Get detailed information about an image**:

```
docker image inspect <IMAGE>
```

Use the `--format` flag to get only a subset of the information (uses Go templates):

```
docker image inspect <IMAGE> --format <TEMPLATE>
```

Provide a Go template to retrieve specific data fields about the image:

```
docker image inspect <IMAGE> --formate "GO_TEMPLATE"
```

```
docker image inspect nginx:1.14.0 --format "{{.Architecture}}"
```

...or...

```
docker image inspect nginx:1.14.0 --format "{{Architecture}} {{.Os}}"
```

**Delete an image.** An image can only face deletion if no containers or other image tags reference it.

```
docker image rm <IMAGE>
```

**Delete an image**:

```
docker rmi <IMAGE>
```

> [!NOTE]
>
> If an image has other tags, they must be deleted first.

Use the `-f` flag to automatically remove all tags and delete the image. Force deletion of an image, even if it gets referenced by something else:

```
docker image rm -f <IMAGE>
```

Find and delete dangling or unused images (not being referenced by any tags or not beng used by any containers):

```
docker image prune
```
