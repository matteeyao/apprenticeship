# Simple Work Flow Service

> **What is SWF?**
>
> Amazon Simple Workflow Service (Amazon SWF) is a web service that makes it easy to coordinate work across distributed application components. SWF enables applications for a range of use cases, including media processing, web application backends, business process workflows, and analytics pipelines, to be designed as a coordination of tasks.

> Tasks represent invocations of various processing steps in an application which can be performed by executable code, web service calls, human actions, and scripts.

SWF is a method of coordinating both your applications and manual tasks/processes.

> **Amazon SWF**
>
> A managed workflow service that helps developers build, run, and scale applications that coordinate work across distributed components.

## SWF Key Details

* SWF is a way of coordinating tasks between application and people. It is a service that combines digital and human-oriented workflows.

* An example of a human-oriented workflow is the process in which Amazon warehouse workers find and ship your item as part of your Amazon order.

* SWF provides a task-oriented API and ensures a task is assigned only once and is never duplicated. Using Amazon warehouse workers as an example again, this would make sense. Amazon wouldn't want to send you the same item twice as they'd lose money.

* The SWF pipeline is composed of three different worker applications that help to bring a job to completion:

  * **SWF Actors** are workers that trigger the beginning of a workflow.

  * **SWF Deciders** are workers that control the flow of the workflow once it's been started.

  * **SWF Activity Workers** are the workers that actually carry out the task to completion.

* W/ SWF, workflow executions can last up to one year compared to the 14 days maximum retention period for SQS.

## Amazon SWF: Benefits

* Logical Separation

* Reliable

* Simple

* Scalable

* Flexible

## Overview of Service Components

* **Domains** ▶︎ collection of related workflows

* **Workflow Starter** ▶︎ Any application that can initiate workflow executions such as a website at which a customer places an order.

* **Workflow** ▶︎ collection of actions and actions are tasks or workflow steps

* **Tasks** are *work assignments*. There are three types of tasks in Amazon SWF:

  * Activity Task

  * Lambda Task

  * Decision Task ▶︎ Contains current workflow history

* **Deciders** implement a workflow's coordination logic; Controls the flow of activity tasks in a workflow execution.

* **Activity workers** implement actions; A process or thread that performs the activity tasks that are part of your workflow.

* **Workflow History** ▶︎ detailed, complete, consistent record of every event that occurred since the workflow execution started.

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
