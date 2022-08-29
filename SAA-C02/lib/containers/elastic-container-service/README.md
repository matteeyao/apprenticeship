# Elastic Container Service

1. A solutions architect is designing a solution to run a containerized web application by using Amazon Elastic Container Service (Amazon ECS). The solutions architect wants to minimize cost by running multiple copies of a task on each container instance. The number of task copies must scale as the load increases and decreases.

Which routing solution distributes the load to the multiple tasks?

[ ] Configure an Application Load Balancer to distribute the requests by using path-based routing. → `Incorrect. With path-based routing, multiple services can use the same listener port on a single Application Load Balancer (ALB). The ALB forwards requests to specific target groups based on the URL path. However, this solution does not help with load distribution between different tasks of the same service.`

[ ] Configure an Application Load Balancer to distribute the requests by using dynamic host port mapping. → `Correct. With dynamic host port mapping, multiple tasks from the same service are allowed for each container instance.`
