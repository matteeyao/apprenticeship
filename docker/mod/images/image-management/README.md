# Image Management

## docker pull

```zsh
docker pull alpine
```

## docker image rundown

```zsh
docker image ls
```

```zsh
docker image rm <IMAGE_NAME>
```

```zsh
docker image prune
```

Remove images w/o container associated w/ them:

```zsh
docker image prune -a
```

```zsh
docker image inspect <IMAGE_NAME>
```

## Learning Summary

### Save an image locally

```zsh
docker pull <IMAGE_NAME>
```

### List available images

```zsh
docker image ls
```

### Remove images

```zsh
docker image rm <image>
```

### Prune all unused images

```zsh
docker image prune -a
```

### View image information

```zsh
docker image inspect <IMAGE_NAME>
```
