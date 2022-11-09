# Swarm Backup and Restore

In a production environment, it's always a good idea to back up critical data.

Backing up Docker swarm data is fairly simple. To back up, do the following on a swarm manager.

1. Stop the Docker service:

```
sudo systemctl stop docker
```

2. Archive the swarm data located in `/var/lib/docker/swarm`, and then start Docker again.

```
sudo tar -zvcf backup.tar.gz /var/lib/docker/swarm
sudo systemctl start docker
```

To restore a previous backup:

1. Stop the Docker service:

2. Delete any data (existing files or directories) currently in the swarm data directory `/var/lib/docker/swarm`:

```
sudo rm -rf /var/lib/docker/swarm/*
```

3. Expand the archived backup data into the swarm data directory and start Docker:

   * Copy the backed-up files to `/var/lib/docker/swarm`.

   * Start the Docker service.

```
sudo tar -zxvf backup.tar.gz -C /var/lib/docker/swarm/
sudo systemctl start docker
```

4. Verify that all the nodes are functioning properly in the swarm after the restore:

```
docker node ls
```
