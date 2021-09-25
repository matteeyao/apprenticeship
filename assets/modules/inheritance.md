# Acquiring Behavior through Inheritance

## Extending Classes

Inheritance is a way to establish a subtype from an existing class in order to reuse code. Let's look at an example:

```rb
class User
  attr_reader :first_name, :last_name

  def initialize(first_name, last_name)
    @first_name, @last_name = first_name, last_name
  end

  def full_name
    "#{first_name} #{last_name}"
  end

  def upvote_article(article)
    article.upvotes += 1
  end
end

class SuperUser < User
  attr_reader :super_powers

  def initialize(first_name, last_name, super_powers)
    super(first_name, last_name)
    @super_powers = super_powers
  end

  def upvote_article(article)
    # extra votes
    article.upvotes += 3
  end

  def delete_user(user)
    return unless super_powers.include?(:user_deletion)

    # super user is authorized to delete user
    puts "Goodbye, #{user.full_name}!"
  end
end
```

Here we use `<` to denote that the `SuperUser` class **inherits** from the `User` class. That means that the `SuperUser` class gets all of the methods of the `User` class. We say that `User` is the **parent class** or **superclass**, and that `SuperUser` is the **child class** or **subclass**. So we can write code like so:

```zsh
[13] pry(main)> load 'test.rb'
=> true
[14] pry(main)> u = User.new("Jamis", "Buck")
=> #<User:0x007f9ba9897d98 @first_name="Jamis", @last_name="Buck">
[15] pry(main)> u.full_name
=> "Jamis Buck"
[16] pry(main)> su = SuperUser.new("David", "Heinemeier Hansson", [:user_deletion])
=> #<SuperUser:0x007f9ba98e66c8
 @first_name="David",
 @last_name="Heinemeier Hansson",
 @super_powers=[:user_deletion]>
[17] pry(main)> su.full_name
=> "David Heinemeier Hansson"
[18] pry(main)> su.delete_user(u)
Goodbye, Jamis Buck!
=> nil
```

`SuperUser` **overrides** some of `User`'s methods: `initialize` and `upvote_article`. The definitions in `SuperUser` will be called instead.

In the case of `initialize`, the `SuperUser` method will call the original definition in `User`. This is done through the call of `super`, which runs the parent class's version of the current method.

Calls to `super` are especially common when overriding initialize.

## Inheritance and Code Reuse

Inheritance has allowed us to avoid duplicating the methods that are common to `User` and `SuperUser`. Here's another example:

```rb
class Magazine
  attr_accessor :editor
end

class Book
  attr_accessor :editor
end
```

We see code being duplicated: a bad sign. We can use inheritance to solve this problem like so:

```rb
class Publication
  attr_accessor :editor
end

class Magazine < Publication
end

class Book < Publication
end
```

This is a toy example, but the more two classes have in common the more it pays for them to share a single base class. This also makes it easier to add new child classes later.

Of course, the `Magazine` and `Book` classes may have their own methods in addition to the shared `editor` method.

## Calling a super method

When overriding a method, it is common to call the original implementation.

Sending `super` in any method passes that message up the superclass chain.

We can call the superclass's implementation of any method using the special `super` keyword. 

There are two major ways in which `super` is called. 

If `super` is called w/o any arguments, the arguments passed to the method will be implicitly passed on to the parent class's implementation.

```rb
class Animal
  def make_n_noises(n = 2)
    n.times { print "Growl " }
  end
end

class Liger < Animal
  def make_n_noises(num = 4)
    num.times { print "Roar " }
    # here we'll call super without any arguments. This will pass on `num`
    # implicitly to super. You can think of this call to super as:
    # `super(num)`
    super
  end
end

Liger.new.make_n_noises(3) # => Roar Roar Roar Growl Growl Growl
```

The most common method where this happens is `initialize`. Consider this setup and try to spot the problem:

```rb
class Animal
  attr_reader :species

  def initialize(species)
    @species = species
  end
end

class Human < Animal
  attr_reader :name

  def initialize(name)
    @name = name
  end
end
```

Uh-oh! When we call `Human.new`, this won't set the species! Let's fix that. Here is the second major way that super is called, passing arguments explicitly:

```rb
class Animal
  attr_reader :species

  def initialize(species)
    @species = species
  end
end

class Human < Animal
  attr_reader :name

  def initialize(name)
    # super calls the original definition of the method
    # If we hadn't passed "Homo Sapiens" to super, then `name` would have
    # been passed by default.
    super("Homo Sapiens")
    @name = name
  end
end
```

## 6.1 Understanding Classical Inheritance

Inheritance is, at its core, a mechanism for *automatic message delegation*.

