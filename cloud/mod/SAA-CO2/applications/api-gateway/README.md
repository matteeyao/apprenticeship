# API Gateway

> **What is API Gateway?**
>
> Amazon API Gateway is a fully managed service that makes it easy for developers to publish, maintain, monitor, and secure APIs at any scale.
>
> W/ a few clicks in the AWS Management Console, you can create an API that acts as a "front door" for applications to access data, business logic, or functionality from your back-end services, such as applications running on Amazon Elastic Compute Cloud (Amazon EC2), code running on AWS Lambda, or any web application.

> **What can API Gateway do?**
>
> * Expose HTTPS endpoints to define a RESTful API
>
> * Serverless-ly connect to services like Lambda & DynamoDB
>
> * Send each API endpoint to a different target
>
> * Run efficiently w/ low cost
>
> * Scale effortlessly
>
> * Track and control usage by API key
>
> * Throttle requests to prevent attacks
>
> * Connect to CloudWatch to log all requests for monitoring
>
> * Maintain multiple versions of your API

## API Gateway configuration

> **How do I configure API Gateway?**
>
> * Define an API (container)
>
> * Define Resources and nested Resources (URL paths)
>
> * For each Resource:
>
>   * Select supported HTTP methods (verbs)
>
>   * Set security
>
>   * Choose target (such as EC2, Lambda, DynamoDB, etc.)
>
>   * Set request and response transformations

## API Gateway deployments

> **How do I deploy API Gateway?**
>
> * Deploy API to a stage:
>
>   * Uses API Gateway domain, by default
>
>   * Can use custom domain
>
>   * Now supports AWS Certificate Manager: free SSL/TLS certs

## API Gateway caching

> You can enable API caching in Amazon API Gateway to cache your endpoint's response. W/ caching, you can reduce the number of calls made to your endpoint and also improve the latency of the requests to your API. When you enable caching for a stage, API Gateway caches responses from your endpoint for a specified time-to-live (TTL) period, in seconds. API Gateway then responds to the request by looking up the endpoint response from the cache instead of making a request to your endpoint.

## Same Origin Policy

> In computing, the same-origin policy is an important concept in the web application security model. Under the policy, a web browser permits scripts contained in a first web page to access data in a second web page, but only if both web pages have the same origin.
>
> **This is done to prevent Cross-Site Scripting (XSS) attacks**.
>
>   * Enforced by web browsers.
>
>   * Ignored by tools like PostMan and curl.

One page can communicate w/ another page provided that the first page has the same domain name.

## CORS Explained

> CORS is one way the server at the other end (not the client code in the browser) can relax the same-origin policy.
>
> Cross-origin resource sharing (CORS) is a mechanism that allows restricted resources (e.g. fonts) on a web page to be requested from another domain outside the domain from which the first resource was served.

## CORS in action

> * Browser makes an HTTP OPTIONS call for a URL (OPTIONS is an HTTP method like GET, PUT, and POST)
>
> * Server returns a response that says:
>
>   * "These other domains are approved to GET this URL."
>
> * Error - "Origin policy cannot be read at the remote resource?" You need to enable CORS on API Gateway.

## Learning summary

> * API Gateway has caching capabilities to increase performance
>
> * API Gateway is low cost and scales automatically
>
> * You can throttle API Gateway to prevent attacks
>
> * You can log results to CloudWatch
>
> * If you are using Javascript/AJAX that uses multiple domains w/ API Gateway, ensure that you have enabled CORS on API Gateway
>
> * CORS is enforced by the client
