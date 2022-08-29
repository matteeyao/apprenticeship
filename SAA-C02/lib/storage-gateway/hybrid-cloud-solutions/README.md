# Tape Gateway - Hybrid Cloud Solutions

1. Which platform option offers offline cloud capabilities to deploy Tape Gateway?

**AWS Snowball Edge**

**Explanation**: Deploy Tape Gateway using AWS Snowball Edge for offline cloud capabilities when bandwidth is an issue in your on-premises environment.

2. What are some common capabilities across *all* AWS Storage Gateway Types? (Select two)

[x] Local caching

[ ] Network file system (NFS) v4 and v4.1 support

[x] Optimized data transfers to storage in AWS

[ ] Server Message Block (SMB)

[ ] Amazon Elastic Block Store (Amazon EBS) snapshots

**Explanation**: All Storage Gateway types use standard storage protocols, local caching, and secure and optimized data transfers.

3. What are the three elements of billing for AWS Storage Gateway?

**Storage, requests, and data transfer**

**Explanation**: You pay only for what you use with the AWS Storage Gateway. You are charged based on the amount of data transferred out of AWS, the type and amount of storage you use, and the requests you make. In addition, if you have deployed Storage Gateway using a hardware appliance, you also have the cost of the appliance.

4. What are common use cases for Amazon S3 File Gateway? (Select three)

[ ] Hosting applications

[x] Building data lakes

[x] Archiving

[x] Backups

5. How are the locally allocated disks used by the Amazon S3 File Gateway appliance?

**As a cache**

S3 File Gateway deploys on premises and connects with a local cache so it can provide low-latency access to frequently accessed data.

6. Which file system protocols does Amazon S3 File Gateway support?

**Server Message Block (SMB) and Network File System (NFS)**

**Explanation**: S3 File Gateway provides access to objects in Amazon S3 as files using NFS or SMB protocols.

7. What protocol is used to present your on-premises applications w/ block storage?

**Internet Small Computer Systems Interface (iSCSI)**

**Explanation**: Volume Gateway presents your applications with block storage using the iSCSI protocol.

8. Which volume types are available in Volume Gateway? (Select two)

[ ] Server message block (SMB)

[x] Cached

[ ] Upload buffer

[ ] Network file system (NFS)

[x] Stored

**Explanation**: Cached volume and stored volume are the two volume types available for Volume Gateway.

9. What are the uses for the local disks of the Volume Gateway for a cached volume? (Select two)

[ ] Amazon S3 bucket

[x] Cache

[ ] Amazon Elastic Block Store (Amazon EBS) snapshot

[x] Upload buffer

[ ] AWS Direct Connect

**Explanation**: Local disks are needed for cache storage and an upload buffer for a cached volume gateway.
