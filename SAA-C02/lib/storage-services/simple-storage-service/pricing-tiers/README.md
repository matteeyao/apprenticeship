# S3 Pricing Tiers

1. A company has stored 200 TB of backup files in Amazon S3. The files are in a vendor-proprietary format. The Solutions Architect needs to use the vendor's proprietary file conversion software to retrieve the files from their Amazon S3 bucket, transform the files to an industry-standard format, and re-upload the files back to Amazon S3. The solution must minimize the data transfer costs.

Which of the following options can satisfy the given requirement?

[ ] Deploy the EC2 instance in the same Region as Amazon S3. Install the file conversion software on the instance. Perform data transformation and re-upload it to Amazon S3.

[ ] Export the data using AWS Snowball Edge device. Install the file conversion software on the device. Transform the data and re-upload it to Amazon S3.

[ ] Install the file conversion software in Amazon S3. Use S3 Batch Operations to perform data transformation.

[ ] Deploy the EC2 instance in a different Region. Install the conversion software on the instance. Perform data transformation and re-upload it to Amazon S3.

**Explanation**: You pay for all bandwidth into and out of Amazon S3, except for the following:

* Data transferred in from the Internet.

* Data transferred out to an Amazon EC2 instance, when the instance is in the same AWS Region as the S3 bucket (including to a different account in the same AWS region).

* Data transferred out to Amazon CloudFront.

To minimize the data transfer charges, you need to deploy the EC2 instance in the same Region as Amazon S3. Take note that there is no data transfer cost between S3 and EC2 in the same AWS Region. Install the conversion software on the instance to perform data transformation and re-upload the data to Amazon S3.

Hence, the correct answer is: **Deploy the EC2 instance in the same Region as Amazon S3. Install the file conversion software on the instance. Perform data transformation and re-upload it to Amazon S3.**

> The option that says: **Install the file conversion software in Amazon S3. Use S3 Batch Operations to perform data transformation** is incorrect because it is not possible to install the software in Amazon S3. The S3 Batch Operations just runs multiple S3 operations in a single request. It can’t be integrated with your conversion software.

> The option that says: **Export the data using AWS Snowball Edge device. Install the file conversion software on the device. Transform the data and re-upload it to Amazon S3** is incorrect. Although this is possible, it is not mentioned in the scenario that the company has an on-premises data center. Thus, there's no need for Snowball.

> The option that says: **Deploy the EC2 instance in a different Region. Install the file conversion software on the instance. Perform data transformation and re-upload it to Amazon S3** is incorrect because this approach wouldn't minimize the data transfer costs. You should deploy the instance in the same Region as Amazon S3.

<br />
