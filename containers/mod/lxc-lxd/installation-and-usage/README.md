# Installing and Basic Usage of LXC/LXD

See what distribution or version of linux being used:

```
cat etc/issue
```

```
sudo apt install lxd lxd-client
```

```
sudo lxd init
```

```
lxc list
```

```
lxc launch ubuntu:16.04
```

```
lxc launch ubuntu:16.04 my-ubuntu
```

```
lxc list
```

```
lxc launch images:alpine/3.5 my-alpine
```

```
lxc list
```

```
lxc exec my-ubuntu -- /bin/bash
```

```
cat /etc/issue
```

```
lxc exec my-alpine -- /bin/ash
```

```
whoami
```

```
echo hello > hi.txt
```

```
lxc exec my-ubuntu -- /bin/bash
```

## Remotes

```
lxc remote list
```

## List out images available on system

```
lxc image list
```

## Learning summary

How would you create an lxc container using Alpine 3.5 named my-alpine?

```
lxc launch images:alpine/3.5 my-alpine
```
