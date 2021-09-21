# Other Design Guidelines

## The SOLID Principles

Like the 4 rules, they focus on making systems flexible and adaptable when changes are required.

## Single Responsibility (SRP)

*"A class (component) should have one, and only one, reason to change"*

Discussions around this consist of defining what is meant by "responsibility" of a component, while the definition talks only about change.

“Change” makes it a bit more concrete, but still leaves a lot open for discussion: what level of change do we look at?

Systems that satisfy the SRP are flexible with isolated behaviors contained in small, cohesive packages.

This allows us to safely make changes to functionality.

At its core, SRP is another way of maximizing cohesion.

After vigorously eliminating duplication and making sure that our pieces are named appropriately and expressively, we generally find that our code satisfies the SRP.

## Open-Closed (OCP)

*"A system should be open for extension, but closed for modification"*

Changing code is dangerous; once we have it written and tested, we want to minimize the chance for bugs to be introduced.

By introducing  or altering behavior only through extension, we benefit from the stability of small, stable core pieces that won't change out from under us.

There is a danger when focusing too much on the OCP. If we plan for extensibility, our systems become riddled w/ unnecessary and unwieldy extension points and extensibility mechanisms.

To counter this, we should focus on isolating knowledge, naturally building only the extension points that truly represent the pieces of our system that will change.

## Liskov Substitution (LSP)

*"Derived types should be substitutable for their bast types"*

Polymorphism is a key part of a flexible design. Being able to substitute a more specific type when a general type is expected allows us to provide different behaviors w/o having complex branching.

There is a danger, though, if the specialized type significantly changed fundamental expectations of the more general type's behavior. Derived types should enhance any base behaviors, rather than change it.

A healthy focus on the names we give our types can help in abiding by LSP. A specialized type's name should reflect that it is an enhancement of the base, not a change.

## Interface Segregation

*"Interfaces should be small, focused on a specific use case"*

The surface area of a class has a direct influence on how easy it is to use.

Although a class might have several different ways to use it, any specific client should see only those behaviors specific to its needs.

When we focus on effectively grouping *and naming* the behaviors of our class, we naturally build small interfaces that provide a clear, cohesive view of what our class does.

If it is difficult to name, that is feedback that our class is getting too large.

## Dependency Inversion

*“Depend on abstractions, rather than concrete implementations”*

One of the most dangerous parts when changing a system is having your changes unexpectedly influence other, unrelated parts of your system.

We want to guard against the situation where a change ripples through the whole system, causing waves and possible bugs throughout.

By depending on abstractions, decoupling ourselves from concrete implementations, we can set up walls between behaviors.

Abstractions better move us into standardized communication methods between components, making it easier to independently replace or change things.

## Law of Demeter

A method can access either locally-instantiated variables, parameters passed in, or instance variables.

A much simpler way to think about it is: *Only one dot per statement.*

At its heart, the LoD is about encapsulation. We don’t want to reach inside an object and manipulate its insides.

Instead, we want to ask objects to perform some action for us. Let the object deal with its collaborators.

The LoD can also be thought of in terms of knowledge duplication. By exposing the internals of an object, we are spreading structural knowledge through our code. Both the object *and* the outside collaborator know about its internals.

Personally, I find the LoD to be an extremely simple, incredibly powerful mechanism for helping ensure proper encapsulation and decoupling of behaviors across an object graph.
