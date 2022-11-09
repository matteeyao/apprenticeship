# Configuring Docker to use External DNS

You may need to customize the external DNS server(s) used by your containers.

You can change the default for the host w/ the `dns` setting in `daemon.json`.

```zsh
sudo vi /etc/docker/daemon.json
```

Set the system-wide default DNS for Docker containers in `daemon.json`:

```json
{
  "dns": ["8.8.8.8"]
}
```

Restart Docker daemon:

```zsh
sudo systemctl restart docker
```

Test our new setting, by running `netshoot` container to test DNS:

```zsh
docker run nicoklaka/netshoot nslookup google.com
```

Set the DNS for an individual container:

```zsh
docker run --dns <DNS_ADDRESS> <IMAGE>
```

For instance:

```zsh
docker run --dns 8.8.4.4 nicolaka/netshoot nslookup
```
