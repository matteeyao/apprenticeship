# Combining Objects w/ Composition

In composition, the larger object is connected to its parts via a *has-a* relationship.

Inherent in the definition of composition is the idea that not only does a bicycle have parts, but it communicates w/ them via an interface.

Part is a *role*, and bicycles are happy to collaborate w/ any object that plays the role.

## Updating the Bicycle Class

The `Bicycle` class is responsible for responding to the `spares` message.

This `spares` message should return a list of spare parts.

If you created an object to hold all of a bicycle's parts, you could delegate the `spares` message to that new object.

Every `Bicycle` needs a `Parts` object; part of what it means to be a `Bicycle` is to *have-a* `Parts`.

In Figure 8.2, the diagram shows the `Bicycle` and `Parts` classes connected by a line. The line attaches to `Bicycle` w/ a black diamond; this black diamond indicates *composition*, it means that a `Bicycle` is composed of `Parts`. The `Parts` side of the line has the number "1." This means there's just one `Parts` object per `Bicycle`.

```rb
class Bicycle
    attr_reader :size, :parts

    def initialize(size:, parts:)
        @size   = size
        @parts  = parts
    end

    def spares
        parts.spares
    end
end
```

Now that `Bicycle` is no longer an abstract superclass which takes many possible arguments, change its initialize method to specify the exact keywords it accepts.

`Bicycle` is now responsible for three things: knowing its `size`, holding onto its `Parts`, and answering its `spares`.

## Creating a Parts Hierarchy

```rb
class Parts
    attr_reader :chain, :tire_size

    def initialize(**opts)
        @chain      = opts[:chain]      || default_chain
        @tire_size  = opts[:tire_size]  || default_tire_size
        post_initialize(opts)
    end

    def spares
        { chain:     chain,
          tire_size: tire_size }.merge(local_spares)
    end

    def default_tire_size
        raise NotImplementedError
    end

    # subclasses may override
    def post_initialize(opts)
        nil
    end

    def local_spares
        {}
    end

    def default_chain
        "11-speed"
    end
end

class RoadBikeParts < Parts
    attr_reader :tape_color

    def post_initialize(**opts)
        @tape_color = opts[:tape_color]
    end

    def local_spares
        { tape_color: tape_color }
    end

    def default_tire_size
        "23"
    end
end

class MountainBikeParts < Parts
    attr_reader :front_shock, :rear_shock

    def post_initialize(**opts)
        @front_shock = opts[:front_shock]
        @rear_shock  = opts[:rear_shock]
    end

    def local_spares
        { front_shock: front_shock }
    end

    def default_tire_size
        "2.1"
    end
end
```

This code is a near exact copy of the `Bicycle` hierarchy; the differences are that the classes have been renamed and the `size` variable has been removed.

After this refactoring, everything still works. As you can see below, regardless of whether it has `RoadBikeParts` or `MountainBikeParts`, a bicycle can still correctly answer its `size` and `spares`.

```rb
road_bike = Bicycle.new(
    size: "L",
    parts: RoadBikeParts.new(tape_color: "red"))

puts road_bike.size # → L

puts road_bike.spares # → {:chain=>"11-speed", :tire_size=>"23", :tape_color=>"red"}

mountain_bike = Bicycle.new(
    size: "L"
    parts: MountainBikeParts.new(
        front_shock: "Manitou",
        rear_shock: "Fox")
)

puts mountain_bike.size # → L
puts mountain_bike.spares # → {:chain=>"11-speed", :tire_size=>"2.1", :front_shock=>"Manitou"}
```

## Creating a Part

The `Parts` object is now composed of `Part` objects.

As illustrated by the class diagram in Figure 8.5, the `1..*` on the line near `Part` indicates that a `Parts` will have one or more `Part` objects.

Introducing this new `Part` class simplifies the existing `Parts` class, which now becomes a simple wrapper around an array of `Part` objects.

`Parts` can filter its list of `Part` objects and return the ones that need spares.

The code below shows three classes: the existing `Bicycle` class, the updated `Parts` class, and the newly introduced `Part` class.

```rb
class Bicycle
    attr_reader :size, :parts

    def initialize(size:, parts:)
        @size   = size
        @parts  = parts
    end

    def spares
        parts.spares
    end
end

class Parts
    attr_reader :parts

    def initialize(parts)
        @parts = parts
    end

    def spares
        parts.select { |part| part.needs_spare }
    end
end

class Part
    attr_reader :name, :description, :needs_spare

    def initialize(name:, description:, needs_spare: true)
        @name           = name
        @description    = description
        @needs_spare    = needs_spare
    end
end
```

