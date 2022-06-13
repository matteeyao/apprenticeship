# IAM Access Keys

> * Long-term credentials
>
> * Username and password for the console
>
> * Access keys for the command line
>
> * Do not change regularly or rotate
>
> * You must explicitly update
>
> * An IAM user can have two sets of access keys but only one username and one password

## Access Key ID

> * Username
>
> * Public

## Secret Access Key

> * Password
>
> * Private

We use the CLI to access our AWS account and resources, but we must use both parts of the access key - the access key ID and the secret access key.

**IAM users** are the only identity that can use access keys in AWS. They're the only ones who use long-term credentials. **IAM roles** are short-term credentials and do not use access keys.
