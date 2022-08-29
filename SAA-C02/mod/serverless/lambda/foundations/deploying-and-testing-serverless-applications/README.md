# Deploying and Testing Serverless Applications

This lesson explores the differences between server-based and serverless deployments and environments. This lesson also introduces how an application framework, such as the AWS Serverless Application Model (AWS SAM), can simplify your deployment practices. AWS SAM can make the move to serverless more efficient. 

Using a real-world analogy of buying a new house, you can compare server-based deployments with serverless deployment. When you decide to buy a new house, you can buy a prebuilt house that is already standing and ready to move into or design and build a new house. 

> ![Fig. 1 Server-based deployments ](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig01.jpeg)
>
> Server-based deployments are compared to moving into a pre-existing house. You know the layout of the house, its infrastructure, and the confines in which you need to work.

**The server-based model is the existing house that you can move into.**

Before you move in, you understand the layout and the rooms and how your family will fit into the house. You don’t need to know how the wood holds the house together or the types of nails used. You work with what’s physically already there. For example, the house has three bedrooms, two bathrooms, and a kitchen.

After you've decided to buy the prebuilt house, you pack your possessions into boxes. The moving company arrives and picks them up to bring (deploy) them to the new house. As a developer, the packing of your boxes and handing them to the movers compares with checking in your code. Then DevOps (the movers) are responsible for taking and deploying the code to the correct environment. The movers know the location of the house and which boxes go into which rooms.

## How a serverless deployment differs from a server-based deployment

**A serverless deployment is like designing and building a house using detailed specifications.**

> Serverless can be compared to building a house by using a blueprint. With this blueprint, you can build the same house anywhere in the world.

When you design and build a house, you must tell the builders every detail of the house:

* Number of rooms to build

* Room size

* Number of windows, doors, and light sockets

* Where to put the light sockets

These details are conveyed to the building contractors through the blueprints. A blueprint is a template of how to build every component of the house. With this blueprint, you can visualize what the house looks like, and you can use that same blueprint to build many identical houses. With serverless, everything that your Lambda function needs is included in the blueprint, called the CloudFormation Template.

**The AWS CloudFormation template is considered the *blueprint* for the Lambda function.**

> ![Fig. 2 CloudFormation template](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig02.jpeg)
>
> When you deploy Lambda functions, the blueprint takes the form of an AWS CloudFormation template.

The CloudFormation template specifies every detail of the Lambda function and the environment required for the Lambda function to run. CloudFormation provides a common language and format that all parts of AWS can read and understand.

**CloudFormation is infrastructure as code.**

> ![Fig. 3 CloudFormation is IaC](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig03.jpeg)
>
> A single template can deploy identical Lambda functions within multiple AWS accounts.

The entire infrastructure needed for your Lambda function is written to a text file. This file (template) then deploys your desired stack. A stack is a collection of AWS resources that you can manage as a single unit. The template becomes the single source of truth for deploying identical stacks into any AWS account. Each time the Lambda function is invoked, it runs by using information provided in the CloudFormation template. 

## Server-based development environments

In general, a developer workflow includes the following basic steps:

1. Author code

2. Test and debug changes in isolation

3. Merge your code into the larger application code and perform application testing

In a server-based deployment, this workflow is achieved by doing the following:

1. Pulling down a local copy of the application

2. Working locally through your IDE to code

3. Testing and debugging your code

4. Checking your changes into source control

The updated code is then picked up by a build-and-deploy process. For many developers, this is when they pass the code to a DevOps team member. DevOps validates the build and deploys the updated application to designated instances. As a developer, you then have access to a particular set of development and test instances. These are the environments where you perform additional application and integration testing and debugging. 

After testing is successful, DevOps follows a similar deployment process to update production instances with the tested application components. The build scripts and the environments that your code is deployed into are already established and waiting for your application code. This process is like the prebuilt house that is ready and waiting for you to move in. 

To view each of the build stages for server-based application deployment, select each number in the following graphic.

> ![Fig. 4 Testing & Debugging Server-based Application](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig04.jpeg)
>
> A single template can deploy identical Lambda functions within multiple AWS accounts.

