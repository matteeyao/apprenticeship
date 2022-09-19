# Installing LXC/LXD

## Introduction

This lab will allow you to practice installing LXC/LXD in a cloud server environment. You will pull down the latest Alpine image and create a container from it to test your configuration.

## Solution

Log in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

## Install the LXD Package

1. Determine which Linux distribution your server is running:

```
cat /etc/issue
```

2. Install `lxd` and `lxd-client`:

```
sudo apt install lxd lxd-client 
```

3. When prompted, enter the password provided on the lab page and hit **Enter**.

## Initialize LXD

1. Initialize LXD:

```
sudo lxd init
```

2. Accept the defaults for each of the prompts, except for configuring IPv6. Select **No** when prompted to set up an IPv6 subnet.

## Create Your First Container

1. Create a container using Alpine 3.14 named `my-alpine`:

```
sudo lxc launch images:alpine/3.14 my-alpine
```

2. List your containers:

```
lxc list
```

...or...

```
sudo lxc list
```

## Connect to a Container

1. Execute an ash shell in your `my-alpine` container:

```
lxc exec my-alpine -- /bin/ash
```

...or...

```
sudo lxc exec my-alpine -- /bin/ash
```

2. Create a file named `hello.txt`:

```
echo hello world! > hello.txt
```

3. Type `ls` to see the file, or (optionally) type `cat hello.txt` to see the content of the file.
