# EC2 Security

> * When you deploy an Amazon EC2 instance, you are responsible for management of the guest operating system (including updates and security patches), any application software or utilities installed on the instances, and the configuration of the AWS-provided firewall (called a security group) on each instance.
>
> * W/ EC2, termination protection of the instance is disabled by default. This means that you do not have a safe-guard in place from accidentally terminating your instance. You must turn this feature on if you want that extra bit of protection.
>
> * Amazon EC2 uses public-key cryptography to encrypt a piece of data, such as a password, and the recipient uses their private key to decrypt the data. The public and private keys are known as a key pair.
>
> * You can encrypt your root device volume which is where you install the underlying OS. You can do this during creation time of the instance or w/ third-party tools like bit locker. Of course, additional or secondary EBS volumes are also encrypt-able as well.
>
> * By default, an EC2 instance w/ an attached AWS Elastic Block Store (EBS) root volume will be deleted together when the instance is terminated. However, any additional or secondary EBS volume that is also attached to the same instance will be preserved. This is b/c the root EBS volume is for OS installations and other low-level settings. This rule can be modified, but it is usually easier to boot a new instance w/ a fresh root device volume than make use of an old one.
