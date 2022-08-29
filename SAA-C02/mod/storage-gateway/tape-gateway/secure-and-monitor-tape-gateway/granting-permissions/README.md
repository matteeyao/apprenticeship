# Granting Permissions

Access to AWS Storage Gateway requires credentials that AWS can use to authenticate your requests. These credentials must have permissions to access AWS resources, such as a gateway, file share, volume, or tape. The primary resource of the Storage Gateway is a gateway. Additional resource types, such as tapes or Internet Small Computer System Interface (iSCSI) targets, are referred to as sub-resources and don't exist unless they are associated with a gateway.

As the solution is deployed, activated, and managed, it will need to connect with other AWS resources such as an Amazon Simple Storage Service (Amazon S3) bucket. The gateway will also need to be accessed from your on-premises client. As a Storage Gateway user, your clients will be using a local mounted storage device, but the gateway appliance will authenticate to AWS and write your tape data to Amazon S3. 

![Fig. 1 Gateway flow](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/secure-and-monitor-tape-gateway/granting-permissions/diag01.png)

## AWS Identity and Access Management

> IAM is a web service that helps you securely control access to AWS resources. IAM provides the infrastructure necessary to control authentication and authorization to your services.

![Fig. 2 IAM includes users, user groups, policies and permissions, and roles](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/secure-and-monitor-tape-gateway/granting-permissions/diag02.png)

IAM is used to control who can sign in (authenticate) and what permissions they have access to perform (authorize). You can have valid credentials to authenticate your request, but you also need permissions to perform tasks.

An *IAM identity* represents a user (a person or an application) that can be authenticated and then authorized to perform actions in AWS. You manage access in AWS by creating policies with specific permissions and attaching them to IAM identities or resources.

### IAM Identities

AWS defines several authentication identities that can be used to provide access to an AWS account.

* **AWS account root user** ▶︎ This is the identity that created the AWS account and thus has unrestricted access to the account. It is recommended not to use the AWS account root user for everyday tasks, not even administrative ones. Instead, adhere to the best practice of using the root user only to create your first IAM user.

* **IAM user** ▶︎ An IAM user can be a person or a service. An IAM user can sign into AWS using a name and a password. The IAM user can have up to two access keys. Access keys are long-term credentials that can be used to sign programmatic requests to AWS. An IAM user can have specific custom permissions through policies that determine what the user can and cannot do within AWS. For example, it might have permissions to create a gateway in Storage Gateway.

* **IAM groups** ▶︎ Users can belong to one or more groups. Using groups simplifies management of permissions. You add permissions to the group and then IAM users can be added or removed from the group, gaining or losing permissions accordingly.

* **IAM role** ▶︎ An IAM role has specific permissions similar to an IAM user, but instead of being associated with one person, it can be assumed by anyone who needs it. When you assume a role, temporary security credentials are provided for the session. A role needs two things: permission policies that specify which resources the role can access and what actions are allowed, and a trust policy that specifies the entities that can assume the role.

### IAM Policies (Permissions)

After the credentials are authenticated, requests have to be authorized. Policies are used to define permissions for actions.

During authorization, requests to AWS are checked against the relevant IAM policies to determine if the request is allowed or denied. Policies can be attached to a user, group, or a role.

Several policy types exist, such as **identity-based policies** for access to AWS resources within a specific account and **resource-based policies** for cross-account access. The most common examples of resource-based policies are Amazon S3 bucket policies and IAM role trust policies.

When granting permissions, always use the principle of least privilege, granting only the permissions required to perform a task.

The following IAM policy allows all List* and Describe* actions on all resources. These actions are read-only actions. Thus, the policy doesn't allow the user to change the state of any resources—that is, the policy doesn't allow the user to perform actions such as DeleteGateway, ActivateGateway, and ShutdownGateway.

```
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Sid": "AllowReadOnlyAccessToAllGateways",
      "Action": [
        "storagegateway:List*",
        "storagegateway:Describe*"
      ],
      "Effect": "Allow",
      "Resource": "*"
    }
  ]
}
```

Remember when you set up your gateway? During activation of the Tape Gateway, your gateway was associated with your AWS account and permissions.

A *resource owner* is the AWS account that created the resource. That is, the resource owner is the AWS account of the principal entity (the root account, an IAM user, or an IAM role) that authenticates the request that creates the resource.

![Fig. 3 Storage Gateway and IAM](../../../../../../img/SAA-CO2/storage-gateway/tape-gateway/secure-and-monitor-tape-gateway/granting-permissions/diag02.png)

To grant resource access, the gateway assumes a role that is associated with an IAM policy that grants this access. The policy determines which actions the role can perform.

## Actions

Storage Gateway defines a set of actions that can be used to grant permission for specific API operations. The table shows a sample of Storage Gateway API operations, required permissions, and resources in the action element of an IAM policy statement. For example, if you create an IAM user in your AWS account, you can grant them permissions to activate a gateway, by granting them the ActivateGateway action. 

<table style="width:100%;"><thead><tr><th style="width:32.407%;"><span style="font-size:17px;">API Operation</span><br></th><th style="width:35.5776%;"><span style="font-size:17px;">Required Permissions (API Actions)</span><br></th><th style="width:31.9256%;"><span style="font-size:17px;">Resources</span><br></th></tr></thead><tbody><tr><td style="width:32.407%;"><span style="font-size:17px;">ActivateGateway</span><br></td><td style="width:35.5776%;"><span style="font-size:17px;">storagegateway:ActivateGateway</span><br></td><td style="width:31.9256%;"><span style="font-size:17px;">Gateway</span><br></td></tr><tr><td style="width:32.407%;"><span style="font-size:17px;">AddCache</span><br></td><td style="width:35.5776%;"><span style="font-size:17px;">storagegateway:AddCache</span><br></td><td style="width:31.9256%;"><span style="font-size:17px;">Gateway</span><br></td></tr><tr><td style="width:32.407%;"><span style="font-size:17px;">AddUploadBuffer</span><br></td><td style="width:35.5776%;"><span style="font-size:17px;">storagegateway:AddUploadBuffer</span><br></td><td style="width:31.9256%;"><span style="font-size:17px;">Gateway</span><br></td></tr><tr><td style="width:32.407%;"><span style="font-size:17px;">AssignTapePool</span><br></td><td style="width:35.5776%;"><span style="font-size:17px;">storagegateway:AssignTapePool</span><br></td><td style="width:31.9256%;"><span style="font-size:17px;">Gateway, tape</span><br></td></tr><tr><td style="width:32.407%;"><span style="font-size:17px;">CreateTapes</span><br></td><td style="width:35.5776%;"><span style="font-size:17px;">storagegateway:CreateTapes</span><br></td><td style="width:31.9256%;"><span style="font-size:17px;">Gateway</span><br></td></tr></tbody></table>
