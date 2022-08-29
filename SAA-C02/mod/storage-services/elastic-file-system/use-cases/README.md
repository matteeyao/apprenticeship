# Amazon EFS use cases

## Big data analytics

Handy to use and scale, Amazon EFS offers the performance and consistency required for machine learning (ML) and big data analytics workloads.

## Web serving and content management

Amazon EFS file systems are scalable across an unconstrained quantity of file servers to generate the required scalability to support spikes in user demand.

Amazon EFS provides the ability to serve files to web applications quickly and in a scalable way to meet the demands of business. As website traffic increases, you can scale the number of web servers to support user demand and provide consistent access to the files stored in Amazon EFS with no need to modify or reconfigure the file system. Amazon EFS is designed for 11 nines of durability, so web content is stored in highly available, highly durable storage. And because Amazon EFS uses standard file system semantics, web developers can use naming and permissions that they are familiar with.

## Application testing and development

Amazon EFS provides development environments a common storage repository that gives you the ability to share code and other files in a secure and organized way.

## Media and entertainment

Media companies are heavy users of file storage throughout the media post production pipeline. This includes everything from ingesting data after it is captured on a recording device to distribution of the end product to consumers of the produced content.

At ingest, Amazon EFS provides high throughput and high durability, which helps companies generating content upload it to the cloud quickly and store it durably. There are also many compute-intensive process steps where having the data in a shared file system and providing access to on-demand compute resources helps to process jobs efficiently. Amazon EFS provides an NFS interface and access to thousands of EC2 instances that can be employed as needed to reduce cycle times, reduce idle time in the creative process, and keep creative professionals occupied.

## Database backups

Database backup is another use case for Amazon EFS that works particularly well. Amazon EFS provides an NFS file system, which is the preferred backup repository for many of the common database backup applications.

Customers are backing up Oracle, SAP HANA, IBM Db2, and others onto Amazon EFS using the management tools offered by those database vendors. The tools offer a lifecycle policy that allows for backups to remain on Amazon EFS for a defined period and then expired per the policy. With high levels of aggregate throughput, Amazon EFS allows database volumes to be quickly restored, providing low recovery time objective (RTO) for the applications using these databases.

## Container storage

Amazon EFS is ideal for container storage, providing persistent shared access to a common file repository.
