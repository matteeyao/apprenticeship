# Mobile application backend architecture

This architecture provides one solution for allowing a mobile application to automatically notify a user’s friends when the user’s status changes.

![Fig. 1 Mobile application backend architecture](../../../../../img/SAA-CO2/databases/dynamodb/mobile-app-backend-architecture/diag01.png)

> ### Access
>
> Within the mobile application, the user can update their status.

> ### Amazon API Gateway
>
> *API Gateway is a service that allows developers to create, public, maintain, monitor, and secure APIs at any scale.*
>
> **In this architecture**, API Gateway is called when the user updates their status.

> ### AWS Lambda
>
> *Lambda lets you run code, called functions, without provisioning or managing servers.*
>
> **In this architecture**, the call made to the API Gateway triggers a Lambda function. The function runs code to look up the friend list in a DynamoDB table and then push status notifications to the user's friends using Amazon SNS.

> ### Amazon DynamoDB
>
> **In this architecture**, DynamoDB stores the friend list for every user of the application.