Now that these three classes exist, you can create individual `Part` objects.

The following code creates a number of different parts and saves each in an instance variable.

```rb
chain = Part.new(name: "chain", description: "11-speed")

road_tire = Part.new(name: "tire_size", description: "23")

tape = Part.new(name: "tape_color", description: "red")

mountain_tire = Part.new(name: "tire_size", description: "2.1")

rear_shock = Part.new(name: "rear_shock", description: "Fox", needs_spare: false)

front_shock = Part.new(name: "front_shock", description: "Manitou")
```

Individual `Part` objects can be grouped together into a `Parts`. The code below combines the road bike `Part` objects into a road bike-suitable `Parts`.

```rb
road_bike_parts = Parts.new([chain, road_tire, tape])

road_bike = Bicycle.new(
    size:  "L"
    parts: road_bike_parts)
# road_bike = Bicycle.new(
#     size:  "L"
#     parts: Parts.new([chain, road_tire, tape]))

puts road_bike.size # → L

puts road_bike.spares.inspect

mountain_bike = Bicycle.new(
    size: "L"
    parts: Parts.new([chain, mountain_tire, front_shock, rear_shock]))

puts mountain_bike.size # → L

puts mountain_bike.spares.inspect
```

While it may be tempting to think of these objects as instances of `Part`, composition tells you to think of them as objects that play the `Part` role.

They don't have to be a *kind-of* the `Part` class, they just have to act like one; that is, they must respond to `name`, `description`, and `needs_spare`.

## Making the Parts Object more like an Array

The `parts` and `spares` methods of `Bicycle` ought to return the same sort of thing, yet the objects that come back don't behave in the same way.

The `Parts` object does *not* behave like an array; and all attempts to treat it as one will fail.

You can fix the proximate problem by adding a `size` method to `Parts`.

This is a simple matter of implements a method to delegate `size` to the actual array, as shown here:

```rb
def size
    parts.size
end
```

Perhaps `Parts` is an `Array`, albeit one w/ a bit of extra behavior.

You could make it one; the next example shows a new version of the `Parts` class, now as a subclass of `Array`.

The above code is a very straightforward expression of the idea that `Parts` is a specialization of `Array`.

It turns out that there are many methods in `Array` that return new arrays, and unfortunately these methods return new instances of the `Array` class, not new instances of your subclass.

Where once you were disappointed to find that `Parts` did not implement `size`, now you might be surprised to find that adding two `Parts` together returns a result that does not understand `spares`.

Somewhere in the middle ground btwn complexity and usability lies the following solution.

The `Parts` class below delegates `size` and `each` to its `@parts` array and includes `Enumerable` to get common traversal and searching methods.

This version of `Parts` does not have all the behavior of `Array`, but at least everything that it claims to do actually works.

```rb
require "forwardable"
class Parts
    extend Forwardable
    def_delegators :@parts, :size, :each
    include Enumerable

    def initialize(parts)
        @parts = parts
    end

    def spares
        select {|part| part.needs_spare}
    end
end
```

This `Parts` class doesn't understand `+`, but b/c `Parts` now responds to `size`, `each`, and all of `Enumerable`, and obligingly raises errors when you mistakenly treat it like an actual `Array`, it may be good enough.

The following example proves that `spares` and `parts` can now both respond to `size`.

```rb
road_bike = Bicycle.new(
    size:   "L",
    parts:  Parts.new([chain, road_tire, tape]))

mountain_bike = Bicycle.new(
    size:   "L",
    parts:  Parts.new([chain, mountain_tire, front_shock, rear_shock]))

puts mountain_bike.spares.size  # → 3
puts mountain_bike.parts.size   # → 4

puts mountain_bike.parts + road_bike.parts
# → undefined method `+` for #<Parts:0x007fc1d59fe040>
```

## Manufacturing Parts

```rb
road_config = [
    ["chain",       "11-speed"],
    ["tire_size",   "23"],
    ["tape_color",  "red"]
]

mountain_config = [
    ["chain",       "11-speed"],
    ["tire_size",   "2.1"],
    ["front_shock", "Manitou"],
    ["rear_shock",  "Fox", false]
]
```

