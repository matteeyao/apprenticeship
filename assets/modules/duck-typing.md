# Reducing Costs w/ Duck Typing

*Duck typing* is a technique that combines messages and a rigorously defined public interface into a powerful design technique that further reduces cost of change.

Duck types are public interfaces that are not tied to any specific class.

These across-class interfaces add enormous flexibility to the application by replacing costly dependencies on class w/ more forgiving dependencies on messages.

Duck typed objects are chameleons that are defined more by their behavior than by their class.

This is how the technique gets its name; if an object quacks like a duck and walks like a duck, then its class is immaterial-it's a duck.

## 5.1 Understanding Duck Typing

If one object knows another type, it know to which messages that object can respond.

A Ruby object can expose a different face to every viewer; it can implement many different interfaces.

Users of an object need not, and should not, be concerned about its class.

Class is just one way for an object to acquire a public interface; the public interface an object obtains by way of its class may be one of several that it contains.

Applications may define many public interfaces that are not related to one specific class; these interfaces cut across class.

Users of any object can blithely expect it to act like any, or all, of the public interfaces it implements.

It's not what an object *is* that matters; it's what it *does*.

Across-class types, duck types, have public interfaces that represent a contract that must be explicit and well-documented.

### 5.1.2 Compounding the Problem

If your design imagination is constrained by class and you find yourself unexpectedly dealing w/ objects that don't understand the message you are sending, your tendency is to go hunt for messages that these new objects *do* understand.

Sequence diagrams should always be simpler than the code they represent; when they are not, something is wrong w/ the design.

### 5.1.3 Finding the Duck

The key to removing the dependencies is to recognize that b/c `Trip`'s `prepare` method serves a single purpose, its arguments arrive wishing to collaborate to accomplish a single goal.

Every argument is here for the same reason, and that reason is unrelated to the argument's underlying class.

Avoid getting sidetracked by your knowledge of what each argument's class already does; think instead about what `prepare` needs.

Consider from `prepare`'s point of view, the problem is straightforward.

The `prepare` method wants to prepare the trip.

Its arguments arrive ready to collaborate in trip preparation.

The design would be simpler if `prepare` just trusted them to do so.

This expectation neatly turns the tables.

You've pried yourself loose from existing classes and invented a duck type.

The next step is to ask what message the `prepare` method can fruitfully send each `Preparer`.

From this point of view, the answer is obvious: `prepare_trip`.

Trip's `prepare` method now expects its arguments to be `Preparers` that can respond to `prepare_trip`.

What kind of thing is `Preparer`? At this point it has no concrete existence; it's an abstraction, an agreement about the public interface on an idea. It's a figment of design.

Objects that implement `prepare_trip` *are* `Preparers` and, conversely, objects that interact w/ `Preparers` only need trust them to implement the `Preparer` interface.

Once you see this underlying abstraction, it's easy to fix the code. `Mechanic`, `TripCoordinator`, and `Driver` should behave like `Preparers`; they should implement `prepare_trip`.

Here's the code for the new design. The `prepare` method now expects its arguments to be `Preparers`, and each argument's class implements the new interface.

```rb
# Trip preparation becomes easier
class Trip
    attr_reader :bicycles, :customers, :vehicle

    def prepare(preparers)
        preparers.each { |preparer|
            preparer.prepare_trip(self)}
    end
    # ...
end

# when every preparer is a Duck
# that responds to `prepare_trip`
class Mechanic
    def prepare_trip(trip)
        trip.bicycles.each { |bicycle|
            prepare_bicycle(bicycle)}
    end
    # ...
end

class Mechanic
    def prepare_trip(trip)
        buy_food(trip.customers)
    end
    # ...
end

class Driver
    def prepare_trip(trip)
        vehicle = trip.vehicle
        gas_up(vehicle)
        fill_water_tank(vehicle)
    end
    # ...
end
```

The `prepare` method can now accept new `Preparers` w/o being forced to change, and it's easy to create additional `Preparers` if the need arises.

### 5.1.4 Consequences of Duck Typing

In the initial example,, the `prepare` method depends on a concrete class. In this most recent example, `prepare` depends on a duck type.

The path between these examples leads through a ticket of complicated, dependent-laden code.

The concreteness of the first example makes it simple to understand but dangerous to extend. The final, duck typed, alternative is more abstract; it places slightly greater demands on your understanding but in return offers ease of extension.

Now that you have discovered the duck, you can elicit new behavior from your application w/o changing any existing code; you simply turn another object into a `Preparer` and pass it into `Trip`'s `prepare` method.

Use of a duk type moves your code along the scale from more concrete to more abstract, making the code easier to extend but casting a veil over the underlying class of the duck.

Once you begin to treat your objects as if they are defined by their behavior rather than by their class, you enter into a new realm of expressive flexible design.

