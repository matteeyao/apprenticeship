# Sharing Role Behavior w/ Modules

A Ruby **module** is like a class, except you don't instantiate a module. 

Modules consist of methods that can be *mixed* in to a Ruby class. 

In Ruby, we use a module to collect methods that may be mixed in and shared by many classes to keep our code DRY.

A Ruby modules uses the techniques of inheritance to share a *role*.

Let's see an example:

```rb
module Greetable
    def greet
        "Hello, my name is #{self.name}"
    end
end

class Human
    include Greetable
    
    def initialize(name)
        @name = name
    end
    
    def name
        @name
    end
end

class Robot
    include Greetable
    
    def name
        "Robot Model #2000"
    end
end
```

We "mix in" a module by using the `#include` method. This will take the methods defined in the module and make them available to instances of `Robot` and `Human`.

Note that module methods may call methods of the class that they are mixed into. In this case, the `Greetable` module needs to access a `name` method. Both `Robot` and `Human` have `name` methods.

A hallmark module is `Enumerable`. All the various methods of `Enumerable` are defined in terms of an `each` method, which the class (be it `Array`, `Hash`, etc.) must define.

Modules, or "power packs", extend the abilities of a class.

## Include versus extend

It is common to mix in a module to add instance methods to a class; we’ve use `include` to do this.

You can also use the `Class#extend` method to mix in module methods *as class methods*. Here’s an example:

```rb
module Findable
    def objects
        @objects ||= {}
    end

    def find(id)
        objects[id] = object
    end

    def track(id, object)
        objects[id] = object
    end
end

class Cat
    extend Findable

    def initialize(name)
        @name = name
        Cat.track(@name, self)
    end
end

Cat.new("Gizmo")
Cat.find("Gizmo") # finds Gizmo Cat object
```

## Mixins versus Multiple Inheritance

Ruby doesn't support multiple inheritance: a class can only have one parent class.

Only a few languages do support multiple inheritance; you can read about the "Diamond problem" if you want to learn why.

Ruby's answer to multiple inheritance is the ability to mix in modules. If two classes should share methods, but it is not feasible for them to share a base class, we can instead factor the common methods out into a module and `include` this in both the classes.

Again, the prototypical example is the `Enumerable` module:

```rb
module Enumerable
    def map(&prc)
        results = []

        # notice how we need `each` to write `map`
        self.each { |el| results << prc.call(el) }

        results
    end

    ...
end

class Array < Object
    include Enumerable
    ...
end

class Hash < Object
    include Enumerable
    ...
end
```

Now all of the methods in the `Enumerable` module (e.g., `map`) are mixed in to `Array` and `Hash`.

## Namespaces

Modules have a second, unrelated purpose: as **namespaces**.

Namespaces prevent name collisions.

Say you have a method `make_bacon` in file `A.rb`. Later, you decide to define a method `make_bacon` in file `B.rb` If you're writing a program that `requires` both files, one `make_bacon` definition is going to overwrite the other and you'll be in trouble.

This is where modules come in; if you wrap the code in `A.rb` and `B.rb` in modules, you won't have difficulty. This is how `A.rb` looks:

```rb
module A
    def self.make_bacon
        ...
    end
end
```

`B.rb` looks like this:

```rb
module B
    def self.make_bacon
        ...
    end
end
```

Let's use `A` and `B` in a program to make some bacon.

```rb
require "A"
require "B"

a_grade_bacon = A.make_bacon
b_grade_bacon = B.make_bacon
```

Two different kinds of bacon!

It doesn't normally make sense to put your application code inside a module, but if you want to make your code widely available as a gem, you would want to wrap it in a module so as to minimize potential conflicts.

## Understanding Roles

Some problems require sharing behavior among otherwise unrelated objects.

This common behavior is orthogonal to class; it's a *role* an object plays. 

Many of the roles needed by an application will be obvious at design time, but it's also common to discover unanticipated roles as you write the code.

WHen formerly unrelated objects begin to play a common role, they enter into a relationship w/ the objects for whom they play the role.

Using a role creates dependencies among the objects involved and these dependencies introduce risks that you must take into account when deciding among design options.

## Finding Roles

