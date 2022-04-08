# IAM Policies

**Objective**: IAM permission boundaries and how AWS evaluates IAM policies

## Amazon Resource Name (ARN)

The ARN uniquely identifies any resource in AWS.

**ARNs all begin with**:

Syntax: `arn:partition:service:region:account_id`

* `partition`: AWS has different partitions that operate. The most common is `aws`. When working w/ AWS in China, `aws-cn`.

* `service`: `s3` | `ec2` | `rds`

* `region`: `us-east-1` | `eu-central-1`

* `account-id`: your 12 digit AWS account ID

**And end with**:

* `resource`

* `resource_type/resource`

* `resource_type/resource/qualifier`

* `resource_type/resource:qualifier`

* `resource_type:resource`

* `resource_type:resource:qualifier`

**Examples**:

* `arn:aws:iam::123456789012:user/mark`

    * ARM for an IAM user
    
    * Two consecutive colons `::` indicates that the IAM is set to Global region, meaning it doesn't exist in a particular region → So this is actually an **Omitted value**

    * Then we have out 12 digit account ID followed by a resource type and then `/resource`. In this case the resource type is `user`, and the resource is `mark`. So this ARN uniquely identifies IAM user Mark within our account

* `arn:aws:s3:::my_awesome_bucket/image.png`

    * ARN for a specific object inside an S3 bucket

    * Three colons `:::` indicates there's no specific region or account ID needed to uniquely identify an object within S3 b/c all bucket names in S3 are globally unique, so you won't need those additional qualifiers for uniqueness

* `arn:aws:dynamodb:us-east-1:123456789012:table/orders`

    * A single table within DynamoDB

    * `us-east-1` is a region name

    * `123456789012` is the 12 digit account ID

    * `table` is our resource type

    * `orders` is the resource name, in this case, the name of our table

* `arn:aws:ec2:us-east-1:123456789012:instance/*`

    * We can use ARNs to specify not just a single resource, but all resources of a particular type

    * So say we want to refer to all of the EC2 instances within a single account in a region

    * `instance` is our resource type

    * `/*` is a wildcard, represents all EC2 instances in that account, in that region

## IAM Policies

> * JSON document that defines permissions
>
> * Identity policy

* Identity policies are attached to an IAM user, group, or role

* These policies let you specify what an identity can do

* In other words, its permissions

> * Resource policy

* Resource-based policies are attached to a resource

* For example, you can attach resource-based policies to S3 buckets, SQS queues, KMS encryption keys, and so on

* W/ resource-based policies, you can specify who has access to the resource and what actions they can perform on it

> * No effect until attached

* Important to understand, w/ IAM policies, just b/c you've created a policy, doesn't mean it has any affect. You have to attach that policy either to an identity or to a resource. So, unless it's attached, it has no effect

> * List of statements

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            // ...
        },
        {
            // ...
        },
        {
            // ...
        }
    ]
}
```

> * A policy document is a list of **statements**

* Each individual statement is enclosed in curly braces

> * Each statement matches an **AWS API request**.

* An AWS API request is any action that you could perform against AWS. For example, when we start an EC2 instance, create a table in DynamoDB, get an object from S3, are all examples of making an API request

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "SpecificTable",
            "Effect": "Allow",
            "Action": [
                "dynamodb:BatchGet*",
                "dynamodb:DescribeStream",
                "dynamodb:DescribeTable",
                "dynamodb:Get*",
                "dynamodb:Query",
                "dynamodb:Scan",
                "dynamodb:BatchWrite*",
                "dynamodb:CreateTable",
                "dynamodb:Delete*",
                "dynamodb:Update*",
                "dynamodb:PullItem",
            ],
            "Resource": "arn:aws:dynamodb:*:*:table/MyTable"
        }
    ]
}
```

* `Sid`, a human readable string, tells you what this statement is for

> **Effect** is either **Allow** or **Deny**

* Our IAM policy can either allow or deny specific actions on a specific resource

* So this particular IAM policy obviously works w/ DynamoDB

> **Matched** based on their **Action**

* This policy is matched based on their action

* Actions are of the form `service name` → `:` → `action name`

* You can see some of the actions have wild cards at the end `*`, which refer to any API request that starts w/ that string: `BatchGet*`, `Get*`, `Delete*`, `Update*`

