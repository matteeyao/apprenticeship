# Adapter

Adapter is a structural design pattern that allows objects w/ incompatible interfaces to collaborate.

## Problem

Images that you're creating a stock market monitoring app. The app downloads the stock data from multiple sources in XML format and then displays nice-looking charts and diagrams for the user.

At some point, you decide to improve the app by integrating a smart 3rd-party analytics library. But there's a catch: the analytics library only works w/ data in JSON format.

![You can't use the analytics library "as is" b/c it expects the data in a format that's incompatible w/ your app.](../img/adapter-1.png)

You could change the library to work w/ XML. However, this might break some existing code that relies on the library. And worse, you might not have access to the library's source code in the first place, making this approach impossible.

## Solution

You can create an *adapter*. This is special object that converts the interface of one object so that another object can understand it.

An adapter wraps one of the objects to hide the complexity of conversion happening behind the scenes.

The wrapped object isn't even aware of the adapter. For example, you can wrap an object that operates in meters and kilometers w/ an adapter that converts all of the data to imperial units such as feet and miles.

Adapters can not only convert data into various formats but can also help objects w/ different interfaces collaborate. Here's how it works:

1. The adapter gets an interface, compatible w/ one of the existing objects.

2. Using this interface, the existing object can safely call the adapter's methods.

3. Upon receiving a call, the adapter passes the request to the second object, but in a format and order that the second object expects.

Sometimes it's even possible to create a two-way adapter that can convert the calls in both directions.

![](../img/adapter-2.png)

Let's get back to our stock market app. To solve the dilemma of incompatible formats, you can create XML-to-JSON adapters for every class of the analytics library that you code works w/ directly.

Then you adjust your code to communicate w/ the library only via these adapters.

When an adapter receives a call, it translates the incoming XML data into a JSON structure and passes the call to the appropriate methods of a wrapped analytics object.

## Structure

### Object adapter

This implementation uses the object composition principle: the adapter implements the interface of one object and wraps the other one. It can be implemented in all popular programming languages.

![](../img/adapter-3.png)

1. The **Client** is a class that contains the existing business logic of the problem.

2. The **Client Interface** describes a protocol that other classes must follow to be able to collaborate w/ the client code.

3. The **Service** is some useful class (usually 3rd-party or legacy). The client can't use this class directly b/c it has an incompatible interface.

4. The **Adapter** is a class that's able to work w/ both the client and the service: it implements the client interface, while wrapping the service object. The adapter received calls from the client via the adapter interface and translates them into calls to the wrapped service object in a format it can understand.

5. The client code doesn't get coupled to the concrete adapter class as long as it works w/ the adapter via the client interface. Thanks to this, you can introduce new types of adapters into the program w/o breaking the existing client code. This can be useful when the interface of the service class gets changed or replaced: you can just create a new adapter class w/o changing the client code.

### Class adapter

This implementation uses inheritance: the adapter inherits interfaces from both objects at the same time. Note that this approach can only be implemented in programming languages that support multiple inheritance, such as C++.

![](../img/adapter-4.png)

1. The **Class Adapter** doesn't need to wrap any objects b/c it inherits behaviors from both the client and the service. The adaptation happens within the overridden methods. The resulting adapter can be used in place of an existing client class.

## #Pseudocode

This example of the **Adapter** pattern is based on the classic conflict btwn square pegs and round holes.

![Adapting square pegs to round holes.](../img/adapter-5.png)

The Adapter pretends to be a round peg, w/ a radius equal to a half of the square's diameter (in other words, the radius of the smallest circle that can accomodate the square peg).

```c#
/* Say you have two classes w/ compatible interfaces:
RoundHole and RoundPeg. */
class RoundHole is
    constructor RoundHole(radius) { ... }
    
    method getRadius() is
        // Return the radius of the hole.
        
    method fits(peg: RoundPeg) is
        return this.getRadius() >= peg.getRadius()
    
class RoundPeg is
    constructor RoundPeg(radius) { ... }
    
    method getRadius() is
        // Return the radius of the peg.
        
// But there's an incompatible class: SquarePeg
class SquarePeg is 
    constructor SquarePeg(width) { ... }
    
    method getWidth() is
        // Return the square peg width.
        
/* An adapter class lets you fit square pegs into round holes.
It extends the RoundPeg class to let the adapter objects act
as round pegs. */
class SquarePegAdapter extends RoundPeg is
    /* In reality, the adapter contains an instance of the
    SquarePeg class. */
    private field peg: SquarePeg

    constructor SquarePegAdapter(peg: SquarePeg) is
        this.peg = peg
    
    method getRadius() is
        /* The adapter pretends that it's a round peg w/ a
        radius that could fit the square peg that the adapter
        actually wraps. */
        return peg.getWidth() * math.sqrt(2) / 2
        
// Somewhere in client code.
hole = new RoundHole(5)
rpeg = new RoundPeg(5)
hole.fits(rpeg) // true

small_sqpeg = new SquarePeg(5)
large_sqpeg = new SquarePeg(10)
hole.fits(small_sqpeg) // this won't compile (incompatible types)
    
small_sqpeg_adapter = new SquarePegAdapter(small_sqpeg)
large_sqpeg_adapter = new SquarePegAdapter(large_sqpeg)
hole.fits(small_sqpeg_adapter) // true
hole.fits(large_sqpeg_adapter) // false
```