The `Preparer` duck type is a role. Objects that implement `Preparer`'s interface play this role. `Mechanic`, `TripCoordinator`, and `Driver` each implement `prepare_trip`; therefore, other objects can interact w/ them as if they are `Preparers` w/o concern for their underlying class.

The existence of a `Preparer` role suggests that there's also a parallel `Preparable` role (these things often come in pairs).

Although the `Preparer` role has multiple players, it is so simple that it is entirely defined by its interface.

To play this role, all an object need do is implement its own personal version of `prepare_trip`.

Objects that act as `Prepares` have only this interface in common.

They share the method signature but no other code.

`Preparer` and `Preparable` are perfectly legitimate duck types.

It's far more common to discover more sophisticated roles, ones where the role requires not only specific message signatures but also specific behavior.

When a role needs shared behavior, you're faced w/ the problem of organizing the shared code.

Many object-oriented languages provide a way to define a named group of methods that are independent of class and can be mixed in to any object.

In Ruby, these mix-ins are called *modules*.

Methods can be defined in a module and then the module can be added to any object.

Modules thus provide a perfect way to allow objects of different classes to play a common role using a single set of code.

When an object includes a module, the methods defined therein become available via automatic delegation.

The total set of messages to which an object can respond includes:

* Those it implements

* Those implemented in all objects above it in the hierarchy

* Those implemented in any module that has been added to it

* Those implemented in all modules added to any object above it in the hierarchy

## Organizing Responsibilities

Consider the problem of scheduling a trip.

Trips occur at specific points in time and involve bicycles, mechanics, and motor vehicles.

FastFeet needs a way to arrange all of these objects on a schedule so that it can determine, for any point in time, which objects are available and which are already committed.

The requirements are that bicycles have a minimum of one day btwn trips, vehicles a minimum of three days, and mechanics, four days.

Assume that a `Schedule` class exists. Its interface already includes these three methods:

```rb
class Schedule
    def scheduled?(schedulable, starting, ending)
        # ...
    end

    def add(target, starting, ending)
        # ...
    end

    def remove(target, starting, ending)
        # ...
    end
end
```

Each of the above methods takes three arguments: the target object and the start and end dates for the period of interest.

The `Schedule` is responsible for knowing it its incoming `target` argument is already scheduled and for adding and removing `targets` from the schedule.

These responsibilities rightly belong here in the `Schedule` itself.

It is true that knowing if an object is scheduled during some time interval is all the information needed to prevent over-scheduling an already busy object.

However, knowing that a object is *not* scheduled during an interval isn't enough information to know if it *can* be scheduled during that same interval.

To properly determine if an object can be scheduled, some object, somewhere, must take lead time into account.

## Removing Unnecessary Dependencies

The `Schedule` expects its target to behave like something that understands `lead_days`, that is, like something that is "schedulable", a duck type.

`Schedulables` must implement `lead_days` but currently have no other code in common.

Discovering and using this duck type improves the code by removing the `Schedule`'s dependency on specific class names, which makes the application more flexible and easier to maintain.

Objects should manage themselves; they should contain their own behavior.

Just as strings respond to `empty?` and can speak for themselves, targets should respond to `schedulable?`.

The `schedulable?` method should be added to the interface of the `Schedulable` role.

## Writing the Concrete Code

As it currently stands, the `Schedulable` role contains only an interface.

Adding the `schedulable?` method to this role requires writing some code and it's not immediately obvious where this code should reside.

You are faced w/ two decisions: You must decide what the code should do and where the code should live.

The simplest way to get started is to separate the two decisions.

Pick an arbitrary concrete class (for example, Bicycle) and implement the `schedulable?` method directly in that class.

Once you have a version that works for Bicycle, you can refactor your way to a code arrangement that allows all `Schedulables` to share the behavior.

`Bicycle` now responds to messages about its own "schedulability."

Before this change, every instigating object had to know about and thus had a dependency on the `Schedule`.

This change allows bicycles to speak for themselves, freeing instigating objects to interact w/ them w/o the aid of a third party.

```rb
class Schedule
    def scheduled?(schedulable, starting, ending)
        puts "This #{schedulable.class} is " + "available #{starting} - #{ending}"
        false
    end
end
```

