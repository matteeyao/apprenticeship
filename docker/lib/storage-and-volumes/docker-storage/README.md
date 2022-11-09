# Storage Drivers

1. Bob has set up a new Docker server. The `overlay2` driver is the default for the server, but he wants to use `devicemapper` instead. Which of the following are ways to implement this change?

[ ] Use a different Docker version.

[x] Set `storage-driver` to `devicemapper` in `/etc/docker/daemon.json`.

[ ] Reformat the storage disk.

[x] Add the `--storage-driver` flag to the `dockerd` call in Docker's unit file.

> We can set the storage driver in `/etc/docker/daemon.json`.
> 
> We can set the storage driver by passing the `--storage-driver` flag to `dockerd`.

2. What command would we use to locate the layered file system data for an image on a machine?

[ ] `docker layer inspect`

[x] `docker image inspect`

[ ] `docker image layers`

[ ] `docker pull history`

> The `docker image inspect` command will return the image metadata, including the location of the layered file system data.

3. Which of the following is true of filesystem storage models? (Choose two)

[x] They store data in regular files on the host machine.

> Filesystem storage models simulate a file system and store the data in regular files onto the host machine.

[x] They are used by `overlay2` and `aufs`.

> The `overlay2` and `aufs` storage drivers both use filesystem storage models.

[ ] They use an external, object-based store.

[ ] They are efficient with write-heavy workloads.
