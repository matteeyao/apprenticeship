# Introduction to Serverless

One of the major benefits of cloud computing is its ability to abstract (hide) the infrastructure layer. This ability eliminates the need to manually manage the underlying physical hardware. In a serverless environment, this abstraction allows you to focus on the code for your applications without spending time building and maintaining the underlying infrastructure. With serverless applications, there are never instances, operating systems, or servers to manage. AWS handles everything required to run and scale your application. By building serverless applications, your developers can focus on the code that makes your business unique.

## Serverless operational tasks

The following chart compares the deployment and operational tasks in a traditional environment to those in a serverless environment. The serverless approach to development reduces overhead and lets you focus, experiment, and innovate faster.

<table style="width:67%;margin-right:calc(33%);">
   <thead>
      <tr>
         <th style="width:54.2516%;background-color:rgb(77, 39, 170);"><span style="color:rgb(255, 255, 255);font-weight:bold;">Deployment and Operational tasks</span></th>
         <th style="width:23.8586%;background-color:rgb(77, 39, 170);"><span style="color:rgb(255, 255, 255);">Traditional Environment</span><br></th>
         <th style="width:22.2326%;background-color:rgb(77, 39, 170);"><span style="color:rgb(255, 255, 255);font-weight:bold;">Serverless&nbsp;</span></th>
      </tr>
   </thead>
   <tbody>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Configure an instance</strong></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">-<br></td>
      </tr>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Update operating system (OS)</strong><br></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">-<br></td>
      </tr>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Install application platform</strong><br></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">-<br></td>
      </tr>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Build and deploy apps</strong><br></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">YES<br></td>
      </tr>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Configure automatic scaling and load balancing</strong><br></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">
            <p>-</p>
         </td>
      </tr>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Continuously secure and monitor instances</strong><br></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">-<br></td>
      </tr>
      <tr>
         <td style="text-align:left;width:54.2516%;"><strong>Monitor and maintain apps</strong><br></td>
         <td style="text-align:center;width:23.8586%;">YES<br></td>
         <td style="text-align:center;width:22.2326%;">YES<br></td>
      </tr>
   </tbody>
</table>

## AWS serverless platform

The AWS serverless platform includes a number of fully managed services that are tightly integrated with AWS Lambda and well-suited for serverless applications. Developer tools, including the AWS Serverless Application Model (AWS SAM), help simplify deployment of your Lambda functions and serverless applications.

Review the services in the following image. These services are part of the AWS serverless platform and are mentioned throughout the course.

![Fig. 01 Serverless services](../../../../../../img/SAA-CO2/serverless/lambda/foundations/introduction-to-serverless/diag01.jpeg)

> AWS Lambda is the compute service for serverless. The AWS serverless platform includes a number of fully managed services that are tightly integrated with Lambda. There are also developer tools including AWS SAM to simplify deployment of your serverless applications.

## What is AWS Lambda?

AWS Lambda is a compute service. You can use it to run code without provisioning or managing servers. Lambda runs your code on a high-availability compute infrastructure. It operates and maintains all of the compute resources, including server and operating system maintenance, capacity provisioning and automatic scaling, code monitoring, and logging. With Lambda, you can run code for almost any type of application or backend service. 

Some benefits of using Lambda include the following:

* You can **run code** without provisioning or maintaining servers.

* It **initiates functions** for you in response to events.

* It **scales** automatically.

* It provides built-in code **monitoring and logging** via Amazon CloudWatch

## AWS Lambda features

* Bring your own code ▶︎ You can write the code for Lambda using languages you already know and are comfortable using. Development in Lambda is not tightly coupled to AWS, so you can easily port code in and out of  AWS.

* Integrates w/ and extends other AWS services ▶︎ Within your Lambda function, you can do anything traditional applications do, including calling an AWS SDK or invoking a third-party API, whether on AWS, in your datacenter, or on the internet.

* Flexible resource and concurrency model ▶︎ Instead of scaling by adding servers, Lambda scales in response to events. You configure memory settings and AWS handles details such as CPU, network, and I/O throughput.

* Flexible permissions model ▶︎ The Lambda permissions model uses AWS Identity & Access Management (IAM) to securely grant access to the desired resources and provide fine-grained control to invoke your functions.

