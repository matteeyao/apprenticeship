# Simple Storage Service

1. Which style of URL lists the bucket name first, and makes the URL more user friendly and easy to read?

**Virtual hosted-style URLs**

In a virtual-hosted-style URL, the bucket name is part of the domain name in the URL, which makes the URL easier to read and more end-user friendly.

2. What is the definition of object based storage?

**Object storage is a method of storing files in a flat address space based on attributes and metadata.**

3. The Block Public Access feature is enabled by default?

**True**

AWS introduced the S3 Block Public Access feature to help you avoid inadvertent data exposure. W/ Block Public Access, you can manage public access of your Amazon S3 resources at both the AWS account level and the bucket level. This helps ensure that your data is not made publicly available. Any new bucket created has *block all public access* enabled by default.

4. How can you organize your objects to mimic a folder hierarchy?

**Use prefixes and delimiters to group items.**

In object storage, you can organize your objects to imitate a hierarchy by using key name prefixes and delimiters. Prefixes and delimiters allow you to group similar items and helps to visually organize and more easily retrieve your data.
