# Configuring Logging Drivers (Splunk, Journald, etc.)

**Logging driver**: a pluggable driver that handles log data from services and containers in Docker.

Logging drivers are a pluggable framework for accessing log data from services and containers in Docker. Docker supports a variety of logging drivers.

Determine the current default logging driver:

```
docker info | grep Logging
```

To det a new default driver, modify `/etc/docker/daemon.json`. The `log-driver` option sets the driver, and `log-opts` can be used to provide driver-specific configuration.

For example:

```
{
    "log-driver": "json-file",
    "log-opts": {
        "max-size": "10m",
        "max-file": "3",
        "labels": "production_status",
        "env": "os,customer"
    } 
}
```

...or...

```
{
    "log-driver": "syslog",
}
```

Remember to utilize `sudo systemctl restart docker` after making any changes to `/etc/docker/daemon.json`.

We can also override the default driver setting for individual containers using the `--log-driver'` and `'--log-opt` flags with `docker run`.

```
docker run --log-driver json-file --log-opt max-size=10m nginx
```

...or...

```
docker run --log-driver syslog --log-opt max-size=10m nginx
```
