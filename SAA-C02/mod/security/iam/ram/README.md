# AWS Resource Access Manager (RAM)

Account isolation within AWS

Multi-account strategy in AWS allows you to use different AWS accounts to separate concerns like administration, billing, or to minimize the so-called blast radius around any mistakes or security vulnerabilities. 

Using a multi-account strategy is great, but it could present a challenge when you need to create and share resources across accounts. That's where Resource Access Manager (RAM) comes in.

> AWS Resource Access Manager (RAM) allows **resource sharing** between accounts.

If you have multiple individual AWS accounts or an AWS Organization, you can create resources centrally and use AWS RAM to share those resources w/ other accounts.

This means that you can reduce operational overhead b/c you won't be duplicating resources in each of your accounts, which can be a real pain to manage.

Now, you can't share every type of resource in AWS using RAM.

> Which AWS resources can I share using RAM?
>
> * App Mesh
>
> * Aurora
>
> * CodeBuild
>
> * EC2
>
> * EC2 Image Builder
>
> * License Manager
>
> * Resource Groups
>
> * Route 53

For example, Launch EC2 instances in a shared subnet.

* Say we have two AWS accounts: Account 1 and Account 2

* And we have a private subnet in Account 1 that we want to share

* Then Account 2 is able to see this private Subnet in Account 1

* This lets Account 2 creates resources in Account 2's private Subnet like EC2 instances

* What's important to understand here is that Account 2 has no control over Account 1's private Subnet, so it can't alter the Subnet in any way w/ the exception of adding tags. In other words, the Subnet isn't copied from Account 1 to Account 2. It's just shared.

RAM works by sending an invitation from Account 1 to Account 2 that Account 2 has to first accept.

## Learning summary

Share resources across accounts or within an AWS Organization using Resource Access Manager (RAM)

> * Resource sharing between accounts
>
> * Works on Individual accounts and AWS Organizations
>
> * Types of resources you can share

* Remember not every AWS service is available in Resource Access Manager
