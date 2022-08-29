# AWS Elastic Map Reduce

1. A company is receiving semi-structured and structured data from different sources every day. The Solutions Architect plans to use big data processing frameworks to analyze vast amounts of data and access it using various business intelligence tools and standard SQL queries.

Which of the following provides the MOST high-performing solution that fulfills this requirement?

[ ] Create an Amazon EC2 instance and store the processed data in Amazon EBS.

[ ] Use Amazon Kinesis Data Analytics and store the processed data in Amazon DynamoDB.

[ ] Use AWS Glue and store the processed data in Amazon S3.

[x] Create an Amazon EMR cluster and store the processed data in Amazon Redshift.

**Explanation**: **Amazon EMR** is a managed cluster platform that simplifies running big data frameworks, such as Apache Hadoop and Apache Spark, on AWS to process and analyze vast amounts of data. By using these frameworks and related open-source projects, such as Apache Hive and Apache Pig, you can process data for analytics purposes and business intelligence workloads. Additionally, you can use Amazon EMR to transform and move large amounts of data into and out of other AWS data stores and databases.

Amazon Redshift is the most widely used cloud data warehouse. It makes it fast, simple and cost-effective to analyze all your data using standard SQL and your existing Business Intelligence (BI) tools. It allows you to run complex analytic queries against terabytes to petabytes of structured and semi-structured data, using sophisticated query optimization, columnar storage on high-performance storage, and massively parallel query execution.

![Fig. 1 Integration w/ Amazon Redshift](img/SAA-CO2/databases/elastic-map-reduce/fig01.png)

The key phrases in the scenario are "big data processing frameworks" and "various business intelligence tools and standard SQL queries" to analyze the data. To leverage big data processing frameworks, you need to use Amazon EMR. The cluster will perform data transformations (ETL) and load the processed data into Amazon Redshift for analytic and business intelligence applications.

Hence, the correct answer is: **Create an Amazon EMR cluster and store the processed data in Amazon Redshift**.

The option that says: **Use AWS Glue and store the processed data in Amazon S3** is incorrect because AWS Glue is just a serverless ETL service that crawls your data, builds a data catalog, performs data preparation, data transformation, and data ingestion. It won't allow you to utilize different big data frameworks effectively, unlike Amazon EMR. In addition, the S3 Select feature in Amazon S3 can only run simple SQL queries against a subset of data from a specific S3 object. To perform queries in the S3 bucket, you need to use Amazon Athena.

The option that says: **Use Amazon Kinesis Data Analytics and store the processed data in Amazon DynamoDB** is incorrect because Amazon DynamoDB doesn't fully support the use of standard SQL and Business Intelligence (BI) tools, unlike Amazon Redshift. It also doesn't allow you to run complex analytic queries against terabytes to petabytes of structured and semi-structured data.

The option that says: **Create an Amazon EC2 instance and store the processed data in Amazon EBS** is incorrect because a single EBS-backed EC2 instance is quite limited in its computing capability. Moreover, it also entails an administrative overhead since you have to manually install and maintain the big data frameworks for the EC2 instance yourself. The most suitable solution to leverage big data frameworks is to use EMR clusters.

<br />

2. A large telecommunications company needs to run analytics against all combined log files from the Application Load Balancer as part of the regulatory requirements.

Which AWS services can be used together to collect logs and then easily perform log analysis?

[ ] Amazon EC2 w/ EBS volumes for storing and analyzing the log files.

[ ] Amazon DynamoDB for storing and EC2 for analyzing the logs.

[ ] Amazon S3 for storing ELB log files and Amazon EMR for analyzing the log files.

[ ] Amazon S3 for storing the ELB log files and an EC2 instance for analyzing the log files using a custom-built application.

**Explanation**: In this scenario, it is best to use a combination of Amazon S3 and Amazon EMR: Amazon S3 for storing ELB log files and Amazon EMR for analyzing the log files. Access logging in the ELB is stored in Amazon S3 which means that the following are valid options:

* Amazon S3 for storing the ELB log files and an EC2 instance for analyzing the log files using a custom-built application.

