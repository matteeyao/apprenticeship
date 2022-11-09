# DaemonSets

**DaemonSets** are a tool that allows you to run a replica pod dynamically on each node in the cluster.

They're similar to deployments in the sense that they allow you to manage a set of pods, but instead of managing a specific number of replica pods, the number of pods matches the number of nodes, and specifically, there will be 1 replica pod running on each node.

They will run a replica on each node and create new replicas on new nodes as they are added to the cluster. So, if you build a new node and add it to your Kubernetes cluster, it will automatically get a replica of that DaemonSet. So anytime you have a particular container that you need to be running on each node in the cluster, DaemonSets are the answer to that use case.
