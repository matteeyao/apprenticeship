# Serverless Application

1. A company has a serverless application made up of AWS Amplify, Amazon API Gateway and a Lambda function. The application is connected to an Amazon RDS MySQL database instance inside a private subnet. A Lambda Function URL is also implemented as the dedicated HTTPS endpoint for the function, which has the following value:

`https://12june1898pil1pinas.lambda-url.us-west-2.on.aws/`

There are times during peak loads when the database throws a “too many connections” error preventing the users from accessing the application.

Which solution could the company take to resolve the issue?

[ ] Increase the rate limit of API Gateway

[ ] Increase the concurrency limit of the Lambda function

[ ] Provision an RDS Proxy between the Lambda function and RDS database instance

[ ] Increase the memory allocation of the Lambda function

**Explanation**: If a "Too Many Connections" error happens to a client connecting to a MySQL database, it means all available connections are in use by other clients. Opening a connection consumes resources on the database server. Since Lambda functions can scale to tens of thousands of concurrent connections, your database needs more resources to open and maintain connections instead of executing queries. The maximum number of connections a database can support is largely determined by the amount of memory allocated to it. Upgrading to a database instance with higher memory is a straightforward way of solving the problem. Another approach would be to maintain a connection pool that clients can reuse. This is where RDS Proxy comes in.

![Fig. 1 RDS Proxy](../../../../../img/SAA-CO2/databases/rds/rds-proxy/fig01.jpeg)

RDS Proxy helps you manage a large number of connections from Lambda to an RDS database by establishing a warm connection pool to the database. Your Lambda functions interact with RDS Proxy instead of your database instance. It handles the connection pooling necessary for scaling many simultaneous connections created by concurrent Lambda functions. This allows your Lambda applications to reuse existing connections, rather than creating new connections for every function invocation.

Thus, the correct answer is: **Provision an RDS Proxy between the Lambda function and RDS database instance format**

The option that says: **Increase the concurrency limit of the Lambda function** is incorrect. The concurrency limit refers to the maximum requests AWS Lambda can handle at the same time. Increasing the limit will allow for more requests to open a database connection, which could potentially worsen the problem.

The option that says: **Increase the rate limit of API Gateway** is incorrect. This won't fix the issue at all as all it does is increase the number of API requests a client can make.

The option that says: **Increase the memory allocation of the Lambda function** is incorrect. Increasing the Lambda function's memory would only make it run processes faster. It can help but it won't likely do any significant effect to get rid of the error. The "too many connections" error is a database-related issue. Solutions that have to do with databases, like upgrading to a larger database instance or, in this case, creating a database connection pool using RDS Proxy have better chance of resolving the problem.
