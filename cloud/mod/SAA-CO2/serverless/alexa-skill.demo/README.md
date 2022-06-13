# Build an Alexa Skill

We've built a website using S3, DynamoDB, API Gateway, SNS, and Polly. To maintain use of the Polly service, instead of building a serverless website, we're going to send the Polly service - encode our MP3 files straight to an S3 bucket. And then we're going to use an Alexa skill. We're going to build a Lambda function using the Serverless Application Repository, and  we're going to point that Lambda function to the S3 bucket, and then you'll be able to build an Alex skill.

## How to build our skills

| **Skill Service** | **Skill Interface** |
|-------------------|---------------------|
| * AWS Lambda      | * Invocation Name   |
|                   | * Intent Schema     |
|                   | * Slot Type         |
|                   | * Utterances        |

* **Invocation Name** is the name we use to enable our skill
