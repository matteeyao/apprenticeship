# EC2 questions

1. You need to know both the private IP and public IP address of your EC2 instance. You should _.

Retrieve the instance Metadata from `http://169.254.169.254/latest/meta-data/local-ipv4` and `http://169.254.169.254/latest/meta-data/public-ipv4`.

Instance Metadata and User Data can be retrieved from within the instance via a special URL. Similar information can be extracted by using the API via the CLI or an SDK. The ipconfig and ifconfig tools don't have the ability to see the Public IP Address directly, since it's attached dynamically inside the AWS Software Defined Network which has to be queried by the API.

2. Standard Reserved Instances can be moved between regions.

**False**. Standard Reserved Instances cannot be moved between regions. You can choose if a Reserved Instance applies to either a specific Availability ZOne, or an Entire Region, but you cannot change the region.

3. Spread Placement Groups can be deployed across multiple Availability Zones.

**True**. Spread Placement Groups can be deployed across availability zones since they spread the instances further apart. Cluster Placement Groups can only exist in one Availability Zone since they are focused on keeping instances together, which you cannot do across Availability Zones.

4. When creating a new security group, all inbound traffic is allowed by default.

**False**. There are slight differences between a normal 'new' Security Group and a 'default` security group in the default VPC. For a 'new' security group nothing is allowed in by default.

5. Which service would you use to run a general Windows File Server w/ minimal overhead?

**FSx for Windows**. Amazon FSx for Windows File Server provides a fully managed native Microsoft Windows file system so you can easily move your Windows-based applications that require shared file storage to AWS. *EBS Multi Attach* allows you to attach a volume to up to 16 instances, but would have issues across multiple availability zones, and could not use NTFS natively. *EFS* uses the NFS protocol, and is explicitly not supported on Windows. *S3* is object-based storage, and would not be suitable as the backend for a file server.

6. Is it possible to perform API actions on an existing Amazon EBS Snapshot?

Yes, it is possible to perform API actions on an existing Amazon EBS Snapshots.

It is possible to perform API actions on an existing Amazon EBS Snapshot through the AWS APIs, CLI, and AWS Console. You can use AWS APIs, CLI, or the AWS Console to copy snapshots, share snapshots, and create volumes from snapshots.

7. Which EC2 feature uses SR-IOV?

**Enhanced networking**. Enhanced networking uses single root I/O virtualization (SR-IOV) to provide high-performance networking capabilities on supported instance types. SR-IOV is a method of device virtualization that provides higher I/O performance and lower CPU utilization when compared to traditional virtualized network interfaces. Enhanced networking provides higher bandwidth, higher packet per second (PPS) performance, and consistently lower inter-instance latencies.

8. When can you attach/replace an IAM role on an EC2 instance?

To attach an IAM role to an instance that has no role, the instance can be in the stopped or running state. To replace the IAM  role on an instance that already has an attached IAM role, the instance must be in the running state.

