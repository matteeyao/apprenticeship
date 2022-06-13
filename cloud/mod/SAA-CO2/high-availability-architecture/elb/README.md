# Elastic Load Balancers

A physical or virtual device that's designed to help you balance the network load across multiple web servers.

## Load balancer types

> **Load balancer types**:
>
> 1. Application Load Balancer
>
> 2. Network Load Balancer
>
> 3. Classic Load Balancer

### Application load balancers

> **Application Load Balancers** are best suited for load balancing of HTTP and HTTPS traffic. They operate at Layer 7 and are application-aware. They are intelligent, and you can create advanced request routing, sending specified requests to specific web servers.

### Network load balancers

> **Network Load Balancers** are best suited for load balancing of TCP traffic where extreme performance is required. Operating at the connection level (layer 4), Network Load Balancers are capable of handing millions of requests per second, while maintaining ultra-low latencies. Use for extreme performance.

### Classic load balancers

> **Classic Load Balancers** are the legacy Elastic Load Balancers. You can load/balance HTTP/HTTPS applications and use Layer 7-specific features, such as X-Forwarded and sticky sessions. You can also use strict Layer 4 load balancing for applications that rely purely on the TCP protocol.

## Errors in load balancing

> **Classic Load Balancers** - if your application stops responding, the ELB (Classic Load Balancer) responds w/ a 504 error. This means that the application is having issues. This could be either at the Web Server layer or at the Database Layer. Identify where the application is failing, and scale it up or out where possible.

## Learning summary

> **3 Different Types of Load Balancers**:
>
> * Application load balancers
>
> * Network load balancers
>
> * Classic load balancers

> 504 Error means the gateway has timed out. This means that the application not responding within the idle timeout period.
>
> Trouble shoot the application. Is it the Web Server or Database Server?

> If you need the IPv4 address of your end user, look for the **X-Forwarded-For** header

> * Instances monitored by ELB are reported as **InService** or **OutService**
>
> * **Health Checks** checks the instance health by talking to it.
>
> * Load Balancers have their own DNS name. You are never given an IP address.
>
> * Read the ELB FAQ for all the load balancer.
