# Introduction to AWS Identity and Access Management (IAM)

## Learning objectives

> ✓ Add the Users to the Proper Groups
>
> ✓ Use the IAM Sign-in link to Sign In as a User

## About this lab

> AWS Identity and Access Management (IAM) is a service that allows AWS customers to manage user access and permissions for their accounts, as well as available APIs/services within AWS. IAM can manage users and security credentials (such as API access keys), and allow users to access AWS resources.
>
> In this lab, we will walk through the foundations of IAM. We'll focus on user and group management, as well as how to assign access to specific resources using IAM-managed policies. We'll learn how to find the login URL where AWS users can log in to their account and explore this from a real-world use case perspective.

![Fig. 1 IAM lab diagram](../../../../img/SAA-CO2/identity-access-management/identity-access-management.demo/diagram.png)

IAM is a service that allows AWS customers to manage user access and permissions to different AWS accounts and the different available APIs at a service level and sometimes resource level within AWS.

W/ identity and access management, you can manage users' security credentials, such as API access keys, and allow users to access those resources w/ those credentials, whether it's through the AWS GUI-based console in a web browser, or through the AWS command line interface, or different development SDKs.

We'll take a look at these managed access control list policies, which help us define, at a granular level, permissions for our users that are members of individual groups.

* The **S3-Support** group provides read-only access to S3

* The **EC2-Support** group provides read-only access to EC2

* The **EC2-Admin** group provides permissions to view, start, and stop EC2 instances

## Environment walk-through

### Explore the Users

1. Navigate to **IAM**.

    * Notice how the region says `Global`. Identity and Access Management (IAM) is a global service on AWS. This means that these users and permissions will apply across all available AWS regions.

    * Within this lab, we'll be stopping and starting an EC2 instance which has already been launched for us inside the `us-east-1` (or the Northern Virginia) region. Instances are regional. However, the permissions will apply regardless of where the instance is. We can provide regional-based access to instances as well, which means I could create a group that "Allow[s] EC2 admins to only start and stop instances in the `us-east-1` region."

    * IAM is powerful when it allows us to manage permissions at a very granular level, which is important for security and compliance.

2. From the left-hand menu, click **Users**.

    * The first user is the `cloud_user`, which is the user you're currently logged into.

    * `user-1`, `user-2`, and `user-3` are the users we're going to use to perform actions to complete this lab.

3. Click **user-1**.

4. Select the **Permissions** and **Groups** tabs, where we'll see `user-1` does not have any permissions assigned to it and does not belong to any groups.

    * You'll see it doesn't have any permissions associated w/ it.

    * It's not a member of any groups.

5. Select the **Security credentials** tab, where you would see user access keys, SSH public keys, and HTTPS Git credentials for AWS CodeCommit.

6. Select the **Access Advisor** tab to see which services the user has accessed and when.

    * The **Access Advisor** tab will inform us of what services this user, `user-1`, has accessed and the last time they accessed it.

7. At the top, under *Summary*, observe the user’s ARN (Amazon Resource Name), path, and creation time.

    * Notice that the user has an Amazon Resource Name (ARN). Each resource inside of AWS has an ARN, which uniquely identifies the resource, and a **Creation time**.

### Explore the Groups

There are 3 groups we're going to focus on:

* `EC2-Admin`: Provides permissions to view, start, and stop EC2 instances

* `EC2-Support`: Provides read-only access to EC2

* `S3-Support`: Provides read-only access to S3

> [!NOTE]
> There are 2 different kinds of policies for these groups:
>
> * **Managed policies**: Policies shared among users and/or groups that are pre-built either by AWS or an administrator within the AWS account. When it's updated, the changes to this policy are immediately applied for all users and groups to which it's attached.
>
> * **Inline policies**: Policies assigned to just one user or group that are typically used in one-off situations.

1. Click **User groups** in the left-hand menu.

2. Click **EC2-Admin**.

3. Click **Permissions**, where we'll see `EC2-Admin` has an inline policy with a set of permissions associated with it.

    * We can also attach a **managed policy**, a prebuilt policy either by AWS or by an administrator inside of your AWS account, someone who has permissions to do that. These managed policies can be attached to either an IAM user or an IAM group. When the policy is updated, the changes to the policy are immediately applied to all of the users and the groups that shared the attached policy. So it's a shared policy. Any user or group that it's attached to will automatically be updated. 

4. Click the plus-sign icon next to the `ec2-admin` policy to view the policy and see the actions the group is allowed to take (and which resources the action can be taken on) or if it has read-only access.