> ### Write and edit code
>
> Code is written locally in the integrated development environment (IDE).

> ### Local test and debug
>
> The code is tested and debugged locally until the developer feels confident in the code.

> ### Code check-in
>
> The code is checked into a source control repository where it can be versioned and the DevOps team can access it.

> ### Build and deploy
>
> A DevOps team member validates the build and deploys the updated application to designated test or development instances.

> ### Developer testing environments
>
> Developers have access to defined test instances to perform application/integration testing and debugging.

> ### Test and debug
>
> After the application is deployed in the development environment, the developer must test and debug the application code.

## Serverless development environments

In a serverless deployment, you provide all the components necessary to deploy your function: 

* Code, bundled with any necessary dependencies

* CloudFormation template, which is the blueprint for building the serverless environment

Like a blueprint for a house, the CloudFormation template can quickly become lengthy and difficult to manage. For example, if you have an application with a single API endpoint, the CloudFormation template must include mappings for IAM roles, API Gateway endpoints, and connections to the Amazon DynamoDB table. Including these details can result in up to 100 lines of code in a simple template.

**A key difference in the developer workflow is how the code and the application are tested.**

> ![Fig. 5 Test and debug serverless application](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig05.jpeg)
>
> With serverless, developers build the application and its environment into a single deployment package that can be used only in the cloud. Developers are not required to test locally or check in code.

Because serverless is hosted in the cloud, no option is available to check out a local copy of the application to do localized testing. You cannot recreate the environment specified in your CloudFormation template locally. Instead, you must have access to designated AWS accounts for testing. You can then perform realistic testing of your application in the cloud. With the appropriate access permissions, you can deploy and test your stack to any development, testing, staging, or production account in your organization. 

**AWS SAM makes serverless development easier.**

> AWS SAM is a simplified set of CloudFormation commands that makes serverless development easier.

**Ensures environmental parity**

> ![Fig. 6 Application Framework](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig06.jpeg)
>
> The application framework ensures that you always deploy the same stack to each account.

**Simplifies experimentation**

Without the overhead of maintaining instances, you can use AWS SAM to quickly start stacks for different feature branches. You can experiment without incurring costs outside of the actual invocations that run on that environment.

## Reduce risk using versions and aliases  

One potential challenge to serverless deployments is that when the function is deployed, it becomes live immediately. This means that a function can potentially go live without testing it, which puts your working applications at risk. This risk is especially true if you move toward an automated CI/CD pipeline and need to easily promote new code or roll back if there's a problem. To mitigate this risk, you can version your Lambda functions and add aliases to ensure safe deployments.

> ### Versioning
>
> You can use versions to manage the deployment of your functions. For example, you can publish a new version of a function for beta testing without affecting users of the stable production version. Lambda creates a new version of your function each time that you publish the function. The new version is a copy of the unpublished version of the function. 
>
> When you create a Lambda function, only one version exists, which is identified by `$LATEST` at the end of the Amazon Resource Name (ARN).
>
> `arn:aws:lambda:aws-region:acct-id:function:helloworld:$LATEST`

> ### Publish
>
> Publish makes a snapshot copy of `$LATEST`.
>
> Enable versioning to create immutable snapshots of your function every time you publish it. 
>
>   * Publish as many versions as you need. 
>
>   * Each version results in a new sequential version number.
>
>   * Add the version number to the function ARN to reference it.
>
>   * The snapshot becomes the new version and is immutable.
>
> `arn:aws:lambda:aws-region:acct-id:function:helloworld:1`

> ### Aliases
>
> A Lambda alias is like a pointer to a specific function version. You can access the function version using the alias ARN. Each alias has a unique ARN. An alias can point only to a function version, not to another alias. You can update an alias to point to a new version of the function. 
>
> `awn:aws:lambda:aws-region:acct-id:function:helloworld:Test`

## Test using alias routing

You can also use routing configuration on an alias to send a portion of traffic to a second function version. For example, you can reduce the risk of deploying a new version by configuring the alias to send most of the traffic to the existing version and only a small percentage of traffic to the new version.

