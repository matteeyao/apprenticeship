# Elastic Computer Cloud (EC2)

> Amazon Elastic Compute Cloud (Amazon EC2) is a web service that provides resizable compute capacity in the cloud. Amazon EC2 reduces the time required to obtain and boot new server instances to minutes, allowing you to quickly scale capacity, both up and down, as your computing requirements change.
>
> W/ **EC2**, you are able to provision virtual machines in the cloud, ready in minutes. When your EC2 instance is running, you are charged on CPU, memory, storage, and networking. When it is stopped, you are only charged for EBS storage.

## EC2 Overview

**EC2** is a VaaS (virtualization as a service) and IaaS (infrastructure as a service) product. Virtualization is the running of one or more operating systems on a piece of physical hardware known as a server. Each operating system is separate along w/ their applications and also allows multiple different privileged applications to run on that same hardware using software to make their calls.

![Fig. 1 EC2 and Virtualization](../../../img/SAA-CO2/elastic-compute-cloud/diagram-i.png)

EC2 instances are virtual machines, so the operating system also has resources such as memory storage, CPU, etc. Like we just discussed, EC2 instances run on an EC2 host, which is the physical hardware and servers that AWS manages for us.

![Fig. 2 EC2 architecture](../../../img/SAA-CO2/elastic-compute-cloud/diagram-ii.png)

EC2 instances run on an **EC2 host**, the physical hardware and servers that AWS manages. EC2 is the default AWS compute service and EC2 hosts are either **shared host** or **dedicated host**. **Shared Hosts** are the default for EC2 host and are shared among different AWS customers. The customers do not get any ownership of the host hardware, so we pay for our individual instances and resources, but it is important to remember that when we use a shared host, our EC2 instance is isolated from other AWS customers and their instances. W/ **Dedicated Hosts**, you are paying for the entire EC2 host, not just the instances that you run on that host. W/ a **dedicated host**, you do not share this host w/ any other AWS customers. You pay for the entire host no matter how many EC2 instances you run.

![Fig. 3 Shared vs. Dedicated Hosts](../../../img/SAA-CO2/elastic-compute-cloud/diagram03.png)

EC2 instances are an Availability Zone resilient service b/c the **EC2 hosts** sits within an Availability Zone, so if that Availability Zone fails, then the host and the instances running on that host will most likely fail. EC2 hosts that sit inside an Availability Zone also have a local storage called **Instance Store**. The **instance store** is temporary storage and if your EC2 instance moves off of one host to another EC2 host, then your storage in the **instance store** is lost.

For networking, you will often hear that security groups are attached to EC2 instances, but this is not necessarily true. When an EC2 instance is lodged into a specific subnet inside your VPC, a primary **Elastic Network Interface** (ENI) is provisioned into that subnet and then mapped to the physical hardware of the EC2 hosts for that Availability Zone. You can add multiple different **ENIs** to your EC2 instance.

Let's go back to storage and discuss how an EC2 instance can connect to an Elastic Block Store (EBS). **EBS** lets you access volumes of persistent storage. Remember **Instance Store** is temporary storage, but **EBS** gives us persistent storage. So inside your VPC you have a data network set up for your **ENIs**, but you also have a storage network to connect to your **EBS** volumes. Again, **EBS** is an Availability Zone resilience service so you can have different **EBS** volumes running in different subnets for different EC2 instances, but you cannot connect EC2 instances to EBS volumes in a different availability zone.

## EC2 Behaviors

> 1. **What happens to your EC2 instance running on a specific host if it is restarted?**
>
> * The EC2 instance will stay w/ the same EC2 host and thus, stay w/ the same **instance store** as well.
>
> 2. **What if AWS stops the EC2 instance for maintenance?**
>
> * If that instance is stopped and then started, or if the AWS stops the instance for maintenance, then the EC2 instance will be reassigned to another host in the same availability zone. 
>
> 3. **Can you migrate your EC2 instance to another Availability Zone?**
>
> * So, EC2 instances live and remain inside one availability zone, but you can migrate your EC2 instance by copying your instance and then creating a new instance in a new availability zone. **Snapshots** and **AMIs** are great tools to help w/ this.

## EC2 Key Details

