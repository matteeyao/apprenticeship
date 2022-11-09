# Encrypting Overlay Networks

1. Given Docker's architecture and built-in security features, which of the following security scenarios should we be concerned about the most?

[ ] If an attacker gains control of a container, they could use it to affect other containers on the same host directly.

[x] If an attacker gains access to the Docker daemon, they could use it to execute commands as root on the host.

[ ] An attacker may intercept swarm-level traffic between swarm nodes and obtain sensitive information from the data.

[ ] An attacker could set up a false machine under their control and join it to the swarm cluster to steal sensitive data, causing containers with sensitive data to execute on a fake device.

> The Docker daemon must run as `root`, so it is essential to ensure that it's being protected and has limited access to it.
