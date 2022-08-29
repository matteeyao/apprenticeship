# EC2 Hibernate

1. A solutions architect is managing an application that runs on a Windows EC2 instance with an attached Amazon FSx for Windows File Server. To save cost, management has decided to stop the instance during off-hours and restart it only when needed. It has been observed that the application takes several minutes to become fully operational which impacts productivity.

How can the solutions architect speed up the instance’s loading time without driving the cost up?

[ ] Migrate the application to an EC2 instance w/ hibernation enabled.

[ ] Enable the hibernation mode on the EC2 instance.

[ ] Disable the Instance Metadata Service to reduce the things that need to be loaded at startup.

[ ] Migrate the application to a Linux-based EC2 instance.

**Explanation**: Hibernation provides the convenience of pausing and resuming the instances, saves time by reducing the startup time taken by applications, and saves effort in setting up the environment or applications all over again. Instead of having to rebuild the memory footprint, hibernation allows applications to pick up exactly where they left off.

While the instance is in hibernation, you pay only for the EBS volumes and Elastic IP Addresses attached to it; there are no other hourly charges (just like any other stopped instance).

Therefore, the correct answer is: **Migrate the application to an EC2 instance with hibernation enabled.**

> The option that says: **Migrate the application to a Linux-based EC2 instance** is incorrect. This does not guarantee a faster load time. Moreover, it is a risky thing to do as the application might have dependencies tied to the previous operating system that won't work on a different OS.

> The option that says: **Enable the hibernation mode on the EC2 instance** is incorrect. It is not possible to enable or disable hibernation for an instance after it has been launched.

> The option that says: **Disable the instance metadata service to reduce the things that need to be loaded at startup** is incorrect. This won't affect the startup load time at all. The Instance Metadata Service is just a service that you can access over the network from within an EC2 instance.

<br />
