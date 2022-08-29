# AMI Types

1. A company is using an On-Demand EC2 instance to host a legacy web application that uses an Amazon Instance Store-Backed AMI. The web application should be decommissioned as soon as possible and hence, you need to terminate the EC2 instance.

When the instance is terminated, what happens to the data on the root volume?

[ ] Data is automatically saved as an EBS volume.

[ ] Data is unavailable until the instance is restarted.

[ ] Data is automatically saved as an EBS snapshot.

[x] Data is automatically deleted.

**Explanation**: **AMIs** are categorized as either *backed by Amazon EBS* or *backed by instance store*. The former means that the root device for an instance launched from the AMI is an Amazon EBS volume created from an Amazon EBS snapshot. The latter means that the root device for an instance launched from the AMI is an instance store volume created from a template stored in Amazon S3.

![Fig. 1 AMI Lifecycle](../../../../img/storage-services/elastic-block-storage/ami-types/fig01.png)

The data on instance store volumes persist only during the life of the instance which means that if the instance is terminated, the data will be automatically deleted.

Hence, the correct answer is: **Data is automatically deleted.**

<br />
