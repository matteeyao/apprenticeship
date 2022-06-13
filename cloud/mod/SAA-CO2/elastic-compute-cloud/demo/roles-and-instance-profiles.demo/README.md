# Using EC2 Roles and Instance Profiles

## Introduction

> AWS Identity and Access Management (IAM) roles for Amazon Elastic Compute Cloud (EC2) provide the ability to grant instances temporary credentials. These temporary credentials can then be used by hosted applications to access permissions configured within the role. IAM roles eliminate the need for managing credentials, help mitigate long-term security risks, and simplify permissions management. Prerequisites for this lab include understanding how to log in to and use the AWS Management Console, EC2 basics (including how to launch an instance), IAM basics (including users, policies, and roles), and how to use the AWS CLI.

> [!NOTE]
> When connecting to the bastion host and the web server, do so independently of each other. The bastion host is used for interacting w/ AWS services via the CLI.

![Roles and Instance Profiles architecture](https://s3.amazonaws.com/assessment_engine/production/labs/2eb1d816-31b5-4a2c-959e-b4e7140df731/lab_diagram_Lab_-_Using_EC2_Roles_and_Instance_Profiles.001.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVKPCGNLN4XK67LDS%2F20220315%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220315T143259Z&X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEPr%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLWVhc3QtMSJGMEQCICHQqqZY5xH1xgK769ZeQ0oUvAHH6%2BnPbLfhb5xp4fBwAiBprEoAfiFQOh2mqvlb%2BrtEBAq6pNgvWKGCHCmJMI%2FodSqDBAhzEAIaDDM2NjA4MzQ2Nzk5NSIMsjKwQQNYUaY0%2B7uzKuADvz5MtLipBVHrG6OMd2gL1Jfd4wd5%2BB1L88ukJduCuSQdkh02qO%2BJeRvSq4tmNjtK7XqhOL8QG4DAmW35FCPQpFmS8SCvM836pT%2F1doFcpceAIRmVjB3Q%2Bxh4RprIT4a2yDw85vT%2Br4QPW5nNLmvK3YZ1FbH3d%2BAdioEpRl8edxtk59p%2FFo42ezasKcDfgkYHjai3KqW5XVsqyBaK5NwAwbLo8SMEKGUPIxId%2B5bYb1d2IQ4PL9CcGZj6oBLDXcg4uzmOBlyhPGp24jts6mRIHH8UxyM4N5laQzI6%2F7nz8bt%2FBN9f1KTJs8BUWyD%2FTg7YhENznz4yu8sK3BumdXdJ2ZbTMPeXAVyeNprGZXD9LRH72QqKn60wWgOnIT9JAwvZ48FfjLLt6CwAYeDpnIFNSG6x5DjGwMhIlexEM2DpG5UnSRhvdbDWAr5AopMp4zxXt4MlVIOqP03ti1gkiaM2qs1%2B9OQ1nK0ttW4aLwIeXRgVRCOIHjYD2LTLDvEZlePL60SEXM7xyl9nQHqvECICrhAe%2BIK8%2BfvcE6q0v%2Bbu4JbiMH7OE%2Fl0NzwWPTf8maw97iyk3pU4MYUXkDoqcFbAyopCLMBGlx%2FuBfPUvUIsGSXXIKe0U7W5pZgDTZsE1sAAMKC4wZEGOqYBtYgtIFr%2B66eFM%2BXKNJI1a8dpIjdXbKT689dgAa9pqqDZyBPzt3%2FK2Lq5zy1JDxguq29flq7AzKpjJFfMHfQAXRUWhFzcBmZJRyOU9p0pz7dFObBsFX%2FIh0yyie4%2FN1Sj%2BaYkKBode8%2FPXEwmlP1i6mWDZuw6jZDd88JhHz09A1nXFT0MNdk0HxoaHEx61lqKPseSTZVEeIgWlYQ8KuYvH0iw46OT8w%3D%3D&X-Amz-SignedHeaders=host&X-Amz-Signature=455844cf8f2b5000d136bbe2cbf1e0545df7d3520717efcc5f2f32fa1c781411)

Applications that run on an EC2 instance must include AWS credentials in their AWS API requests. You could have your developers store AWS credentials directly within the EC2 instance, allowing applications in that instance to use those credentials, but developers would then have to manage their credentials, ensure they securely pass the credentials to each instance, and update each instance when it's time to rotate the credentials.

In this lab, we'll create some Roles and associate permissions w/ those roles to access the S3 buckets. Now the roles are entities that define a set of permissions for making AWS service requests. Think of an IAM role for EC2 as "what you can do", but you can associate a role directly w/ an EC2 instance and you need instance profile to do so.

An instance profile is an entity or a container that's used for connecting an IAM role to an EC2 instance. So think of an instance profile as "who am I?".

Again, the **Role** is "what can I do?" And the **Instance Profile** is "who am I?"

Instance profiles provide temporary credentials, which are rotated automatically.

When you create and attach the role to an EC2 instance in the AWS Management console, the creation and use of the **Instance Profile** is actually handled behind the scenes. This can cause some confustion for those who are new to AWS.

## Solution

Log in to the AWS console using the `cloud_user` credentials provided. Once inside the AWS account, make sure you are using the `us-east-1` (N. Virginia) as the selected region.

> [!HINT]
> When copying and pasting code into Vim from the lab guide, first enter `:set paste` (and then `i` to enter insert mode) to avoid adding unnecessary spaces and hashes.

## Create a Trust Policy and Role using the AWS CLI

### Obtain the `labreferences.txt` File

1. Navigate to S3.

2. From the list of buckets, open the one that contains the text *s3bucketlookupfiles* in the middle of its name.

3. Select the `labreferences.txt` file.

4. Click **Actions** ▶︎ **Download**.

5. Open the `labreferences.txt` file, as we will need to reference it throughout the lab.

### Log in to Bastion Host and set the AWS CLI region and output type

1. Navigate to **EC2** ▶︎ **Instances**.

2. Copy the public IP of the *Bastion Host* instance.

3. Open a terminal, and log in to the bastion host via SSH:

```zsh
ssh cloud_user@<BASTION_HOST_PUBLIC_IP>
```

For more information on how to connect to a Linux instance using SSH, please refer to the AWS Documentation. For more information on how to connect to a Linux instance using PuTTY, please refer to Connect to your Linux instance from Windows using PuTTY.

4. Enter the password provided for it on the lab page.

5. Run the following command:

```zsh
[cloud_user@bastion]$ aws configure
```

6. Press **Enter** twice to leave the *AWS Accesss Key ID** and **AWS Secret Access Key** blank.

7. Enter `us-east-1` as the default region name.

8. Enter `json` as the default output format.

### Create IAM Trust Policy for an EC2 Role

1. Create a file called `trust_policy_ec2.json`:

```zsh
[cloud_user@bastion]$ vim trust_policy_ec2.json
```

2. To avoid adding unnecessary spaces or hashes, type `:set paste` and then `i` to enter `insert` mode.

3. Paste the following content:

```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {"Service": "ec2.amazonaws.com"},
      "Action": "sts:AssumeRole"
    }
  ]
}
```

4. Save and quit the file by pressing **Escape** followed by `:wq!`.

### Create the `DEV_ROLE` IAM Role

1. Run the following AWS CLI command:

```zsh
[cloud_user@bastion]$ aws iam create-role --role-name DEV_ROLE --assume-role-policy-document file://trust_policy_ec2.json
```

2. To avoid unnecessary spaces or hashes, type `:set paste` and then `i` to enter `insert` mode.

3. Enter the following content, replacing `<DEV_S3_BUCKET_NAME>` w/ the bucket name provided in the `labreferences.txt` file:

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
          "Sid": "AllowUserToSeeBucketListInTheConsole",
          "Action": ["s3:ListAllMyBuckets", "s3:GetBucketLocation"],
          "Effect": "Allow",
          "Resource": ["arn:aws:s3:::*"]
        },
        {
            "Effect": "Allow",
            "Action": [
                "s3:Get*",
                "s3:List*"
            ],
            "Resource": [
                "arn:aws:s3:::<DEV_S3_BUCKET_NAME>/*",
                "arn:aws:s3:::<DEV_S3_BUCKET_NAME>"
            ]
        }
    ]
}
```

4. Save and quit the file by pressing **Escape** followed by `:wq!`.

5. Create the managed policy called `DevS3ReadAccess`:

```zsh
[cloud_user@bastion]$ aws iam create-policy --policy-name DevS3ReadAccess --policy-document file://dev_s3_read_access.json
```

6. Copy the policy ARN in the output, and paste it into the `labreferences.txt` file - we'll need it in a minute.

## Create Instance Profile and attach Role to an EC2 instance

### Attach managed policy to role

1. Attach the managed policy to the role, replacing `<DevS3ReadAccess_POLICY_ARN>` w/ the ARN you just copied:

```zsh
[cloud_user@bastion]$ aws iam attach-role-policy --role-name DEV_ROLE --policy-arn "<DevS3ReadAccess_POLICY_ARN>"
```

2. Verify the managed policy was attached.

```zsh
[cloud_user@bastion]$ aws iam list-attached-role-policies --role-name DEV_ROLE
```

### Create the Instance Profile and add the `DEV_ROLE` via the AWS CLI

1. Create instance profile named `DEV_PROFILE`:

```zsh
[cloud_user@bastion]$ aws iam create-instance-profile --instance-profile-name DEV_PROFILE
```

2. Add role to the `DEV_PROFILE` called `DEV_ROLE`:

```zsh
[cloud_user@bastion]$ aws iam add-role-to-instance-profile --instance-profile-name DEV_PROFILE --role-name DEV_ROLE
```

3. Verify the configuration:

```zsh
[cloud_user@bastion]$ aws iam get-instance-profile --instance-profile-name DEV_PROFILE
```

### Attach the `DEV_PROFILE` Role to an Instance

1. In the AWS console, navigate to **EC2** ▶︎ **Instances**

2. Copy the instance ID of the instance named *Web Server* instance and paste it into the `labreferences.txt` file - we'll need it in a second.

3. In the terminal, attach the `DEV_PROFILE` to an EC2 instance, replacing `<LAB_WEB_SERVER_INSTANCE_ID>` w/ the *Web Server* instance ID you just copied:

```zsh
[cloud_user@bastion]$ aws ec2 associate-iam-instance-profile --instance-id <LAB_WEB_SERVER_INSTANCE_ID> --iam-instance-profile Name="DEV_PROFILE"
```

4. Verify the configuration (be sure to replace `<LAB_WEB_SERVER_INSTANCE_ID>` w/ the *Web Server* instance ID again):

```zsh
[cloud_user@bastion]$ aws ec2 describe-instances --instance-ids <LAB_WEB_SERVER_INSTANCE_ID>
```

This command's output should show this instance is using `DEV_PROFILE` as an `IamInstanceProfile`. Verify this by locating the `IamInstanceProfile` section in the output, and look below to make sure the `"Arn"` ends in `/DEV_PROFILE`.

## Test S3 Permissions via the AWS CLI

1. In the AWS console, copy the public IP of the *Web Server* instance.

2. Open a new terminal.

3. Log in to the web server instance via SSH:

```zsh
ssh cloud_user@<WEB_SERVER_PUBLIC_IP>
```

4. Use the same password for the bastion host provided on the lab page.

5. Verify the instance is assuming the `DEV_ROLE` role:

```zsh
[cloud_user@webserver]$ aws sts get-caller-identity
```

We should see `DEV_ROLE` in the `Arn`.

6. List the buckets in the account:

```zsh
[cloud_user@webserver]$ aws s3 ls
```

Copy the entire name (starting w/ `cfst`) of the bucket w/ `s3bucketdev` in its name.

7. Attempt to view the files in the `s3bucketdev-` bucket, replacing `<s3bucketdev-123>` w/ the bucket name you just copied:

```zsh
[cloud_user@webserver]$ aws s3 ls s3://<s3bucketdev-123>
```

We should see a list of files.

## Create an IAM Policy and Role using the AWS Management console

### Create policy

1. In the AWS console, navigate to **IAM** ▶︎ **Policies**.

2. Click **Create policy**.

3. Click the **JSON** tab.

4. Paste the following text as the policy, replacing `<PROD_S3_BUCKET_NAME>` w/ the bucket name provided in the `labreferences.txt` file:

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
          "Sid": "AllowUserToSeeBucketListInTheConsole",
          "Action": ["s3:ListAllMyBuckets", "s3:GetBucketLocation"],
          "Effect": "Allow",
          "Resource": ["arn:aws:s3:::*"]
        },
        {
            "Effect": "Allow",
            "Action": [
                "s3:Get*",
                "s3:List*"
            ],
            "Resource": [
                "arn:aws:s3:::<PROD_S3_BUCKET_NAME>/*",
                "arn:aws:s3:::<PROD_S3_BUCKET_NAME>"
            ]
        }
    ]
}
```

