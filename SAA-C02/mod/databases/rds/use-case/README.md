# Read-heavy OLTP application: Amazon RDS

In this online transaction processing (OLTP) application, Amazon Relational Database Service (Amazon RDS) is used with a read replica, which is asynchronously updated. Amazon RDS read replicas make it easy to elastically scale out for read-heavy database workloads beyond the capacity constraints of a single database instance. You can create one or more replicas of a given source database instance and serve high-volume application read traffic from multiple copies of your data, thereby increasing aggregate read throughput.

![Fig. 1 Read-heavy OLTP application: Amazon RDS](../../../../../img/SAA-CO2/databases/rds/use-case/diag01.png)
