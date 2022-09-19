# `chroot` command

The `chroot` command changes the apparent root directory for the current running process and its children.

```
ssh user@54.69.216.149
```

```
sudo su -
```

```
mkdir /home/arkham
mkdir /home/arkham/{bin,lib64}
groupadd inmates
useradd -g inmates quinn
groups quinn
cd /home/arkham/bin
cp /usr/bin/ls .
cp /usr/bin/bash .
ldd /bin/bash
```

```
cp -v /lib64/libselinux.so.1 /lib64/libscap.so.2 /lib64/libacl.so.1 /lib64/libc.so.6 /lib64/libpcre.so.1 /lib64/libdl.so.2 /lib64/libpthread.so.0 /home/arkham/lib64
```

```
ldd /bin/ls
```

```
cp -v /lib64/libattr.so.1 /lib64/ld-linux-x86-64.so.2 /lib64/libtinfo.so.5 /home/arkham/lib64
```

```
cd ..
pwd
vim escape.txt
```

```
chroot /home/arkham/ /bin/bash
ls
cat escape.txt
exit
```

```
vim /etc/ssh/sshd_config
```

```
Match group inmates
          ChrootDirectory /home/arkham/
          X11Forwarding no
          Allow TcpForwarding no
```

```
systemctl restart sshd
```

```
password quinn
```

```
ssh quinn@54.69.216.149
```
