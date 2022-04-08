# Identity Access Management (IAM)

IAM allows you to manage users and their level of access to the AWS Console.

It is important to understand IAM and how it works, both for the exam and for administrating a company's AWS account in real life.

IAM allows you to set up users, groups, permissions, and roles, allowing you to grant access to different parts of the AWS platform.

Identity Access Management (IAM) offers the following features:

* Centralized control of your AWS account

* Shared Access your AWS account

* Granular permissions 

    * permissions to services and to create users, groups, and roles

* Identity Federation (including Active Directory, Facebook, LinkedIn, etc)

* Multifactor authentication

* Provide temporary access for users/devices and services where necessary

* Allows you to set up your own passed rotation policy

* Integrates w/ many different AWS services

* Supports PCI DSS Compliance (compliant framework for working w/ credit cards)


## Key terminology for IAM

Identity Access Management consists of the following:

1. **Users**: End Users such as people, employees of an organization, etc.

2. **Groups**: A collection of users. Each user in the group will inherit the permissions of the group.

    * If you apply a policy to a group, and you have users within that group, they're going to inherit that policy automatically.

3. **Policies**: Policies are made up of documents. These documents are in a format called JSON and they give **permissions** as to what a User/Group/Role is able to do.

4. **Roles**: You create roles and then assign them to AWS Resources. A role is a way of allowing one part of AWS to do something w/ another part. So you might give a virtual machine inside AWS the ability to write files to S3, which is a type of storage within AWS.

    * We created a role when we turned on cross region replication. If you actually go into IAM and have a look at your roles, you'll see a role there for cross region replication.


## Learning recap

* **IAM is universal**. It does not apply to regions at this time, so when you create a user, you're creating a user globally. Same when you create a role or when you create a group.

* The **"root account"** is simply the account created when you first setup your AWS account. It has complete Admin access.

* New Users have **No permissions** when first created → follows the principle of Least Privilege. That user is not going to have any rights or privileges until you grant them privileges. To give our new users permission, we created an administrator access policy and we assigned that to the Dev group. Likewise, when we look at S3, when we create out bucket, it's locked down, it's not public.

* New users are assigned **Access Key ID** & **Secret Access Keys** when first created. They use this to programmatically access the AWS ecosystem - console and/or programmatic access.

    * **These are not the same as a password**. You cannot use the Access Key ID and Secret Access Key to login to the console. You can use this to access AWS via the APIs and Command Line, however.

    * **You only get to view these once**. If you lose them, you have to regenerate them. So, save them in a secure location.

* Always setup Multifactor Authentication on your root account.

* You can create and customize your own password rotation policies.
