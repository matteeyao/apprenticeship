# Configuring DeviceMapper

**Device Mapper** is one of the Docker storage drivers available for some Linux distributions. It is the default block storage driver for CentOS 7 and earlier.

You can customize your Device Mapper configuration using the **daemon config file**.

DeviceMapper supports two modes:

* **loop-lvm** mode ▶︎ This is the default mode, but it is recommended for testing only, not for production use. 

  * Loopback mechanism simulates an additional physical disk using files on the local disk.

  * Minimal setup, does not requires an additional storage device.

  * Bad performance, only use for testing.

* **direct-lvm** mode ▶︎ A production-ready mode, which requires additional configuration and a special block storage device.

  * Stores data on a separate device.

  * Requires an additional storage device.

  * Good performance, use for production.

Below is sample configuration to enable `direct-lvm` in `daemon.json`:

> [!IMPORTANT]
> 
> Remember, this assumes that there is a block storage device called `/dev/xvdb`.

```json
{
    "storage-driver": "devicemapper",
    "storage-opts": [
      "dm.directlvm_device=/dev/xvdb",
      "dm.thinp_percent=95",
      "dm.thinp_metapercent=1",
      "dm.thinp_autoextend_threshold=80",
      "dm.thinp_autoextend_percent=20",
      "dm.directlvm_device_force=true"
  ]
}
```

## Demonstration

```
sudo systemctl disable docker
```

```
sudo systemctl stop docker
```

```
sudo rm -rf /var/lib/docker
```

```
sudo vi /etc/docker/daemon.json
```

```json
{
    "storage-driver": "devicemapper",
    "storage-opts": [
      "dm.directlvm_device=/dev/xvdb",
      "dm.thinp_percent=95",
      "dm.thinp_metapercent=1",
      "dm.thinp_autoextend_threshold=80",
      "dm.thinp_autoextend_percent=20",
      "dm.directlvm_device_force=true"
  ]
}
```

```
sudo systemctl enable docker
```

```
sudo systemctl start docker
```

```
docker info
```

```
docker run hello-world
```
