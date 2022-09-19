# The Dockerfile

```
docker images
```

```
mkdir onboarding
```

```
cd onboarding/
```

```
vim dockerfile
```

```dockerfile
FROM ubuntu:16.04
LABEL maintainer="ell.marquez@linuxacademy.com"
RUN apt-get update
RUN apt-get install -y python3
```

```
docker build .
```
