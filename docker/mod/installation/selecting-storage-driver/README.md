# Selecting a Storage Driver

## Storage Driver Basics

**Storage Driver**: A pluggable driver that handles internal storage for containers.

Storage drivers provide a pluggable framework for managing the temporary, internal storage of a container's writable layer.

Docker supports a variety of storage drivers. The best storage driver to use depends on your environment and your storage needs.

* `overlay2`: File-based storage. Default for Ubuntu and CentOS 8+.

* `devicemapper`: Block storage, more efficient for doing lots of writes. Default for CentOS 7 and earlier.

Currently, the default driver for CentOS and Ubuntu systems is `overlay2`.

The `devicemapper` storage driver is sometimes used on CentOS/RedHat systems, especially in older Docker versions.

We can determine the current storage driver w/ `docker info`:

```
docker info | grep "Storage"
```

## Using a Daemon Flag to set the Storage Driver

Docker automatically selects a default storage driver that is compatible w/ your environment.

However, in some cases you may want to **override the default** to use a different driver.

There are two ways to do this:

* Set the `--storage-driver` flag when starting Docker (in your system unit file for example).

* Set the `"storage-driver"` value in `/etc/docker/daemon.json`.

One way to select a different storage driver is to pass the `--storage-driver` flag over to the Docker daemon.

For example, we can modify Docker's `systemd` unit file: `/usr/lib/systemd/system/docker.service`.

Remember, add the flag `--storage-driver <DRIVER_NAME>` to the call to `dockerd`.

## Using the Daemon Config File to Set the Storage Driver

> [!NOTE]
>
> This is the recommended method for setting the storage driver.

1. Create or edit `/etc/docker/daemon.json`:

```
sudo vi /etc/docker/daemon.json
```

2. Add the value `"storage-driver": "<driver name>"`:

This example sets the storage driver to `devicemapper`.

```
{
    "storage-driver": "devicemapper"
}
```

3. After any changes are made to `/etc/docker/daemon.json`, remember to restart Docker. It is also a good idea to check the status of Docker after restarting, as a malformed config file will cause Docker to encounter startup failure. Use the following commands:

```
sudo systemctl restart docker
sudo systemctl status docker
```
