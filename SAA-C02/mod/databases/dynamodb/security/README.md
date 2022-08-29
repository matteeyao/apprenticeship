# Security

> * Encryption at rest using **KMS**

* DynamoDB encryption at rest provides enhanced security by encrypting all of your data using encryption keys stored in KMS, the key management service.

> * Site-to-site VPN
>
> * Direct Connect (DX)
>
> * IAM policies and roles

* Use IAM to manage the access permissions and implement security policies for both DynamoDB and DAX.

> * Fine-grained access

* IAM policy that allows users access to only certain attributes within DynamoDB table items

> * CloudWatch and CloudTrail
>
> * VPC endpoints

* Use VPC endpoints for DynamoDB to enable EC2 instances in your VPC to use their private IP addresses to access DynamoDB w/ no exposure to the public internet.
