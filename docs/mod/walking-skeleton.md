# Kickstart Your Next Project with a Walking Skeleton

Acceptance test → system needs to talk to a datastore or two, communicate w/ a couple internal services, and maybe an external service as well.

Since it's hard to build both the infrastructure and the business logic at the same time you make a few assumptions in your test and stub out these dependencies, adding them to TODO list.

Setting up datastores, resolving security-related issues w/ the external service, lack of documentation

## Deploy a walking skeleton first

In order to reduce risks on projects like the above you need to figure out all the unknowns as early as possible.

The best way to do this is to have a *real end-to-end test* w/ no stubs against a system that's *deployed in production*.

Enter the Walking Skeleton: a "tiny implementation of the system that performs a small end-to-end function. It need not use the final architecture, but it should link together the main architectural components. The architecture and the functionality can then evolve in parallel."

If the system needs to talk to one ore more datastores then the walking skeleton should perform a simple query against each of them, as well as simple requests against any external or internal service.

If it needs to output something to the screen, insert an item to a queue or create a file, you need to exercise these in the simplest possible way.

As part of building it you should write your deployment and build scripts, setup the project, including its tests, and make sure all the automations are in place - such as CI integration, monitoring and exception handling.

The focis is the infrastructure, not the features.

Only after you have your walking skeleton should you write your first acceptance test and begin the TDD cycle.

This is only the skeleton of the application, but the parts are connected and the skeleton does walk in the sense that it exercises all the system's parts as you currently understand them.

B/c of this partial understanding, you must make the walking skeleton minimal. But it's not a prototype and not a proof of concept - it's production code, so you should definitely write tests as you work on it.

These tests will assert things like "accepts a request", "pushes some content to S3", or "pushes an empty message to the queue".

A similar concept called "Tracer Bullets" was introduced in **The Pragmatic Programmer**.

## Start w/ the Riskiest Task

According to Hofstadter's Law, "It always takes longer than you expect, even when you take into account Hofstadter's Law".

Amazingly, the law is always spot on. It makes sense then to work on the riskiest parts of the project first, which are usually the parts which have dependencies: on third party services, on in house services, on other groups in the organization you belong to.

It makes sense to get the ball rolling w/ these groups simply b/c you don't know how long it will take and what problems should arise.

## Don't cut corners

It's important to stress that until the walking skeleton is deployed to production (possibly behind a feature flag or just hidden from the outside world) you are not ready to write the first acceptance test.

You want to exercise your deployment and build scripts and discover as many potential problems as you can as early as possible.

The Walking Skeleton is a way to validate the design and get early feedback so that it can be improved.

You will be missing this feedback if you cut or take shortcuts.

## Kickstart the TDD process

You can also think about it as a way to start the TDD process. It can be daunting or just too much work to build the infrastructure along w/ the first acceptance test.

Furthermore, changes in one may require changes in the other (it's the "first-feature paradox from GOOS).

This is why you first work on the infrastructure and only then move on to work on the first feature.

## Obstacles and Tradeoffs

By front-loading all infrastructure work you're postponing the delivery of the first feature.

Some managers might feel uncomfortable when this happens, as they expect very rapid pace at the beginning of the project. You might feel some pressure to cut corners.

However, heir confidence should increase when you deliver the walking skeleton and they have a real, albeit minimal, system to play w/.

Most hard problems in software development are communication problems, and this is no exception.

You should explain how the walking skeleton will reduce unexpected delays at the end of the project.

The walking skeleton may not save you from the recursiveness of Hofstadter's Law but it may make the last few days of the project a little more sane.