5. Click **Next**: **Tags**.

6. Click **Next**: **Review**.

7. Enter **ProdS3ReadAccess** as the policy name.

8. Click **Create policy**.

### Create Role

1. Click **Roles** in the left-hand menu.

2. Click **Create role**.

3. Under *Choose a use case*, select **EC2**.

4. Click **Next**: **Permissions**.

5. In the *Filter policies* search box, enter **ProdS3ReadAccess**.

6. Click the checkbox to select **ProdS3ReadAccess**.

7. Click **Next**: **Tags**.

8. Click **Next**: **Review**.

9. Give it a *Role name* of **PROD_ROLE**.

10. Click **Create role**.

## Attach IAM Role to an EC2 instance using the AWS Management console

1. Navigate to **EC2** ▶︎ **Instances**

2. Select the *Web Server* instance.

3. Click **Actions** ▶︎ **Security** ▶︎ **Modify IAM role**.

4. In the *IAM role* dropdown, select **PROD_ROLE**.

5. Click **Save**.

### Test the configuration

1. Open the existing terminal connected to the *Web Server* instance (You may need to reconnect if you've been disconnected).

2. Determine the identity currently being used:

```zsh
[cloud_user@webserver]$ aws sts get-caller-identity
```

This time, we should see `PROD_ROLE` in the `Arn`.

3. List the buckets:

```zsh
[cloud_user@webserver]$ aws s3 ls
```

4. Copy the entire name (starting w/ `cfst`) of the bucket w/ `s3bucketprod` in its name.

5. Attempt to view the files in the `s3bucketprod-` bucket, replacing `<s3bucketprod-123>` w/ the bucket name you just copied:

```zsh
[cloud_user@webserver]$ aws s3 ls s3://<s3bucketprod-123>
```

It should list the files.

6. In the `aws s3 ls` command output, copy the entire name (starting w/ `cfst`) of the bucket w/ `s3bucketsecret` in its name.

7. Attempt to view the files in the `<s3bucketsecret-123>` bucket, replacing `<s3bucketsecret-123>` w/ the bucket name you just copied:

```zsh
[cloud_user@webserver]$ aws s3 ls s3://<s3bucketsecret-123>
```

This time, our access will be denied - which means our configuration is properly set up.

## Additional resources

### Scenario

You are responsible for ensuring your applications hosted in Amazon EC2 are able to securely access other AWS services. Credentials need to be rotated regularly to minimize the adverse impact of a security breach. You want to minimize the time it takes to manage these credentials. AWS IAM roles provide the ability to automatically grant instances temporary credentials without the need for manual management. IAM instance profiles provide the mechanism to attach IIAM roles to EC2 instances.

### Logging in

Please log in to the AWS console using the `cloud_user` credentials provided. Once inside the AWS account, make sure you are using `us-east-1` (N. Virginia) as the selected region.

> [!NOTE]
> When connecting to the bastion host and the web server, do so independently of each other. The bastion host is used for interacting w/ AWS services via the CLI.

### Important notes

> [!IMPORTANT]
> Your first task is to download and open the `labreferences.txt` file. Keep this file open throughout the entirety of the lab, as it contains the names of S3 buckets you will need to include in multiple commands and scripts throughout the entire lab.

## Recap

We created a Role, we assigned it permission so that EC2 can assume that role. We created a policy allowing that role to access an S3 bucket. We then associated that policy w/ the Role and we created a development instance profile and associated that instance profile w/ the role. We then attached that instance profile to an EC2 instance. Then we logged into that instance, in this case, the webserver to verify the access worked.

We created permissions for our production environment, using the AWS Management console instead of the CLI.