* Amazon S3 for storing ELB log files and Amazon EMR for analyzing the log files.

However, log analysis can be automatically provided by Amazon EMR, which is more economical than building a custom-built log analysis application and hosting it in EC2. Hence, the option that says: **Amazon S3 for storing ELB log files and Amazon EMR for analyzing the log files** is the best answer between the two.

Access logging is an optional feature of Elastic Load Balancing that is disabled by default. After you enable access logging for your load balancer, Elastic Load Balancing captures the logs and stores them in the Amazon S3 bucket that you specify as compressed files. You can disable access logging at any time.

**Amazon EMR** provides a managed Hadoop framework that makes it easy, fast, and cost-effective to process vast amounts of data across dynamically scalable Amazon EC2 instances. It securely and reliably handles a broad set of big data use cases, including log analysis, web indexing, data transformations (ETL), machine learning, financial analysis, scientific simulation, and bioinformatics. You can also run other popular distributed frameworks such as Apache Spark, HBase, Presto, and Flink in Amazon EMR, and interact with data in other AWS data stores such as Amazon S3 and Amazon DynamoDB.

> The option that says: **Amazon DynamoDB for storing and EC2 for analyzing the logs** is incorrect because DynamoDB is a noSQL database solution of AWS. It would be inefficient to store logs in DynamoDB while using EC2 to analyze them.

> The option that says: **Amazon EC2 with EBS volumes for storing and analyzing the log files** is incorrect because using EC2 with EBS would be costly, and EBS might not provide the most durable storage for your logs, unlike S3.

> The option that says: **Amazon S3 for storing the ELB log files and an EC2 instance for analyzing the log files using a custom-built application** is incorrect because using EC2 to analyze logs would be inefficient and expensive since you will have to program the analyzer yourself.

<br />

3. A company has a set of Linux servers running on multiple On-Demand EC2 Instances. The Audit team wants to collect and process the application log files generated from these servers for their report.

Which of the following services is best to use in this case?

[ ] Amazon S3 Glacier for storing the application log files and Spot EC2 Instances for processing them.

[ ] Amazon S3 for storing the application log files and Amazon Elastic MapReduce for processing the log files.

[ ] A single On-Demand Amazon EC2 instance for both storing and processing the log files.

[ ] Amazon S3 Glacier Deep Archive for storing the application log files and AWS ParallelCluster for processing the log files.

**Explanation**: **Amazon EMR** is a managed cluster platform that simplifies running big data frameworks, such as Apache Hadoop and Apache Spark, on AWS to process and analyze vast amounts of data. By using these frameworks and related open-source projects such as Apache Hive and Apache Pig, you can process data for analytics purposes and business intelligence workloads. Additionally, you can use Amazon EMR to transform and move large amounts of data into and out of other AWS data stores and databases such as Amazon Simple Storage Service (Amazon S3) and Amazon DynamoDB.

![Fig. 1 EMR Flow](../../../img/databases/emr/fig01.png)

Hence, the correct answer is: **Amazon S3 for storing the application log files and Amazon Elastic MapReduce for processing the log files.**

> The option that says: **Amazon S3 Glacier for storing the application log files and Spot EC2 Instances for processing them** is incorrect as Amazon S3 Glacier is used for data archive only.

> The option that says: **A single On-Demand Amazon EC2 instance for both storing and processing the log files is incorrect as an EC2 instance** is not a recommended storage service. In addition, Amazon EC2 does not have a built-in data processing engine to process large amounts of data.

> The option that says: **Amazon S3 Glacier Deep Archive for storing the application log files and AWS ParallelCluster for processing the log files** is incorrect because the long retrieval time of Amazon S3 Glacier Deep Archive makes this option unsuitable. Moreover, AWS ParallelCluster is just an AWS-supported open-source cluster management tool that makes it easy for you to deploy and manage High-Performance Computing (HPC) clusters on AWS. ParallelCluster uses a simple text file to model and provision all the resources needed for your HPC applications in an automated and secure manner.

<br />
