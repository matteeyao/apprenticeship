# The SOLID Principles

Like the 4 rules of simple design, the SOLID principles focus on making systems flexible and adaptable when changes are required.

As we’ll see, focusing on the 4 rules of simple design can lead us to satisfying these principles. Let’s look at these principles, and how
they can relate to flexible designs.

## Single Responsibility (SRP)
*“A class (component) should have one, and only one, reason to change”*

The Single Responsibility Principle is by far one of the most popular, while simultaneously one of the least understood. B/c of the name, discussions around this consist of defining what is meant by “responsibility” of a component, while the definition talks only about change. “Change” makes it a bit more concrete, but still leaves a lot open for discussion: what level of change do we look at?

Systems that satisfy the SRP are flexible with isolated behaviors contained in small, cohesive packages. This allows us to safely make changes to functionality. At its core, SRP is another way of maximizing cohesion. After vigorously eliminating duplication and making sure that our pieces are named appropriately and expressively, we generally find that our code satisfies the SRP.

## Open-Closed (OCP)
*“A system should be open for extension, but closed for modification”*

Changing code is dangerous; once we have it written and tested, we want to minimize the chance for bugs to be introduced. By introducing or altering behavior only through extension, we benefit from the stability of small, stable core pieces that won’t change out from under us.

There is a danger when focusing too much on the OCP. If we plan for extensibility, our systems become riddled with unnecessary and unwieldy extension points and extensibility mechanisms. To counter this, we should focus on isolating knowledge, naturally building only the extension points that truly represent the pieces of our system that will change.

## Liskov Substitution (LSP)

*“Derived types should be substitutable for their base types”*

Polymorphism is a key part of a flexible design. Being able to substitute a more specific type when a general type is expected allows us to provide different behaviors w/o having complex branching. 

There is a danger, though, if the specialized type significantly changes fundamental expectations of the more general type’s behavior Derived types should enhance any base behaviors, rather than change it.

A healthy focus on the names we give our types can help in abiding by LSP. A specialized type’s name should reflect that it is an enhancement of the base, not a change.

### 7.2.3 Honor the Contract

Subclasses agree to a *contract*; they promise to be substitutable for their superclasses. 

Substitutability is possible only when objects behave as expected, and subclasses are *expected* to conform to their superclass's interface. They must respond to every message in that interface, taking the same kinds of inputs and returning the same kinds of outputs. They are not permitted to do anything that forces others to check their type in order to know how to treat them or what to expect of them.

Where superclasses place restrictions on input arguments and return values, subclasses can indulge in a slight bit of freedom w/o violating their contract. Subclasses may accept input parameters that have broader restrictions and may return results that have narrower restrictions, all while remaining perfectly substitutable for their superclasses.

Subclasses that fail to honor their contract are difficult to use. They're "special" and cannot be freely substituted for their superclasses. These subclasses are declaring that they are not really a *kind-of* their superclass and cast doubt on the correctness of the entire hierarchy.

When you honor the contract, you are following the **Liskov Substitution Principle**. Her principle states:

* Let *q(x)* be a property about objects of *x* of type *T*. Then *q(y)* should be true for objects *y* of type *S* where *S* is a subtype of *T*.

In order for a type system to be sane, sub-types must be substitutable for their supertypes.

Following this principle crates applications where a subclass can be used anywhere its superclass would be and where objects that include modules can be trusted to interchangeably play the module's role.

## Interface Segregation

*“Interfaces should be small, focused on a specific use case”*

The surface area of a class has a direct influence on how easy it is to use. Although a class might have several different ways to use it, any specific client should see only those behaviors specific to its needs.

When we focus on effectively grouping and naming the behaviors of our class, we naturally build small interfaces that provide a clear, cohesive view of what our class does. If it is difficult to name, that is feedback that our class is getting too large.

## Dependency Inversion

*“Depend on abstractions, rather than concrete implementations”*

One of the most dangerous parts when changing a system is having your changes unexpectedly influence other, unrelated parts of your system. 

We want to guard against the situation where a change ripples through the whole system, causing waves and possible bugs throughout.

By depending on abstractions, decoupling ourselves from concrete implementations, we can set up walls between behaviors. Abstractions
better move us into standardized communication methods between components, making it easier to independently replace or change things.
