# Image Cleanup

To display Docker's disk usage on a system:

```
docker system df
```

To display a more detailed disk usage report:

```
docker system df -v
```

Remove dangling/unused images (images not referenced by any tag or container):

```
docker image prune
```

Once we have verified that the images are not being used by a container, we can use the following command to delete all of them:

```
docker image prune -a
```