Polymorphism in OOP refers to the ability of many different objects to respond to the same message. Senders of the message need not care about the class of the receiver; receivers supply their own specific version of the behavior.

There are a number of ways to achieve polymorphism; duck typing, as you have surely guessed, is one. Inheritance and behavior sharing (via Ruby modules) are others.

Polymorphic methods honor an implicit bargain; they agree to be interchangeable *from the sender's point of view*.

Any object implementing a polymorphic method can be substituted for any other; the sender of the message need not know or care about his substitution.

## 5.2 Writing Code That Relies on Ducks

Using duck typing relies on your ability to recognize the places where your application would benefit from across-class interfaces.

It is relatively easy to implement a duck type; your design challenge is to notice that you need one and to abstract its interface.

### 5.2.1 Recognizing Hidden Ducks

Many times, unacknowledged duck types already exist, lurking within existing code. Several common coding patterns indicate the presence of a hidden duck. You can replace the following w/ ducks:

* Case statements that switch on class

* `kind_of?` and `is_a?`

* `responds_to?`

**Case Statements That Switch on Class**

A case statement that switches on the class names of domain objects of your application.

```rb
class Trip
    def prepare(preparers)
        preparers.each { |preparer|
            case preparer
            when Mechanic
                preparer.prepare_bicycles(bicycles)
            when TripCoordinator
                preparer.buy_food(customers)
            when Driver
                preparer.gas_up(vehicle)
                preparer.fill_water_tank(vehicle)
            end
        }
    end
    # ...
end
```

"What is it that `prepare` wants from each of its arguments?"

The answer to that question suggests the message you should send; this message begins to define the underlying duck type.

Here the `prepare` method wants its arguments to prepare the trip. Thus, `prepare_trip` becomes a method in the public interface of the new `Preparer` duck.

**`kind_of?` and `is_a?`**

There are various ways to check the class of an object. The case statement above is one of them. The `kind_of?` and `is_a?` messages (they are synonymous) also check class.

Rewriting the previous example in the following way does nothing to improve the code.

```rb
if preparer.kind_of?(Mechanic)
    preparer.prepare_bicycles(bicycles)
elsif preparer.kind_of?(TripCoordinator)
    preparer.buy_food(customers)
elsif preparer.kind_of?(Driver)
    preparer.gas_up(vehicle)
    preparer.fill_water_tank(vehicle)
end
```

Using `kind_of?` is no different than using a case statement that switches on class; they are the same thing, they cause exactly the same problems, and they should be corrected using the same techniques.

**`responds_to?`**

```rb
if preparer.respond_to?(:prepare_bicycles)
    preparer.prepare_bicycles(bicycles)
elsif preparer.respond_to?(:buy_food)
    preparer.buy_food(customers)
elsif preparer.respond_to?(:gas_up)
    preparer.gas_up(vehicle)
    preparer.fill_water_tank(vehicle)
end
```

Don't be fooled by the removal of explicit class references. This example still expects very specific classes.

The code pattern still contains unnecessary dependencies; it controls rather than trusts other objects.

### 5.2.2 Placing Trust in Your Ducks

Use of `kind_of?`, `is_a?`, `responds_to?`, and case statements that switch on your classes indicate the presence of an unidentified duck.

Just as in Demeter violations, this style of code is an indication that you are missing an object, one whose public interface you have not yet discovered.

The fact that the missing object is a duck type instead of a concrete class matters not at all; it's the interface that matters, not the class of the object that implements it.

Flexible applications are built on objects that operate on trust; it is your job to make your objects trustworthy. 

When you see these code patterns, concentrate on the offending code's expectations to find the duck type.

Once you have a duck type in mind, define its interface, implement that interface where necessary, and then trust those implementers to behave correctly.

### 5.2.3 Documenting Duck Types

The simplest kind of duck type is one that exists merely as an agreement about its public interface.

This chapter's example code implements that kind of duck, where several different classes implement `prepare_trip` and can thus be treated like `Preparers`.

The `Preparer` duck type and its public interface are a concrete part of the design but a virtual part of the code. `Preparers` are abstract; this gives them strength as a design tool, but this very abstraction makes the duck type less than obvious in the code.

When you create duck types, you must both document *and* test their public interfaces.

### 5.2.4 Sharing Code between Ducks

In this chapter, `Preparer` ducks provide class-specific versions of the behavior required by their interface.

`Mechanic`, `Driver`, and `TripCoordinator` each implement the method `prepare_trip`. This method signature is the only thing they have in common. They share only the interface, not the implementation.

Once you start using duck types, however, you'll find that classes that implement them often need to share behavior.

### 5.2.5 Choosing Your Ducks Wisely

The example below patently uses class to decide how to deal w/ its input, a technique that is in direct opposition to the guidelines stated above.

The `find_with_ids` method below clearly decides how to behave based on the class of its `id`s argument.

