# Creating Flexible Interfaces

The conversation btwn objects takes place using their *interfaces*; this chapter explores creating flexible interfaces that allow applications to grow and to change.

Interface *within* a class: Classes implement methods; some of those methods are intended to be used by others, and these methods make up its public interface.

An alternative kind of interface is one that spans across classes and that is independent of any single class. Used in this sense, the word interface represents a set of messages where the messages themselves define the interface.

## Responsibilities, Dependencies, and Interfaces

The public parts of a class are the stable parts; the private parts are the changeable parts.

When you mark methods as public or private, you tell users of your class upon which methods they may safely depend.

## 4.3 Finding the Public Interface

### 4.3.2 Constructing an Intention

*Domain objects* are persistent; they stand for big, visible real-world things that will en up w/ a representation in the database.

### 4.3.3 Using Sequence Diagrams

Sequence diagrams are defined in the Unified Modeling Language (UML) and are one of many diagrams that UML supports.

Sequence diagrams provide a simple way to experiment w/ different object arrangements and message-passing schemas.

They show two things:

* objects, and

* the *messages* passing between them

Messages are shown as horizontal lines. When a message is sent, the line is labeled w/ the message name. Message lines end or begin w/ an arrow; this arrow points to the receiver. When an object is busy processing a received message, it is *active* and its vertical line is expanded to a vertical rectangle.

Sequence diagrams specify the messages tat pass btwn objects, and b/c objects should only communicate using public interfaces, sequence diagrams are a vehicle for exposing, experiment w/, and ultimately defining interfaces.

You don't send messages b/c you have objects, you have objects b/c you send messages.

### 4.3.4 Asking for "What" Instead of Telling "How"

When the conversation btwn two objects switches from how to *what*, one side effect is that the size of the public interface in Mechanic is drastically reduced.

### 4.3.5 Seeking Context Independence

The context that an object expects has a direct effect on how difficult it is to reuse.

Objects that have a simple context are easy to use and easy to test; they expect few things from their surroundings.

Objects that have a complicated context are hard to use and hard to test; they require complicated setup before they can do anything.

The best possible situation is for an object to be completely independent of its context. An object that could collaborate w/ others w/o knowing who they are or what they do could be reused in novel and unanticipated ways.

You already know the technique for collaborating w/ others w/o knowing who they are-dependency injection.

### 4.3.6 Trusting Other Objects

Allows objects to collaborate w/o binding themselves to context and is necessary in any application that expects to grow and change.

### 4.3.8 Creating a Message-Based Application

Sequence diagrams help keep the focus on messages and allow you to form a rational intention about the first thing to assert in a test.

Switching your attention from objects to messages allows you to concentrate on designing an application built upon public interfaces.

## 4.4 Writing Code That Puts Its Best (Inter)Face Forward

### 4.4.1 Create Explicit Interfaces

Every time you create a class, declare its interfaces, methods in the *public* interface should:

* Be explicitly identified as such.

* Be more about what then *how*.

* Have names that, insofar as you can anticipate, will not change.

* Prefer keyword arguments.

Ruby provides three relevant keywords: `public`, `protected`, and `private`. Use of these keywords serves two distinct purposes. 

First, they indicate which methods are stable and which are unstable. 

Second, they control how visible a method is to other parts of your application.

## 4.5 The Law of Demeter

The Law of Demeter is a set of coding rules that results in loosely coupled objects. 

Loose coupling is nearly always a virtue but is just one component of design and must be balanced against competing needs.

Some Demeter violations are harmless, but others expose a failure to correctly identify and define public interfaces.

### 4.5.1 Defining Demeter

Demeter restricts the set of objects to which a method may *send* messages; it prohibits routing a message to a third object via a second object of a different type.

Demeter is often paraphrased as "only talk to your immediate neighbors" or "use only one dot."

### 4.5.2 Consequences of Violations