The first column contains the part name (`chain`, `tire_size`, etc.); the second, the part description (`11-speed`, `23`, etc.); and the third (which is optional), a Boolean that indicates whether this part needs a spare. Only `read_shock` below puts a value in this third column; the other parts would like to default to `true`, as they require spares.

## Creating the PartsFactory

An object hat manufactures other objects is a factory.

The code below shows a new `PartsFactory` module. Its job is to take an array like one of those listed above and manufacture a `Parts` object.

Along the way it may well create `Part` objects, but this action is private.

Its public responsibility is to create an instance of `Parts`.

The first version of `PartFactory` takes three arguments, a `config`, the names of the classes to be used for `Part`, and `Parts`.

```rb
module PartsFactory
    def self.build(config:,
        part_class: Part,
        parts_class: Parts)

        parts_class.new(
            config.collect {|part_config|
                part_class.new(
                    name:           part_config[0],
                    description:    part_config[1],
                    needs_spare:    part_config.fetch(2, true))})
    end
end
```

This factory knows the structure of the `config` array.

Putting knowledge of `config`'s structure in the factory has two consequences.

First, the `config` can be expressed very tersely. B/c `PartsFactory` understands `config`'s internal structure, `config` can be specified as an array rather than a hash.

Second, once you commit to keeping `config` in an array, you should *always* create new `Parts` objects using the factory.

Now that `PartsFactory` exists, you can use the configuration arrays defined above to easily create new `Parts`, as shown here:

```rb
puts PartsFactory.build(config: road_config).inspect

puts PartsFactory.build(config: mountain_config).inspect
```

`PartsFactory`, combined w/ the new configuration arrays, isolates all the knowledge needed to create a valid `Parts`. 

This information was previously dispersed throughout the application but now it is contained in this one class and these two arrays.

## Leveraging the PartsFactory

```rb
class Part
    attr_reader :name, :description, :needs_spare

    def initialize(name:, description:, needs_spare: true)
        @name           = name
        @description    = description
        @needs_spare    = needs_spare
    end
end
```

`Part` is simple. Not only that, the only even *slightly* complicated idea is that `needs_spare` should default to `true` (shown in the `initialize` method).

This bit of knowledge about the domain has been duplicated in `PartsFactory`.

Therefore, if `PartsFactory` were used to create every `Part`, this defaulting code could be removed from `Part`'s `initialize` method.

```rb
class Part
    attr_reader :name, :description, :needs_spare

    def initialize(name:, description:, needs_spare: true)
        @name           = name
        @description    = description
        @needs_spare    = needs_spare
    end
end
```

Once you remove the code that defaults `need_spare` to `true`, there's almost nothing left in `Part`.

The remaining `Part` class can then be replaced w/ a simple `OpenStruct`.

Ruby's `OpenStruct` class is a lot like the `Struct` class that you've already seen: It provides a convenient way to bundle a number of attributes into an object.

The difference btwn the two is that `Struct` takes position order initialization arguments while `OpenStruct` takes a hash for its initialization and then derives attributes from the hash.

You can remove all trace of `Part` by deleting the class and then changing `PartsFactory` to use `OpenStruct` to create an object that plays the `Part` *role*.

The following code shows a new version of `PartFactory` where part creation has been refactored into a method of its own.

```rb
require "ostruct"
module PartsFactory
    def self.build(config:, parts_class: Parts)
        parts_class.new(
            config.collect {|part_config| create_part(part_config)})
    end

    def self.create_part(part_config)
        OpenStruct.new(
            name:           part_config[0],
            description:    part_config[1],
            needs_spare:    part_config.fetch(2, true))
        )
    end
end
```

`needs_spare: part_config.fetch(2, true))` above is now the only place in the application that defaults `needs_spare` to `true`, so `PartsFactory` must be solely responsible for manufacturing `Parts`.

This new version of `PartsFactory` works. As shown below, it returns a `Parts` that contains an array of `OpenStruct` objects, each of which plays the `Part` role.

```rb
mountain_bike = Bicycle.new(
    size:   "L",
    parts:  PartsFactory.build(config: mountain_config)
)

puts mountain_bike.spares.class # → Array

puts mountain_bike.spares
```

## The Composed Bicycle

The following code shows that `Bicycle` now uses composition.

It shows `Bicycle`, `Parts`, and `PartsFactory` and the configuration arrays for road and mountain bikes.

Bicycle *has-a* `Parts`, which in turn *has-a* collection of `Part` objects.

