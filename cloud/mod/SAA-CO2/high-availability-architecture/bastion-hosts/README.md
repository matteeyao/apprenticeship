# High Availability w/ Bastion Hosts

> * Two hosts in two separate Availability Zones. Use a Network Load Balancer w/ static IP addresses and health checks to fail over from one host to the other.
>
> * Can't use an Application Load Balancer, as it is layer 7 and you need to use layer 4.
>
> * One host in one Availability Zone behind an Auto Scaling group w/ health checks and a fixed EIP. If the host fails, the health check will fail and the Auto Scaling group will provision a new EC2 instance in a separate Availability Zone. You can use a user data script to provision the same EIP to the new host. This is the cheapest option, but it is not 100% fault tolerant.
