# Well-Architected Framework

1. You are responsible for a web application running on Amazon EC2 instances. You want to track the number of 404 errors that users see in the application.

Which of the following options can you use?

[ ] Use VPC Flow Logs `(VPC Flow Logs capture layer 3 and 4 IP-level logs and do not capture layer 7 HTTP 404 errors)`

[ ] Use CloudWatch Metrics `(CloudWatch Metrics do not capture 404 errors by default)`

[x] Use CloudWatch Logs to get the web server logs from EC2 instances

[ ] Web applications on AWS never have 404 errors `(Web applications on AWS can have 404 errors)`