You can point an alias to a maximum of two Lambda function versions. The versions must meet the following criteria:

* Both versions must have the same runtime role.

* Both versions must have the same dead-letter queue configuration, or no dead-letter queue configuration.

* Both versions must be published. The alias cannot point to `$LATEST`.

> ![Fig. 7 Updating alias](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig07.jpeg)
>
> When an alias is updated to point to a new version, incoming requests immediately point to the new version. If the new version encounters problems, it could potentially affect 100% of your users.

> ![Fig. 8 Weighted aliases](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig08.jpeg)
>
> One way to mitigate risk in testing is to use weighted aliases on your Lambda function so that only a portion of the traffic goes to the new version. In this example, if the new version encounters any errors, only 10% of your users are affected.

### Integrate with AWS CodeDeploy

Lambda is integrated with AWS CodeDeploy for automated rollout with traffic shifting. CodeDeploy supports multiple traffic shifting methods, in addition to alarms and hooks. CodeDeploy supports the following traffic-shifting patterns:

* **Canary** – Traffic is shifted in two increments. If the first increment is successful, the second is completed based on the time specified in the deployment.

* **Linear** – With linear traffic shifting, traffic is slowly shifted in a predetermined percentage every X minutes based on how you have it configured.

* **All-at-once** – Shifts all traffic from the original Lambda function to the updated Lambda function version at once.

Additionally, it supports the following testing options:

* **Alarms** – These instruct CloudWatch to monitor the deployment and trigger an alarm if any errors occurred during rollout. Any alarms would automatically roll back your deployment.

* **Hooks** – Give you the option to run pre-traffic and post-traffic test functions that run sanity checks before traffic-shifting starts to the new version and after traffic-shifting completes. 

> [!NOTE]
>
> When the alarms or hooks trigger a rollback, everything in the CloudFormation template being deployed is rolled back. Best practice is to keep your AWS SAM templates and CloudFormation templates as concise in scope as possible. As a guideline, examine no fewer than one template per service that you are deploying.

## Shift traffic for Lambda using AWS CodeDeploy

You can use AWS SAM to configure your CodeDeploy traffic-shifting options. Review the additions to your AWS SAM template that can add safe deployment checks when using CodeDeploy.

> ![Fig. 9 AutoPublishAlias](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig09.jpeg)
>
> Include the AutoPublishAlias configuration to direct AWS SAM to publish an alias and automatically increment the version on every deployment.

> ![Fig. 10 DeploymentPreference option](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig10.jpeg)
>
> Use the **DeploymentPreference** option to specify which traffic-shifting pattern to use. In addition to canary and linear options, you can use the all-at-once option to shift all traffic from the original Lambda function to the updated Lambda function version at once.

> ![Fig. 11 Alarms option](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig11.jpeg)
>
> You can use the Alarms option to instruct CloudWatch to monitor the deployment and trigger an alarm if any errors occur during rollout. Alarms would automatically roll back your deployment.

> ![Fig. 12 Hooks](../../../../../../img/SAA-CO2/serverless/lambda/foundations/deploying-and-testing-serverless-applications/fig12.jpeg)
>
> Use Hooks to run PreTraffic and PostTraffic test functions that run sanity checks before traffic shifting starts and after traffic shifting completes.
>
> [!NOTE]
>
> When the alarms or hooks trigger a rollback, **everything** in the CloudFormation template being deployed is rolled back. Keep your AWS SAM templates and CloudFormation templates as concise in scope as possible.

## Learning summary

1. Which statements are true? (Select THREE.)

[x] When you create a Lambda function, only one version exists: the `$LATEST` version.

[ ] Versioning is a requirement for your Lambda functions.

[ ] You can specify a versioning number scheme that Lambda will use.

[x] You can reference any version with an alias.

[x] You can reference a version or an alias in the Amazon Resource Name (ARN).

**Explanation**: You can reference any version with an alias, you can reference a version or an alias in the Amazon Resource Name (ARN), and when you create a Lambda function, only the `$Latest` version exists.
