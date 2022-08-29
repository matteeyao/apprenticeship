# Global Infrastructure

## Understand the difference between a region, an Availability Zone (AZ) and an Edge Location

> * A region is a physical location in the world which consists of two or more Availability Zones (AZ's)
>
> * An AZ is one or more discrete data centers, each with redundant power, networking and connectivity, housed in separate facilities
>
> * Edge Locations are endpoints for AWS which are used for caching content. Typically this consists of CloudFront, Amazon's Content Delivery Network (CDN)

## AWS Regions

A geographical area that consists of two or more Availability Zones (AZs). If your data sits in a particular region, then your data will not leave that region unless you specifically move your data. Each region is fault-tolerant.

## Availability Zones

AWS also has over 50 Availability Zones. An Availability Zone is one or more data centers w/ redundant power, networking, connectivity that are housed in separate facilities. These data centers sit inside different AWS regions. AWS also has multiple data centers across the world so we can think of an Availability Zone as one or more of these data centers. Data centers comprise servers, SAN, switches, load balancers, firewall, storage, etc. and you can have more than one data center in each Availability Zone.

These Availability Zones are isolated compute, storage, network, etc. and you can distribute your infrastructure across multiple Availability Zones inside your region for high availability. So if one of your Availability Zones fail, then you have your other Availability Zone that should remain operational. And again, the Availability Zones are isolated from each other but they're connected by the global high-speed network and your services can be placed across multiple Availability Zones to add resiliency and high availability.

## Edge Locations

An Edge Location is a global service. An endpoint for AWS and used for caching content. So AWS's Content Delivery Network or CDN is CloudFront. What CloudFront offers is that if a user requests certain information, then that information is cached at edge locations, so that the next time another user requests that same information, that information is already available and delivered to that user much faster compared to having to go all the way back to the data center to retrieve that specific information. AWS has over 150 edge locations around the world.

> [!NOTE]
> IAM is a globally resilient service, so **Regions** will be greyed out. But if we go back to Services and jump into EC2, you are able to select your region as EC2 is not a global service.