> * You can launch different types of instances from a single AMI. An instance type essentially determines the hardware of the host computer used for your instance. Each instance type offers different compute and memory capabilities. YOu should select an instance type based on the amount of memory and computing power that you need for the application or software that you plan to run on top of the instance.
>
> * You have the option of using dedicated tenancy w/ your instance. This means that within an AWS data center, you have exclusive assess to physical hardware. Naturally, this option incurs a high cost, but it makes sense if you work w/ technology that has a strict licensing policy.
>
> * W/ **EC2 VM Import**, you can import existing VMs into AWS as long as those hosts use VMware ESX, VMWare Workstation, Microsoft Hyper-V, or Citrix Xen virtualization formats.
>
> * When you launch a new EC2 instance, EC2 attempts to place the instance in such a way that all of your VMs are spread out across different hardware to limit failure to a single location. You can use **placement groups** to influence the placement of a group of independent instances that meet the needs of your workload.
>
> * By default, the public IP address of an EC2 Instance is released when the instance is stopped even if its stopped temporarily. Therefore, it is best to refer to an instance by its external DNS hostname. If you require a persistent public Ip address that can be associated to the same instance, use an Elastic IP address which is basically a static IP address instead.
>
> * If you have requirements to self-manage a SQL database, EC2 can be a solid alternative to RDS. To ensure high availability, remember to have at least one other EC2 instance in a separate Availability zone so even if a DB instance goes down, the other(s) will still be available.
>
> * A **golden image** is simply an AMI that you have fully customized to your liking w/ all necessary software/data/configuration details set and ready to go once. This personal AMI can then be the source from which you launch new instances.
>
> * Instance status checks check the health of the running EC2 server, systems status check monitor the health of the underlying hypervisor. If you ever notice a systems status issue, just stop the instance and start it again (no reboot is required) as the VM will start up again on a new hypervisor.

## EC2 Instance Pricing

> 1. **On-Demand instances** are based on a fixed rate by the hour or second. As the name implies, you can start an On-Demand instance whenever you need one and can stop it when you no longer need it. There is no requirement for a long-term commitment.
>
> * Allows you to pay a fixed rate by the hour (or by the second) with no commitment.
>
> * So you can spin it up, have it run for a couple of hours, and then terminate the instance.

> 2. **Reserved* instances** ensure that you keep exclusive use of an instance on 1 or 3 year contract terms. The long-term commitment provides significantly reduced discounts tat the hourly rate.
>
> * Provides you w/ a capacity reservation, and offers a significant discount on the hourly charge for an instance
>
> * Contract Terms are 1 year or 3 year terms

> 3. **Spot instances** take advantage of Amazon's excess capacity and work in an interesting manner. In order to use them, you must financially bid for access. B/c Spot instances are only available when Amazon has excess capacity, this option makes sense only if your app has flexible start and end times. You won't be charged if your instance stops due to a price change (e.g., someone else just bid a higher price for the access) and so consequently your workload doesn't complete. However, if you terminate an instance yourself you will be charged for any hour the instance ran. Spot instances are normally used in batch processing jobs.
>
> * Enables you to bid whatever price you want for instance capacity, providing for even greater savings if your applications have flexible start and end times
>
> * Basically, when Amazon has excess capacity, when not everyone is using EC2 at once, prices of EC2 instances will decline to try and get people to use that capacity. However, when other people are provisioning on-demand or other EC2 instances, and EC2 instances run out of capacity, that capacity will be needed back. So the price moves around w/ spot instances.
>
> * You set the price that you want to bid at. If it hits that price, you have your instances. If it goes above, you lose your instances.

> 4. **Dedicated Hosts**: Physical EC2 server dedicated for your use. Dedicated Hosts can help you reduce costs by allowing you to use your existing server-bound software licenses
>
> * Useful where you've got existing server bound software licenses, or perhaps a regulation saying that you cannot use multi-tenant virtualization

## On-Demand Pricing

On Demand pricing is useful for:

* Users that want the low cost and flexibility of Amazon EC2 without any up-front payment or long-term commitment

* Applications w/ short term, spiky, or unpredictable workloads that cannot be interrupted

