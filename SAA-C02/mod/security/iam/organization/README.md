# AWS Organizations

> **AWS Organizations**: Multiple AWS accounts management
>
> * Policy-based account management
>
> * Group-based account management
>
> * APIs that automate account management
>
> * Consolidated billing

## AWS Organizations Overview and Build

**AWS organization** is designed so that they can have one master AWS account and zero or more AWS member accounts. There is also the ability to create new AWS accounts from an AWS organization w/ just a valid email address. By adding accounts this way, then there is no need for that invitation to be sent and accepted.

![Fig. 1 AWS Organizations structure](../../../../img/SAA-CO2/identity-access-management/organization/diagram.png)

**AWS Organizations** add simplicity to managing all of our AWS accounts by grouping different accounts to match our business organization. Gives us the ability to consolidate all of our AWS accounts into one bill known as **Consolidated billing**. AWS members accounts pass their billing information up to the master account which is going to be the payer account. This single consolidated bill covers a usage for the master account along w/ the usage for the AWS members accounts too.

Another benefit of using AWS organization is that some AWS services get cheaper the more usage you have. Other services are cheaper if you reserve that service and pay in advance for that usage. W/ **AWS Organizations**, all of these services and usage is pulled for all AWS accounts inside an AWS organization. So all of the accounts can benefit from the combined cost savings. You can also use **IAM roles** to allow access throughout the organization so that you can access other AWS accounts within your organization. You do not need to create separate IAM users for each AWS member account. Instead, use **IAM roles**, specifically **IAM role switch** to interact w/ other accounts.

**AWS Organization** also has a feature that restricts what member accounts in the organization can do - **Service Control Policies** (SCPs).

Recall that we can make use of **identity providers** and **identity federation** to allow users into an AWS organization as well.

## Creating the AWS Organization

![Fig. 2 Development and Production accounts](../../../../img/SAA-CO2/identity-access-management/organization/diagram-ii.png)

Next, we're going to create an IAM role inside our production account, which is now a member of our AWS organization. That role will be called the organization account access role. That **IAM role**'s trust policy will trust our development account. Finally, we're going to create a new AWS account for our AWS organization, called "Security Account". B/c we're creating this account inside our AWS organization, the account will automatically come w/ the organization account access role that we created in our production account.

W/ three accounts under one AWS organization, we will be able to log into our production and security account using the IAM role, the organization account access role.

![Fig. 3 AWS Organization accounts](../../../../img/SAA-CO2/identity-access-management/organization/diagram-iii.png)

### Notable steps

* Create an **IAM role** `OrganizationAccountAccessRole` that will allow us to log into the production account if we're logged into our development account w/o having to actually log in.

* Setup **Trust policy** for IAM role, so the IAM role is going to trust our development account, which is our master account inside the organization.

* Setup **Permissions policy** for the IAM role

## AWS Organizations - Service Control Policies (SCPs)

Currently our development (Master Account), production (Member Account), and security (Member Account) accounts are in the root container of our AWS organization. They're not inside an **Organizational Unit**. So an **SCP policy** is a JSON policy document, and SCPs can be attached to an AWS organization as a whole by attaching the SCP to the root container or by attaching to one or more OUs (Organizational Units). Or they can be attached to one or more AWS accounts directly.

So an **SCP** inherits down the AWS organization structure. So if an **SCP** is attached to the organization, then that **SCP** affects all accounts in the organization. If they are attached to an organizational unit, then they affect all accounts in that organizational unit, and the accounts below that organizational unit. If an **SCP** is attached to one or more accounts, then the SCPs only affect those accounts. So SCPs are permission boundaries and limit what the accounts can do, including the account root user.

So if you have an **SCP** in your account that prevents usage of an AWS service, then nothing in that account, including the root user, can use that AWS service.

> **Service Control Policies (SCPs)**
>
> ✓ SCPs can limit activity outside of a region or allow **certain types of services**
>
>   * Used to block services by default and then allow certain services = `Allow` list
>
>   * Used to allow by default and block certain services = `Deny` list
>
> ✓ SCPs do not **specifically** grant access
>
> ✓ SCPs only control **which permissions** can be granted
>
> ✓ When SCPs are **enabled**, there is an **explicit** `Deny`

The default for an AWS organization is to use the `deny` list, so "allow all services by default and block access to certain services". So when you enable SCPs in an AWS organization, AWS will apply a default policy called full AWS access for the AWS organization. This policy allows all AWS access.

So if the initial SCP policy that allows access to everything is not present, then an explicit `Deny` is going to apply, meaning that now AWS accounts would have access to any AWS service. As the admin, you must add services that you want to restrict or deny. An explicit `deny` always wins.

**SCPs** cannot affect the master account, b/c that master account cannot be restricted. **SCPs** don't grant permissions, but define what is and what is not allowed in an account. **SCPs** can apply account wide limits to different AWS services.

![Fig. 3 Service Control Policies](../../../../img/SAA-CO2/identity-access-management/organization/diagram-iv.png)
