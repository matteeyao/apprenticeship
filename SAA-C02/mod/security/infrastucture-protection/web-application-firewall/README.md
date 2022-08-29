# Web Application Firewall (WAF)

A solution that lets you quickly secure your applications using Firewall rules in the cloud.

> **What is WAF?**
>
> Web application firewall that lets you monitor HTTP(S) requests to **CloudFront**, **ALB**, or **API Gateway**
>
> * Control access to content
>
> * Configure **filtering rules** to allow/deny traffic:
>
>   * IP addresses
>
>   * Query string parameters (e.g. http://example.com/page?foo=bar&abc=xyz&baz=123)
>
>   * SQL query injection
>
> * Blocked traffic returns HTTP 403 Forbidden

![Fig. 1 WAF use-case diagram](../../../../img/aws/security/waf/web-application-firewall-use-diagram.png)

Based on rules that you specify, WAF directs your service to respond to requests either w/ the requested content or an error. You do this by configuring what are called filtering rules to allow or deny traffic.

You can configure WAF to inspect values of query string parameters for anything that you might want to allow or deny and it can also protect you against SQL. Attackers sometimes insert malicious SQL code into web requests in an effort to extract data from your database. So, in cases where your WAF rules result in blocking traffic, that blocked traffic returns an HTTP 403 forbidden error code.

## How does WAF work?

> WAF allows three different behaviors
>
> * **Allow** all requests, except the ones you specify
>
> * **Block** all requests, except the ones you specify
>
> * **Count** the requests that match the properties you specify
>
> * Request properties:
>
>   * Originating IP address
>
>   * Originating country
>
>   * Request size
>
>   * Values in request headers
>
>   * Strings in request matching regular expressions (regex) patterns
>
>   * SQL code (injection)
>
>   * Cross-site scripting (XSS)

You might want to enforce an upper or lower bound on the size of the request coming into your application.

You can also inspect the string values in any of the HTTP request headers. So, for example, you might want to block HTTP requests that don't contain a user agent header.

You can also inspect strings and requests that match regular expressions or Regex patterns. For instance, you can use Regex to block certain known bad bots by looking for patterns in the user-agent header.

You can also use WAF to block SQL code injection. So, perhaps, we use WAF's built-in SQL injection condition in conjunction w/ a Regex-based condition to look for SQL injection attempts only on URLs ending w/ ".PHP" while ignoring URLs ending w/ ".JPEG"

WAF can also block cross-site scripting or XSS attacks. Cross-site scripting attacks are those where the attacker uses vulnerabilities in an otherwise benign website as a vehicle to inject malicious client-side scripts like JavaScript into other legitimate users web browsers. This cross-site scripting feature prevents these vulnerabilities in your web applications by inspecting different elements of the incoming request.

## AWS Firewall Manager

> Centrally configure and manage firewall rules across an **AWS Organization**
>
> * WAF rules:
>
>   * ALB
>
>   * API Gateway
>
>   * CloudFront distributions
>
> * AWS Shield Advanced protections:
>
>   * ALB
>
>   * ELB Classic
>
>   * EIP
>
>   * CloudFront distributions
>
> * Enable security groups for EC2 and ENIs

There is another service that's tightly integrated w/ AWS WAF - AWS Firewall Manager. This is a security management service that allows you to centrally configure and manage Firewall rules across an AWS organization.

Using Firewall Manager, you can deploy WAF rules for your Application Load Balancer, API Gateway, or CloudFront distributions.

You can also create AWS Shield Advanced Protection policies to automatically discover resources like Application Load Balancers, ELB Classic Load Balancers, Elastic IPs, or CloudFront distributions and consistently apply DDoS protection to all those resources or use tags to specify a subset of resources.

You can also use AWS Firewall to configure security groups across multiple accounts in your AWS organization and continuously audit them to detect overly permissive or misconfigured rules that apply to EC2 instances and Elastic Network Interfaces.