* Applications being developed or tested on Amazon EC2 for the first time

## Reserved Pricing

Reserved pricing is useful for:

* Applications w/ steady state or predictable usage

* Applications that require reserved capacity

* Users able to make upfront payments to reduce their total computing costs even further

### Reserved Pricing types

> 1. **Standard Reserved Instances** have inflexible reservations that are discounted at 75% off of On-Demand instances. Standard Reserved Instances cannot be moved between regions. You can choose if a Reserved Instance applies to either a specific Availability Zone, or an Entire Region, but you cannot change the region. These offer up to 75% off on demand instances. The more you pay up front and the longer the contract, the greater the discount.

> 2. **Convertible Reserved instances** are instances that offer up to 54% off of On-Demand instances, but you can also modify the instance type and change the attributes of the Reserved Instances (RI) as long as the exchange results in the creation of Reserved Instances of equal or greater value at any point.
>
> * For example, let's say you suspect that after a few months your VM might need to change from general purpose to memory optimized, but you aren't sure just yet. So if you think that in the future you might need to change your VM type or upgrade your VMs capacity, choose Convertible Reserved Instances. There is no downgrading instance types w/ this option though.

> [!NOTE]
> The way EC2 works is you have a virtual machine, but you have different types of virtual machines. So you'll have ones that have very high RAM utilization, or you'll have ones that have very good CPUs that you can pick and choose, and they're called "instance types."
>
> Now w/ standard Reserved Instances (RI), you can't convert one reserved instance to another. So if you get a T2, and you want to go over to an R4, you can't do that w/ **Standard RI**.
>
> W/ **Convertible RI**, you can change between your different instance types.

> 3. **Scheduled Reserved Instances** are reserved according to a specified timeline that you set. For example, you might use Scheduled Reserved Instances if you run education software that only needs to be available during school hours. This option allows you to better match your needed capacity w/ a recurring schedule so that you can save money.
>
> * These are available to launch within the time windows you reserve.
>
> * This option allows you to match your capacity reservation to a predictable recurring schedule that only requires a fraction of a day, a week, or a month.

## Spot pricing

Recall, spot pricing is Amazon selling off their excess capacity at a lower rate

> If the Spot instance is terminated by Amazon EC2, you will not be charged for a partial hour of usage. However, if you terminate the instance yourself, you will be charged for any hour in which the instance ran.

Spot pricing is useful for:

* Applications that have flexible start and end times

* Applications that are only feasible at very low compute prices

* Users w/ urgent computing needs for large amounts of additional capacity

## Dedicated hosts pricing

Dedicated Hosts pricing is useful for:

* Useful for regulatory requirements that may not support multi-tenant virtualization

* Great for licensing which does not support multi-tenancy or cloud deployments

* Can be purchased On-Demand (hourly)

* Can be purchased as a Reservation for up to 70% off the On-Demand price

## EC2 instance types

| **Family** | **Specialty**                                     | **Use case**                                                                                                            |
|------------|---------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------|
| **F1**     | Field Programmable Gate Array                     | Genomics research, financial analytics, real-time video processing, big data, etc                                       |
| **I3**     | High Speed Storage                                | NoSQL DBs, Data Warehousing, etc                                                                                        |
| **G3**     | Graphics Intensive                                | Video Encoding / 3D Application Streaming                                                                               |
| **H1**     | High Disk Throughput                              | MapReduce-based workloads, distributed file systems such as HDFS and MapR-FS                                            |
| **T3**     | Lowest Cost, General Purpose                      | Web Servers / Small DBs                                                                                                 |
| **D2**     | Dense Storage                                     | Fileservers / Data Warehousing / Hadoop                                                                                 |
| **R5**     | Memory Optimized                                  | Memory Intensive Apps/DBs                                                                                               |
| **M5**     | General Purpose                                   | Application Servers                                                                                                     |
| **C5**     | Compute Optimized                                 | CPU Intensive Apps/DBs                                                                                                  |
| **P3**     | Graphics/General Purpose GPU                      | Machine Learning, Bitcoin mining, etc                                                                                   |
| **X1**     | Memory Optimized                                  | SAP HANA / Apache Spark etc                                                                                             |
| **Z1D**    | High compute capacity and a high memory footprint | Ideal for electronic design automation (EDA) and certain relational database workloads w/ high per-core licensing costs |
| **A1**     | Arm-based workloads                               | Scale-out workloads such as web servers                                                                                 |
| **U-6tb1** | Bare Metal                                        | Bare metal capabilities that eliminate virtualization overhead                                                          |

