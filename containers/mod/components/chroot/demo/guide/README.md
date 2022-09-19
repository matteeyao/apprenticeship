# Creating a Chrooted Environment

## Introduction

This lab will allow you to practice creating a chrooted environment, adding access to Linux commands, along with including shared libraries for these commands.

You will create a chrooted jailed environment as well as ensuring the inmates can `cat` out the false escape plans that you will leave behind.

## Solution

1. Begin by logging in to the lab server using the credentials provided on the hands-on lab page:

```
ssh cloud_user@PUBLIC_IP_ADDRESS
```

2. Become the `root` user:

```
sudo su -
```

## Create a directory named `/home/elba`

1. Create a directory for `elba` within the `/home` directory.

```
mkdir /home/elba
```

## Create a new user called `napoleon`

1. Create a new user for your environment named `napoleon`.

```
useradd napoleon
```

## Create the `bin` and `lib64` directories in `/home/elba`

1. Create two new directories, `bin` and `lib64`, within the `/home/elba` directory.

```
mkdir /home/elba/{bin,lib64}
```

## Copy `/bin/bash` into `/home/elba/bin/bash`

1. Copy `/bin/bash` into `/home/elba/bin/bash`.

```
cp /bin/bash /home/elba/bin/bash
```

## Copy `/bin/ls` into `/home/elba/bin/ls`

1. Copy `/bin/ls` into `/home/elba/bin/ls`.

```
cp /bin/ls /home/elba/bin/ls
```

## Copy `/bin/cat` into `/home/elba/bin/cat`

1. Copy `/bin/cat` into `/home/elba/bin/cat`.

```
cp /bin/cat /home/elba/bin/cat
```

## Copy the libraries needed for `bash`, `ls`, and `cat` over to `/home/elba/lib64`

1. Find and copy the libraries needed for `bash`, `ls`, and `cat` over to `/home/elba/lib64`.

```
ldd /bin/bash /bin/ls /bin/cat
cp /lib64/libtinfo.so.5 \
/lib64/libdl.so.2 \
/lib64/ld-linux-x86-64.so.2 \
/lib64/libselinux.so.1 \
/lib64/librt.so.1 \
/lib64/libcap.so.2 \
/lib64/libacl.so.1 \
/lib64/libc.so.6 \
/lib64/libpthread.so.0 \
/lib64/libattr.so.1 \
/lib64/libpcre.so.1 /home/elba/lib64
```

## Create the `waterloo.txt` file in the `/home/elba` directory

1. Using `vi`, create the `waterloo.txt` file in the `/home/elba` directory with instructions on how to escape.

```
vi /home/elba/waterloo.txt
```

Inside the file, add the following:

```txt
There is no escape!

```

Save and close the file:

```
:wq
```

## Create a chrooted environment in `/home/elba` with a Bash shell

1. Using the `chroot` command, create a chrooted environment in `/home/elba` with a Bash shell.

```
chroot /home/elba /bin/bash
```

## Using the command `pwd`, confirm the present working directory and then confirm that you can use the `ls` command.

1. Using the command `pwd`, confirm the present working directory and then confirm that you can use the `ls` command.

```
pwd
ls
cat
```

## View the contents of `waterloo.txt` and find out how to escape your environment

1. View the contents of `waterloo.txt` and find out how to escape your environment.

```
cat waterloo.txt
```