This next example shows `Bicycle`'s implementation of `schedulable?`.

`Bicycle` knows its own scheduling lead time, and delegates `scheduled?` to the `Schedule` itself.

```rb
class Bicycle
    attr_reader :schedule, :size, :chain, :tire_size

    # Inject the Schedule and provide a default
    def initialize(**opts)
        @schedule = opts[:schedule] || Schedule.new
        # ...
    end

    # Return true if this bicycle is available during 
    # this (now Bicycle specific) interval.
    def schedulable?(starting, ending)
        !scheduled?(starting - lead_days, ending)
    end

    # Return the number of lead_days before a bicycle can be scheduled
    def lead_days
        1
    end
    # ...
end

require "date"
starting = Date.parse("2019/09/04")
ending = Date.parse("2019/09/10")

b = Bicycle.new
puts b.schedulable?(starting, ending)
# → This Bicycle is available 2019-09-03 - 2019-09-10
# → true
```

Running the code confirms that `Bicycle` has correctly adjusted the starting date to include the bicycle-specific lead days.

This code hides knowledge of who the `Schedule` is and what the `Schedule` does inside of `Bicycle`.

Objects holding onto a `Bicycle` no longer need know about the existence or behavior of the `Schedule`.

## Extracting the Abstraction

The code above solves the first part of current problem in that it decides what the `schedulable?` method should do, but `Bicycle` is not the only kind of thing that is "schedulable." 

`Mechanic` and `Vehicle` also play this role and therefore need this behavior.

It's time to rearrange the code so that it can be shared among objects of different classes.

The following example shows a new `Schedulable` module, which contains an abstraction extracted from the `Bicycle` class above.

The `schedulable?` and `scheduled?` methods are exact copies of the ones formerly implemented in `Bicycle`.

```rb
module Schedulable
    attr_writer :schedule

    def schedule
        @schedule ||= Schedule.new
    end

    def schedulable?(starting, ending)
        !scheduled?(starting - lead_days, ending)
    end

    def scheduled?(starting, ending)
        schedule.scheduled?(self, starting, ending)
    end

    # includers may override
    def lead_days
        0
    end
end
```

Two things have changed from the code as it previously existed in `Bicycle`.

First, a `schedule` method has been added. This method returns an instance of the overall `Schedule`.

In the code above, the dependency on `Schedule` has been removed from `Bicycle` and moved into the `Schedulable` module.

The second change is to the `lead_days` method. `Bicycle`'s former implementation returned a bicycle-specific number; the module's implementation now returns a more generic default of zero days.

Even if there were no reasonable application default for lead days, the `Schedulable` module must still implement the `lead_days` method.

The rules for modules are the same as for classical inheritance.

If a module sends a message, it must provide an implementation, even if that implementation raises an error indicating that users of the module must implement the method.

```rb
class Bicycle
    include Schedulable

    def lead_days
        1
    end
    # ...
end

require "date"
starting = Date.parse("2019/09/04")
ending   = Date.parse("2019/09/10")

b = Bicycle.new
puts b.schedulable?(starting, ending)
# → This Bicycle is available 2019-09-03 - 2019-09-10
# → true
```

The `lead_days` method is a hook that follows the template method pattern.

`Bicycle` overrides this hook to provide a specialization.

The pattern of messages has changed from that of sending `schedulable?` to a `Bicycle` to sending `schedulable?` to a `Schedulable`.

Once you include this module in all of the classes that can be scheduled, the pattern of code becomes strongly reminiscent of inheritance.

The following example shows `Vehicle` and `Mechanic` including the `Schedulable` module and responding to the `schedulable?` message.

```rb
class Vehicle
    include Schedulable

    def lead_days
        3
    end
    # ...
end

class Mechanic
    include Schedulable

    def lead_days
        4
    end
    # ...
end

v = Vehicle.new
puts v.schedulable?(starting, ending)
# → This Vehicle is available 2019-09-01 - 2019-09-10
# → true

m = Mechanic.new
puts m.schedulable?(starting, ending)
# → This Mechanic is available 2019-08-31 - 2019-09-10
# → true
```

