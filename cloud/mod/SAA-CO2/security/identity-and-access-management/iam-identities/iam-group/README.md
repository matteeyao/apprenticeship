# IAM Groups

> 1. **IAM group:** A collection of IAM users
>
> 2. IAM groups have **no credentials** and are not considered to be an identity.
>
> 3. IAM users can be a member of **10 IAM groups**, but there is no limit to how many users can be in 1 group.
>
> 4. IAM groups have **policies attached**.
>
> 5. IAM policies or resource policies cannot grant access to an IAM group.

**IAM Groups** help organize large sets of related IAM users.

**IAM users** and **IAM roles** are considered true identities, but groups are not.

Policies use the ARN to identify the principal or the resource for that policy. Thus, IAM or resource policies cannot grant access to an IAM group. A *resource policy* is simply a policy on a resource and can reference IAM users and IAM roles using ARNs.
