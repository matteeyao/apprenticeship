# Redshift Backup

1. A data analytics company, which uses machine learning to collect and analyze consumer data, is using Redshift cluster as their data warehouse. You are instructed to implement a disaster recovery plan for their systems to ensure business continuity even in the event of an AWS region outage.   

Which of the following is the best approach to meet this requirement?

[ ] Create a scheduled job that will automatically take the snapshot of your Redshift Cluster and store it to a S3 bucket. Restore the snapshot in case of an AWS region outage.

[x] Enable Cross-Region Snapshots Copy in your Amazon Redshift Cluster.

[ ] Do nothing b/c Amazon Redshift is a highly available, fully-managed data warehouse which can withstand an outage of an entire AWS region.

[ ] Use automated snapshots of your Redshift Cluster.

**Explanation**: You can configure Amazon Redshift to copy snapshots for a cluster to another region. To configure cross-region snapshot copy, you need to enable this copy feature for each cluster and configure where to copy snapshots and how long to keep copied automated snapshots in the destination region. When cross-region copy is enabled for a cluster, all new manual and automatic snapshots are copied to the specified region.

> The option that says: **Create a scheduled job that will automatically take the snapshot of your Redshift Cluster and store it to an S3 bucket. Restore the snapshot in case of an AWS region outage** is incorrect because although this option is possible, this entails a lot of manual work and hence, not the best option. You should configure cross-region snapshot copy instead.

> The option that says: **Do nothing because Amazon Redshift is a highly available, fully-managed data warehouse which can withstand an outage of an entire AWS region** is incorrect because although Amazon Redshift is a fully-managed data warehouse, you will still need to configure cross-region snapshot copy to ensure that your data is properly replicated to another region.

> **Using Automated snapshots of your Redshift Cluster** is incorrect because using automated snapshots is not enough and will not be available in case the entire AWS region is down.

<br />