* Availability and fault tolerance are built in ▶︎ B/c Lambda is a fully managed service, high availability and fault tolerance are built into the service w/o needing you to perform any additional configuration.

* Pay for value ▶︎ Lambda functions only run when you initiate them. You pay only for the compute time that you consume. When the code is invoked, you are billed in 1-millisecond (ms) increments.

## Event-driven architectures

An event-driven architecture uses events to initiate actions and communication between decoupled services. An event is a change in state, a user request, or an update, like an item being placed in a shopping cart in an e-commerce website. When an event occurs, the information is published for other services to consume it. In event-driven architectures, events are the primary mechanism for sharing information across services. These events are observable, such as a new message in a log file, rather than directed, such as a command to specifically do something. 

## Producers, routers, consumers

AWS Lambda is an example of an event-driven architecture. Most AWS services generate events and act as an event source for Lambda. Lambda runs custom code (functions) in response to events. Lambda functions are designed to process these events and, once invoked, may initiate other actions or subsequent events.

![Fig. 02 Event Driven Architecture](../../../../../../img/SAA-CO2/serverless/lambda/foundations/introduction-to-serverless/diag02.jpeg)

> ### Event producers
>
> Producers create the events. Events contain all of the information required for the consumers to take action on the event. Producers are only aware of the event router. They do not know who the consumer is.

> ### Event router
>
> The router ingests, filters, and pushes events to the appropriate consumers. It uses a set of rules or another services such as Amazon Simple Notification Service (Amazon SNS) to send the messages.

> ### Event consumers
>
> Consumers either subscribe to receive notification about events or they monitor an event stream and only act on events that pertain to them.

> ### Events
>
> An event is a change in state of whatever you are monitoring, for example an updated shopping cart, an entry in a log file, or a new file uploaded to Amazon Simple Storage Service (Amazon S3).

> ### Amazon EventBridge
>
> EventBridge can ingest and filter events using rules to forward them only to the consumers who need to know.

> ### Events sent
>
> The events are sent only to the consumers who subscribed to them. For example, the warehouse would subscribe to Order, Inventory, and Question events. The warehouse needs to see complete orders and returns, and needs to answer customer questions about whether an item is out of stock.
>
> The financial system would subscribe to Order and Inventory events so they could charge and refund credit cards. It doesn't need to answer customer questions, so it would not subscribe to Question events.

> ### Warehouse Management Database
>
> The Question, Order, and Return events prompts the warehouse to update inventory and item availability.

> ### Financial system
>
> The Order and Inventory events prompt the finance system to update based on the scale and return of the items.

> ### Customer Service
>
> Customer Service would subscribe to Order and Question events so the customer service team can respond. They wouldn't subscribe to the Inventory events b/c inventory is not a function of their job.

## What is a Lambda function?

The code you run on AWS Lambda is called a *Lambda function*. Think of a function as a small, self-contained application. After you create your Lambda function, it is ready to run as soon as it is initiated. Each function includes your code as well as some associated configuration information, including the function name and resource requirements. Lambda functions are *stateless*, with no affinity to the underlying infrastructure. Lambda can rapidly launch as many copies of the function as needed to scale to the rate of incoming events.

Learn more about the actions you can take w/ AWS Lambda:

![Fig. 03 AWS Lambda Function](../../../../../../img/SAA-CO2/serverless/lambda/foundations/introduction-to-serverless/diag03.jpeg)

> ### Access Permissions
>
> Define what services Lambda is permitted to interact with.

> ### Triggering events
>
> Specify which events or event sources can initiate the function.

> ### Write your Function
>
> Provide the code and any dependencies or libraries necessary to run your code.

> ### Configure Execution Parameters
>
> Define the execution parameters, such as memory, timeout, and concurrency.

Which of the following are features of Lambda? (Select FOUR.)

[x] You can run code w/o provisioning or managing servers.

[x] It initiates functions on your behalf in response to events.

[ ] You don't need to configure memory or CPU.

[x] It scales automatically.

[x] It provides built-in monitoring and logging.

[ ] You can update the operating system dynamically.

With AWS Lambda, you can run code without provisioning or managing servers. Lambda initiates events on your behalf, scales automatically, and provides built-in monitoring and logging. You can write code in your preferred language. You do configure the memory for your function, but not CPU. You don't work with the OS. AWS provides the operating environment at runtime.