If sending a message based on the class of the receiving object is the death knell for your application, why is this code acceptable?

```rb
def find_with_ids(*ids)
    raise UnknownPrimaryKey.new(@klass) if primary_key.nil?

    expects_array = ids.first.kind_of?(Array)
    return ids.first if expects_array && ids.first.empty?

    ids = ids.flatten.compact.uniq

    case ids.size
    when 0
        raise RecordNotFound, "Couldn't find #{@klass.name} without an ID"
    when 1
        result = find_one(ids.first)
        expects_array ? [ result ] : result
    else
        find_some(ids)
    end

    rescue ::RangeError
        raise RecordNotFound, "Couldn't find #{@klass.name} with an out of range ID"
end
```

The major difference between this example and the previous one is the stability of the classes that are being checked.

When `find_with_ids` depends on `Array` and `NilClass` (the `nil?` message is a type check), it is depending on core Ruby classes that are far more stable than it is.

The likelihood of `Array` or `NilClass` changing in such a way as to force `find_with_ids` to change is vanishingly small. The dependency is safe. 

There probably *is* a duck type hidden somewhere in this code, but it will likely not reduce your overall application costs to find and implement it.

From this example, you can see that the decision to create a new duck type relies on judgement.

The purpose of design is to lower costs; bring this measuring stick to every situation.

If creating a duck type would reduce unstable dependencies, do so. Use your best judgement.

The above example's underlying duck spans `Array` and `NilClass`, and therefore its implementation would require making changes to Ruby base classes. 

Changing base classes is known as *monkey patching* and is a delightful feature of Ruby but can be perilous in untutored hands.

## 5.3 Conquering a Fear of Duck Typing

### 5.3.1 Subverting Duck Types w/ Static Typing

Methods that cannot behave correctly unless they know the classes of their arguments will fail (w/ type errors) when new classes appear.

Programmers who believe in static typing take these failures as proof that more type checking is needed.

When more checks are added, the code becomes less flexible and even more dependent on class.

The new dependencies cause additional type failures, adn the programmer responds to these failures by adding yet more type checking.

Duck typing provides a way out of this trap. It removes the dependencies on class and thus avoids the subsequent type failures. It reveals stable abstractions on which your code can safely depend.

### 5.3.2 Static vs Dynamic Typing

Static and dynamic typing both make promises, and each has costs and benefits.

Static typing aficionados cite the following qualities:

* The compiler unearths type errors at compile time.

* Visible type information serves as documentation.

* Compiled code is optimized to run quickly.

These qualities represent strengths in a programming language only if you accept this set of corresponding assumptions:

* Runtime type errors will occur unless the compiler performs type checks.

* Programmers will not otherwise understand the code; they cannot infer an object's type from its context.

* The application will run too slowly w/o these optimizations.

Dynamic typing proponents list these qualities:

* Code is interpreted and can be dynamically loaded; there is no compile/make cycle.

* Source code does not include explicit information.

* Meta-programming is easier.

These qualities are strengths if you accept this set of assumptions:

* Overall application development is faster w/o a compile/make cycle.

* Programmers find the code easier to understand when it does not contain type declarations; they can infer an object's type from its context.

* Meta-programming is a desirable language feature.

### 5.3.3  Embracing Dynamic Typing

Ease of metaprogramming is a strong benefit of dynamic typing.

The two remaining qualities are static typing's compile-time type checking and dynamic typing's lack of a compile/make cycle.

Static typing advocates assert that preventing unexpected type errors at runtime is so necessary and so valuable that its benefit trumps the greater programming efficiency that is gained by removing the compiler.

This argument rests on static typing's premise that:

* The compiler truly *can* save you from accidental type errors.

* W/o the compiler's help, these type errors *will* occur.

The compiler cannot *guarantee* safety from accidental type errors.

Any language that allows casting a variable to a new type is vulnerable.

Once you start casting, all bets are off; the compiler excuses itself, and you are left to rely on your own wits to prevent type errors.

The code is only as good as your tests; runtime failures can still occur.

The notion that static typing provides perfect safety, comforting though it may be, is an illusion.

Also, in well-designed object-oriented code, runtime type errors *almost never occur*.

This is not to suggest that you'll never experience a runtime type error.

Discovering at runtime that `nil` doesn't understand the message it received is not something the compiler could have prevented.

Duck typing is built on dynamic typing; to reap the full benefit from duck typing; you must *embrace* this dynamism.

## 5.4 Summary

Messages are at the center of object-oriented applications, and they pass among objects along public interfaces.

Duck typing detaches these public interfaces from specific classes, creating virtual types that are defined by what they do instead of by who they are.

Duck typing reveals underlying abstractions that might otherwise be invisible.

Depending on these abstractions reduces risk and increases flexibility, making your application cheaper to maintain and easier to change.
