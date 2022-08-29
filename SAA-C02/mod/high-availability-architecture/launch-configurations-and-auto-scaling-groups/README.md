# Launch Configuration and Auto Scaling Groups

**EC2** ▶︎ **Auto Scaling** ▶︎ **Launch Configurations** ▶︎ **Create launch configuration**

Set **IAM role** to `S3AdminAccess`

**Bootstrap script**:

```script
#!/bin/bash
yum update -y
yum install httpd -y
service httpd start
chkconfig httpd on
cd /var/www/html
echo "<html><h1>Welcome to the EC2 Fleet!</h1></html>" > index.html
```

Leave **IP Address Type** set to `Only assign a public IP address to instances launched in the default VPC and subnet. (default)`

**Create Launch Configuration** ▶︎ Select our created `WebDMZ` for our **existing** security group.
