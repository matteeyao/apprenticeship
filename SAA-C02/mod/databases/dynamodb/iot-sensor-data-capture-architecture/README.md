# IoT sensor data capture architecture

Capturing data from thousands of Internet of Things (IoT) sensors can be a challenge. This architecture represents one solution to this challenge.

![Fig. 1 IoT sensor data capture architecture](img/SAA-CO2/databases/dynamodb/iot-sensor-data-capture-architecture/diag01.png)

> ### Amazon Simple Queue Service (Amazon SQS)
>
> *Amazon SQS is a message queueing service.*
>
> **In this architecture**, many IoT devices send messages to the Amazon SQS service.

> ### AWS Lambda
>
> *Lambda lets you run code, called functions, without provisioning or managing servers.*
>
> **In this architecture**, Amazon SQS receives a new message, which triggers a Lambda function. The function loads the message into a DynamoDB table.

> ### Amazon DynamoDB
>
> **In this architecture**, DynamoDB serves as a first stage repository for all messages sent to the Amazon SQS queue.

> ### Amazon EMR
>
> *Amazon EMR is a service that is used to gather, process, and load data for the purpose of data analytics and storage.*
>
> **In this architecture**, Amazon EMR gathers data from DynamoDB, processes it, then stores it in an Amazon S3 data lake.

> ### Amazon Simple Storage Service (Amazon S3) data lake
>
> *An Amazon S3 data lake is a storage location for many types of data.*
>
> **In this architecture**, it stores the processed data from the Amazon EMR.

> ### Amazon Athena
>
> *Athena is an interactive query service that makes it easy to analyze data in Amazon S3 using standard SQL.*
>
> **In this architecture**, you can use Athena to easily query the data now stored in the Amazon S3 data lake.
