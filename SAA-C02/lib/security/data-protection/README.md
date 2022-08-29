# Data Protection

1. A company’s security team requires that all data stored in the cloud be encrypted at rest at all times
using encryption keys stored on-premises.

Which encryption options meet these requirements? Select two.

[ ] Use Server-Side Encryption with Amazon S3 Managed Keys (SSE-S3).

[ ] Use Server-Side Encryption with AWS KMS Managed Keys (SSE-KMS).

[x] Use Server-Side Encryption with Customer Provided Keys (SSE-C).

[x] Use client-side encryption to provide at-rest encryption.

[ ] Use an AWS Lambda function triggered by Amazon S3 events to encrypt the data using the customer’s keys.

**Explanation**: Server-Side Encryption with Customer-Provided Keys (SSE-C) enables Amazon S3 to encrypt objects server side using an encryption key provided in the PUT request. The same key must be provided in GET requests for Amazon S3 to decrypt the object. Customers also have the option to encrypt data client side before uploading it to Amazon S3 and decrypting it after downloading it. AWS SDKs provide an S3 encryption client that streamlines the process.

<br />

2. A software development company is using serverless computing with AWS Lambda to build and run applications without having to set up or manage servers. They have a Lambda function that connects to a MongoDB Atlas, which is a popular Database as a Service (DBaaS) platform and also uses a third party API to fetch certain data for their application. One of the developers was instructed to create the environment variables for the MongoDB database hostname, username, and password as well as the API credentials that will be used by the Lambda function for DEV, SIT, UAT, and PROD environments.

Considering that the Lambda function is storing sensitive database and API credentials, how can this information be secured to prevent other developers in the team, or anyone, from seeing these credentials in plain text? Select the best option that provides maximum security.

[x] Create a new KMS key and use it to enable encryption helpers that leverage on AWS KMS to store and encrypt the sensitive information.

[ ] AWS Lambda does not provide encryption for the environment variables. Deploy your code to an EC2 instance instead.

[ ] Enable SSL encryption that leverages on AWS CloudHSM to store and encrypt the sensitive information.

[ ] There is no need to do anything b/c, by default, AWS Lambda already encrypts the environment variables using the AWS Key Management Service.

**Explanation**: When you create or update Lambda functions that use environment variables, AWS Lambda encrypts them using the AWS Key Management Service. When your Lambda function is invoked, those values are decrypted and made available to the Lambda code.

The first time you create or update Lambda functions that use environment variables in a region, a default service key is created for you automatically within AWS KMS. This key is used to encrypt environment variables. However, if you wish to use encryption helpers and use KMS to encrypt environment variables after your Lambda function is created, you must create your own AWS KMS key and choose it instead of the default key. The default key will give errors when chosen. Creating your own key gives you more flexibility, including the ability to create, rotate, disable, and define access controls, and to audit the encryption keys used to protect your data.

> **There is no need to do anything because, by default, AWS Lambda already encrypts the environment variables using the AWS Key Management Service** is incorrect. Although Lambda encrypts the environment variables in your function by default, the sensitive information would still be visible to other users who have access to the Lambda console. This is because Lambda uses a default KMS key to encrypt the variables, which is usually accessible by other users. The best option in this scenario is to use encryption helpers to secure your environment variables.

> **Enable SSL encryption that leverages on AWS CloudHSM to store and encrypt the sensitive information** is also incorrect since enabling SSL would encrypt data only when in-transit. Your other teams would still be able to view the plaintext at-rest. Use AWS KMS instead.

> **AWS Lambda does not provide encryption for the environment variables. Deploy your code to an EC2 instance instead** is incorrect since, as mentioned, Lambda does provide encryption functionality of environment variables.

<br />
