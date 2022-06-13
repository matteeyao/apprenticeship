# AWS WAF

> AWS WAF is a web application firewall that lets you monitor the HTTP and HTTPS requests that are forwarded to Amazon CloudFront, an Application Load Balancer or API Gateway.
>
> AWS WAF also lets you control access to your content.
>
> Example: `http://acloud.guru?id=1001&name=ryan`

A web application firewall (WAF) is a layer seven aware firewall, able to monitor query string parameters and actual information being sent to our web servers

Whereas a physical hardware file would only be able to go up to layer four → WAFs are therefore more secure, able to see a lot more information than a typical firewall

> You can configure conditions such as what IP addresses are allowed to make this request or what query string parameters need to be passed for the request to be allowed.
>
> Then the application load balancer or CloudFront or API Gateway will either allow this content to be received or to give a HTTP 403 Status Code.

> At its most basic level, AWS WAF allows 3 different behaviors:
>
> 1. Allow all requests except the ones you specify
>
> 2. Block all requests except the ones you specify
>
> 3. Count the requests that match the properties you specify

## WAF protection

> Extra protection against web attacks using conditions you specify. You can define conditions by using characteristics of web requests such as:
>
> * IP addresses that requests originate from.
>
> * Country that requests originate from.
>
> * Values in request headers.
>
> * Strings that appear in requests, either specific strings or strings that match regular expression (regex) patterns.
>
> * Length of requests.
>
> * Presence of SQL code that is likely to be malicious (known as SQL injection).
>
> * Presence of a script that is likely to be malicious (known as cross-site scripting).

## Learning summary

> In the exam you will be given different scenarios and you will be asked how to block malicious IP addresses.
>
> * **Use AWS WAF**
>
> * **Use Network ACLs**
