# Configuring Docker to use External DNS

1. Which of the following is a valid method that we can use for setting the default DNS server for all containers on a host?

[ ] We can use `docker config set dns`.

[ ] We can use the `--dns` flag with `docker run`.

[x] We can set `"dns"` in `/etc/docker/daemon.json`.

[ ] We can edit the `/etc/hosts` file on the host.

> This method will set the default DNS for all containers on the host.

2. Which of the following tasks can we perform to set a custom DNS server for a container?

[x] We can use the `--dns` flag with `docker run`.

[ ] We can use the `--nameserver` flag with `docker run`.

[ ] We can set `"dns"` in `/etc/docker/daemon.json`.

[ ] We can use the `--dns-override` flag with `docker run`.

> This method would allow us to set a custom DNS server for the container.