It defines a forwarding path for not-understood messages.

It creates relationships such that, if one object cannot respond to a received message, it delegates that message to another.

In classical inheritance, these relationships are defined by creating subclasses.

Messages are forwarded from subclass to superclass; the shared code is defined in the class hierarchy.

## 6.2 Recognizing Where to Use Inheritance

### 6.2.1 Starting w/ a Concrete Class

```rb
class Bicycle
  attr_reader :size, :tape_color

  def initialize(**opts)
    @size       = opts[:size]
    @tape_color = opts[:tape_color]
  end

  # every bike has the same defaults for tire and chain size
  def spares
    { chain:      '11-speed',
      tire_size:  '23',
      tape_color: tape_color }
  end

  # Many other methods...
end

bike = Bicycle.new(
  size:       'M',
  tape_color: 'red')

puts bike.size
# → M

puts bike.spares
# → {:chain=>"11-speed", :tire_size=>"23", :tape_color=>"red"}
```

Bikes have an overall size, a handlebar tape color, a tire size, and a chain type.

These attributes are passed to a new bike in the `initialize` method via the `**opts` argument.

`**opts` indicates that `initialize` will accept any number of keyword arguments and return them as a `Hash`. This leads to the `opts` references.

`Bicycle` instances can respond to the `spares`, `size`, and `tape_color` messages, and a `Mechanic` can figure out what spare parts to take by asking each `Bicycle` for its `spares`.

Despite the fact that the `spares` method commits the sin of embedding default strings directly inside itself, the above code is fairly reasonable.

### 6.2.2 Embedding Multiple Types

*antipattern*: a common pattern that appears to be beneficial but is actually detrimental, and for which there is a well-known alternative.

A common *antipattern* is when an object decides what message to send based on a category of the receiver.

You can think of *the class of an object* as merely a specific case of *an attribute that holds the category of self*; considered this way, these patterns are the same.

In each case, if the sender could talk, it would be saying, "I know *who you are*, and b/c of that, I know *what you do*."

This knowledge is a dependency that raises the cost of change.

### 6.2.3 Finding the Embedded Types

The `if` stmt in the `spares` method above switches on a variable named `style`, but it would have been just as natural to call that variable `type` or `category`.

Variables w/ these kinds of names are your cue to notice the underlying pattern.

`Type` and `category` are words perilously similar to those you would use when describing a class. After all, what is a class if noy a category or type?

This is the exact problem that inheritance solves: that of highly related types that share common behavior but differ along some dimension.

### 6.2.4 Choosing Inheritance

No matter how complicated the code, the receiving object ultimately handles any message in one of two ways.

It either responds directly or it passes the message on to some other object for a response.

Inheritance provides a way to define two objects as having a relationship such that when the first receives a message that it does not understand, it *automatically* forwards, or delegates, the message to the second.

Message forwarding via classical inheritance takes place btwn `classes`. B/c duck types cut across classes, they do not use classical inheritance to share common behavior.

You also already benefit from automatic delegation of messages to superclasses.

When an object receives a message it does not understand, Ruby automatically forwards that message up the superclass chain in search of a matching method implementation.

In Ruby, when you send `nil?` to an instance of `NilClass`, it obviously answers `true`.

When you send `nil?` to anything else, the message travels up the hierarchy from one superclass to the next until it reaches `Object`, where it invokes the implementation that answers `false`.

Subclasses are specializations of their superclasses.

## 6.4 Finding the Abstraction

Subclasses are *specializations* of their superclasses.

For inheritance to work, two things must always be true.

First, the objects that you are modeling must truly have a generalization-specialization relationship.

Second, you must use the correct coding techniques.

### 6.4.1 Creating an Abstract Superclass

*Abstract* classes are disassociated from any specific instance.

Some object-oriented programming languages have syntax that allows you to explicitly declare classes as abstract. 

Java, for example, has the *abstract* keyword. The Java compiler itself prevents creation of instances of classes to which this keyword has been applied.

Abstract classes exist to be subclassed. This is their sole purpose. They provide a common repository for behavior that is shared across a set of subclasses-subclasses that in turn supply specializations.

It almost never makes sense to create an abstract superclass w/ only one subclass.

### 6.4.2 Promoting Abstract Behavior

When a `RoadBike` receives `size`, Ruby itself delegates the message up the superclass chain, searching for an implementation and finding the one in `Bicycle`.

This message delegation happens automatically b/c `RoadBike` is a subclass of `Bicycle`.

Untrustworthy hierarchies force objects that interact w/ them to know their quirks.

The general rule for refactoring into a new inheritance hierarchy is to arrange code so that you can promote abstractions rather than demote concretions.

