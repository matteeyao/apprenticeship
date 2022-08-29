# AWS Command Line

1. In your EC2 Instances Dashboard, copy the `IPv4 Public IP` for the specific instance `MyCLIEC2`

2. Create an `SSH` directory in your **Downloads** directory

```script
mkdir SSH
```

3. Move the key-pair to the `SSH` directory, now located in your **Downloads** directory

```script
mv MyUSE1KP.pem SSH
```

4. `cd` into your `SSH` directory

```script
cd SSH
```

5. Change permissions to 400 to lock it down

```script
chmod 400 LondonKey.pem
```

6. `SSH` into the EC2 instance

```script
ssh ec2-user@<COPIED_IPV4_PUBLIC_IP> -i <key-pair>
```

or, in our case:

```script
ssh ec2-user@54.166.184.127 -i MyUSE1KP.pem
```

7. Elevate privileges to root

```script
sudo su
```

8. Configure AWS credentials

```script
[root@ip-172-31-94-96 ec2-user]# aws configure
AWS Access Key ID [None]: AKIAVBFE7WEKTNLPAP2C
AWS Secret Access Key [None]: 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
Default region name [None]: us-east-1
Default output format [None]:
```

9. W/ credentials configured, we can now use the AWS command line. To use the AWS command line and an AWS service, execute:

```script
aws <AWS_service>
```

The following command lists the S3 buckets that we have:

```script
[root@ip-172-31-94-96 ec2-user]# aws s3 ls
2022-03-07 04:05:53 replication-bucket-tokyo-acg
2022-03-04 17:51:36 s3-learning-demo
2022-03-04 19:20:54 s3-version-control-demo
```

To make a bucket

```script
aws s3 mb s3://<BUCKET_NAME>
```

10. Navigate to the home directory:

```script
cd ~
```

11. Navigate to the hidden directory `.aws`

```script
ls
cd .aws
```

In the `.aws` hidden directory are our config and credentials:

```script
[root@ip-172-31-94-96 .aws]# ls
config  credentials
```

To have a look at your credentials:

```script
nano credentials
```

You'll see your credentials saved in the `credentials` file in the `.aws` directory:

```script
[default]
aws_access_key_id = AKIAIFS3YQEAYE5ROILA
aws_secret_access_key = kxtCIcluX4gPkKcuBUeUDT4B5dK7+fHPFk4LYEzs
```

## Learning summary

You can use the command line to start provisioning resources within AWS. We can provision EC2 instances, etc

> * You can interact w/ AWS from anywhere in the world just by using the command line (CLI)
>
> * You will need to set up access in IAM
>
> * Commands themselves are not in the exam, but some basic commands will be useful to know for real life
