# Instance stores

The data in an instance store persists only during the lifetime of its associated EC2 instance. If an EC2 instance reboots whether intentionally or unintentionally, data in the instance store persists. However, data in the instance store is lost under any of the following circumstances:

* The underlying storage drive fails.

* The EC2 instance stops.

* The EC2 instance hibernates.

* The EC2 instance terminates.

Therefore, do not rely on instance stores for valuable, long-term data. Instead, use Amazon EBS for durable block storage.

Use instance stores for the correct data availability and durability use cases. Use only for data that can be easily recreated or does not need to persist after your EC2 instance is terminated.

An instance store is ideal ofr temporary storage of information that changes frequently, such as buffers, caches, scratch data, and other temporary content, or for data that is replicated across a fleet of instances, such as a load-balanced pool of web servers.

## Design Resilient Architectures ▶︎ EC2 Instance Store

* Ephemeral volumes

* Only certain EC2 instances

* Fixed capacity

* Disk type and capacity depends on EC2 instance type

* Application-level durability
