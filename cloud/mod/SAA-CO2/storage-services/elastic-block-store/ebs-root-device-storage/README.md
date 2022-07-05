# EBS Root Device Storage

* All AMI root volumes (where the EC2's OS is installed) are of two types: EBS-backed or Instance Store-backed

* When you delete an EC2 instance that was using an Instance Store-backed root volume, your root volume will also be deleted. Any additional or secondary volumes will persist however.

* If you use an EBS-backed root volume, the root volume will not be terminated w/ its EC2 instance when the instance is brought offline. EBS-backed volumes are not temporary storage devices like Instance Store-backed volumes.

* EBS-backed Volumes are launched from an AWS EBS snapshot, as the name implies

* Instance Store-backed Volumes are launched from an AWS S3 stored template. They are ephemeral, so be careful when shutting down an instance.

* Secondary instance stores for an instance-store backed root device must be installed during the original provisioning of the server. You cannot add more after the fact. However, you can add EBS volumes to the same instance after the server's creation.

* W/ these drawbacks of Instance Store volumes, why pick one? B/c they have a very high IOPS rate. So while an Instance Store can't provide data persistence, it can provide much higher IOPS compared to network attached storage like EBS.

* Further, Instance stores are ideal for temporary storage of information that changes frequently such as buffers, caches, scratch data, and other temporary content, or for data that is replicated across a fleet of instances, such as load-balanced pool of web servers.

* When to use one over the other?

  * Use EBS for DB data, critical logs, and application configs

  * Use Instance storage for in-process data, noncritical logs, and transient application state

  * Use S3 for data shared between systems like input datasets and processed results, or for static data needed by each new system when launched
