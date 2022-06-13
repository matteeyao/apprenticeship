# AWS Elasticity and Scaling

## Horizontal and vertical scaling
When scaling, we are adding or removing resources to and from a system.

Vertical scaling is resizing your EC2 instance to a larger size. For instance, resizing from a T2.micro to a T2.medium. By resizing our instance, we are actually adding more CPU and more memory for our system to handle the increase in demand. If you choose to scale vertically, you could experience a small disruption b/c that instance, when it's resized, will need to be rebooted.

Tieing in *Cost Optimization*, when you vertically scale to a larger instance size, you will observe an immediate increase in your cost. Ensure we are right-sizing our instances from the get-go.

A benefit of vertical scaling is that there is usually no application modifications needed and usually works for most applications.

B/c horizontal scaling was designed to fix some of the issues that we have w/ vertical scaling. W/ horizontal scaling, instead of increasing the size of our instance, we actually add more instances of the same size to handle that load. And instead of having one running copy of our application, we now have a copy for each instance that's scaled, and we can also use a load balancer here to help distribute the load across all of our instances.

W/ horizontal scaling, a main concern is *sessions*. We don't want to interrupt our customer sessions w/ our applications. When we have a single application running on a single server, then all of our sessions are stored on that server. But when scaling w/ multiple servers w/ copies of our application, the load balancer sends requests to different servers b/c horizontal scaling is meant to even out the load. So the load balancer sends requests to different servers that have a copy of our application.

![Fig. 1 Sticky sessions and horizontal scaling](../../../../../img/SAA-CO2/design-resilient-architectures/well-architected-framework/elasticity-and-scaling/img.png)

## Elasticity and scaling

Elasticity utilizes automation along w/ horizontal scaling to match capacity supply w/ ever-changing demand. AWS also provides launch configurations and auto scaling to scale out our systems to match our capacity to that demand, allowing our environment to scale out adding additional resources as the demand increases.