"What will happen *when* I'm wrong?" Every decision you make includes two costs: one to implement it and another to change it when you discover that you were wrong.

### 6.4.4 Implementing every Template Method

Explicitly stating that subclasses are required to implement a message provides useful documentation for those who can be relied upon to read it and useful error messages for those who cannot.

Always document template method requirements by implementing matching methods that raise useful errors.

## 6.5 Managing Coupling btwn Superclasses and Subclasses

Managing coupling is important; tightly coupled classes stick together and may be possible to change independently.

### 6.5.1 Understanding Coupling

```rb
class Bicycle
  attr_reader :size, :chain, :tire_size # ← promoted from RoadBike
  
  def initialize
    @size       = opts[:size] # ← promoted from RoadBike
    @chain      = opts[:chain]      || default_chain
    @tire_size  = opts[:tire_size]  || default_tire_size
  end

  def spares
    { tire_size:  tire_size,
      chain:      chain }
  end

  def default_chain # ← common default
    "11-speed"
  end

  def default_tire_size
    raise NotImplementedError,
      "#{self.class} should have implemented..."
  end
end

class RoadBike < Bicycle
  attr_reader :tape_color

  def initialize(**opts)
    @tape_color = opts[:tape_color]
    super # ← RoadBike MUST send `super`
  end

  def spares
    super.merge(tape_color: tape_color)
  end

  def default_tire_size # ← subclass default
    "23"
  end
end

class MountainBike < Bicycle
  attr_reader :front_shock, :rear_shock

  def initialize(**opts)
    @front_shock = opts[:front_shock]
    @rear_shock  = opts[:rear_shock]
    super
  end

  def spares
    super.merge(front_shock: front_shock)
  end

  def default_tire_size # ← subclass default
    "2.1"
  end
end
```

```rb
road_bike = RoadBike.new(
  size:         "M",
  tape_color:   "red"
)

puts road_bike.size       # → M
puts road_bike.tire_size  # → 23
puts road_bike.chain      # → 11-speed

mountain_bike = MountainBike.new(
  size:         "S",
  front_shock:  "Manitou",
  rear_shock:   "Fox")

puts mountain_bike.size       # → S
puts mountain_bike.tire_size  # → 2.1
puts mountain_bike.chain      # → 11-speed
```

Notice that the `MountainBike` and `RoadBike` subclasses follow a similar pattern.

They each know things about themselves (their spare parts specializations) and things about their superclass (that it implements `spares` to return a hash and that it responds to `initialize`).

Knowing things about other classes, as always, creates dependencies, and dependencies couple objects together.

The dependencies in the code above are also booby traps; both are created by the sends of `super` in the subclasses.

The pattern of code in this hierarchy requires that subclasses not only know what they do but also how they are supposed to interact w/ their superclass.

It makes sense that subclasses know the specializations they contribute (they are obviously the only classes who *can* know them), but forcing a subclass to know how to interact w/ its abstract superclass causes many problems.

It pushes knowledge of the algorithm down into the subclasses, forcing each to explicitly send `super` to participate.

It causes duplication of code across subclasses, requiring that all send `super` in exactly the same places.

When a subclass sends `super`, it's effectively declaring that it knows the algorithm, it depends on this knowledge. If the algorithm changes, then the subclasses may break even if their own specializations are not otherwise affected.

### 6.5.2 Decoupling Subclasses Using Hook Messages

Instead of allowing subclasses to know the algorithm and requiring that they send `super`, superclasses can instead send `hook` messages, ones that exist solely to provide subclasses a place to contribute information by implementing matching methods.

This strategy removes knowledge of the algorithm from the subclass and returns control to the superclass.

In the following example, this technique is used to give subclasses a way to contribute to initialization.

Bicycle's `initialize` method now sends `post_initialize` and, as always, implements the matching method, one that in this case does nothing.

`RoadBike` supplies its own specialized initialization by overriding `post_initialize`:

```rb
class Bicycle
  attr_reader :size, :chain, :tire_size

  def initialize(**opts)
    @size       = opts[:size]
    @chain      = opts[:chain]      || default_chain
    @tire_size  = opts[:tire_size]  || default_tire_size

    post_initialize(opts)     # Bicycle both sends
  end

  def post_initialize(opts)   # and implements this
  end
  # ...
end

class RoadBike < Bicycle
  attr_reader :tape_color

  def post_initialize(opts)           # RoadBike can
    @tape_color = opts[:tape_color]   # optionally
  end                                 # override it
end
```

This change doesn't just remove the send of `super` from `RoadBike`'s `initialize` method, it remove the `initialize` method altogether.

