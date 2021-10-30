# Container Process Monitoring

An important part of working w/ Docker - or any other process running on a computer - is being able to monitor the resources taken up by a certain process

If you've ever had a runaway process (think of an infinite loop) you've probably encountered the situation where you needed to know which processes on your computer are taking up the most resources

Docker allows you to easily monitor separate containers and resources taken up by those containers through a couple of commands

Use `docker container stats` to see all of the containers currently running and the resources on your computer that those containers are taking up

You should get an output that is similar to this:

```
CONTAINER ID        NAME                CPU %               MEM USAGE / LIMIT     MEM %               NET I/O             BLOCK I/O           PIDS
1be21246b909        httpd1              0.02%               5.562MiB / 2.934GiB   0.19%               648B / 0B           98.3kB / 0B         82
4f0ace5722fc        mysql1              2.18%               376.2MiB / 2.934GiB   12.52%              718B / 0B           111kB / 1.26GB      38
1b00fb53a1a6        nginx1              0.00%               1.918MiB / 2.934GiB   0.06%               1.35kB / 0B         0B / 0B             2
```

You can see the IDs of the containers, as well as how much power and memory each container is using (from what you allocated Docker to use)

The `docker container top` command will present you w/ a list of all the processes running within a container, and the `PID` for each process

```
$ ~ docker container top nginx1
PID                 USER                TIME                COMMAND
8800                root                0:00                nginx: master process nginx -g daemon off;
8839                101                 0:00                nginx: worker process
```