## Applicability

**Use the Adapter class when you want to use some existing class, but its interface isn't compatible w/ the rest of your code.**

The Adapter pattern lets you create a middle-layer class that serves as a translator btwn your code and a legacy class, a 3rd-party class or any other class w/ a weird interface.

**Use the pattern when you want to reuse several existing subclasses that lack some common functionality that can't be added to the superclass.**

You could extend each subclass and put the missing functionality into new child classes.

However, you'll need to duplicate the code across all of these new classes, which smells really bad.

The much more elegant solution would be to put the missing functionality into an adapter class.

Then you would wrap objects w/ missing features inside the adapter, gaining needed features dynamically.

For this to work, the target classes must have a common interface, and the adapter's field should follow that interface.

This approach looks very similar to the **Decorator** pattern.

## How to Implement

1. Make sure that you have at least two classes w/ incompatible interfaces:

    * A useful *service* class, which you can't change (often 3rd-party, legacy, or w/ lots of existing dependencies).

    * One or several *client* classes that would benefit from using the service class. 

2. Declare the client interface and describe how clients communicate w/ the service.

3. Create the adapter class and make it follow the client interface. Leave all the methods empty for now.

4. Add a field to to the adapter class to store a reference to the service object. The common practice is to initialize this field via the constructor, but sometimes it's more convenient to pass it to the adapter when calling its methods.

5. One by one, implement all methods of the client interface in the adapter class. The adapter should delegate most of the real work to the service object, handling only the interface or data format conversion.

6. Clients should use the adapter via the client interface. This will let you change or extend the adapters w/o affecting the client code.

## Relations with Other Patterns

* **Bridge** is usually designed up-front, letting you develop parts of an application independently of each other. On the other hand, **Adapter** is commonly used w/ an existing app to make some-otherwise incompatible classes work together nicely.

* **Adapter** changes the interface of an existing object, while **Decorator** enhances an object w/o changing its interface. In addition, *Decorator* supports recursive composition, which isn't possible when you use *Adapter*.

* **Adapter** provides a different interface to the wrapped object, **Proxy** provides it w/ the same interface, and **Decorator** provides it w/ an enhanced interface.

* **Facade** defines a new interface for existing objects, whereas **Adapter** tries to make the existing interface usable. *Adapter* usually wraps just one object, while **Facade** works w/ an entire subsystem of objects.

* **Bridge**, **State**, **Strategy** (and to some degree **Adapter**) have very similar structures. Indeed, all of these patterns are based on composition, which is delegating work to other objects. However, they all solve different problems. A pattern isn't just a recipe for structuring your code in a specific way. It can also communicate to other developers the problem the pattern solves. 

## Usage of the pattern in C#

**Adapter** is a structural design pattern, which allows incompatible objects to collaborate.

The Adapter acts as a wrapper between two objects. It catches calls for one object and transforms them to format and interface recognizably w/ a second object.

**Identification**: Adapter is recognizable by a constructor which takes an instance of a different abstract/interface type. When the adapter receives a call to any of its methods, it translates parameters to the appropriate format and then directs the call to one or several methods of the wrapped object.

## Conceptual Example

This example illustrates the structure of the **Adapter** design pattern. It focuses on answering these questions:

* What classes does it consist of?

* What roles do these classes play?

* In what way the elements of the pattern are related?

### Program.cs: Conceptual example

```c#
using System;

namespace RefactoringGuru.DesignPatterns.Adapter.Conceptual
{
    // The Target defines the domain-specific interface used by the client code.
    public interface ITarget
    {
        string GetRequest();
    }
    
    /* The Adaptee contains some useful behavior, but its interface is
    incompatible w/ the existing client code. The Adaptee needs som
    adaptation before the client code can use it. */
    class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }
    
    /* The Adapter makes the Adaptee's interface compatible w/ the Target's
    interface. */
    class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;
        
        public Adapter(Adaptee adaptee)
        {
            this._adaptee = adaptee;
        }
        
        public string GetRequest()
        {
            return $"This is '{this._adaptee.GetSpecificRequest()}'";
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);
            
            Console.WriteLine("Adaptee interface is incompatible with the client.");
            Console.WriteLine("But with adapter client can call it's method.");
            
            Console.WriteLine(target.GetRequest());
        }
    }
}
```

### Output.txt: Execution result

```
Adaptee interface is incompatible with the client.
But with adapter client can call it's method.
This is 'Specific request.'
```