> [!NOTE]
> From this policy, we have permission to view, start, and stop EC2 instances on all resources, view elastic load balancers, list metrics, get metric statistics, and describe metrics (which our CloudWatch metrics automatically configured with our EC2 instance). The same permissions apply to our Auto Scaling service.

    * This inline policy is a policy that's assigned to just one user or one group. Inline policies are typically used to apply permissions in one-off situations. The policy is displaying our access control information in JSON format.

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Action": [
                "ec2:Describe*",
                "ec2:StartInstances",
                "ec2:StopInstances"
            ],
            "Resource": "*",
            "Effect": "Allow"
        },
        {
            "Action": "elasticloadbalancing:Describe*",
            "Resource": "*",
            "Effect": "Allow"
        },
        {
            "Action": [
                "cloudwatch:ListMetrics",
                "cloudwatch:GetMetricStatistics",
                "cloudwatch:Describe*"
            ],
            "Resource": "*",
            "Effect": "Allow"
        },
        {
            "Action": "autoscaling:Describe*",
            "Resource": "*",
            "Effect": "Allow"
        }
    ]
}
```

    * The `ec2-admin` inline policy defines granular access to AWS resources. For example, members of the `EC2-Admin` group have permissions to perform all these actions. These actions map back to essentially programmatic or API function calls within AWS.

```json
// ...
"Action": [
    "ec2:Describe*",
    "ec2:StartInstances",
    "ec2:StopInstances"
],
// ...
```

    * So here, we can `Describe` or view all of the EC2 instances. We can start instances or we can stop instances.

```json
// ...
"Resource": "*",
// ...
```

    * We're permitted to do this on all resources

```json
// ...
"Effect": "Allow"
// ...
```

    * And the effect is "Allow". If this effect is "Deny", we would not be allowed to perform any of those actions.

    * The EC2 service does allow resource-level permissions, which means we could stop and start permissions to a single EC2 instace if we wanted to get that granular.

    * Above, we leave it as `"*"`, which represents all instances.

    * The next statement allows us to view all elastic load balancers.

```json
// ...
"Action": "elasticloadbalancing:Describe*",
"Resource": "*",
"Effect": "Allow"
// ...
```

    * And we have permissions to list metrics, get metric statitics, and describe metrics, which are CloudWatch metrics automatically configured w/ our EC2 instance.

```json
// ...
{
    "Action": [
        "cloudwatch:ListMetrics",
        "cloudwatch:GetMetricStatistics",
        "cloudwatch:Describe*"
    ],
    "Resource": "*",
    "Effect": "Allow"
},
// ...
```

    * Same thing for the Auto Scaling service.

```json
// ...
{
    "Action": "autoscaling:Describe*",
    "Resource": "*",
    "Effect": "Allow"
}
// ...
```


5. Click **Cancel**.

6. Click **User groups** in the left-hand menu.

7. Click **EC2-Support**.

    * Notice that the **EC2-Support** group has a managed policy and not an inline policy.

8. Click **Permissions**, where we'll see it has a managed policy created by AWS.

    * What this policy does not allow us to do is stop, start, or create EC2 instances, or a lot of other functions.

    * This is a read-only access policy, which means I can view what's happening inside of EC2, but we can't do anything inside of EC2.

9. Click the plus-sign icon next to the `AmazonEC2ReadOnlyAccess` policy.

> [!NOTE]
> This group can describe EC2 instances, elastic load balancers, CloudWatch metrics, and our Auto Scaling configurations. It doesn't allow us to stop, start, or create EC2 instances. It's a read-only policy, meaning we can view what's happening inside EC2, but we can't make changes to the resource.

10. Cick **Cancel**.

11. Click **User groups** in the left-hand menu.

12. Click **S3-Support**.

13. Click **Permissions**. Our `S3-Support` group is only allowed read-only access.

    * The `Get` and `List` actions allow us to view the objects in an S3 bucket, as well as view the S3 bucket itself.

14. Click the plus-sign icon next to the `AmazonS3ReadOnlyAccess` policy, where we'll see the `Get` and `List` actions that allow us to view the S3 bucket and the objects in it.

15. Click **Cancel**.

## Add the Users to the Proper Groups

1. Click **User groups** in the left-hand menu.

2. Click **S3-Support**.

3. In the *Users* section, click **Add users**.

4. Select `user-1`, and click **Add users**.

5. Click **User groups** in the left-hand menu.

6. Click **EC2-Support**.

7. In the Users section, click **Add users**.

8. Select `user-2`, and click **Add users**.

9. Click **Groups** in the left-hand menu.

10. Click **EC2-Admin**.

11. In the *Users* section, click **Add users**.

12. Select `user-3`, and click **Add users**.

13. Click **Users** in the left-hand menu.

14. Click **user-3**.

15. Under *Permissions*, expand the `ec2-admin` policy.

16. Click **Edit policy**.

17. Click the plus-sign icon next to the `ec2-admin` policy.

18. Review the policy, but do not make any changes.

## Use the IAM Sign-in link to sign in as a user

1. Click **Dashboard** in the left-hand menu.

2. Copy the sign-in URL located on the right, under *AWS Account*.

3. In a new browser tab, navigate to the URL.

### `user-1`

1. Using the credentials provided in the lab overview, log in to the `user-1` account.

2. Navigate to **S3**.

3. Click **Create bucket**.

4. Enter a globally unique bucket name.

5. Click **Create bucket**.

    * You should receive an "Access Denied" error, since this user cannot create buckets in S3.

6. Navigate to **EC2**.

    * You won't be able to see any instances.

7. Click on the username in the top-right corner of the page.

8. Click **Sign Out**.

### `user-2`

1. Using the credentials provided in the lab overview, log in to the `user-2` account.

2. Navigate to **EC2** > **Instances (running)**.

    * This time, you should be able to view the running instances.

3. Select the running instance.

4. Click **Instance state** > **Stop instance** > **Stop**.

    * You should see an error message, since this user doesn't have permission to stop instances.

5. Navigate to **S3**.

    * You should see an error message that you don't have permission to list buckets.

6. Click on the username in the top-right corner of the page.

7. Click **Sign Out**.

### `user-3`

1. Using the credentials provided in the lab overview, log in to the `user-3` account.

2. Navigate to **EC2** > **Instances (running)**.

3. Select the running instance.

4. Click **Instance state** > **Stop instance** > **Stop**.

    * You should see it enters the *Stopping* state.

5. Refresh the table.

6. Click **Clear filters**.

7. Once the instance has stopped successfully, select it and click **Instance state** > **Start instance**.

8. Refresh the table to make sure it enters the *Running* state.