The code in `Schedulable` is the abstraction and it uses the template method pattern to invite objects to provide specializations to the algorithm it supplies.

`Schedulable` override `lead_days` to supply those specializations.

When `schedulable?` arrives at any `Schedulable`, the message is automatically delegated to the method defined in the module.

## Looking Up Methods

When a single class includes several different modules, the modules are placed in the method lookup path in *reverse* order of module inclusion.

Thus, the methods of the last included module are encountered first in the lookup path.

It is also possible to add a module's methods to a single object, using Ruby's `extend` keyword.

B/c `extend` adds the module's behavior directly to an object, extending a class w/ a module creates class methods *in that class* and extending an instance of a class w/ a module creates instance methods *in that instance*.

These two things are exactly the same; classes are, after all, just plain old objects, and `extend` behaves the same for all.

## Recognize the Anti-patterns

There are two anti-patterns that indicate that your code might benefit from inheritance.

First, an object that uses a variable w/ a name like `type` or `category` to determine what message to send to `self` contains two highly related by slightly different types.

Code like this can be rearranged to use classical inheritance by putting the common code in an abstract superclass and creating subclasses for the different types. This rearrangement allows you ti create new subtypes by adding new subclasses.

These subclasses extend the hierarchy w/o changing the exiting code.

Second, when a sending object checks the class of a receiving object to determine what message to send, you have overlooked a duck type.

In this situation, all of the possible receiving objects play a common role. This role should be codified as a duck type, and receivers should implement the duck type's interface.

Once they do, the original object can send one single message to every receiver, confident that b/c each receiver plays the role, it will understand the common message.

In addition to sharing an interface, duck types might also share behavior. When they do, place the shared code in a module and include that module in each class or object that plays the role.

## Insist on the Abstraction

All of the code in an abstract superclass should apply to every class that inherits it.

Superclasses should not contain code that applies to some, but not all, subclasses.

This restriction also applies to modules: The code in a module must apply to all who use it.

If you cannot correctly identify the abstraction, there may not be one, and if no common abstraction exists, then inheritance is not the solution to your design problem.

## Honor the Contract

Subclasses agree to a *contract*; they promise to be substitutable for their superclasses.

Substitutability is possible only when objects behave as expected, and subclasses are *expected* to conform to their superclass's interface.

They must respond to every message in that interface, taking the same kinds of inputs and returning the same kinds of outputs.

They are not permitted to do anything that forces others to check their type in order to know how to treat them or what to expect of them.

Where superclasses place restrictions on input arguments and return values, subclasses can indulge in a slight bit of freedom w/o violating their contract.

Subclasses may accept input parameters that have broader restrictions and may return results that have narrower restrictions, all while remaining perfectly substitutable for their superclasses.

When you honor the contract, you are following the Liskov Substitution Principle, which supplies the "L" in the SOLID design principles.

Her principle states:

* Let *q(x)* be a property provable about objects *x* of type *T*. Then *q(y)* should be true for objects *y* of type *S* where *S* is a subtype of *T*.

Following this principle creates applications where a subclass can be used anywhere its superclass would be and where objects that include modules can be trusted to interchangeably play the module's role.

## Preemptively Decouple Classes

Avoid writing code that requires its inheritors to send `super`; instead use hook messages to allow subclasses to participate while absolving them of responsibility for knowing the abstract algorithm.

Hook methods solve the problem of sending `super`, but, unfortunately, only for adjacent levels of the hierarchy.

For example, `Bicycle` sent hook method `local_spares` that `MountainBike` overrode to provide specializations.

This hook method serves its purpose admirably, but the original problem reoccurs if you add another level to the hierarchy by creating subclass `MonsterMountainBike` under `MountainBike`.

In order to combine its own spare parts w/ those of its parent, `MonsterMountainBike` would be forced to override `local_spares`, and within it, send `super`.

## Summary

When objects that play a common role need to share behavior, they do so via a Ruby module.

The code defined in a module can be added to any object, be it an instance of a class, a class itself, or another module.

Modules should use the template method pattern to invite those that `include` them to supply specializations and should implement hook methods to avoid forcing `includer`s to send `super` (and thus know the algorithm).
