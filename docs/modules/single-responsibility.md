# Single Responsibility Principle (SRP)

*“A class (component) should have one, and only one, reason to change”*

## Benefits of the Single Responsibility Principle

Makes your software easier to implement and prevents unexpected side-effects of future changes.

## Frequency and Effects of Changes

B/c of the name, discussions around this consist of defining what is meant by “responsibility” of a component, while the definition talks only about change. 

“Change” makes it a bit more concrete, but still leaves a lot open for discussion: what level of change do we look at?

Requirements change over time. Each of them also changes the responsibility of at least one class. 

The more responsibilities your class has, the more often you need to change it.

If your class implements multiple responsibilities, they are no longer independent of each other.

You need to change your class as soon as one of its responsibilities changes. That is obviously more often than you would need to change it if it had only one responsibility.

That might not seem like a big deal, but it also affects all classes or components that depend on the changed class. Depending on your change, you might need to update the dependencies or recompile the dependent classes even though they are not directly affected by your change. They only use one of the other responsibilities implemented by your class, but you need to update them anyway.

In the end, you need to change your class more often, and each change is more complicated, has more side-effects, and requires a lot more work than it should have.

So, it's better to avoid these problems by making sure that each class has only one responsibility.

Systems that satisfy the SRP are flexible with isolated behaviors contained in small, cohesive packages. 

This allows us to safely make changes to functionality. 

At its core, SRP is another way of maximizing cohesion. 

After vigorously eliminating duplication and making sure that our pieces are named appropriately and expressively, we generally find that our code satisfies the SRP.

## Easier to Understand

Classes, software components and micro-services that have only one responsibility are much easier to explain, understand and implement than the ones that provide a solution for everything.

However, make sure to not oversimplify your code. Some developers take the single responsibility principle to the extreme by creating classes w/ just one function. Later, when they want to write some actual code, they have to inject many dependencies which makes the code very unreadable and confusing.

## A simple question to validate your design

If you build your software over a longer period and if you need to adapt it to changing requirements, it might seem like the easiest and fastest approach is adding a method or functionality to your existing code instead of writing a new class or component.

But that often results in classes with more than responsibility and makes it more and more difficult to maintain the software.

You can avoid these problems by asking a simple question before you make any changes: What is the responsibility of your class/component/microservice?

If your answer includes the word "and," you're most likely breaking the single responsibility principle.