`Parts` and `Part` may exist as classes, but the objects in which they are contained think of them as roles.

`Parts` is a class that plays the `Parts` role; it implements `spares`.

The role of `Part` is played by an `OpenStruct`, which implements `name`, `description`, and `needs_spare`.

```rb
class Bicycle
    attr_reader :size, :parts

    def initialize(size:, parts:)
        @size   = size
        @parts  = parts
    end

    def spares
        parts.spares
    end
end

require "forwardable"
class Parts
    extend Forwardable
    def_delegators :@parts, :size, :each
    include Enumerable

    def initialize(parts)
        @parts = parts
    end

    def spares
        select {|part| part.needs_spare}
    end
end

require "ostruct"
module PartsFactory
    def self.build(config:, parts_class: Parts)
        parts_class.new(
            config.collect { |part_config| 
                create_part(part_config) })
    end
    
    def self.create_part(part_config)
        OpenStruct.new(
            name:           part_config[0],
            description:    part_config[1],
            needs_spare:    part_config.fetch(2, true)
        )
    end
end

road_config = [
    ["chain",       "11-speed"],
    ["tire_size",   "23"],
    ["tape_color",  "red"]
]

mountain_config = [
    ["chain",       "11-speed"],
    ["tire_size",   "2.1"],
    ["front_shock", "Manitou"],
    ["rear_shock",  "Fox", false]
]
```

This new code works much like the prior `Bicycle` hierarchy.

The only different is that the `spares` message now returns an array of `Part`-like objects instead of a hash.

```rb
road_bike = Bicycle.new(
    size:   "L",
    parts:  PartsFactory.build(config: road_config))

puts road_bike.spares
# => #<OpenStruct
# =>    name="chain",
# =>    description="11-speed",
# =>    needs_spare=true>
# => #<OpenStruct
# =>    name="tire_size",
# =>    description="23",
# =>    needs_spare=true>
# => #<OpenStruct
# =>    name="tape_color",
# =>    description="red",
# =>    needs_spare=true>

mountain_bike = Bicycle.new(
    size:   "L",
    parts:  PartsFactory.build(config: mountain_config))

puts mountain_bike.spares
# => #<OpenStruct
# =>    name="chain",
# =>    description="11-speed",
# =>    needs_spare=true>
# => #<OpenStruct
# =>    name="tire_size",
# =>    description="2.1",
# =>    needs_spare=true>
# => #<OpenStruct
# =>    name="front_shock",
# =>    description="Manitou",
# =>    needs_spare=true>
```

Now that these new classes exist, it's very easy to create a new kind of bike.

```rb
recumbent_config = [
    ["chain",       "9-speed"],
    ["tire_size",   "28"],
    ["flag",        "tall and orange"]
]

recumbent_bike = Bicycle.new(
    size:   "L",
    parts:  PartsFactory.build(config: recumbent_config)
)

puts recumbent_bike.spares
# => #<OpenStruct
# =>    name="chain",
# =>    description="9-speed",
# =>    needs_spare=true>
# => #<OpenStruct
# =>    name="tire_size",
# =>    description="28",
# =>    needs_spare=true>
# => #<OpenStruct
# =>    name="flag",
# =>    description="tall and orange",
# =>    needs_spare=true>
```

## Aggregation: A Special Kind of Composition

*Delegation* is when one object receives a message and merely forwards it to another.

Delegation creates dependencies; the receiving object must recognize the message *and* know where to send it.

*Composition* often involves delegation but the term means something more.

A *composed* object is made up of parts w/ which it expects to interact via well-defined interfaces.

*Composition* describes a *has-a* relationship.

The composed object depends on the interface of the role.

B/c meals interact w/ appetizers using an interface, new objects that wish to act as appetizers need only implement this interface.

Unanticipated appetizers fit seamlessly and interchangeably into existing meals.

The term *composition* can be a bit confusing b/c it gets used for two slightly different concepts.

In most cases when you see *composition*, it will indicate nothing more than this general *has-a* relationship between two objects.

However, as formally defined, it means something a bit more specific; it indicates a *has-a* relationship where the contained object has no life independent of its container.

When *composition* is used in this stricter sense, you know not only that meals have appetizers but also that once the meal is eaten the appetizer is also gone.

This leaves a gap in the definition that is filled by the term *aggregation*.

Aggregation is exactly like composition except that the contained object has an independent life.

Universities have departments, which in turn have professors.

