# Elastic Network Adapter

1. A company is planning to launch a High Performance Computing (HPC) cluster in AWS that does Computational Fluid Dynamics (CFD) simulations. The solution should scale-out their simulation jobs to experiment with more tunable parameters for faster and more accurate results. The cluster is composed of Windows servers hosted on t3a.medium EC2 instances. As the Solutions Architect, you should ensure that the architecture provides higher bandwidth, higher packet per second (PPS) performance, and consistently lower inter-instance latencies.

Which is the MOST suitable and cost-effective solution that the Architect should implement to achieve the above requirements?

[ ] Enable Enhanced Networking w/ Elastic Fabric Adapter (EFA) on the Windows EC2 Instances.

[ ] Enable Enhanced Networking w/ Elastic Network Adapter (ENA) on the Windows EC2 Instances.

[ ] Use AWS ParallelCluster to deploy and manage the HPC cluster to provide higher bandwidth, higher packet per second (PPS) performance, and lower inter-instance latencies.

[ ] Enable Enhanced Networking w/ Intel 82599 Virtual Function (VF) interface on the Windows EC2 instances.

**Explanation**: Enhanced networking uses single root I/O virtualization (SR-IOV) to provide high-performance networking capabilities on supported instance types. SR-IOV is a method of device virtualization that provides higher I/O performance and lower CPU utilization when compared to traditional virtualized network interfaces. Enhanced networking provides higher bandwidth, higher packet per second (PPS) performance, and consistently lower inter-instance latencies. There is no additional charge for using enhanced networking.

Amazon EC2 provides enhanced networking capabilities through the Elastic Network Adapter (ENA). It supports network speeds of up to 100 Gbps for supported instance types. Elastic Network Adapters (ENAs) provide traditional IP networking features that are required to support VPC networking.

An Elastic Fabric Adapter (EFA) is simply an Elastic Network Adapter (ENA) with added capabilities. It provides all of the functionality of an ENA, with additional OS-bypass functionality. OS-bypass is an access model that allows HPC and machine learning applications to communicate directly with the network interface hardware to provide low-latency, reliable transport functionality.

The OS-bypass capabilities of EFAs are not supported on Windows instances. If you attach an EFA to a Windows instance, the instance functions as an Elastic Network Adapter, without the added EFA capabilities.

Hence, the correct answer is to **enable Enhanced Networking with Elastic Network Adapter (ENA) on the Windows EC2 Instances.**

> **Enabling Enhanced Networking with Elastic Fabric Adapter (EFA) on the Windows EC2 Instances** is incorrect because the OS-bypass capabilities of the Elastic Fabric Adapter (EFA) are not supported on Windows instances. Although you can attach EFA to your Windows instances, this will just act as a regular Elastic Network Adapter, without the added EFA capabilities. Moreover, it doesn't support the t3a.medium instance type that is being used in the HPC cluster.

> **Enabling Enhanced Networking with Intel 82599 Virtual Function (VF) interface on the Windows EC2 Instances** is incorrect because although you can attach an Intel 82599 Virtual Function (VF) interface on your Windows EC2 Instances to improve its networking capabilities, it doesn't support the t3a.medium instance type that is being used in the HPC cluster.

> **Using AWS ParallelCluster to deploy and manage the HPC cluster to provide higher bandwidth, higher packet per second (PPS) performance, and lower inter-instance latencies** is incorrect because an AWS ParallelCluster is just an AWS-supported open-source cluster management tool that makes it easy for you to deploy and manage High Performance Computing (HPC) clusters on AWS. It does not provide higher bandwidth, higher packet per second (PPS) performance, and lower inter-instance latencies, unlike ENA or EFA.

<br />