`RoadBike` no longer controls initialization; it instead contributes specializations to a larger, abstract algorithm.

This algorithm is defined in the abstract superclass `Bicycle`, which in turn is responsible for sending `post_initialize`.

`RoadBike` is still responsible for *what* initialization it needs but is no longer responsible for *when* its initialization occurs.

This change allows `RoadBike` to know less about `Bicycle`, reducing the coupling btwn them and making each more flexible in the face of an uncertain future.

`RoadBike` doesn't know when its `post_initialize` method will be called, and it doesn't care what object actually sends the message.

`Bicycle` (or any other object) could send this message at any time; there is no requirement that it be sent during object initialization.

Putting control of the timing in the superclass means the algorithm can changes w/o forcing changes upon the subclasses.

This same technique can be used to remove the send of `super` from the `spares` method. Instead of forcing `RoadBike` to know that `Bicycle` implements `spares` and that `Bicycle`'s implementation returns a hash, you can loosen coupling by implementing a hook that gives control back to `Bicycle`.

The following example changes `Bicycle`'s `spares` method to send `local_spares`.

Bicycle provides a default `local_spares` implementation that returns an empty hash.

`RoadBike` takes advantage of this hook and overrides `local_spares` to contribute its own specific spare parts.

```rb
class Bicycle
  attr_reader :size, :chain, :tire_size

  def initialize(**opts)
    @size       = opts[:size]
    @chain      = opts[:chain]      || default_chain
    @tire_size  = opts[:tire_size]  || default_tire_size
    post_initialize(opts)     # Bicycle both sends
  end

  # subclasses may override
  def post_initialize(opts)   # and implements this
  end
  
  def spares
    { tire_size:  tire_size,
      chain:      chain }.merge(local_spares)
  end

  def default_tire_size
    raise NotImplementedError,
      "#{self.class} should have implemented..."
  end

  # hook for subclasses to override
  def local_spares
    {}
  end

  def default_chain
    "11-speed"
  end
end

class RoadBike < Bicycle
  attr_reader :tape_color

  def post_initialize(opts)           # RoadBike can
    @tape_color = opts[:tape_color]   # optionally
  end                                 # override it

  def local_spares
    { tape_color: tape_color }
  end

  def default_tire_size
    "23"
  end
end

class MountainBike < Bicycle
  attr_reader :front_shock, :rear_shock

  def post_initialize(opts)
    @front_shock  = opts[:front_shock]
    @rear_shock   = opts[:rear_shock]
  end

  def local_spares
    { front_shock: front_shock }
  end

  def default_tire_size
    "2.1"
  end
```

```rb
road_bike = RoadBike.new(
  size:       "M",
  tape_color: "red"
)

puts road_bike.tire_size  # → 23
puts road_bike.chain      # → 11-speed
puts road_bike.spares
# → {:tire_size=>"23", :chain=>"11-speed", :tape_color=>"red"}

mountain_bike = MountainBike.new(
  size:         "S",
  front_shock:  "Manitou",
  rear_shock:   "Fox"
)

puts mountain_bike.tire_size  # → 2.1
puts mountain_bike.chain      # → 11-speed
puts mountain_bike.spares
# → {:tire_size=>"2.1", :chain=>"11-speed", :front_shock=>"Manitou",}
```

`RoadBike`'s new implementation of `local_spares` replaces its former implementation of `spares`.

This change preserves the specialization supplied by `RoadBike` but reduces its coupling to `Bicycle`.

`RoadBike` no longer has to know that `Bicycle` implements a `spares` method; it merely expects that its own implementation of `local_spares` will be called, by some object, at some time.

```rb
class RecumbentBike < Bicycle
  attr_reader :flag

  def post_initialize(opts)
    @flag = opts[:flag]
  end

  def local_spares
    { flag: flag }
  end

  def default_chain
    "10-speed"
  end

  def default_tire_size
    "28"
  end
end

bent = RecumbentBike.new(
  size: "M",
  flag: "tall and orange"
)

puts bent.spares
# → {:tire_size=>"28", :chain=>"10-speed", :flag=>"tall and orange"}
```

## 6.6 Summary

The best way to create an abstract superclass is by pushing code up from concrete subclasses.

Identifying the correct abstraction is easiest if you have access to at least three existing concrete classes.

Abstract superclasses use the template method pattern to invite inheritors to supply specializations, and they use hook methods to allow these inheritors to contribute these specializations w/o being forced to send `super`.

Hook methods allow subclasses to contribute specializations w/o knowing the abstract algorithm. They remove the need for subclasses to send `super` and therefore reduce the couping between layers of the hierarchy and increase its tolerance for change.
