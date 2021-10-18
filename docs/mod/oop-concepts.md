# Object-Oriented Programming Concepts

**encapsulation**, **abstraction**, **inheritance**, and **polymorphism**

## Encapsulation

Encapsulation is achieved when each object keeps its state **private**, inside a class.

Other objects don't have direct access to this state.

Instead, they can only call a list of public functions - called methods.

So, the object manages its own state via methods - and no other class can touch it unless explicitly allowed.

If you want to communicate w/ the object, you should use the methods provided.

But (by default), you can't change the state.

Let's say we're building a tiny Sims game.

There are ppl and there is a cat.

They communicate w/ each other. We want to apply encapsulation, so we encapsulate all "cat" logic into a `Cat` class. It may look like this:

![You can feed the cat. But you can't directly change how hungry the cat is.](../img/encapsulation.png)

Here the "state" is the **private variables** `mood`, `hungry`, and `energy`.

It also has a private method `meow()`.

It can call it whenever it wants, the other classes can't tell the cat when to meow.

What they can do is defined in the **public methods** `sleep()`, `play()`, and `feed()`.

Each of them modifies the internal state somehow and may invoke `meow()`.

Thus, the binding btwn the private state and public methods is made.

This is encapsulation.

## Abstraction

Abstraction can be thought of as a natural extension of encapsulation.

In object-oriented design, programs are often extremely large. And separate objects communicate w/ each other a lot.

So maintaining a large codebase like this for years - w/ changes along the way - is difficult.

Abstraction is a concept aiming to ease this problem.

Applying abstraction means that each object should **only** expose a high-level mechanism for using it.

This mechanism should hide internal implementation details.

It should only reveal operations relevant for the other objects.

Think - a coffee machine. It does a lot of stuff and makes quirky noises under the hood. But all you have to do is put in coffee and press a button.

Preferably, this mechanism should be easy to use and should rarely change over time.

Think of it as a small set of public methods which any other class can call w/o "knowing" how they work.

Another real-life example of abstraction?

Think about how you use your phone:

![Cell phones are complex. But using them is simple.](../img/abstraction.png)

You interact w/ your phone by using only a few buttons.

What's going on under the hood?

You don't have to know - implementation details are hidden.

You only need to know a short set of actions.

Implementation changes - for example, a software update -rarely affect the abstraction you use.

## Inheritance

So we've seen how encapsulation and abstraction can help us develop and maintain a big codebase

Objects are often very similar. They share common logic. But they're not **entirely** the same.

So how do we reuse common logic and extract the unique logic into a separate class? One way to achieve this is inheritance.

It means that you create a (child) class by deriving from another (parent) class. This way, we form a hierarchy.

The child class reuses all fields and methods of the parent class (common part) and can implement its own (unique part).

For example:

![A private teacher is a type of teacher. And any teacher is a type of Person.](../img/inheritance.png)

If our program needs to manage public and private teachers, but also other types of people like students, we can implement this class hierarchy.

This way, each class adds only what is necessary for it while reusing common logic with the parent classes.

## Polymorphism

We’re down to the most complex word! Polymorphism means “many shapes” in Greek.

So we already know the power of inheritance and happily use it. But there comes this problem.

Say we have a parent class and a few child classes which inherit from it.

Sometimes we want to use a collection — for example a list — which contains a mix of all these classes.

Or we have a method implemented for the parent class — but we’d like to use it for the children, too.

This can be solved by using polymorphism.

Simply put, polymorphism gives a way to use a class exactly like its parent so there’s no confusion with mixing types.

But each child class keeps its own methods as they are.

This typically happens by defining a (parent) interface to be reused. It outlines a bunch of common methods. Then, each child class implements its own version of these methods.

Any time a collection (such as a list) or a method expects an instance of the parent (where common methods are outlined), the language takes care of evaluating the right implementation of the common method — regardless of which child is passed.

Take a look at a sketch of geometric figures implementation. They reuse a common interface for calculating surface area and perimeter:

![Triangle, Circle, and Rectangle now can be used in the same collection](../img/polymorphism.png)

Having these three figures inheriting the parent `Figure Interface` lets you create a list of mixed `triangles`, `circles`, and `rectangles`. And treat them like the same type of object.

Then, if this list attempts to calculate the surface for an element, the correct method is found and executed. If the element is a triangle, triangle’s `CalculateSurface()` is called. If it’s a circle — then circle’s `CalculateSurface()` is called. And so on.

If you have a function which operates with a figure by using its parameter, you don’t have to define it three times — once for a triangle, a circle, and a rectangle.

You can define it once and accept a Figure as an argument. Whether you pass a `triangle`, circle or a rectangle — as long as they implement `CalculateParamter()`, their type doesn’t matter.
