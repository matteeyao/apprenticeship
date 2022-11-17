# Backing Up and Restoring Etcd Cluster Data

## Why back up etcd?

**etcd** is the backend data storage solution for your Kubernetes cluster. As such, all your Kubernetes objects, applications, and configurations are stored in etcd.

Therefore, you will likely want to be able to back up your cluster's data by backing up etcd.

## Backing up etcd

You can back up etcd data using the etcd command line tool, **etcdctl**.

Use the **etcdl snapshot save** command to back up the data.

```zsh
ETCDCTL_API=3 etcdctl --endpoints $ENDPOINT snapshot save <FILE_NAME>
```

## Restoring etcd

You can restore etcd data from a backup using the `etcdl snapshot restore` command.

You will need to supply some additional parameters, as the restore operation creates a new logical cluster in order to repopulate your data from that saved backup file.

```zsh
ETCDCTL_API=3 etcdctl snapshot restore <FILE_NAME>
```
