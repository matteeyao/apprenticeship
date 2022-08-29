# EC2 Placement Groups

1. A Solutions Architect is working for a weather station in Asia with a weather monitoring system that needs to be migrated to AWS. Since the monitoring system requires a low network latency and high network throughput, the Architect decided to launch the EC2 instances to a new cluster placement group. The system was working fine for a couple of weeks, however, when they try to add new instances to the placement group that already has running EC2 instances, they receive an 'insufficient capacity error'.

How will the Architect fix this issue?

[ ] Verify all running instances are of the same size and type and then try the launch again.

[ ] Stop and restart the instances in the Placement Group and then try the launch again.

[ ] Create another Placement Group and launch the new instances in the new group.

[ ] Submit a capacity increase request to AWS as you are initially limited to only 12 instances per Placement Group.

**Explanation**: A cluster placement group is a logical grouping of instances within a single Availability Zone. A cluster placement group can span peered VPCs in the same Region. Instances in the same cluster placement group enjoy a higher per-flow throughput limit for TCP/IP traffic and are placed in the same high-bisection bandwidth segment of the network.

It is recommended that you launch the number of instances that you need in the placement group in a single launch request and that you use the same instance type for all instances in the placement group. If you try to add more instances to the placement group later, or if you try to launch more than one instance type in the placement group, you increase your chances of getting an insufficient capacity error. If you stop an instance in a placement group and then start it again, it still runs in the placement group. However, the start fails if there isn't enough capacity for the instance.

If you receive a capacity error when launching an instance in a placement group that already has running instances, stop and start all of the instances in the placement group, and try the launch again. Restarting the instances may migrate them to hardware that has capacity for all the requested instances.

Stop and restart the instances in the Placement group and then try the launch again can resolve this issue. If the instances are stopped and restarted, AWS may move the instances to a hardware that has the capacity for all the requested instances.

Hence, the correct answer is: **Stop and restart the instances in the Placement Group and then try the launch again.**

> The option that says: **Create another Placement Group and launch the new instances in the new group** is incorrect because to benefit from the enhanced networking, all the instances should be in the same Placement Group. Launching the new ones in a new Placement Group will not work in this case.

> The option that says: **Verify all running instances are of the same size and type and then try the launch again** is incorrect because the capacity error is not related to the instance size.

> The option that says: **Submit a capacity increase request to AWS as you are initially limited to only 12 instances per Placement Group** is incorrect because there is no such limit on the number of instances in a Placement Group.

<br />
