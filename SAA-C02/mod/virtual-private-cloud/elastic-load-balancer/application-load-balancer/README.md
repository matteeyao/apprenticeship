# Application Load Balancer

If your application is composed of several individual services, an Application Load Balancer can route a request to a service based on the content of the request such as Host field, Path URL, HTTP header, HTTP method, Query string, or Source IP address. Path-based routing allows you to route a client request based on the URL path of the HTTP header. Each path condition has one path pattern. If the URL in a request matches the path pattern in a listener rule exactly, the request is routed using that rule.

You can use path conditions to define rules that forward requests to different target groups based on the URL in the request (also known as path-based routing).

Host-based routing defines rules that forward requests to different target groups based on the hostname in the host header instead of the URL.

## Enhanced Features

✓ Supported Protocols ▶︎ HTTP, HTTPS, HTTP/2, and WebSockets

✓ CloudWatch Metrics ▶︎ Additional load balance metrics and Target Group metric dimension

✓ Access Logs ▶︎ Ability to see connection details for WebSocket connections

✓ Health Checks ▶︎ Insight into target and application health at more granular level

## Additional Features

✓ Ability to enable additional routing mechanisms for your requests using **Path and Host-based Routing**

  * Path-based provides rules that forward requests to different target groups

  * Host-based can be used to define rules that forward requests to different target groups based on host name

✓ **Native IPv6 Support** within a VPC

✓ **AWS WAF** integration

✓ Dynamic Ports

  * Amazon ECS integrates w/ Application Load Balancer to expose Dynamic Ports utilized by scheduled containers

✓ Deletion Protection & Request Tracing

  * Request tracing can be used to track HTTP requests from clients to target

## Key Terms

<table class="tg">
  <thead>
    <tr>
      <th class="tg-0pky"><span style="font-weight:bold">Concept</span></th>
      <th class="tg-0pky"><span style="font-weight:bold">Description</span></th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td class="tg-0pky">Listeners</td>
      <td class="tg-0pky">A listener is a process that checks for connection requests, using the protocol and port that you configure. The rules that you define for a listener determine how the load balancer routes requests to the targets in one or more target groups.</td>
    </tr>
    <tr>
      <td class="tg-0pky">Target</td>
      <td class="tg-0pky">A target is a destination for traffic based on the established listener rules.</td>
    </tr>
    <tr>
      <td class="tg-0lax">Target Group</td>
      <td class="tg-0lax">Each target group routes requests to one or more registered targets using the protocol and port number specified. A target can be registered w/ multiple target groups. Health checks can be configured on a per target group basis.</td>
    </tr>
  </tbody>
</table>

## Target Group

When you create a target group in your Application Load Balancer, you specify its target type. This determines the type of target you specify when registering with this target group. You can select the following target types:

1. **instance** ▶︎ The targets are specified by instance ID.

2. **ip** ▶︎ The targets are IP addresses.

3. **Lambda** ▶︎ The target is a Lambda function.

## Application load balancer w/ WAF

* Monitor web requests and protect web applications from malicious requests at the load balancer

* Block or allow requests based on conditions such as IP addresses

* Pre-configured protection to block common attacks like AWL injection or cross-site scripting

* Set up web access control lists (web ACLs) and rules from the WAF console and apply them to the load balancer

## Pre-defined Security Policies on Application Load Balancer

* **ELBSecurityPolicy-TLS-1-1-2017-01**: Supports TLS 1.1 and above

* **ELBSecurityPolicy-TLS-1-2-2017-01**: Strictly supports TLS1.2

* **ELBSecurityPolicy-2016-08**: New Default policy-Same as Classic Load Balancer default policy

## Server Name Indication (SNI)

* Host multiple TLS secured applications, each w/ its own TLS certificate

* Bind multiple certificates to the same secure listener on your load balancer.

* Application Load Balancer will automatically choose the optimal TLS certificate for each client

* Support both the classic RSA algorithm and the newer, faster Elliptic-curve based ECDSA algorithm

## Application Load Balancer Sample Architecture

![Fig. 1 Application Load Balancer Sample Architecture](../../../../../img/SAA-CO2/virtual-private-cloud/elastic-load-balancer/application-load-balancer/diag01.png)

## IP as a Target

Use any IPv4 address from the load balancer's VPC CIDR **for targets within load balancer's VPC**.

Use any IP address from the RFC 6598 range (100.64.0.0/10) and in RFC 1918 ranges (10.0.0.0/8, 172,16.0.0/12, and 192.168.0.0/16) **for targets located outside the load balancer's VPC**. (This includes peered VPC, EC2-Classic, and on-premises targets reachable over Direct Connect or VPN).

![Fig. 2 Application Load Balancer Sample Architecture - IP](../../../../../img/SAA-CO2/virtual-private-cloud/elastic-load-balancer/application-load-balancer/diag02.png)
