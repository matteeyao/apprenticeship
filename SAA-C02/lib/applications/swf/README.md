# Simple Workflow Service

1. A software company has resources hosted in AWS and on-premises servers. You have been requested to create a decoupled architecture for applications which make use of both resources.

Which of the following options are valid? (Select TWO.)

[ ] Use DynamoDB to utilize both on-premises servers and EC2 instances for your decoupled application

[ ] Use RDS to utilize both on-premises servers and EC2 instances for your decoupled application

[ ] Use SWF to utilize both on-premises servers and EC2 instances for your decoupled application

[ ] Use VPC peering to connect both on-premises servers and EC2 instances for your decoupled application

[ ] Use SQS to utilize both on-premises servers and EC2 instances for your decoupled application

**Explanation**: **Amazon Simple Queue Service (SQS)** and **Amazon Simple Workflow Service (SWF)** are the services that you can use for creating a decoupled architecture in AWS. Decoupled architecture is a type of computing architecture that enables computing components or layers to execute independently while still interfacing with each other.

Amazon SQS offers reliable, highly-scalable hosted queues for storing messages while they travel between applications or microservices. Amazon SQS lets you move data between distributed application components and helps you decouple these components. Amazon SWF is a web service that makes it easy to coordinate work across distributed application components.

> **Using RDS to utilize both on-premises servers and EC2 instances for your decoupled application** **and using DynamoDB to utilize both on-premises servers and EC2 instances for your decoupled application** are incorrect as RDS and DynamoDB are database services.

> **Using VPC peering to connect both on-premises servers and EC2 instances for your decoupled application** is incorrect because you can't create a VPC peering for your on-premises network and AWS VPC.

<br />


6. A company has a web-based order processing system that is currently using a standard queue in Amazon SQS. The IT Manager noticed that there are a lot of cases where an order was processed twice. This issue has caused a lot of trouble in processing and made the customers very unhappy. The manager has asked you to ensure that this issue will not recur.

What can you do to prevent this from happening again in the future? (Select TWO.)

[ ] Change the message size in SQS.

[ ] Alter the visibility timeout of SQS.

[ ] Replace Amazon SQS and instead, use Amazon Simple Workflow service.

[ ] Use an Amazon SQS FIFO Queue instead.

[ ] Alter the retention period in Amazon SQS.

**Explanation**: **Amazon SQS FIFO (First-In-First-Out) Queues** have all the capabilities of the standard queue with additional capabilities designed to enhance messaging between applications when the order of operations and events is critical, or where **duplicates can't be tolerated**, for example:

* Ensure that user-entered commands are executed in the right order.

* Display the correct product price by sending price modifications in the right order.

* Prevent a student from enrolling in a course before registering for an account.

**Amazon SWF** provides useful guarantees around task assignments. It ensures that a task is never duplicated and is assigned only once. Thus, even though you may have multiple workers for a particular activity type (or a number of instances of a decider), Amazon SWF will give a specific task to only one worker (or one decider instance). Additionally, Amazon SWF keeps at most one decision task outstanding at a time for a workflow execution. Thus, you can run multiple decider instances without worrying about two instances operating on the same execution simultaneously. These facilities enable you to coordinate your workflow without worrying about duplicate, lost, or conflicting tasks.

The main issue in this scenario is that the order management system produces duplicate orders at times. Since the company is using SQS, there is a possibility that a message can have a duplicate in case an EC2 instance failed to delete the already processed message. To prevent this issue from happening, you have to use Amazon Simple Workflow service instead of SQS.

Therefore, the correct answers are:

* Replace Amazon SQS and instead, use Amazon Simple Workflow service.

* Use an Amazon SQS FIFO Queue instead.

> **Altering the retention period in Amazon SQS** is incorrect because the retention period simply specifies if the Amazon SQS should delete the messages that have been in a queue for a certain period of time.

> **Altering the visibility timeout of SQS** is incorrect because for standard queues, the visibility timeout isn't a guarantee against receiving a message twice. To avoid duplicate SQS messages, it is better to design your applications to be idempotent (they should not be affected adversely when processing the same message more than once).

> **Changing the message size in SQS** is incorrect because this is not related at all in this scenario.

<br />
