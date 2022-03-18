# HPC on AWS

> It's never been easier to get started w/ **high-performance computing** (HPC) than in any other time in history - and AWS is the perfect place to perform it
>
> You can create a large number of resources in almost no time. You only pay for the resources you use - and, once finished, **you can destroy the resources**
>
> HPC is used for industries such as genomics, finance, and financial risk modeling, machine learning, weather prediction, and even autonomous driving

## Achieving HPC on AWS

What are the different services we can use to achieve HPC on AWS?

1. Data transfer

2. Compute and networking

3. Storage

4. Orchestration and automation

## Data Transfer

> What are some ways we can get our data into AWS?
>
> * Snowball, Snowmobile (**terabytes/petabytes** worth of data)
>
> * **AWS DataSync** to store on S3, EFS, FSx for Windows, etc
>
> * Direct Connect

### Data Transfer: AWS Direct Connect

> **AWS Direct Connect** is a cloud service solution that makes it easy to establish a dedicated network connection from your premises to AWS. Using AWS Direct Connect, you can establish private connectivity between your AWS and your data center, office, or co-location environment - which, in many cases, **can reduce your network costs, increase bandwitdth throughput, and provide a more consistent network experience** than internet-based connections.

* **AWS Direct Connect** is a dedicated line from your data center into AWS, and in many cases, this can reduce your network costs, increase the bandwidth throughput b/c it's dedicated to you and provide a more consistent network experience than internet based connections

## Compute & Networking

What are the **compute and networking services** that allow us to achieve HPC on AWS?

* EC2 instances that are GPU or CPU optimized

* EC2 fleets (Spot Instances or Spot Fleets)

* Placement groups (cluster placement groups)

* Enhanced networking

* Elastic Network Adapters

* Elastic Fabric Adapters

### Enhanced networking

> * Enhanced networking uses **single root I/O virtualization (SR-IOV)** to provide high-performance networking capabilities on supported instance types. SR-IOV is a method of device virtualization that provides **higher I/O performance** and **lower CPU utilization** when compared to traditional virtualized network interfaces.
>
> * Enhanced networking provides higher bandwidth, higher packet per second (PPS) performance, and consistently lower inter-instance latencies. There is no additional charge for using enhanced networking.
>
> * Use where you want good **networking performance**.

> **Depending on your instance type, enhanced networking can be enabled using an:**
>
> * **Elastic Network Adapter (ENA)**, which supports network speeds of up to **100 Gbps** for supported instance types.
>
> **or**
>
> * Intel 82599 **Virtual Function (VF)** interface, which supports network speeds of up to **10 Gbps** for supported instance types. This is typically used on older instances **(LEGACY)**

> [!NOTE]
> In any **scenario question**, if given the option, you probably want to **choose ENA over VF**

### Elastic Fabric Adapters

> * An **Elastic Fabric Adapter (EFA) is a network device you can attach to your Amazon EC2 instance to accelerate HPC and machine learning applications
>
> * EFA provides **lower, more consistent latency** and **higher throughput** than the TCP transport traditionally used in cloud-based HPC systems
>
> * EFA can use **OS-bypass**, which enables HPC and machine learning applications to bypass the operating system kernel and communicate directly w/ the EFA device. It makes it a lot faster w/ much lower latency. It is not supported w/ Windows currently - only Linux.

## Storage

> What are the **storage services** that allow us to achieve HPC on AWS?
>
> **Instance-attached storage**:
>
> * **EBS**: Scale up to 64,000 IOPS w/ Provisioned IOPS (PIOPS)
> 
> * **Instance Store**: Scale to millions of IOPS; low latency
>
> **Network storage**:
>
> * **Amazon S3**: Distributed object-based storage; not a file system
>
> * **Amazon EFS**: Scale IOPS based on total size, or use Provisioned IOPS
>
> * **Amazon FSx for Lustre**: HPC-optimized distributed file system; millions of IOPS, which is also backed by S3

## Orchestration and Automation

> **What are the orchestration and automation services that allow us to achieve HPC on AWS?**

> **AWS Batch**
>
> * **AWS Batch** enables developers, scientists, and engineers to easily and efficiently run hundreds of thousands of batch computing jobs on AWS.
>
> * AWS Batch supports multi-node parallel jobs, which allows you to run a single job that spans **multiple EC2 instances**.
>
> * You can easily schedule jobs and launch **EC2 instances** according to your needs.

> **AWS ParallelCluster**
>
> 1. Open-source cluster management tool that **makes it easy for you to deploy and manage** HPC clusters on AWS.
>
> 2. ParallelCluster uses a **simple text file to model and provision all the resources needed** for your HPC applications in an automated and secure manner.
>
> 3. **Automate creation** of VPC, subnet, cluster type, and instance types.

## Learning summary

We can achieve HPC on AWS through

* Data transfer

    * Snowball, Snowmobile (**terabytes/petabytes worth of data**)

    * AWS DataSync to store on S3, EFS, FSx for Windows, etc

    * Direct Connect

* Compute and networking

    * EC2 instances that are **GPU** or **CPU** optimized

    * **EC2 fleets** (Spot Instances or Spot Fleets)

    * Placement groups (cluster placement groups)

    * Enhanced networking single root I/O virtualization (**SR-IOV**)

    * Elastic Network Adapters or **Intel 82599 Virtual Function** (VF) interface

    * Elastic Fabric Adapters

* Storage

    * Instance-attached storage:

        * **EBS**: Scale up to 64,000 IOPS w/ Provisioned IOPS (PIOPS)

        * **Instance Store**: Scale to millions of IOPS; low latency

    * Network storage:

        * **Amazon S3**: Distributed object-based storage; not a file system

        * **Amazon EFS**: Scale IOPS based on total size, or use Provisioned IOPS

        * **Amazon FSx for Lustre**: HPC-optimized distributed file system; millions of IOPS, which is also backed by S3

* Orchestration and automation

    * AWS Batch

    * AWS ParallelCluster
