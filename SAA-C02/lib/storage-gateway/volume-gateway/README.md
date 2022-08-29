# Storage Gateway Volume Gateway

1. A company currently stores data for on-premises applications on local drives. The chief technology officer wants to reduce hardware costs by storing the data in Amazon S3 but does not want to make modifications to the applications. To minimize latency, frequently accessed data should be available locally.

What is a reliable and durable solution for a solutions architect to implement that will reduce the cost of
local storage?

[ ] Deploy an SFTP client on a local server and transfer data to Amazon S3 using AWS Transfer for SFTP

[x] Deploy an AWS Storage Gateway volume gateway configured in cached volume mode.

[ ] Deploy an AWS DataSync agent on a local server and configure an S3 bucket as the destination. `(DataSync is used to transfer data to AWS)`

[ ] Deploy an AWS Storage Gateway volume gateway configured in stored volume mode.

**Explanation**: An AWS Storage Gateway volume gateway connects an on-premises software application with cloud- backed storage volumes that can be mounted as Internet Small Computer System Interface (iSCSI) devices from on-premises application servers. In cached volumes mode, all the data is stored in Amazon S3 and a copy of frequently accessed data is stored locally.
