# Node Labels

**Node Label**: Custom metadata about a node in the cluster.

Use these labels to determine which nodes tasks will run on.

Example use case:

* W/ nodes in multiple datacenters or availability zones, you can use labels to specify which zone each node is in. Then, execute tasks in specific zones or distribute them evenly across zones.

1. To list current nodes, enter:

```
docker node ls
```

2. To add a label to a node, input:

```
docker node update --label-add <LABEL_NAME>=<LABEL_VALUE> <NODE>
```

3. To view existing node labels, run:

```
docker node inspect --pretty <NODE>
```

## Node Constraints

1. To run a service's tasks only on nodes w/ a specific label value, use the `--constraint` flag w/ `docker service create`:

```
docker service create --constraint node.labels.<LABEL>==<VALUE> <IMAGE>
```

We can use node constraints to control which nodes a service's tasks will run on based upon node labels. Here is an example:

```zsh
docker service create --constraint node.labels.availability_zone==east --replicas 3 nginx
```

View running services:

```
docker service ps nginx-east
```

2. You can also use a constraint to run only on nodes w/o a particular value:

```
docker service create --constraint node.labels.<LABEL>!=<VALUE> <IMAGE>
```

To use various expressions for constraints, such as inequality, we can run, for instance:

```
docker service create --name nginx-west --constraint node.labels.availability_zone!=east --replicas 3 nginx
```

```
docker service ps nginx-west
```

## Placement-pref

Use `--placement-pref` w/ the spread strategy to spread tasks evenly across all values of a particular label:

```
docker service create --placement-pref spread=node.labels.<LABEL> <IMAGE>
```

For example, if you have a label called `availability_zone` w/ three values (east, west, and south), the tasks will be divided evenly among the node groups w/ each of those three values, no matter how many nodes are in each group.

Use `--placement-pref` to spread tasks evenly based on the value of a specific label:

```
docker service create --placement-pref spread=node.labels.availability_zone --replicas 3 nginx
```

```
docker service ps nginx-spread
```