> And the **Resource the Action is against

* And finally, we have the `Resource`

* So this IAM policy allows all of these actions to this particular resource, in this case, a table called `MyTable`

## Creating a policy

Two types of policies in AWS:

* **AWS managed policies**

    * Created by AWS for your convenience

    * Denoted w/ an orange box icon

    * Not editable, by able use as many as you like

* **Customer managed policies**

    * Created by us for a specific purpose

So now our policy has effect as it is attached to a role. If you attach this role to any EC2 instances, it will implicitly be granted access or denied access based on what that policy contains.

In our case, any EC2 instances that have this `S3TestPolicy` role attached will have the ability to list objects within that test bucket, as well as `get`, `put`, and `delete` objects from that bucket.

Now, let's say for example, we want to grant some special permission to this role, but we don't want to define a policy that really live outside the scope of this role. That's where we'll use `Add inline policy`. **Inline policies** work just like any other kind of policy, except the scope is limited to just this role. You can't use this inline policy w/ any other role. You'll typically use this w/ any kind of ad hoc permissions management that you're doing, but it's typically not best practice to use inline policy like this.

## Evaluating IAM Policies

> * Not explicitly allowed == **implicitly denied**

* Any permissions that are not explicitly allowed or implicitly denied. So if your identity or resource doesn't have a policy that explicitly allows an AWS API action, it's implicitly denied.

> * Explicit deny > everything else

* If you have a policy that's an explicit deny, that overrides anything else in any other policies. So for example, if you have a policy that allows access to an S3 bucket, but another policy that explicitly denies access to that same or all S3 buckets, the explicit deny will always override

> * Only attached polices have effect

* Just b/c you've defined a policy, doesn't mean it's doing anything until it's attached to a user, group, or role

> * AWS **joins** all applicable policies

* When you have multiple policies attached to either an identity or a resource, AWS will join or union all of those policies together when it performs its evaluation.

* So if an action is allowed by an identity based policy, a resource based policy, or both, then AWS allows the action and explicit denying either of these policies overrides the `allow`

> * AWS-managed versus customer-managed

## Permission Boundaries

> * Used to **delegate** administration to other users

* So if you have system administrators that need to be able to create IAM roles and users, developers that need to be able to create roles for Lambda functions, or any similar scenario, then you need permission boundaries

* AWS supports permission boundaries for IAM entities (think users or roles)

> * Prevent **privilege escalation** or **unnecessarily broad permissions**

* Permissions boundaries is an advanced feature for using a managed policy to set the maximum permissions that an identity-based policy can grant to an IAM entity

* These are used to prevent privilege escalation or unnecessarily broad permissions

> * Control **maximum** permissions an IAM policy can grant

* An entities permission boundary allows it to perform only the actions that are allowed by both its identity-based policies and its permission boundaries

> * Use cases:

>   * Developers creating roles for Lambda functions
>
>   * Application owners creating roles for EC2 instances
>
>   * Admins creating ad hoc users

* We'll want to apply a permissions boundary in each of these cases

* **Demo**: So now even though we have the administrator access policy directly attached to this user, the permissions boundary is going to govern the maximum permissions that this user har. So even though he's an administrator, he can only work within DynamoDB while this permissions boundary is set.

## Learning summary

> * ARN
>
> * IAM policy structure

* JSON document composed of a number of discrete statements

* Each statement contains an effect, an action, and a resource

> * Effect/Action/Resource

* An `Effect` is either `allow` or `deny`

* The `Action` is an API call. So for example, get object on S3 or create table on DynamoDB

* The `Resource` is the entity inside AWS that the policy is effective for, so an S3 bucket or DynamoDB table, etc.

> * Identity vs. resource policies
>
> * Policy evaluation logic

* Say you have two policies that are in effect for the same resource

* One has an `allow`. One has a `deny`. Always remember that `deny` supersedes `allows`

> * AWS managed vs. customer managed

* Understand that AWS managed policies can't be edited by you as the customer and you can create as many customer managed policies as you'd like

> * Permission boundaries

* Recall that Permission boundaries don't allow or deny permissions on their own. They simply define the maximum permissions an identity can have.