* **General Purpose** instances are the default steady state workload.

* **Compute Optimized** instances are designed for high-performance computing (media processing, machine learning, gaming, scientific modeling). The resource ration for compute optimize is usually more CPU than memory, and they can provide access to high performance CPUs.

* **Memory Optimized** instances are applicable for processing large in-memory datasets, and database workloads. The resource ration is usually more memory than CPU.

* **Accelerated Computing** instances are designed for specific requirements, like hardware GPU, field programmable gate arrays (FPGAs)

* **Storage Optimized** instances are designed for applications using data warehousing, analytic workloads, Elasticsearch, sequential and random IOs, etc.

## Types of Instances

> ### Bustable Performance Instances
>
> Bustable Performance Instances have a normal CPU load, which is usually low, and then you have an allocation of burst credits that allows you to burst up and then return to the normal level. Bustable instances are usually a great and cheaper option.

* AMIs are regional so each region has its own regional AMIs and AMIs can only be used in the region it is in. AMIs also control permissions by default. And that's specific to your AWS account or add specific AWS accounts onto that AMI. You can use a template or create an AMI from an existing EC2 instance to take the current configuration, make a template, and then make more instances (certain operating system, or certain volumes attached and configured for an exact business requirement).

* AMI banking is the concept of creating an AMI from a configured instance, plus that instance application.

* AMIs cannot be updated. We would have to update the configuration, and then create a new AMI. AMIs can be copied across regions and default permissions are only for our AWS account, but we can update those permissions.

* AMIs do contain EBS snapshots, so you're going to be billed for that data that is stored.

## EC2 Instance Types mnemonic

* **F** → for FPGA

* **I** → for IOPS

* **G** → Graphics

* **H** → High Disk Throughput

* **T** → cheap general purpose (think T2 Micro)

* **D** → for Density

* **R** → for RAM

* **M** → Main choice for general purpose apps

* **C** → for Compute

* **P** → Graphics (think Pics)

* **X** → Extreme Memory

* **Z** → Extreme Memory AND CPU

* **A** → Arm-based workloads

* **U** → Bare Metal

## Learning summary

Amazon Elastic Compute Cloud (Amazon EC2) is a web service that provides resizable compute capacity in the cloud. Amazon EC2 reduces the time required to obtain and boot new server instances to minutes, allowing you to quickly scale capacity, both up and down, as your computing requirements change.

If the Spot instance is terminated by Amazon EC2, you will not be charged for a partial hour of usage. However, if you terminate the instance yourself, you will be charged for any hour in which the instance ran.

> ### EC2 Behaviors
>
> 1. **What happens to your EC2 instance running on a specific host if it is restarted?**
>
> ▶︎ The **EC2 instance** will remain w/ the same EC2 host.
>
> ▶︎ Usually, an **EC2 Host** contains lots of different EC2 instances of the same type, but different sizes.
>
> 2. **What if AWS stops the EC2 instance for maintenance, etc.?**
>
> ▶︎ The **instance** will be reassigned to another EC2 host in the same Availability Zone.
>
> 3. **Can you migrate your EC2 instance to another Availability Zone?**
>
> ▶︎ EC2 instances live and remain inside one Availability Zone, but you can migrate your EC2 instance by copying your EC2 instance and then creating a new EC2 instance inside a new Availability Zone. The tool used for this is known as **Snapshots** and **AMIs**.

* Data transferred between Amazon EC2, Amazon RDS, Amazon Redshift, Amazon ElastiCache instances, and Elastic Network Interfaces in the same Availability Zone is free. Instead of using the public network to transfer the data, you can use the private network to reduce the overall data transfer costs. Even if the instances are deployed in the same Region, they could still be charged with inter-Availability Zone data transfers if the instances are distributed across different availability zones. You must deploy the instances in the same Availability Zone to avoid the data transfer costs.
