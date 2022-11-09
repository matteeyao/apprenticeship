# Node Labels

1. Which of the following commands will allow us to add a label to a Docker Swarm node?

[ ] `docker label add <LABEL> <NODE_NAME>`

[ ] `docker node update --labels <LABEL> <NODE_NAME>`

[ ] `docker node tag <LABEL> <NODE_NAME>`

[x] `docker node update --label-add <LABEL> <NODE_NAME>`

2. Daniel has some nodes with labels that specify the availability zone of each node. He wants to run a service that can run tasks on any node and that do not have the label `availability_zone=east`. Which command should he use?

[x] `docker service create --constraint node.labels.availability_zone!=east nginx`

> This command will prevent the service's tasks from running on nodes with the `availability_zone==east` label.

[ ] `docker service create --constraint node.labels.availability_zone==west nginx`

[ ] `docker service create --placement-pref node.labels.availability_zone==west nginx`

[ ] `docker service create --label node.labels.availability_zone!=east nginx`

3. Which of the following commands will evenly spread out tasks based upon the values of a node label?

[x] `docker service create --placement-pref spread=node.labels.availability_zone nginx`

[ ] `docker service create --constraint spread=node.labels.availability_zone nginx`

[ ] `docker service create --placement-pref even-spread=node.labels.availability_zone nginx`

[ ] `docker service create --placement-pref spread nginx`

> This command will evenly spread out tasks based upon the values of the `availability_zone` label.
