# Managing Dependencies

## Recognizing Dependencies

```rb
class Gear
    attr_reader :chainring, :cog, :rim, :tire
    def initialize(chainring, cog, rim, tire)
        @chainring = chainring
        @cog       = cog
        @rim       = rim
        @tire      = tire
    end

    def gear_inches
        ratio * Wheel.new(rim, tire).diameter
    end

    def ratio
        chainring / cog.to_f
    end

    #...
end

class Wheel
    attr_reader :rim, :tire
    def initialize(rim, tire)
        @rim  = rim
        @tire = tire
    end

    def diameter
        rim + (tire * 2)
    end

    #...
end

puts Gear.new(52, 11, 26, 1.5).gear_inches # → 137.0909091
```

An object has a dependency when it knows:

* The name of another class. `Gear` expects a class named `Wheel` to exist.

* The name of a message that it intends to send to someone other than `self`. `Gear` expects a `Wheel` instance to respond to `diameter`.

* The arguments that a message requires. `Gear` knows that `Wheel.new` requires a `rim` and `tire`.

* The order of those arguments. `Gear` knows that `Wheel` takes *positional* arguments and that the first should be `rim`, the second, `tire`.

## Writing Loosely Coupled Code

The following examples illustrate coding techniques that reduce dependencies by decoupling code.

### Inject Dependencies

Above, the `gear_inches` method contains an explicit reference to class `Wheel`.

When `Gear` hard-codes a reference to `Wheel` deep inside of its `gear_inches` method, it is explicitly declaring that it is only willing to calculate gear inches for instances of `Wheel`. `Gear` refuses to collaborate w/ any other kind of object, even if that object has a diameter and uses gears.

Instead of being glued to `Wheel`, this next version of `Gear` expects to be initialized w/ an object that can respond to diameter.

```rb
class Gear
    attr_reader :chainring, :cog, :wheel
    def initialize(chainring, cog, wheel)
        @chainring = chainring
        @cog       = cog
        @wheel     = wheel
    end

    def gear_inches
        ratio * wheel.diameter
    end

    #...
end

# Gear expects a `Duck` that knows `diameter`
puts Gear.new(52, 11, Wheel.new(26, 1.5)).gear_inches # → 137.0909091
```

### Isolate Instance Creation

If you are so constrained that you cannot change the code to inject a `Wheel` into a `Gear`, you should isolate the creation of a new `Wheel` inside the `Gear` class. The intent is to explicitly expose the dependency while reducing its reach into your class.

The next two examples illustrate this idea.

In the first, creation of the new instance of `Wheel` has been moved from `Gear`'s `gear_inches` method to `Gear`'s initialization method. This cleans up the `gear_inches` method and publicly exposes the dependency n the `initialize` method. Notice that this technique unconditionally creates a new `Wheel` each time a new `Gear` is created.

```rb
class Gear
    attr_reader :chainring, :cog, :wheel
    def initialize(chainring, cog, rim, tire)
        @chainring = chainring
        @cog       = cog
        @wheel     = Wheel.new(rim, tire)
    end

    def gear_inches
        ratio * wheel.diameter
    end
    #...
end

puts Gear.new(52, 11, 26, 1.5).gear_inches # → 137.0909091
```

The next alternative isolates creation of a new `Wheel` in its own explicitly defined `wheel` method. This new method lazily creates a new instance of `Wheel`, using Ruby's `||=` operator. In this case, creation of a new instance of `Wheel` is deferred until `gear_inches` invokes the new wheel method.

```rb
class Gear
    attr_reader :chainring, :cog, :rim, :tire
    def initialize(chainring, cog, rim, tire)
        @chainring = chainring
        @cog       = cog
        @rim       = rim
        @tire      = tire
    end

    def gear_inches
        ratio * wheel.diameter
    end

    def wheel
        @wheel ||= Wheel.new(rim, tire)
    end
    #...
end

puts Gear.new(52, 11, 26, 1.5).gear_inches # → 137.0909091
```

### Isolate Vulnerable External Messages

You can reduce your chance of being forced to make a change to `gear_inches` by removing the external dependency and encapsulating it in a method of its own, as in this next example:

```rb
def gear_inches
    #... a few lines of scary math
    ratio * diameter
    #... a few lines of scary math
end

def diameter
    wheel.diameter
end
```

