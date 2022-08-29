# Register a domain name

In this lab, we will register a domain name, and examine all the Route 53 routing policies.

We're going to use this domain name to set up three EC2 instances. Each one is going to be in a different area in the world. And then we'll be ready to start learning about the different Route 53 routing policies and testing them.

EC2 provisioning script:

```bash
#!/bin/bash
yum update -y
yum install httpd -y
service httpd start
chkconfig httpd on
cd /var/www/html
echo "<html><h1>Hello Cloud Gurus! This is the <Region> Web Server</h1></html>" > index.html
```

By the end of this lab, we will have our three EC2 instances around the world and have registered our domain name.

## Learning summary

> * You can buy domain names directly w/ AWS.
>
> * It can take up to 3 days to register depending on the circumstances.
