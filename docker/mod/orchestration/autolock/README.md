# Locking and Unlocking a Swarm Cluster

## Autolock

Docker swarm encrypts sensitive data for security reasons, such as:

* Raft logs on swarm managers.

* TLS communication between swarm nodes.

By default, Docker manages the keys used for this encryption automatically, but they are stored unencrypted on the managers' disks.

**Autolock**: A feature of Docker Swarm. Prevents sensitive keys from being stored insecurely on swarm managers, but requires us to enter an unlock key whenever the Docker daemon restarts on a swarm manager.This gives you control of the keys and can allow for greater security.

However, it requires you to unlock the swarm every time Docker is restarted on one of your managers.

1. The command that enables the autolock feature:

```
docker swarm update --autolock=true
```

2. The command that disables the autolock feature:

```
docker swarm update --autolock=false
```

3. Whenever Docker restarts on a manager, you must unlock the swarm. The command that unlocks a locked Swarm manager:

```
docker swarm unlock
```

4. To obtain the unlock key from an unlocked Swarm manager:

```
docker swarm unlock-key
```

5. To rotate the unlock key, which will automatically orchestrate key rotation across all nodes in the cluster:

```
docker swarm unlock-key --rotate
```
