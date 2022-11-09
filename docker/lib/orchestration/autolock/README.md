# Locking and Unlocking a Swarm Cluster

1. Kelly has a Docker swarm cluster with `--autolock` enabled. One of her manager nodes has become locked, and she has lost the unlock key. Fortunately, there are still some swarm nodes that are not locked. How can she obtain the unlock key from one of the unlocked nodes?

[ ] Kelly can look in the file located at `/etc/docker/swarm/unlock.key`.

[ ] Kelly can use the `docker swarm key print` command.

[x] Kelly can use the `docker swarm unlock-key` command.

> This command will retrieve the unlock key from a manager node that is currently not locked.

[ ] Kelly can use the `docker swarm unlock` command.
