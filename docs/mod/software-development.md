# Start Your Project w/ a Walking Skeleton

In order to reduce risk on large software development projects, you need to figure out all the big unknowns as early as possible.

The best way to do this is to have a real end-to-end test w/ no stubs against a system that's deployed in production.

You could do this by building a so-called Walking Skeleton, a tiny implementation of the system that performs a small end-to-end function.

It need not use the final architecture, but it should link together the main architectural components.

The architecture and the functionality can evolve in parallel.

A Walking Skeleton is a tiny implementation of the system that performs a small end-to-end function. It need not use the final architecture, but it should link together the main architectural components. The architecture and the functionality can then evolve in parallel.

If the system needs to talk to one or more datastores then the walking skeleton should perform a simple query against each of them, as well as simple requests against any external or internal service.

If it needs to output something to the screen, insert an item to a queue or create a file, you need to exercise these in the simplest possible way.

As part of building it, you should write your deployment and build scripts, set up the project, including its test, and make sure all the automations are in place - such as Continuous Integration, monitoring, and exception handling.

The focus is the infrastructure, not the features.

Only after you have your walking skeleton, should you write your first automated acceptance tests.

This is only the skeleton of the application, but the parts are connected and the skeleton does walk in the sense that it exercises all the system's parts as you currently understand them.

B/c of this partial understanding, you must make the walking skeleton minimal. But it's not a prototype and not a proof of concept - it's production code, so you should definitely write tests as you work on it.

## High Risk First

According to Hofstadter's Law, "It always takes longer than you expect, even when you take into account Hofstadter's Law".

This law is valid all too often. It makes sense then to work on the riskiest parts of the projects first, which are usually the parts that have dependencies: on third-party services, on in-house services, on other groups in the organization you belong to.

It makes sense to get the ball rolling w/ these groups simply b/c you don't know how long it will take and what problems should arise.

Making changes to architecture is harder and more expensive the longer it has been around and the bigger it gets. We want to find mistakes early.

This approach gives us a short feedback cycle, from which we can more quickly adapt and work iteratively as necessary to meet the business' prioritized list of runtime-discernible quality attributes.

Assumptions about the architecture are validated earlier.

The architecture is more easily evolved b/c problems are found at an earlier stage when less has been invested in its implementation.

The bigger the system, the more important it is to use this strategy. In a small application, one developer can implement a feature from top to bottom relatively quickly, but this becomes impractical w/ larger systems.

It is quite common to have multiple developers on a single team or even on multiple, possibly distributed teams involved in implementing end-to-end. Consequently, more coordination is necessary.

## No Shortcuts

It’s important to stress that until the walking skeleton is deployed to production (possibly behind a feature flag or just hidden from the outside world) you are not ready to write the first acceptance test.

You want to exercise your deployment and build scripts and discover as many potential problems as you can as early as possible.

The Walking Skeleton is a way to validate the architecture and get early feedback so that it can be improved. You will be missing this feedback if you cut corners or take shortcuts.
