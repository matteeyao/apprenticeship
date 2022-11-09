# Installing Docker CE

## Add Cloud User to docker group

```
sudo usermod -a -G docker cloud_user
```

* In order for the `sudo` to take effect, you will need to `exit` and `ssh` back into the cloud_user

## Installation and Configuration: Linux

```
wget -q0- https://get.docker.com |sh
```

```
sudo usermod -aG docker cloud_user
```

```
docker version
```

```
sudo systemctl start docker
```

```
sudo systemctl enable docker
```

```
docker info
```

```
exit
```

After logging back in:

```
docker version
```
