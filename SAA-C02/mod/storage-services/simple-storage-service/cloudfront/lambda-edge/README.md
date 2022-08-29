# Lambda@Edge

* You can use Lambda@Edge to allow your Lambda functions to customize the content that CloudFront delivers.

* It adds compute capacity to your CloudFront edge locations and allows you to execute the functions in AWS locations closer to your application's viewers. The functions run in response to CloudFront events, without provisioning or managing servers. You can use Lambda functions to change CloudFront requests and responses at the following points:

  * After CloudFront receives a request from a viewer (viewer request)

  * Before CloudFront forwards the request to the origin (origin request)

  * After CloudFront receives the response from the origin (origin response)

  * Before CloudFront forwards the response to the viewer (viewer response)

![Fig. 01 Lambda@Edge](../../../../../img/SAA-CO2/serverless/lambda/lambda-edge/diag01.png)

* You'd use Lambda@Edge to simplify and reduce origin infrastructure.