In the original code, `gear_inches` knew that `wheel` had a `diameter`. This knowledge is a dangerous dependency that couples `gear_inches` to an external object and one of *its* methods. After this change, `gear_inches` is more abstract. `Gear` now isolates `wheel.diameter` in a separate method, and `gear_inches` can depend on a message sent to `self`.

## Remove Argument-Order Dependencies

### Use Keyword Arguments

There's a simple way to avoid depending on positional arguments. If you have control over `Gear`'s `initialize` method, change the code to take *keyword* arguments.

```rb
class Gear
    attr_reader :chainring, :cog, :wheel
    def initialize(chainring:, cog:, wheel:)
        @chainring = chainring
        @cog       = cog
        @wheel     = wheel
    end

    def gear_inches
        ratio * wheel.diameter
    end

    def wheel
        @wheel ||= Wheel.new(rim, tire)
    end
    #...
end

puts Gear.new(52, 11, 26, 1.5).gear_inches # → 137.0909091
```

The arguments now end in `:`, which denotes that they are keyword arguments. Keyword arguments are referenced just like positional arguments. You can pass keyword arguments as a hash, as shown in the following example:

```rb
puts Gear.new(
    :cog       => 11,
    :chainring => 52,
    :wheel     => Wheel.new(26, 1.5)).gear_inches # → 137.0909091
)
```

You can also use the explicit keyword syntax:

```rb
puts Gear.new(
    wheel: Wheel.new(26, 1.5),
    chainring: 52,
    cog: 11).gear_inches # → 137.0909091
```

### Explicitly Define Defaults

```rb
class Gear
    attr_reader :chainring, :cog, :wheel
    def initialize(chainring: 40, cog: 18, wheel:)
        @chainring = chainring
        @cog       = cog
        @wheel     = wheel
    end

    #...
end

puts Gear.new(wheel: Wheel.new(26, 1.5)).chainring # → 40
```

Notice that the syntax for adding defaults to keyword arguments is a bit different than that of positional arguments. Keywords omit the `=` operator and state the default directly after the trailing `:`. Adding a default renders the keyword argument optional.

```rb
class Gear
    attr_reader :chainring, :cog, :wheel
    def initialize(chainring: default_chainring, cog: 18, wheel:)
        @chainring = chainring
        @cog       = cog
        @wheel     = wheel
    end

    def default_chainring
        (100/2) - 10
    end
    #...
end

puts Gear.new(wheel: Wheel.new(26, 1.5)).chainring # → 40
puts Gear.new(chainring: 52, wheel: Wheel.new(26, 1.5)).chainring # → 52
```

Recognize that `initialize` executes in the new instance of `Gear`. It is therefore entirely appropriate for `initialize` to send messages to `self`. It's best to embed simple defaults right in the parameter list, but if getting the default requires running a bit of code, don't hesitate to send a message.

### Isolate Multiparameter Initialization

Use a wrapping method to isolate external dependencies.

In this example, the `SomeFramework::Gear` class is not owned by your application; it is part of an external framework. Its initialization method requires positional arguments. The `GearWrapper` module was created to avoid having multiple dependencies on the order of those arguments. `GearWrapper` isolates all knowledge of the external interface in one place and, equally important, it provides an improved interface for your application.

As you can see, `GearWrapper` allows your application to create a new instance of `Gear` using keyword arguments.

```rb
# When Gear is part of an external interface
module SomeFramework
    class Gear
        attr_reader :chainring, :cog, :wheel
        def initialize(chainring, cog, wheel)
            @chainring = chainring
            @cog = cog
            @wheel = wheel
        end
        #...
    end
end

# wrap the interface to protect yourself from changes
module GearWrapper
    def self.gear(chainring:, cog:, wheel:)
        SomeFrameWork::Gear.new(chainring, cog, wheel)
    end
end

# Now you can create a new Gear using keyword arguments
puts GearWrapper.gear(
    chainring: 52,
    cog: 11,
    wheel: Wheel.new(26, 1.5)
).gear_inches # → 137.0909091
```

`GearWrapper` is a Ruby module. `GearWrapper` is responsible for creating new instances of `SomeFramework::Gear`.

Using a module here lets you define a separate and distinct object to which you can send the `gear` message while simultaneously conveying the idea that you don't expect to have instances of `GearWrapper`.

The sole purpose of `GearWrapper` is to create instances of some other class. 

The above technique for replacing positional arguments w/ keywords is perfect for cases where you are forced to depend on external interfaces that you cannot change.
