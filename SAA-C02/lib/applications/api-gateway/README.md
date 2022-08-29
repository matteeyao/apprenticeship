# API Gateway

1. A Solutions Architect of a multinational gaming company develops video games for PS4, Xbox One, and Nintendo Switch consoles, plus a number of mobile games for Android and iOS. Due to the wide range of their products and services, the architect proposed that they use API Gateway.

What are the key features of API Gateway that the architect can tell to the client? (Select TWO.)

[ ] Provides you w/ static anycast IP addresses that serve as a fixed entry point to your applications hosed in one or more AWS Regions.

[ ] It automatically provides a query language for your APIs similar to GraphQL.

[x] You only pay for the API calls you receive and the amount of data transferred out.

[x] Enables you to build RESTful APIs and WebSocket APIs that are optimized for serverless workloads.

[ ] Enables you to run applications requiring high levels of inter-node communications at scale on AWS through its custom-built operating system (OS_ bypass hardware interface.

**Explanation**: **Amazon API Gateway** is a fully managed service that makes it easy for developers to create, publish, maintain, monitor, and secure APIs at any scale. With a few clicks in the AWS Management Console, you can create an API that acts as a “front door” for applications to access data, business logic, or functionality from your back-end services, such as workloads running on Amazon Elastic Compute Cloud (Amazon EC2), code running on AWS Lambda, or any web application. Since it can use AWS Lambda, you can run your APIs without servers.

Amazon API Gateway handles all the tasks involved in accepting and processing up to hundreds of thousands of concurrent API calls, including traffic management, authorization and access control, monitoring, and API version management. Amazon API Gateway has no minimum fees or startup costs. You pay only for the API calls you receive and the amount of data transferred out.

Hence, the correct answers are:

  * Enables you to build RESTful APIs and WebSocket APIs that are optimized for serverless workloads

  * You pay only for the API calls you receive and the amount of data transferred out.

> The option that says: **It automatically provides a query language for your APIs similar to GraphQL** is incorrect because this is not provided by API Gateway.

> The option that says: **Provides you with static anycast IP addresses that serve as a fixed entry point to your applications hosted in one or more AWS Regions** is incorrect because this is a capability of AWS Global Accelerator and not API Gateway.

> The option that says: **Enables you to run applications requiring high levels of inter-node communications at scale on AWS through its custom-built operating system (OS) bypass hardware interface** is incorrect because this is a capability of Elastic Fabric Adapter and not API Gateway.

<br />
