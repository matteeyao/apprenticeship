# Introducing Demeter and its Laws

## Adaptive Programming

AP deals w/ specifying the connections btwn objects as loosely as possible (this is called "structure-shy" programming).

The Demeter system and tools are all about "Adaptive" programming.

An extension of OOP that attempts to wait to bind algorithms to data-structures as late as possible.

A special kind of language supported by the Demeter tools to work w/ other OOPLs, lets you write "adaptive" programs that try to express things like "starting from object `A`, go to object `C` via all objects w/ an attribute named "`x`"."

Thus, the precise details about going from `A` to `Z` to `Q` to `P` to `M` and then to `C`, are distilled into the above "traversal specification" which tries to represent the essence of what is really required at a more generic level.

Hence, if your class hierarchy structure or network changes so that the exact path that was used for the above "access" (`A` → `Z` → `Q` → `P` → `M` → `C`) now has changed to encompass new classes or more paths, the actual code for the algorithm or behavior doesn't need to change b/c the condition of `A` to `C` via "`x`" is still correct.

The structure needs to be updated, but not the method implementation.

This is "Adaptive Programming", it makes your programs more flexible, more resilient to change, and more adaptable to varying configurations of classes within a given domain.

## The Law of Demeter

More precisely the "Law of Demeter for Functions/Methods" (LoD-F).

A design-style rule for object-oriented programs.

Its essence is the "principle of least knowledge" re the object instances used within a method.

The idea is to assume as little as possible about the structure and properties of instances and their subparts.

Thus, it is okay to request a service of an objects instance, but if I reach into that object to access another sub-object and request a service of that sub-object, I am assuming knowledge of the deeper structure of the original object that was made available to me.

The Law of Demeter says that if I need to request a service of an objects sub-parts, I should instead make the request of the object itself and let it propagate this request to all relevant sub-parts, thus the object is responsible for knowing its internal make-up instead of the method that uses it.

Stated more formally, the Law of Demeter for functions says that:

A method "`M`" of an object "`O`" should invoke *only* the methods of the following kinds of objects:

1. itself

2. its parameters

3. any objects it creates/instantiates

4. its direct component objects

The basic idea is to avoid invoking methods of a member object that is returned by another method.

When you do this, you make structural assumptions about the container object that may be likely to change.

The container may later need to be modified to contain a different number of the contained objects, or it may end up being changed to contain another object which contains the original component object.

If the "returned" object isn't a subpart of the object whose method was invoked, nor of some other object, then it typically is *not* a violation of LoD to invoke a method of the returned object (particularly if the object was created by the invoking method).

Using the Law of Demeter ("LoD"), you instead ask the container to invoke a method on its elements and return the result. The details of how the container propagates the message to its elements are encapsulated by the containing object.

A side-effect of this is that if you conform to LoD, while it may quite increase the maintainability and adaptability of your software system, you also end up having to write *lots* of little wrapper methods to propagate method calls to its components (which can add noticeable time and space overhead).

This problem is addressed by the Demeter tools, which automates the solution.

There is also a "*Law of Demeter for Adaptive Programs*" (abbreviated LoD-AP). LoD-AP is a style guideline for how to form your traversal specification.

LoD-AP is a style guideline for how to form your traversal specification.

## The Demeter Tools

The Demeter tools for java make use of "Adaptive Visitors" objects which are AP analogs of the "Visitor" pattern from the now famous **Design Patterns** book by the "gang of four" (or GoF): Erich Gamma, Richard Helm, Ralph Johnson, and John Vlissides.

The Visitor objects are responsible for carrying out the traversal specification. The Adaptive Visitors are a more seamless fit w/ OOPLs than a relational-ish graph-navigation-specification language.

Before the Adaptive Visitor objects were used, Adaptive C++/Demeter programs looked kind of like SQL-style from-to statements and constraints w/ embedded C++ code (or vice versa).

Now (w/ Java and adaptive visitors) they look more like a single language rather than two mixed together.