IAM Roles can be attached to instances in the stopped or running state, or replaced for instances in the running state. Prior to early 2017, you would only be able to attach an IAM role at launch, and if you wanted to attach a role, you would have to terminate and re-launch the instance. Reference: [Attaching an IAM role to an instance](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/iam-roles-for-amazon-ec2.html#attach-iam-role).

9. The use of a cluster placement group is ideal _.

Your fleet of EC2 instances require high network throughput and low latency within a single availability zone.

Cluster Placement Groups are primarily about keeping you compute resources within one network hop of each other on high speed rack switches. This is only helpful when you have compute loads w/ network loads that are either very high or very sensitive to latency.

10. In order to enable encryption at rest using EC2 and Elastic Block Store, you must _.

Configure encryption when creating the EBS volume.

The use of encryption at rest is default requirement for many industry compliance certifications. Using AWS managed keys to provide EBS encryption at rest is a relatively painless and reliable way to protect assets and demonstrate your professionalism in any commercial situation.

11. What type of storage are Amazon's EBS volumes based on?

**Block-based**. EBS uses Block-based storage, where the data is stored on a virtual disk managed by the Operating System. *EFS* uses File-based storage, where the underlying filesystem is managed by AWS. *S3* uses Object-based storage, where files are kept in a flat structure.

12. Which of the following provide the least expensive EBS options?

**Cold (sc1)** and **Throughput Optimized (st1)**. Of all the EBS types, both current and of the previous generation, HDD based volumes will always be less expensive than SSD types. Therefore, of the options available in the question, the **Cold (Sc1)** and **Throughput Optimized (st1)** types are HDD based and will be the least expensive options.

13. Can Spread Placement Groups be deployed across multiple Availability Zones?

**Yes**. Spread Placement Groups can be deployed across availability zones since they spread the instance further apart. Cluster Placement Groups can only exist in one Availability Zone since they are focused on keeping instances together, which you cannot do across Availability Zones.

14. Which AWS CLI command should I use to create a snapshot of an EBS volume?

```zsh
aws ec2 create-snapshot
```

When looking at the AWS CLI, remember the vers, like `create`, which are used as part of commands. This helps you build the necessary command in your head, w/o referring to the documentation. For example, we might create a new image along w/ this snapshot. From this, we could understand that the command would likely be `aws ec2 create-image`.

15. Will an Amazon EBS root volume persist independently from the life of the terminated EC2 instance to which it was previously attached? In other words, if I terminated an EC2 instance, would that EBS root volume persist?

No - Unless 'Delete on Termination' is unchecked for the root volume.

You can control whether an EBS root volume is deleted when its associated instance is terminated. The default delete-on-termination behavior depends on whether the volume is a root volume, or an additional volume. By default, the `DeleteOnTermination` attribute for root volumes is set to `true`. However, this attribute may be changed at launch by using either the AWS Console or the command line. For an instance that is already running, the `DeleteOnTermination` attribute must be changed using the CLI.

16. To help you manage your Amazon EC2 instances, you can assign your own metadata in the form of _.

**Tags**. Tagging is a key part of managing an environment. Even in a lab, it is easy to lose track of the purpose of resources, and tricky to determine why it was created and if it is still needed. This can rapidly translate into lost time and lost money.

17. What are the valid underlying hypervisors for EC2?

AWS originally used a modified version of the **Xen** Hypervisor to host EC2. In 2017, AWS began rolling out their own Hypervisor called **Nitro**.

18. Specifically, where in the AWS Global Infrastructure are EC2 instances provisioned?

**In Availability Zones**. When you're setting up an EC2 instance, you select which subnet you'd like to place your EC2 instance in. Each subnet is tied to a specific availability zone. You cannot move an instance between Availability Zones, w/o setting up a copied version of the instance. Whilst they exist in Regions, they are not portable across the whole region, nor across the whole globe.

19. EBS Snapshots are backed up to S3 in what manner?

**Incrementally**. EBS snapshots use incremental backups and are stored in S3. Restores can be done from any of the snapshots. The original full snapshot can be safely deleted w/o impacting the ability to use the other related incremental backups.

20. Can I delete a snapshot of the root device of an EBS volume used by a registered AMI?

**No**> If the original snapshot was deleted, then the AMI would not be able to use it as the basis to create new instances. For this reason, AWS protects you from accidentally deleting the EBS Snapshot, since it could be critical to your systems. To delete an EBS Snapshot attached to a registered AMI, first remove the AMI, then the snapshot can be deleted.

21. To retrieve instance metadata or user data you will need to use the following IP Address:

`http://169.254.169.254`. This IP Address is specific to AWS, where you can use it on any instance to acquire information about that instance. It is a specific type of address called a 'link-local address', and is only accessible from that particular instance. You can also disable the metadata service to prevent it's misuse.

22. When updating the policy used by an IAM Role attached to an EC2 instance, what needs to happen for the changes to take effect?

Nothing - It will take effect almost immediately.

Changes to IAM Policies take effect almost immediately (w/ maybe a few seconds delay). No substantial waiting time is required, nor changes to the system. This is b/c the IAM Policy exists in the AWS API, rather than on the instance itself. As a way to remember it in a scenario, if you think about a compromised system, you would need to revoke the access immediately, without waiting for changes to take effect. Reference: [AWS IAM FAQs](https://aws.amazon.com/iam/faqs/). Real world note: While changes you make to IAM entities are reflected in the IAM APIs immediately, it can take noticeable time for the information to be reflected globally. In most cases, changes you make are reflected in less than a minute.

23. If an Amazon EBS volume is attached as an additional disk (not the root volume), can I detach it w/o stopping the instance?

Yes, although it may take some time.

Since the additional disk does not contain the operating system, you can detach it in the EC2 Console while the instance is running. However, any data on that drive would becomes inaccessible, and possibly cause problems for the EC2 instance.