If your application manages many universities and knows about thousands of professors, it's quite reasonable to expect that although a department completely disappears when its university goes defunct, its professors continue to exist.

The university-department relationship is one of composition (in its strictest sense) and the department-professor relationship is aggregation.

Destroying a department does not destroy its professors; they have an existence and life of their own.

This distinction between composition and aggregation may have little practical effect on your code.

Now that you are familiar w/ both terms, you can use *composition* to refer to both kinds of relationships and be more explicit only if the need arises.

## Deciding between Inheritance and Composition

Recall that classical inheritance is a *code arrangement technique*.

Behavior is dispersed among objects and these objects are organized into class relationships such that automatic delegation of messages invokes the correct behavior.

Think of it this way: for the cost of arranging objects in a hierarchy, you get message delegation for free.

Composition is an alternative that reverses these costs and benefits.

In composition, the relationship between objects is not codified in the class hierarchy; instead objects stand alone and as a result must explicitly know about and delegate messages to one another.

Composition allows objects to have structural independence, but at the cost of explicit message delegation.

The general is that, faced w/ a problem that composition can solve, you should be biased toward doing so.

If you cannot explicitly defend inheritance as a better solution, use composition.

Composition contains far fewer built-in dependencies than inheritance; it is very often the best choice.

Inheritance *is* a better solution when its use provides high rewards for low risk.

## Accepting the Consequences of Inheritance

**Benefits of Inheritance**

Code should be transparent, reasonable, usable, and exemplary.

Methods defined near the top of inheritance hierarchies have widespread influence b/x the height of the hierarchy acts as a lever that multiplies their effects.

Changes made to these methods ripple down the inheritance tree.

Correctly modeled hierarchies are thus extremely *reasonable*; big changes in behavior can be achieved via small changes in code

Use of inheritance results in code that can be described as *open-closed*; hierarchies are open for extension while remaining closed for modification.

Adding a new subclass to an existing hierarchy requires no changes to existing code.

Hierarchies are thus *usable*; you can easily create new subclasses to accommodate new variants.

Correctly written hierarchies are easy to extend.

The hierarchy embodies the abstraction and every new subclass plugs in a few concrete differences.

The existing pattern is easy to follow and replicating it will be the natural choice of any programmer charged w/ creating new subclasses.

Hierarchies are therefore *exemplary*; by their nature they provide guidance for writing the code to extend them.

In Ruby, the `Numeric` class provides an excellent example. `Integer` and `Float` are modeled as subclasses of `Numeric`; this *is-a* relationship is exactly right.

`Integer` and `Float` are fundamentally *numbers*. Allowing these two classes to share a common abstraction is the most parsimonious way to organize code.

**Costs of Inheritance**

Inheritance is a place where the question "What will happen when I'm wrong" assumes special importance.

Inheritance by definition comes w/ a deeply embedded set of dependencies.

Subclasses depend on the methods defined in their superclasses *and* on the automatic delegation of messages to those superclasses.

This is classical inheritance's greatest strength and biggest weakness; subclasses are bound, irrevocably and by design, to the classes above them in the hierarchy.

These built-in dependencies amplify the effects of modifications made to superclasses.

Enormous, broad-reaching changes of behavior can be achieved w/ very small changes in code.

Avoid writing frameworks that require users of your code to subclass your objects in order to gain your behavior.

Their application's objects may already be arranged in a hierarchy; inheriting from your framework may not be possible.

## Accepting the Consequences of Composition

Objects built using composition differ from those built using inheritance in two basic ways.

Composed objects do not depend on the structure of the class hierarchy, and they delegate their own messages.

**Benefits of Composition**

When using composition, the natural tendency is to create many small objects that contain straightforward responsibilities that are accessible through clearly defined interfaces.

These small objects have a single responsibility and specify their own behavior.

They are *transparent*; it's easy to understand the code and it's clear what will happen if it changes.

Also, the composed object's independence from the hierarchy means that it inherits very little code and so is generally immune from suffering side effects as a result of changes to classes above it in the hierarchy.

B/c composed objects deal w/ their parts via an interface, adding a new kind of part is a simple matter of plugging in a new object that honors the interface.

From the point of view of the composed object, adding a new variant of an existing part is *reasonable* and requires no changes to its code.

By their very nature, objects that participate in composition are small, structurally independent, and have well-defined interfaces.

This allows their seamless transition into pluggable, interchangeable components. 

