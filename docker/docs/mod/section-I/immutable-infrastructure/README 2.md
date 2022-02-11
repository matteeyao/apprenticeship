# An introduction to immutable infrastructure

## Why you should stop managing infrastructure and start really programming it.

Immutable infrastructure (II) provides stability, efficiency, and fidelity to your applications through automation and the use of successful patterns from programming. No rigorous or standardized definition of immutable infrastructure exists yet, but the basic idea is that you create and operate your infrastructure using the programming concept of immutability: once you instantiate something, you never change it. Instead, you replace it w/ another instance to make changes or ensure proper behavior.

Immutable infrastructure (II) requires full automation of your runtime environment. This is only possible in compute environments that have an API over all aspects of configuration and monitoring. Therefore, II can be fully realized only in true cloud environments. It is possible to realize some benefits of II w/ partial implementations, but the true benefits of efficiency and resiliency are realized w/ thorough implementation

## Give up on artisanal infrastructure

Historically, we've thought of machine uptime and maintenance as desirable b/c we associate the health of the overall service or application w/ them. In the data center, hardware is expensive and we need to carefully craft and maintain each individual server to preserve our investments over time. In the cloud, this is an anachronistic perspective and one we should give up on in order to create more resilient, simpler, and ultimately more secure services and applications. Werner Vogels, CTO of Amazon and an early leading thinker on cloud systems, captures this sentiment by imploring us to stop "hugging servers" (they don't hug back)

There are a variety of reasons artisanally maintained infrastructure composed of traditional, long-lived (and therefore mutable) components is insufficient to the task of operating modern, distributed services in the cloud

**Increasing operational complexity**.

* The rise of distributed service architectures, and the use of dynamic scaling results in vastly more stuff to keep track of. Using mutable maintenance methods for updates or patching configurations across fleets of hundreds or thousands of compute instances is difficult, error-prone, and a time sink

**Slower deployments, more failures**.

* When infrastructure is comprised of snowflake components resulting from mutable maintenance methods (whether via scripts or configuration management tools), there's a lot more that can go wrong. Deviating from a straight-from-source-control process means accurately knowing the state of your infrastructure is impossible. Fidelity is lost as infrastructure behaves in unpredictable ways and time is wasted chasing down configuration drift and debugging the runtime

**Identifying errors and threats in order to mitigate harm**.

* Long-lived, mutable systems rely on identifying error or threat to prevent damage. We now know that this is a Sisyphean undertaking, as the near daily announcements of high profile and damaging enterprise exploits attest. And those are only the ones reported. W/ Immutable infrastructure (II) and automated regeneration of compute resources, many errors and threats are mitigated whether they are detected or not

**Fire drills**.

* Artisanal infrastructure allows us to take shortcuts on automation that come back to bite us in unexpected ways, such as when a cloud provider reboots underlying instances to perform their own updates or patches. If we build and maintain our infrastructure manually, and aren't in the regular routine of Immutable Infrastructure (II) automation, these events become fire drills

## Immutable infrastructure provides hope

Immutable infrastructure (II) shares much in common w/ how natures maintains advanced biological systems, like you and me. The primary mechanism of fidelity in humans is the constant destruction and replacement of subcomponents. It underlies the immune system, which destroys cells to maintain health. It underlies the growth system, which allows different subsystems to mature over time through destruction and replacement. The individual human being maintains a sense of self and intention, while the underlying components are constantly replaced. Systems managed using II patterns are no different

The benefits of immutable infrastructure are manifold if applied appropriately to your application and have fully automated deployment and recovery methods for your infrastructure

**Simplifying operations**.

* W/ fully-automated deployment methods, you can replace old components w/ new versions to ensure your systems are never far in time from their initial "known-good" state. Maintaining a fleet of instances becomes much simpler w/ Immutable infrastructure (II) since there's no need to track the changes that occur w/ mutable maintenance methods

**Continuous deployments, fewer failures**.

* W/ Immutable infrastructure (II), you know what's running and how it behaves, deploying updates can become routine and continuous, w/ fewer failures occurring in production. All change is tracked by your source control and Continuous Integration/Continuous Deployment processes

**Reduces errors and threats**.

* Services are built atop a complex stack of hardware and software, and things do wrong over time. By automating replacement instead of maintaining instances, we are, in effect, regenerating instances regularly and more often. This reduces configuration drift, vulnerability surface, and level of effort to keep Service Level Agreements. Many of the maintenance fire drills in mutable systems are taken care of naturally

**Cloud reboot? No problem!**

* W/ Immutable infrastructure (II) you know what you have running, and w/ fully automated recovery methods for your services in place, cloud reboots of your underlying instances should be handled gracefully and w/ minimal, if any, application downtime

![Mutable versus Immutable Cloud Reboot](https://www.oreilly.com/radar/wp-content/uploads/sites/3/2019/06/immutable_infrastructure-8346d81e892e98c1308f707a037f4040.gif)

We have to work very hard to maintain things, and when those things were physical boxes in a rack, this was necessary work b/c we manually configured hardware. But w/ logically isolated compute instances that can be instantiated w/ an API call in an effectively infinite cloud, "maintaining boxes" is an intellectual ball and chain. It ties us to caring about and working on the wrong things. Giving up on them enables you to focus on what matters to the success of your application, rather than being constantly pulled down by high maintenance costs and the difficulty in adopting new patterns
