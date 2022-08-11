# Standard Queue Service

1. A company is experiencing problems w/ its message-processing application. During periods of high demand, the application becomes overloaded. The application is based on a monolithic design and is hosted in an on-premises data center. The company wants to move the application to the AWS Cloud and decouple the monolithic architecture. A solutions architect must design a solution that allows worker components of the application to access the messages and handle the peak volume.

Which solution meets these requirements w/ the HIGHEST throughput?

[ ] Use a Network Load Balancer w/ target groups that are configured to perform the path-based routing to Amazon EC2 instances

[ ] Use Amazon Simple Queue Service (Amazon SQS) FIFO queues in combination w/ Amazon EC2 instances that are scaled by an Auto Scaling group

[ ] Use an Application Load Balancer w/ target groups that are configured to perform path-based routing to Amazon EC2 instances.

[x] Use Amazon Simple Queue Service (Amazon SQS) standard queues in combination w/ Amazon EC2 instances that are scaled by an Auto Scaling group.

**Explanation**: There is no indication that path-based routing will distribute traffic evenly. The use of a queueing tool is a better way to decouple this monolithic architecture. "Overload" and "decouple" are key words that signal SQS. SQS is an in-transit message service that prevents messages from being lost. If the order of messages processed need to be processed in the order that they are received or are "time-sensitive", FIFO SQS queues would be the better choice.