Well-composed objects are therefore easily *usable* in new and unexpected contexts.

At its best, composition results in applications built of simple, pluggable objects that are easy to extend and have a high tolerance for change.

**Costs of Composition**

A composed object relies on its many parts.

Even if each part is small and easily understood, the combined operation of the whole may be less than obvious.

While every individual part may indeed by *transparent*, the whole may not be.

The benefits of structural independence are gained at the cost of automatic message delegation.

The composed object must explicitly know which messages to delegate and to whom.

Identical delegation code may be needed by many different objects; composition provides no way to share this code.

As these costs and benefits illustrate, composition is excellent at prescribing rules for assembling an object made of parts but doesn't provide as much help for the problem of arranging code for a collection of parts that are very nearly identical.

## Choosing Relationships

Classical inheritance, behavior sharing via modules, and composition are each the perfect solution for the problem they solve.

The trick to lowering your application costs is to apply each technique to the right problem.

Use composition when the behavior is more than the sum of its parts.

**Use Inheritance for is-a Relationships**

When you select inheritance over composition, you are placing a bet that the benefits thereby accrued will outweigh the costs.

Small sets of real-world objects that fall naturally into static, transparently obvious specialization hierarchies are candidates to be modeled using classical inheritance.

The hierarchy's small size makes it understandable, intention revealing, and easily extendable.

B/c these objects meet the criteria for successful use of inheritance, the risk of being wrong is low, but in the unlikely event that you *are* wrong, the cost of changing your mind is also low.

You can achieve the benefits of inheritance while exposing yourself to few of its risks.

In terms of this chapter's example, each different shock plays the role of `Part`.

It inherits common shock behavior *and* the `Part` role from its abstract `Shock` superclass.

The `PartsFactory` currently assumes that every part can be represented by the `Part` `OpenStruct`, but you could easily extend the part configuration array to supply the class name for a specific shock.

B/c you already think of `Part` as an interface, it's easy to plug in a new kind of part, even if this part uses inheritance to get some of its behavior.

If requirements change such that there is an explosion in the kinds of shocks, reassess this design decision.

If modeling a bevy of new shocks requires dramatically expanding the hierarchy, or if the new shocks don't conveniently fit into the existing code, reconsider alternatives *at that time*.

**Use Duck Types for behaves-like-a Relationships**

Some problems require many different objects to play a common role.

In addition to their core responsibilities, objects might play roles like *schedulable*, *perparable*, *printable*, or *persistable*.

There are two key ways to recognize the existence of a role.

First, although an object plays it, the role is not the object's main responsibility. A bicycle *behaves-like-a* schedulable but it *is-a* bicycle.

Second, the need is widespread; many otherwise unrelated objects share a desire to play the same role.

The most illuminating way to think about roles is from the outside, from the point of view of a holder of a role player rather than that of a player of a role.

The holder  of a *schedulable* expects it to implement `Schedulable`'s interface and to honor `Schedulable`'s contract.

All `schedulables` are alike in that they mut meet these expectations.

Your design task is to recognize that a role exists, define the interface of its duck type and provide an implementation of that interface for every possible player.

Some roles consists only of their interface, but others share common behavior.

Define the common behavior in a Ruby module to allow objects to play the role w/o duplicating the code.

**Use Composition for has-a Relationships**

Many objects contain numerous parts but are more than the sums of those parts.

`Bicycles` *have-a* `Parts`, but the bike itself is something more.

It has behavior that is separate from and in addition to the behavior of its parts.

Given the current requirements of the bicycle example, the most cost-effective way to model the `Bicycle` object is via composition.

This *is-a* versus *has-a* distinction is at the core of deciding btwn inheritance and composition.

The more parts an object has, the more likely it is that it should be modeled w/ composition.

The more deeper you drill down into individual parts, the more likely it is that you'll discover a specific part that has a few specialized variants and is thus a reasonable candidate for inheritance.

## Summary

Composition allows you to combine small parts to create complex objects such that the whole becomes more than the sum of its parts.

Composed objects tend to consist of simple, discrete entities that can easily be rearranged into new combinations.

These simple objects are easy to understand, reuse, and test, but because they combine into a more complicated whole, the operation of the bigger application may not be as easy to understand as that of the individual parts.

Composition, classical inheritance, and behavior sharing via modules are competing techniques for arranging code.

Each has different costs and benefits; these differences predispose them to be better at solving slightly different problems.
