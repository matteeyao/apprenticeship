# Creating a Chrooted Environment

## About this lab

This lab will allow you to practice creating a chrooted environment, adding access to Linux commands, along with including shared libraries for these commands.

You will create a chrooted jailed environment as well as ensuring the inmates can `cat` out the false escape plans that you will leave behind.

![Fig. 1 Lab diagram](../../../containers/img/components/chroot/demo/diag01.png)

The user `napoleon` will be needing access to our server. Create the user `napoleon`. Create the `/home/elba` directory to be used as our chrooted environment. Ensure `napolean` is able to use the `ls`, `cat` and `bash` commands in a Bash environment. Utilizing the `chroot` command and using a Bash environment, confirm that the user `napoleon` has access to the `vi` and `cat` commands by creating the `waterloo.txt` file and then `cat`ting out the file.

## Learning objectives

[ ] Create a directory named `/home/elba`

[ ] Create a new user called `napoleon`

[ ] Create the bin and lib64 directories in `/home/elba`

[ ] Copy `/bin/bash` into `/home/elba/bin/bash`

[ ] Copy `/bin/ls` in to `/home/elba/bin/ls`

[ ] Copy `/bin/cat` in to `/home/elba/bin/cat`

[ ] Copy the libraries needed for bash, ls, and cat over to `/home/elba/lib64`

[ ] Create the `waterloo.txt` file in the `/home/elba` directory

[ ] Create a chrooted environment in `/home/elba` with a bash shell

[ ] Confirm commands work

[ ] View the contents of `waterloo.txt` and find out how to escape your environment