It may be cheapest, *in your specific case*, to reach through intermediate objects to retrieve distant attributes.

Balance the likelihood and cost of change against the cost of removing the violation.

If, for example, you are printing a report of a set of related objects, the most rational strategy may be to explicitly specify the intermediate objects and to change the report if it becomes necessary.

B/c the risk incurred by Demeter violations is low for stable attributes, this may be the most cost-efficient strategy.

The third message chain, `hash.keys.sort.join` is perfectly reasonable and, despite the abundance of dots, may not be a Demeter violation at all. Instead of evaluating this phrase by counting the "dots", evaluate it by checking the types of the intermediate objects.

* `hash.keys` returns an `Enumerable`

* `hash.keys.sort` returns an `Enumerable`

* `hash.keys.sort.join` returns a `String`

Demeter is subtler than it first appears. Its fixed rules are not an end in themselves; like every design principle, it exists *in service* of your overall goals.

Certain "violations" of Demeter reduce your application's flexibility and maintainability, while others make perfect sense.

### 4.5.3 Avoiding Violations

One common way to remove train wrecks from code is to use delegation to avoid the dots. 

In object-oriented terms, to *delegate* a message is to pass it on to another object, often via a wrapper method.

The wrapper method encapsulates, or hides, knowledge that would otherwise be embodied in the message chain.

There are a number of ways to accomplish delegation.

Ruby provides support via `delegate.rb` and `forwardable.rb`, which makes it easy for an object to automatically intercept a message sent to *self* and to instead send it somewhere else.

This technique is sometimes useful, but beware: It can result in code that obeys the letter of the law while ignoring its spirit.

Using delegation to hide tight coupling is not the same as decoupling the code.

### 4.5.5 Listening to Demeter

Message chains like `customer.bicycle.wheel.rotate` occur when your design thoughts are unduly influenced by objects you already know.

Your familiarity w/ the public interfaces of known objects may lead you to string together long message chains to get at distant behavior.

Reaching across disparate objects to invoke distant behavior is tantamount to saying, "There's some behavior way over there that I need right here, and I know how to go get it."

The code knows not only what it wants (to rotate) but *how* to navigate through a bunch of intermediate objects to reach the desired behavior.

Tight coupling causes all kinds of problems. The most obvious is that it raises the risk that `Trip` will be forced to change b/c of an unrelated change somewhere in the message chain.

However, there's another problem here that is even more serious.

When the `depart` method knows this chain of objects, it binds itself to a very specific implementation, and it cannot be reused in any other context. `Customers` must always have `Bicycles`, which in turn must have `Wheels` that rotate.

Consider that this message chain would look like if you has started out by deciding *what* `depart` wants from `customer`. From a message-based point of view, the answer is obvious: `customer.ride`

The `ride` method of customer hides implementation details from `Trip` and reduces both its context and its dependencies, significantly improving the design.

When FastFeet changes and begins leasing hiking trips, it's much easier to generalize from `customer.ride` to `customer.depart` or `customer.go` than to disentangle the tentacles of this message chain from your application.

The train wrecks of Demeter violations are clues that there are objects whose public interfaces are lacking.

Listening to Demeter means paying attention to your point of view.

If you shift to a message-based perspective, the messages you find will become public interfaces in the objects they lead you to discover.

However, if you are bound by the shackles of existing domain object, you'll end up assembling their existing public interfaces into long message chains and thus will miss the opportunity to find and construct flexible public interfaces.

## 4.6 Summary

Object-oriented applications are defined by the messages that pass between objects.

This message passing takes place along "public" interfaces; well-defined public interfaces consist of stable methods that expose the responsibilities of their underlying classes and provide maximal benefit at minimal cost.

Focusing on messages reveals objects that might otherwise be overlooked.

When messages are *trusting* and ask for what the sender wants instead of telling the receiver how to behave, objects naturally evolve public interfaces that are flexible and reusable in novel and unexpected ways.