AP, in general, and the Demeter method and tools, life OOP to a higher level of abstraction by allowing the user to focus on the essential classes in a network and the invariants among their relationships.

## The Demeter Method

One uses a "*class graph*" of a system to indicate all the inheritance and containment relationships btwn objects in the system.

The graph can be represented textually as a grammar.

If the "grammar" is LL(1) (or can be reduced to an LL(1) grammar - which is usually the case) then there are lots of nice properties it has that lets the Demeter tools determine and automate all sorts of nice things for you.

Hence, the gist of it is that if you need to code a method that has to navigate (via other methods and/or objects) across several links and intermediate links to process information, then using the Demeter tools, you (the "adaptive programmer") rarely have to concern yourself w/ knowledge of any objects along the navigation path other than the source and destination classes/vertices and perhaps some (but not all) key intermediate objects/vertices.

The Demeter tools can generate all the intermediate navigations from the class graph and your from-to spec (w/ optional constraints).

Later, when your class graph evolves and changes, the code for the method your wrote doesn't need to be modified at all (since you specified it "adaptively" as a propagation pattern using succinct traversal specifications).

All you usually need to do is just regenerate the traversal code w/ Demeter and then recompile.

## Object "Waves" and "Particles"

In Physics, sometimes it is useful to view an electron as a particle and other times as a wave.

In software, I think the same is true of objects.

Sometimes, we take the particle view (an object encapsulated by the boundaries of information hiding) and sometimes we take the "wave" view (the set ("wave") of collaborations and collaborators for a given operation).

A common deficiency of current OOPLs is that while they provide semantic constructs to minimize dependencies when employing the "particle view", they don't provide semantic constructs to yield the same benefit for the "wave" view.

They let us separate interface from implementation within the bounds of a single object or method (the particle view), but not necessarily within the relationships of all of the collaborators for a single method.

The Demeter method and tools address this deficiency.

They let us use protocols that specify only the precondtions, postconditions, and invariants not only for a single object method, but also for the entire family of actors that collaborate to carry out the entire "logical operation".

Demeter's "succinct traversal specifications" allow us to encode _only_ the source, destination, and key intermediate members (invariants) of an information access (non-trivial data-flow) w/o having to know about all the non-critical links in the chain.

So when the non-critical links come and go (or otherwise "evolve") no manual effort is required for us to adapt to this change.

The traversal invariant still holds, and only the changes to the class graph need to be made.

The same codification of behavior still works simply by re-compiling the program using the Demeter tools (no procedural changes were necessary).

The Demeter tools syntax may seem a bit unsettling at first, but it allows you to get the same or greater functionality by assuming "less" information.

The less you have to assume, the less you need to know and depend on.

The less you have to know, the simpler things should be.

And, of course, the fewer dependencies you have, the more maintainable, reusable, adaptable (and hence durable) your program will be.

So if you have a method that takes a "BigMac" as an argument and needs to access the seeds on the pickles, you don't have to know about the lettuce, onions, and sesame seed bun that are all "on route" to the pickle seeds (much less that there are 2 all beef patties lurking underneath all of that), all you need to know is that a path *exists* from the BigMac to its pickle seeds.

You don't have to know what that path is or exactly how to traverse it, and you don't have to encode that path in your methods.

That way when the burger later changes such that the pickles are now on top of the onions instead of the other way around, you don't have to care.

## Corollaries to LoD

More detailed refinements or applications of LoD.

They present a bunch of design/programming guidelines and rules for writing classes and class libraries to make them into reusable class frameworks (usable by client subclasses as well as "normal" clients).

Some of the rules they propose are:

1. Whenever an object needs to request a service of some other external object, this external service request should be encapsulated in an internal non-public method of the object. This allows derived classes to override the service request w/ a more specialized one (it is also a use of the GoF Template pattern).

2. Whenever an object needs to instantiate some other externally associated object, it should do so using a non-public method to perform the instantiation. This allows derived classes to instantiate a more specialized object if needed.

A public method M of a class C should invoke only its own public and private methods, and those of its superclasses. (Any non-public methods should obey LoD.)
