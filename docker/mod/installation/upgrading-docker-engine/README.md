# Upgrading Docker Engine

1.  To upgrade the Docker engine, first we must stop the Docker service and install newer versions of `docker-ce` and `docker-ce-cli`.

```
sudo systemctl stop docker
sudo apt-get install -y docker-ce=<new version> docker-ce-cli=<NEW_VERSION>
```

2. Conversely, to downgrade the Docker engine:

```
sudo systemctl stop docker
sudo apt-get remove -y docker0ce docker-ce-cli
sudo apt-get update
suo apt-get install -y docker-ce=<PREVIOUS_VERSION>
```

e.g.

```
suo apt-get install -y docker-ce=5.18.09.4~3-0~ubuntu-bionic docker-ce-cli=5.18.09.4~3-0~ubuntu-bionic
```

3. Check the current version:

```
docker version
```
