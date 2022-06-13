# Simple Work Flow Service

> **What is SWF?**
>
> Amazon Simple Workflow Service (Amazon SWF) is a web service that makes it easy to coordinate work across distributed application components. SWF enables applications for a range of use cases, including media processing, web application backends, business process workflows, and analytics pipelines, to be designed as a coordination of tasks.

> Tasks represent invocations of various processing steps in an application which can be performed by executable code, web service calls, human actions, and scripts.

SWF is a method of coordinating both your applications and manual tasks/processes.

## Learning summary

> **SWF vs. SQS**
>
> * SQS has a retention period of up to 14 days; w/ SWF, workflow executions can last up to 1 year.
>
> * Amazon SWF presents a task-oriented API, whereas Amazon SQS offers a message-oriented API.
>
> * Amazon SWF ensures that a task is assigned only once and is never duplicated. W/ Amazon SQS, you need to handle duplicated messages and may also need to ensure that a message is processed only once.
>
> * Amazon SWF keeps track of all the tasks and events in an application. W/ Amazon SQS, you need to implement your own application-level tracking, especially if your application uses multiple queues.

> **SWF Actors**
>
> * **Workflow Starters**
>
>   * An application that can initiate (start) a workflow. Could be your e-commerce website following the placement of an order, or a mobile app searching for bus times.
>
> * **Deciders**
>
>   * Control the flow of activity tasks in a workflow execution. If something has finished (or failed) in a workflow, a Decider decides what to do next.
>
> * **Activity Workers**
>
>   * Carry out the activity tasks.

## Amazon SWF Domains

Domains provide a way of scoping Amazon SWF resources within your AWS account. All the components of a workflow, such as the workflow type and activity types, must be specified to be in a domain. It is possible to have more than one workflow in a domain; however, workflows in different domains can't interact w/ each other.

When setting up a new workflow, before you set up any of the other workflow components, you need to register a domain if you have not already done so.

When you register a domain, you specify a *workflow history retention period*. This period is the length of time that Amazon SQF will continue to retain information about hte workflow execution after the workflow execution is complete.
