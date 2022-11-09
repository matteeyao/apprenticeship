# Configuring DeviceMapper

1. Which of the following configurations would be best for enabling `direct-lvm` mode with `devicemapper`?

[ ] Set `dm.loop-lvm=false` in `/etc/docker/daemon.json`.

[ ] Set `dm.mode=direct-lvm` in `/etc/docker/daemon.json`.

[ ] Set `dm.direct-lvm=true` in `/etc/docker/daemon.json`.

[x] Set `dm.directlvm_device` in `/etc/docker/daemon.json`.

> We can enable `direct-lvm` by setting this value in `daemon.json` to a block storage device.
