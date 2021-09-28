# Designing Cost-Effective Tests

Refactoring is the process of changing a software system in such a way that it does not alter the external behavior of the code yet improves the internal structure.

Refactoring, as formally defined, does not add new behavior, it improves existing structure.

It's a precise process that alters code via tiny, crab-like steps and carefully, incrementally, and unerringly transforms one design into another.

Good design preserves maximum flexibility at minimum cost by putting off decisions at every opportunity,  deferring commitments until more specific requirements arrive.

When that day comes, *refactoring* is how you morph the current code structure into one that will accommodate the new requirements.

New features will be added only after you have successfully refactored the code.

Efficient tests prove that altered code continues to behave correctly w/o raising overall costs.

Good tests weather code refactorings w/ aplomb; they are written such that changes to the code do not force rewrites of the tests.

Well-designed code is easy to change, refactoring is how you change from one design to the next, and test free you to refactor w/ impunity.

## Intentional Testing

Getting good value from tests requires clarity of intention and knowing what, when, and how to test.

## Knowing Your Intentions

**Finding Bugs**

Knowing that you can (or can't) do something early on may cause you to choose alternatives in the present that alter the design options available in the future.

Also, as code accumulates, embedded bugs acquire dependencies. Fixing these bugs late in the process may necessitate changing a lot of dependent code.

Fixing bugs early always lowers costs

**Deferring Design Decisions**

As your design skills improve you will begin to write applications that are sprinkled w/ places where you know the design needs *something* but you don't yet have enough information to know exactly what.

These are the places where you are awaiting additional information, resisting the urge to commit to a specific design.

You know that at some point you will be better served by code that handles these many concrete cases as a single abstraction, but right now you don't have enough information to anticipate what that abstraction will be.

When your tests depend on interfaces, you can refactor the underlying code w/ reckless abandon.

The tests verify the continued good behavior of the interface and changes to the underlying code do not force rewrites of the tests.

Intentionally depending on interfaces allows you to use tests to put off design decisions safely and w/o penalty.

**Supporting Abstractions**

When more information finally arrives and you make the next design decision, you'll change the code in ways that increase its level of abstraction.

Herein les another of the benefits of tests on design.

Good design naturally progresses toward small independent objects that rely on abstractions.

The behavior of a well-design application gradually becomes the result of interactions among these abstractions.

Abstractions are wonderfully flexible design components but the improvements they provide come at one slight cost: While each individual abstraction might be easy to understand, there is no single place in the code that makes obvious the behavior of the whole.

As the code base expands and the number of abstractions grows, tests become increasingly necessary.

There is a level of design abstraction where it is almost impossible to safely make any change unless the code has tests.

Tests are your record of the interface of every abstraction and as such they are the wall at your back.

They let you put off design decisions and create abstractions to any useful depth.

**Exposing Design Flaws**

The next benefit of tests is that they expose design flaws in the underlying code.

If a test requires painful setup, the code expects too much context.

If testing one object drags a bunch of others into the mix, the code has too many dependencies.

If the test is hard to write, other objects will find the code difficult to reuse.

Tests are the canary in the coal min; when the design is bad, testing is hard.

The inverse, however, is not guaranteed to be true.

Costly tests do not necessarily mean that the application is poorly designed.

It is quite technically possible to write bad tests for well-designed code.

Therefore, for tests to lower your costs, both the underlying application *and* the tests must be well-designed.

Your goal is to gain all of the benefits of testing for the least cost possible.

The best way to achieve this goal is to write loosely coupled tests about only the things that matter.

## Knowing What to Test

One simple way to get better value from tests is to write fewer of them.

The safest way to accomplish this is to test everything just once and in the proper place.

Removing duplication from testing lowers the cost of changing tests in reaction to application changes, and putting tests in the right place guarantees they'll be forced to change only when absolutely necessary.

Distilling your tests to their essence requires having a very clear idea about what you intend to test, one that can be derived from design principles you already know.

Dealing w/ objects as if they are only and exactly the messages to which they respond lets you design a changeable application, and it is your understanding of the importance of this perspective that allows you to create tests that provide maximum benefit at minimum cost.

Not only should you limit couplings, but the few you allow should be to stable things.

The most stable thing about any object is its public interface; it logically follows that the tests you write should be for messages that are defined in public interfaces.

The most costly and least useful tests are those that blast holes in an object's containment walls by coupling to unstable internal details.

These overeager tests prove nothing about the overall correctness of an application but nonetheless raise costs b/c they break w/ every refactoring of underlying class.

Tests should concentrate on the incoming or outgoing messages that cross an object's boundaries.

The incoming messages make up the public interface of the receiving object.

The outgoing messages, by definition, are incoming into other objects and so are part of some other object's interface.

Tests that make assertions about the values that messages return are tests of *state*. Such tests commonly assert that the results returned by a message equal an expected value.

The general rule is that objects should make assertions about state *only* for messages in their own public interfaces.

Following this rule confines tests of message return values to a single place and removes unnecessary duplication, which DRYs out tests and lowers maintenance costs.

Some outgoing messages have no side effects and thus matter only to their senders. 

The sender surely cares about the result it gets back (why else send the message?), but no other part of the application cares if the message gets sent.

Outgoing messages like this are known as *queries* and they need not be tested by the sending object.

Query messages are part of the public interface of their receiver, which already implements every necessary test of state.

However, many outgoing messages *do* have side effects (a file gets written, a database record is saved, an action is taken by an observer) upon which your application depends.

These messages are *commands* and it is the responsibility of the sending object to prove that they are properly sent.

Proving that a message gets sent is a test of behavior, not state, and involves assertions about the number of times, and w/ what arguments, the message is sent.

Here, then, are the guidelines for what to test: Incoming messages should be tested for the state they return. Outgoing command messages should be tested to ensure they get sent. Outgoing query messages should not be tested.

As long as your application's objects deal w/ one another strictly via public interfaces, your tests need know nothing more.

When you test this minimal set of messages, no change in the private behavior of any object can affect any test.

When you test outgoing command messages only to prove they get sent, your loosely coupled tests can tolerate application changes w/o being forced to change in turn.

As long as the public interfaces remain stable, you can write tests once and they will keep you safe forever.

## Knowing When to Test

Writing tests first forces a modicum of reusability to be built into an object from its inception; it would otherwise be impossible to write tests at all.

## Knowing How to Test

BDD takes an outside-in approach, creating objects at the boundary of an application and working its way inward, mocking as necessary to supply as-yet-unwritten objects.

TDD takes an inside-out approach, usually starting w/ tests of domain objects and then reusing these newly created domain objects in the tests of adjacent layers of code.

When testing, it's useful to think of your application's objects as divided into two major categories. The first category contains the object that you're testing, referred to from now on as the *object under test*. The second category contains everything else.

Your tests must obviously know things about the first category, that is, about the object under test, but they should remain as ignorant as possible about the second.

Pretend that the rest of the application is opaque, that the only information available during the test is that which can be gained from looking at the object under test.

Allow for tests to assume a viewpoint that sights along the edges of the object under test, where they can know only about messages that come and go.

## Testing Incoming Messages

Incoming messages make up an object's public interface, the face it presents to the world.

These messages need tests b/c other application objects depend on their signatures and on the results they return.

```rb
class Wheel
    attr_reader :rim, :tire
    def initialize(rim, tire)
        @rim = rim
        @tire = tire
    end

    def diameter
        rim + (tire * 2)
    end
    # ...
end

class Gear
    attr_reader :chainring, :cog, :rim, :tire
    def initialize(chainring:, cog:, rim:, tire:)
        @chainring = chainring
        @cog = cog
        @rim = rim
        @tire = tire
    end

    def gear_inches
        ratio * Wheel.new(rim, tire).diameter
    end

    def ratio
        chainring / cog.to_f
    end
    # ...
end
```

The table below shows the messages (other than those that return simple attributes) that cross these object's boundaries.

`Wheel` responds to one incoming message, `diameter` (which in turn is sent by, or outgoing from, `Gear`) and `Gear` respond to two incoming messages, `gear_inches` and `ratio`.

| Object | Incoming Messages | Outgoing Messages | Has Dependents? |
|--------|-------------------|-------------------|-----------------|
| Wheel  | diameter          |                   | Yes             |
| Gear   |                   | diameter          | No              |
| Gear   | gear_inches       |                   | Yes             |
| Gear   | ratio             |                   | Yes             |

## Deleting Unused Interfaces

Incoming messages ought to have dependents.

This is true for `diameter`, `gear_inches`, and `ratio` where they are incoming messages.

Some object *other than the original implementer* depends on each of these messages.

Do not test an incoming message that has no dependents; delete it.

Your application is improved by ruthlessly eliminating code that is not actively being used.

## Proving the Public Interface

Incoming messages are tested by making assertions about the value, or state, that their invocation returns.

The first requirement for testing an incoming message is to prove that it returns the correct value in every possible situation.

The following code shows a test of `Wheel`'s `diameter` method.

```rb
class WheelTest < Minitest::Test
    def test_calculates_diameter
        wheel = Wheel.new(26, 1.5)

        assert_in_delta(29, wheel.diameter, 0.01)
    end
end
```

This test is extremely simple, and it invokes very little code. `Wheel` has no hidden dependencies, so no other application objects get created as a side effect of running this test.

`Wheel`'s design allows you to test it independently of every other class in your application.

Testing `Gear` is a bit more interesting. 

`Gear` requires a few more arguments than `Wheel`, but even so, the overall structure of these two tests is very similar. In the `gear_inches` test below, `Gear.new` create a new instance of `Gear` and `assert_in_delta` makes assertions about the method's results.

```rb
class GearTest < Minitest::Test
    def test_calculates_gear_inches
        gear = Gear.new(
            chainring: 52,
            cog:       11,
            rim:       26,
            tire:      1.5
        )

        assert_in_delta(137.1, gear.gear_inches, 0.01)
    end
end
```

This test has entanglements that the `diameter` test did not have.

`Gear`'s implementation of `gear_inches` unconditionally creates and uses another object, `Wheel`.

`Gear` and `Wheel` are coupled in the code *and* in the tests, though it's not obvious here.

The fact that `Gear`'s `gear_inches` method creates and uses another object affects how long this test runs and how likely it is to suffer unintended consequences as a result of changes to unrelated parts of the application.

The coupling that creates this problem, however, is hidden inside of `Gear` and so totally invisible in this test.

The test's purpose is to prove that `gear_inches` returns the right result and it certainly fulfills that requirement, but the way the underlying code is structured adds hidden risk.

B/c tests are the first reuse of code, this problem is but a harbinger of things to come for your application as a whole.

## Isolating the Object under Test

The goal of this test is to ensure that gear inches are calculated correctly but it turns out that running `gear_inches` relies on code in objects other than `Gear`.

This difficulty in isolating `Gear` for testing reveals that it is bound to a specific context, one that imposes limitations that will interfere w/ reuse.

Chpt 3 broke this binding by removing the creation of `Wheel` from `Gear`.

Here's a copy of the code that made that transition; `Gear` now expects to be injected w/ an object that understands `diameter`.

```rb
class Gear
    attr_reader :chainring, :cog, :wheel
    def initialize(chainring:, cog:, wheel:)
        @chainring = chainring
        @cog       = cog
        @wheel     = wheel
    end

    def gear_inches
        # The object in the `wheel` variable plays the `Diameterizable` role
        ratio * wheel.diameter
    end

    def ratio
        chainring / cog.to_f
    end
end
```

`Gear` no longer cares about the class of the injected object; it merely expects that it implements `diameter`.

The `diameter` method is part of the public interface of a *role*, one that might reasonably be named `Diameterizable`.

Now that `Gear` is decoupled from `Wheel`, you must inject an instance of `Diameterizable` during every `Gear` creation.

However, b/c `Wheel` is the only application class that plays this role, your runtime options are severely limited.

In real life, as the code currently exists, every `Gear` that you create will of necessity be injected w/ an instance of `Wheel`.

As circular as this sounds, injecting a `Wheel` into `Gear` is not the same as injecting a `Diameterizable`.

Thinking of the injected object as an instance of its role gives you more choices about what kind of `Diameterizable` to inject into `Gear` during your tests.

One possible `Diameterizable` is, obviously, `Wheel`, b/c it clearly implements the correct interface.

```rb
class GearTest < Minitest::Test
    def test_calculates_gear_inches
        gear = Gear.new(
            chainring: 52,
            cog:       11,
            wheel:     Wheel.new(26, 1.5)
        )

        assert_in_delta(137.1, gear.gear_inches, 0.01)
    end
end
```

Using a `Wheel` for the injected `Diameterizable` results in test code that exactly mirrors the application.

It is now obvious, both in the application and in the tests, that `Gear` is using `Wheel`.

The invisible coupling between these classes has been publicly exposed.

## Injecting Dependencies Using Classes

When the code in your test uses the same collaborating objects as the code in your application, your tests always break when they should.

The value of this cannot be underestimated.

Here's a simple example. Imagine that `Diameterizable`'s public interface changes.

Another programmer goes into the `Wheel` class and changes the `diameter` method's name to `width`.

```rb
class Wheel
    attr_reader :rim, :tire
    def initialize(rim, tire)
        @rim  = rim
        @tire = tire
    end

    def width # <---- used to be `diameter`
        rim + (tire * 2)
    end
    # ...
end
```

Imagine further that this programmer failed to update the name of the sent message in `Gear`.

`Gear` still sends `diameter` in its `gear_inches` method, as you can see in this reminder of `Gear`'s current code:

```rb
class Gear
    # ...
    def gear_inches
        ratio * wheel.diameter # <--- obsolete
    end
end
```

B/c the `Gear` test injects an instance of `Wheel` and `Wheel` implements width but `Gear` sends `diameter`, the test now fails.

This failure is unsurprising; it is exactly what should happen when two concrete objects collaborate and the receiver of a message changes but its sender does not.

`Wheel` has changed and as a result `Gear` needs to change.

## Injecting Dependencies as Roles

`Gear` and `Wheel` both have relationships w/ a third thing, the `Diameterizable` role. 

`Diameterizable` is depended on by `Gear` and implemented by `Wheel`.

There are two places in the code where an object depends on knowledge of `Diameterizable`.

First, `Gear` thinks that it knows `Diameterizable`'s interface; that is, it believes it can send `diameter` to the injected object.

Second, the code that created the object to be injected believes that `Wheel` implements this interface; that is, it expects `Wheel` to implement `diameter`.

Now that `Diameterizable` has changed, there's a problem.

`Wheel` has been updated to implement the new interface, but unfortunately `Gear` still expects the old one.

The whole point of dependency injection is that it allows you to substitute different concrete classes w/o changing existing code.

You can assemble new behavior by creating new objects that play existing roles and injecting these objects where those roles are expected.

Object-oriented design tells you to inject dependencies b/c it believes that specific concrete classes will vary more than these roles, or conversely, roles will be more stable than the classes from which they were abstracted.

Unfortunately the opposite just happened.

In this example, it was not the class of the injected object that changed, it was the interface of the role.

It is still correct to inject a `Wheel`, but now incorrect to send that `Wheel` the diameter message.

When a role has a single player, that one concrete player and the abstract role are so closely aligned that the boundaries btwn them are easily blurred, and it is a practical fact that sometimes this blurring doesn't matter.

In this case, `Wheel` is the only player of `Diameterizable`, and you don't currently expect to have others.

If `Wheel`s are cheap, injecting an actual `Wheel` has little negative effect on your tests.

When the application code can only be written one way, mirroring that arrangement is often the most effective way to write tests.

Doing so permits tests to correctly fail regardless of whether the connection (the name of the `Wheel` class) or the abstraction (the interface to the `diameter` method) changes.

If your application contains many different `Diameterizables`, you might want to create an idealized one so your tests clearly convey the idea of this role.

If all `Diameterizable`s are expensive, you may want to fake a cheap one to make your tests run faster.

If you are doing BDD, your application might not yet contain any object that plays this role; you may be forced to manufacture *something* just to write the test.

**Creating Test Doubles**

For this test, assume `Diameterizable`'s interface has reverted to the original `diameter` method and that `diameter` is again correctly implemented by `Wheel` and sent by `Gear`.

`DiameterDouble` below creates a fake, `Diameter-Double`. `DiameterDouble.new` injects this fake into `Gear`.

```rb
# Create a player of the `Diameterizable` role
class DiameterDouble
    def diameter
        10
    end
end

class GearTest < Minitest::Test
    def test_calculates_gear_inches
        gear = Gear.new(
            chainring: 52,
            cog:       11,
            wheel:     DiameterDouble.new
        )
    
        assert_in_delta(47.27, gear.gear_inches, 0.01)
    end
end
```

A test double is a stylized instance of a role player that is used exclusively for testing.

Each variation emphasizes a single interesting feature and allows the underlying object's other details to recede into the background.

This double *stubs* diameter, that is, it implements a version of `diameter` that returns a canned answer.

`DiameterDouble` is quite limited, but that's the whole point.

The face that it always returns 10 for `diameter` is perfect.

This stubbed return value provides a dependable foundation on which to construct the test.

Many test frameworks have built-in ways to create doubles and to stub return values.

`DiameterDouble` is *not* a mock. It's easy to slip into the habit of using the word `mock` to describe this double, but mocks are something else entirely and will be covered later in the section "Testing Outgoing Messages."

Injecting this double decouples the `Gear` test from the `Wheel` class. It no longer matters if `Wheel` is slow b/c `DiameterDouble` is always fast.

**Living the Dream**

Imagine now that the code undergoes the same alterations as before: `Diameterizable`'s interface changes from `diameter` to `width` and `Wheel` gets updated but `Gear` does not.

This change once again breaks the application.

Remember that the previous `Gear` test (which injected a `Wheel` instead of using a double) noticed this problem right away and began to fail w/ an `undefined method 'diameter'` error.


Now that you're injecting `DiameterDouble`, however, the test *continues to pass* even though the application is definitely broken.

This application cannot possibly work; `Gear` sends `diameter` but `Wheel` implements `width`.

The application contains a `Diameterizable` role. This role originally had one player, `Wheel`. When `GearTest` created `DiameterDouble`, it introduced *a second player of the role*.

When the interface of a role changes, all players of the role must adopt the new interface.

It's easy, however, to overlook role players that were constructed specifically for tests and that is exactly what happened here.

`Wheel` got updated w/ the new interface, but `DiameterDouble` did not.

**Using Tests to Document Roles**

When remembering that the role even exists is a challenge, forgetting that test doubles play it is inevitable.

One way to raise the role's visibility is to assert that `Wheel` plays it. `test_implements_the_diameterizable_interface` does just this; it documents the role and proves that `Wheel` correctly implements its interface.

```rb
class WheelTest < Minitest::Test
    def setup
        @wheel = Wheel.new(26, 1.5)
    end

    def test_implements_the_diameterizable_interface
        assert_respond_to(@wheel, :diameter)
    end

    def test_calculates_diameter
        assert_in_delta(29, @wheel.diameter, 0.01)
    end
end
```

The `implements_the_diameterizable_interface` test introduces the idea of tests for roles but is not a completely satisfactory solution.

It is, in fact, woefully incomplete.

First, it cannot be shared w/ other `Diameterizable`s.

Other players of this role would have to duplicate this test.

Next, it does nothing to help w/ the "living the dream" problem from the `Gear` test.

`Wheel`'s assertion that it plays this role does not prevent `Gear`'s `DiameterDouble` from becoming obsolete and allowing the `gear_inches` test to erroneously pass.

Fortunately, the problem of documenting and testing roles has a simple solution, one that will be thoroughly covered in the subsequent section "Testing Duck Types." 

For now, it's enough to recognize that roles need tests of their own.

The goal of this section was to prove public interfaces by testing incoming messages.

`Wheel` was cheap to test.

The original `Gear` test was more expensive b/c it depended on a hidden coupling to `Wheel`.

Replacing that coupling w/ an injected dependency on `Diameterizable` isolated the object under test but created a dilemma about whether to inject a real or a fake object.

## Testing Private Methods

Sometimes the object under test sends messages to itself.

Messages sent to `self` invoke methods that are defined in the receiver's private interface.

Dealing w/ private methods requires judgement and flexibility.

## Ignoring Private Methods during Tests

Testing private methods can mislead others into using them.

Tests provide documentation about the object under test.

They tell a story about how it expects to interact w/ the world at large.

Including private methods in this story distracts the readers from its main purpose and encourages them to break encapsulation and to depend on these methods.

Your tests should hide private methods, not expose them.

## Removing Private Methods from the Class under Test

An object w/ many private methods exudes the design smell of having too many responsibilities.

If your object has so many private methods that you dare not leave them untested, consider extracting the methods into new object.

The extracted methods form the core of the responsibilities of the new object and so make up its public interface, which is (theoretically) stable and thus safe to depend upon.

This strategy is a good one, but unfortunately is only truly helpful if the new interface is indeed stable.

Sometimes the new interface is not, and it is at this point that theory and practice part ways.

This new public interface will be exactly as stable (or as unstable) as was the original private interface.

Methods don't magically become more reliable just because they got moved.

It is costly to couple to unstable methods-regardless of whether they are portrayed as public or private.

## Choosing to Test a Private Method

Hiding smelly code is easily done; just wrap the offending code in a private method.

If you create a mess and never fix it, your costs will eventually go up, but in the short term, for the right problem, having enough confidence to write embarrassing code can save money.

When your intention is to defer a design decision, do the simplest thing that solves today's problem.

Isolate the code behind the best interface you can conceive and hunker down and wait for more information.

These tests of private methods aren't necessary in order to know that a change broke something; the public interface tests still serve that purpose admirably.

Tests of private methods produce error messages that directly pinpoint the failing parts of private code.

These more specific errors are tight couplings that increase maintenance costs, but they make it easier to understand the effects of changes, and so they take some of the pain out of refactoring complex private code.

Once the fog clears and a design reveals itself, the methods will become more stable.

As stability improves, the cost of maintaining *and* the need for tests will go down.

Eventually it will be possible to extract the private methods into a separate class and safely expose them to the world.

The rules of thumb for testing private methods are: Never write them, and if you do, never test them, unless of course it makes sense to do so.

## Testing Outgoing Messages

Outgoing messages are either *queries* or *commands*.

Query messages matter only to the object that sends them, while command messages have effects that are visible to other objects in your application.

## Ignoring Query Messages

Messages that have no side effects are known as `query` messages.

Here's a simple example, where `Gear`'s `gear_inches` method sends `diameter`.

```rb
class Gear
    # ...
    def gear_inches
        ratio * wheel.diameter
    end
end
```

Nothing in the application other than the `gear_inches` method cares that `diameter` gets sent.

The `diameter` method has no side effects, running it leaves no visible trace, and no other objects depend on its execution.

In the same way that tests should ignore messages sent to self, they also should ignore outgoing query messages.

The consequences of sending `diameter` are hidden inside of `Gear`.

B/c the overall application does not need this message to be sent, your tests need not care.

`Gear`'s `gear_inches` method depends on the result that `diameter` returns, but tests to prove the correctness of `diameter` belonging in `Wheel`, not here in `Gear`.

It is redundant for `Gear` to duplicate those tests; maintenance costs will increase if it does.

`Gear`'s only responsibility is to prove that `gear_inches` works correctly, and it can do this by simply testing that `gear_inches` always returns appropriate results.

## Proving Command Messages

Sometimes, however, *does* matter that a message gets sent; other parts of your application depend on something that happens as a result.

In this case, the object under test is responsible for sending the message and your tests must prove it does so.

Imagine a game where players race virtual bicycles. These bicycles, obviously, have gears.

The `Gear` class is now responsible for letting the application know when a player changes gears so the application can update the bicycle's behavior.

In the following code, `Gear` meets this new requirement by adding an `observer`.

When a player shifts gears, the `set_cog` or `set_chainring` methods execute.

These methods save the new value and then invoke `Gear`'s `changed` method. This method then sends `changed` to `observer`, passing along the current `chainring` and `cog`.

```rb
class Gear
    attr_reader :chainring, :cog, :wheel, :observer
    def initialize(chainring:, cog:, wheel:, observer:)
        # ...
        @observer = observer
    end

    # ...

    def set_cog(new_cog)
        @cog = new_cog
        changed
    end

    def set_chainring(new_chainring)
        @chainring = new_chainring
        changed
    end

    def changed
        observer.changed(chainring, cog)
    end
end
```

`Gear` has a new responsibility; it must notify `observer` when `cog`s or `chainring`s change.

This new responsibility is just as important as its previous obligation to calculate gear inches.

When a player changes gears the application will be correct only if `Gear` sends `changed` to `observer`.

Your tests should prove this message gets sent.

Not only should they prove it, but they also should do so w/o making assertions about the result that `observer`'s `changed` method returns.

Just as `Wheel`'s tests claimed sole responsibility for making assertions about the results of its own `diameter` method, `observer`'s tests are responsible for making assertions about the results of its `changed` method.

The responsibility for testing a message's return value lies w/ its receiver.

Doing so anywhere else duplicates tests and raises costs.

To avoid duplication, you need a way to prove that `Gear` sends `changed` to `observer` that does not force you to rely on checking what comes back when it does.

Fortunately, this is easy; you need a *mock*.

Mocks are tests of behavior, as opposed to tests of state.

Instead of making assertions about what a messages returns, mocks define an expectation that a message will get sent.

The test below proves that `Gear` fulfills its responsibilities and it does so w/o binding itself to details about how `observer` behaves.

The test creates a mock that injects in place of the observer.

Each test method tells the mock to expect to receive the `changed` message and then verifies that it did so.

```rb
class GearTest < Minitest::Test
    def setup
        @observer = Minitest::Mock.new
        @gear     = Gear.new(
            chainring: 52,
            cog:       11,
            wheel:     DiameterDouble.new,
            observer:  @observer)
    end

    def test_notifies_observers_when_cogs_change
        @observer.expect(:changed, true, [52, 27])
        @gear.set_cog(27)
        @observer.verify
    end

    def test_notifies_observers_when_chainrings_change
        @observer.expect(:changed, true. [42, 11])
        @gear.set_chainring(42)
        @observer.verify
    end
end
```

This is the classic usage pattern for a mock.

In the `notifies_observers_when_cogs_change` test above, `@observer.expect(:changed, true, [52, 27])` tells the mock what message to expect, `@gear.set_cog(27)` triggers the behavior that should cause this expectation to be met, and then `@observer.verify` asks the mock to verify that it indeed was.

The test passes only if sending `set_cog` to `gear` does something that causes `observer` to receive `changed` w/ the given arguments.

Notice that all the mock did w/ the message was remember that it received it.

If the object under test depends on the result it gets back when `observer` receives `changed`, the mock can be configured to return an appropriate value.

This return value, however, is beside the point.

Mocks are meant to prove messages get sent, they return results only when necessary to allow tests to run.

The fact that `Gear` works just fine even after you mock `observer`'s `changed` method such that it does absolutely nothing proves that `Gear` doesn't care what that method actually does.

`Gear`'s only responsibility is to send the message; this test should restrict itself to proving `Gear` does so.

In a well-designed application, testing outgoing messages is simple.

If you have proactively injected dependencies, you can easily substitute mocks.

Setting expectations on these mocks allows you to prove that the object under test fulfills its responsibilities w/o duplicating assertions that belong elsewhere.

## Testing Duck Types

This section shows how to create tests that role players can share and then returns to the original problem and uses shareable tests to prevent test doubles from becoming obsolete.

## Testing Roles

The code for this first example comes from the `Preparer` duck type of "Reducing Costs w/ Duck Typing."

Here's a reminder of the original `Mechanic`, `TripCoordinator`, and `Driver` classes:

```rb
class Mechanic
    def prepare_bicycles(bicycles)
        bicycles.each {|bicycle| prepare_bicycle(bicycle)}
    end

    def prepare_bicycles(bicycle)
        # ...
    end
end

class TripCoordinator
    def buy_food(customers)
        # ...
    end
end

class Driver
    def gas_up(vehicle)
        # ...
    end

    def fill_water_tank(vehicle)
        # ...
    end
end
```

Each of these classes has a reasonable public interface, yet when `Trip` used these interfaces to prepare a trip it was forced to check the class of each object to determine which message to send, as shown here:

```rb
class Trip
    attr_reader :bicycles, :customers, :vehicle

    def prepare(preparers)
        preparers.each {|preparer|
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
end
```

The `case` stmt above couples `prepare` to three existing concrete classes.

Imagine trying to test the `prepare` method or the consequences of adding a new kind of `preparer` into this mix.

This method is painful to test and expensive to maintain.

If you come upon code that uses this antipattern but does not have tests, consider refactoring to a better design before writing them.

It's always dangerous to make changes in the absence of tests, but this teetering pile of code is so fragile that refactoring it first might well be the most cost-effective strategy.

The refactoring that fixes this problem is simple and makes all subsequent change easier.

The first part of the refactoring is to decide on `Preparer`'s interface and to implement that interface in every player of the role.

If the public interface of `Preparer` is `prepare_trip`, the following changes allow `Mechanic`, `TripCoordinator`, and `Driver` to play the role:

```rb
class Mechanic
    def prepare_trip(trip)
        trip.bicycles.each {|bicycle|
            prepare_bicycle(bicycle)}
    end
    # ...
end

class TripCoordinator
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

Now that `Preparer`s exist, `Trip`'s `prepare` method can be vastly simplified.

The following refactoring alters `Trip`'s `prepare` method to collaborate w/ `Preparer`s instead of sending unique messages to each specific class:

```rb
class Trip
    attr_reader :bicycles, :customers, :vehicle

    def prepare(preparers)
        preparers.each {|preparer|
            preparer.prepare_trip(self)}
    end
    # ...
end
```

The above code contains a collaboration btwn `Preparer`s and a `Trip`, which can now be thought of as a `Preparable`.

Your tests should document the existence of the `Preparer` role, prove that each of its platers behaves correctly, and show that `Trip` interacts w/ them appropriately.

B/c several different classes act as `Preparer`s, the role's test should be written once and shared by every player.

Minitest is a low ceremony testing framework, and it supports sharing tests in the simplest possible way, via Ruby modules.

Here's a module that tests and documents the `Preparer` interface:

```rb
module PreparerInterfaceTest
    def test_implements_the_preparer_interface
        assert_respond_to(@object, :prepare_trip)
    end
end
```

This module proves that `@object` responds to `prepare_trip`. The test below uses this module to prove that `Mechanic` is a `Preparer`.

It includes the module and provides a `Mechanic` during setup via the `@object` variable.

```rb
class MechanicTest < Minitest::Test
    include PreparerInterfaceTest

    def setup
        @mechanic = @object = Mechanic.new
    end

    # other tests which rely on @mechanic
end
```

The `TripCoordinator` and `Driver` tests follow this same pattern.

They also include the module and initialize `@object` in their setup methods.

```rb
class TripCoordinatorTest < Minitest::Test
    include PreparerInterfaceTest

    def setup
        @trip_coordinator = @object = TripCoordinator.new
    end
end

class DriverTest < Minitest::Test
    include PreparerInterfaceTest

    def setup
        @driver = @object = Driver.new
    end
end
```

Defining the `PreparerInterfaceTest` as a module allows you to write the test once and then reuse it in every object that plays the role.

The module serves as a test and as a documentation.

It raises the visibility of the role and makes it easy to prove that any newly created `Preparer` successfully fulfills its obligations.

The `test_implements_the_preparer_interface` method tests an incoming message and as such belongs w/ the receiving object's tests, which is why the module gets included in the tests of `Mechanic`, `TripCoordinator`, and `Driver`.

Incoming messages, however, go hand-in-hand w/ outgoing messages and you must test both sides of this equation.

You have proven that all receivers correctly implement `prepare_trip`; now you must also prove that `Trip` correctly sends it.

As you know, proving that an outgoing message gets sent is done by setting expectations on a mock.

The following test creates a mock, tells it to expect `prepare_trip`, triggers `Trip`'s `prepare` method, and then verifies that the mock received the proper message.

```rb
class TripTest < Minitest::Test

    def test_requests_trip_preparation
        preparer = Minitest::Mock.new
        trip     = Trip.new([], [], [])

        preparer.expect(:prepare_trip, nil, [trip])

        trip.prepare([preparer])
        preparer.verify
    end
end
```

The `test_requests_trip_preparation` test lives directly in `TripTest`. 

`Trip` is the only `Preparable` in the application so there's no other object w/ which to share this test. 

If other `Preparables` arise the test should be extracted into a module and shared among `Preparables` at that time.

Running this test proves that `Trip` collaborates w/ `Preparers` using the correct interface.

This completes the tests of the `Preparer` role. It's now possible to return to the problem of brittleness when using doubles to play roles in tests.

## Using Role Tests to Validate Doubles

Now that you know how to write reusable tests that prove an object correctly plays a role, you can use this technique to reduce the brittleness caused by stubbing.

Recall:

```rb
class DiameterDouble
    def diameter    # ← Wrong implementation of
        10          # ← Diameterizable API
    end
end

class GearTest < Minitest::Test
    def test_calculates_gear_inches
        gear = Gear.new(
            chainring: 52,
            cog:       11,
            wheel:     DiameterDouble.new
        )

        assert_in_delta(47.27, gear.gear_inches, 0.01)
    end
end
```

The problem w/ this test is that `DiameterDouble` purports to play the `Diameterizable` role but it does so incorrectly.

Now that `Diameterizable`'s interface has changed, `DiameterDouble` is out of date.

This obsolete double enables the test to bumble along in the mistaken belief that `Gear` works correctly, when in actual fact `GearTest` only works when combined w/ its similarly confused test double.

This application is broken but you cannot tell by running this test.

You last saw `WheelTest` in the "Using Tests to Document Roles" section, where it was attempting to counter this problem by raising the visibility of `Diameterizable`'s interface.

Here's an example where line `test_implements_the_diameterizable_interface` proves that `Wheel` acts like a `Diameterizable` that implements `width`:

```rb
class WheelTest < Minitest::Test
    def setup
        @wheel = @object = Wheel.new(26, 1.5)
    end

    def test_implements_the_diameterizable_interface
        assert_respond_to(@wheel, :width)
    end

    def test_calculates_diameter
        # ...
    end
end
```

W/ this test, you now hold all the pieces needed to solve the brittleness problem.

You know how to share tests among players of a role, you recognize that you have two players of the `Diameterizable` role, and you have a test that any object can use to prove that it correctly plays the role.

The first step in solving the problem is to extract `test_implements_the_diameterizable_interface` from `Wheel` into a module of its own:

```rb
module DiameterizableInterfaceTest
    def test_implements_the_diameterizable_interface
        assert_respond_to(@object, :width)
    end
end
```

Once this module exists, reintroducing the extracted behavior back into `Wheel - Test` is a simple matter of including the module and initializing `@object` w/ a `Wheel`:

```rb
class WheelTest < Minitest::Test
    include DiameterizableInterfaceTest

    def setup
        @wheel = @object = Wheel.new(26, 1.5)
    end

    def test_calculates_diameter
        # ...
    end
end
```

At this point `WheelTest` works just as it did before the extraction, as you can see by running the test.

This refactoring serves a broader purpose than that of merely rearranging the code.

Now that you have an independent module that proves that a `Diameterizable` behaves correctly, you can use the module to prevent test doubles from silently becoming obsolete.

The `GearTest` below has been updated to use this new module. `class DiameterDoubleTest` defines a new test class, `DiameterDoubleTest`. `DiameterDoubleTest` is not a `Gear` per se; its purpose is to prevent test brittleness by ensuring the ongoing soundness of the double.

```rb
class DiameterDouble
    def diameter
        10
    end
end

# Prove the test double honors the interface this expects.
class DiameterDoubleTest < MiniTest::Unit::TestCase
    include DiameterizableInterfaceTest

    def setup
        @object = DiameterDouble.new
    end
end

class GearTest < MiniTest::Unit::TestCase
    def test_calculates_gear_inches
        gear = Gear.new(
            chainring: 52,
            cog:       11,
            wheel:     DiameterDouble.new)
        
        assert_in_delta(47.27, gear.gear_inches, 0.01)
    end
end
```

The face that `DiameterDouble` and `Gear` are both incorrect has been allowing previous versions of this test to pass.

Now that the double is being tested to ensure it honestly plays its role, running the test finally produces an error.

The `GearTest` still passes erroneously, but that's no longer a problem b/c `DiameterDoubleTest` now informs you that `DiameterDouble` is wrong.

This failure causes you to correct `DiameterDouble` to implement `width`:

```rb
class DiameterDouble
    def width
        10
    end
end
```

After this change, re-running the test produces the desired failure in `GearTest`.

Now that `DiameterDoubleTest` passes, `GearTest` fails.

This failure points directly to the offending line of code in `Gear`.

The tests finally tell you to change `Gear`'s `gear_inches` method to send `width` instead of `diameter`, as in this example:

```rb
class Gear
    # ...
    def gear_inches
        ratio * wheel.width # `width` instead of `diameter`
    end
end
```

Once you make this final change, the application is correct and the tests pass.

Not only does this test pass, but it will continue to pass (or fail) appropriately, no matter what happens to the `Diameterizable` interface.

When you treat test doubles as you would any other role player and test them to prove their correctness, you avoid brittleness and can stub w/o fear of consequence.

The desire to test duck types creates a need for shareable tests for roles, and once you acquire this role-based perspective, you can use it to your advantage in many situations.

From the pov of the object under test, every other object is a role, and dealing w/ objects as if they are representatives of the roles they play loosens coupling and increases flexibility, both in your application and in your tests.

Having to write your own role tests is the price you pay for the benefits of dynamic typing.

In statically typed languages you can lean on the compiler to enforce the interfaces of roles, but in dynamically typed languages, roles are *virtual*.

If you fear that human communication will be insufficient to keep all players of a role in sync, write these tests.

## Testing Inherited Code

The example used here is the final `Bicycle` hierarchy from "Acquiring Behavior through Inheritance."

Even though that hierarchy eventually proved unsuitable for inheritance, the underlying code is fine and serves admirably as a basis for these tests.

## Specifying the Inherited Interface

```rb
class Bicycle
    attr_reader :size, :chain, :tire_size

    def initialize(**opts)
        @size      = opts[:size]
        @chain     = opts[:chain]       || default_chain
        @tire_size = opts[:tire_size]   || default_tire_size
        post_initialize(opts)
    end

    def spares
        {tire_size: tire_size,
         chain:     chain}.merge(local_spares)
    end

    def default_tire_size
        raise NotImplementedError
    end

    # subclasses may override
    def pos_initialize(opts)
    end

    def local_spares
        {}
    end

    def default_chain
        "11-speed"
    end
end
```

Here is the code for `RoadBike`, one of `Bicycle`'s subclasses:

```rb
class RoadBike < Bicycle
    attr_reader :tape_color

    def post_initialize(opts)
        @tape_color = opts[:tape_color]
    end

    def local_spares
        { tape_color: tape_color }
    end

    def default_tire_size
        "23"
    end
end
```

The first goal of testing is to prove that all objects in this hierarchy honor their contract.

The Liskov Substitution Principle declares that subtypes should be substitutable for their subtypes.

Violations of Liskov result in unreliable objects that don't behave as expected.

The easiest way to prove that every object in the hierarchy obeys Liskov is to write a shared test for the common contract and include this test in every object.

The contract is embodied in a shared interface.

The following test articulates the interface and therefore defines what it means to be a `Bicycle`:

```rb
module BicycleInterfaceTest
    def test_responds_to_default_tire_size
        assert_respond_to(@object, :default_tire_size)
    end

    def test_responds_to_default_chain
        assert_respond_to(@object, :default_chain)
    end

    def test_responds_to_chain
        assert_respond_to(@object, :chain)
    end

    def test_responds_to_size
        assert_respond_to(@object, :size)
    end

    def test_responds_to_tire_size
        assert_respond_to(@object, :tire_size)
    end

    def test_responds_to_spares
        assert_respond_to(@object, :spares)
    end
end
```

Ny object that passes the `BicycleInterfaceTest` can be trusted to act like a `Bicycle`.

All of the classes in the `Bicycle` hierarchy must respond to this interface and should be able to pass this test.

The following example includes this interface test in the abstract superclass `BicycleTest`, and in the concrete subclass `RoadBikeTest`:

```rb
class BicycleTest < MiniTest::Test
    include BicycleInterfaceTest

    def setup
        @bike = @object = Bicycle.new({tire_size: 0})
    end
end

class RoadBikeTest < MiniTest::Test
    include BicycleInterfaceTest

    def setup
        @bike = @object = RoadBike.new
    end
end
```

The `BicycleInterfaceTest` will work for every kind of `Bicycle` and can be easily included in any new subclass.

It documents the interface and prevents accidental regressions.

## Specifying Subclass Responsibilities

Not only do all `Bicycles` share a common interface, the abstract `Bicycle` super-class imposes requirements upon its subclasses.

**Confirming Subclass Behavior**

B/c there are many subclasses, they should share a common test to prove that ach meets the requirements.

Here's a test that documents the requirements for subclasses:

```rb
module BicycleSubclassTest
    def test_responds_to_post_initialize
        assert_respond_to(@object, :post_initialize)
    end

    def test_responds_to_local_spares
        assert_respond_to(@object, :local_spares)
    end

    def test_responds_to_default_tire_size
        assert_respond_to(@object, :default_tire_size)
    end
end
```

This test codifies the requirements for subclasses of `Bicycle`.

It doesn't force subclasses to implement these methods; in fact, any subclass is free to inherit `post_initialize` and `local_spares`.

This test just proves that a subclass does nothing so crazy that it causes these messages to fail.

The only method that must be implemented by subclasses is `default_tire_size`.

The superclass implementation of `default_tire_size` raises an error; this test will fail unless the subclass implements its own specialized version.

`RoadBike` acts like a `Bicycle`, so its test already includes the `BicycleInterfaceTest`.

The test below has been changed to include the new `BicycleSubclassTest`; `RoadBike` should also act like a subclass of `Bicycle`.

```rb
class RoadBikeTest < MiniTest::Test
    include BicycleInterfaceTest
    include BicycleSubclassTest

    def setup
        @bike = @object = RoadBike.new
    end
end
```

Running this modified test tells an enhanced story.

Every subclass of `Bicycle` can share these same two modules, b/c every subclass should act both like a `Bicycle` and like a subclass of `Bicycle`.

Even though it's been a while since you've seen the `MountainBike` subclass, you can surely appreciate the ability to ensure that `MountainBikes` are good citizens by simply adding these two modules to its test, as shown here:

```rb
class MountainBikeTest < MiniTest::Test
    include BicycleInterfaceTest
    include BicycleSubclassTest

    def setup
        @bike = @object = MountainBike.new
    end
end
```

The `BicycleInterfaceTest` and the `BicycleSubclassTest`, combined, take all of the pain out of testing the common behavior of subclasses.

These tests give you confidence that subclasses aren't drifting away from the standard, and they allow novices to create new subclasses in complete safety.

Newly arrived programmers don't have to scour the superclasses to unearth requirements, they can just include these tests when they write new subclasses.

**Confirming Superclass Enforcement**

The `Bicycle` class should raise an error if a subclass does not implement `default_tire_size`.

Even though this requirement applies to subclasses, the actual enforcement behavior is in `Bicycle`.

The test is therefore placed directly in `BicycleTest`, as shown on line `test_forces_subclasses_to_implement_default_tire_size` below:

```rb
class BicycleTest < Minitest::Test
    include BicycleInterfaceTest

    def setup
        @bike = @object = Bicycle.new({tire_size: 0})
    end

    def test_forces_subclasses_to_implement_default_tire_size
        assert_raises(NotImplementedError) {@bike.default_tire_size}
    end
end
```

Notice that line `@bike = @object = Bicycle.new({tire_size: 0})` of `BicycleTest` supplies a tire size, albeit an odd one, at `Bicycle` creation time.

If you look back at `Bicycle`'s `initialize` method you'll see why.

The `initialize` method expects to either receive an input value for `tire_size` or to be able to retrieve one by subsequently sending the `default_tire_size` message.

If you remove the `tire_size` argument from line `@bike = @object = Bicycle.new({tire_size: 0})`, this test dies in its `setup` method while creating a `Bicycle`.

W/o this argument, `Bicycle` can't successfully get through object initialization.

The `tire_size` argument is necessary b/c `Bicycle` is an abstract class that does not expect to receive the `new` message.

`Bicycle` doesn't have a nice, friendly creation protocol.

It doesn't need one b/c the actual application never creates instances of `Bicycle`.

However, the fact that the application doesn't create new `Bicycle`s doesn't mean this never happens.

It surely does. Line `@bike = @object = Bicycle.new({tire_size: 0})` of the `BicycleTest` above clearly creates a new instance of this abstract class.

The problem is ubiquitous when testing abstract classes.

The `BicycleTest` needs an object on which to run tests and the most obvious candidate is an instance of `Bicycle`.

However, creating a new instance of an abstract class can range from difficult to impossible.

This test if fortunate in that `Bicycle`'s creation protocol allows the test to create a concrete `Bicycle` instance by passing `tire_size`, but creating a testable object is not always this easy, and you may find it necessary to employ a more sophisticated strategy.

Fortunately, there's an easy way to overcome this general problem that will be covered below in the section "Testing Abstract Superclass Behavior."

For now, supplying the `tire_size` argument works just fine.

Running `BicycleTest` now produces output that looks more like that of an abstract superclass.

## Testing Unique Behavior

The inheritance tests have so far concentrated on testing common qualities.

Most of the resulting tests were shareable and ended up being placed in modules (`BicycleInterfaceTest` and `BicycleSubclassTest`), although one test (`forces_subclasses_to_implement_default_tire_size`) did get placed directly into `BicycleTest`.

Now that you have dispensed w/ the common behavior, two gaps remain. There are as yet no tests for specializations, neither for the ones provided by the concrete subclasses nor for those defined in the abstract superclass.

The following section concentrates on the first; it tests specializations supplied by individual subclasses.

The section after moves the focus upward in the hierarchy and tests behavior that is unique to `Bicycle`.

**Testing Concrete Subclass Behavior**

Now is the time to renew your commitment to writing the absolute minimum number of tests.

Look back at the `RoadBike` class.

The shared modules already prove most of its behavior.

The only thing left to test are the specializations that `RoadBike` supplies.

It's important to test these specializations w/o embedding knowledge of the superclass into the test.

For example, `RoadBike` implements `local_spares` and also responds to `spares`.

The `RoadBikeTest` should ensure that `local_spares` works while maintaining deliberate ignorance about the existence of the `spares` method.

The shared `BicycleInterfaceTest` already proves that `RoadBike` responds correctly to `spares`; it is redundant and ultimately limiting to reference that method directly in this test.

The `local_spares` method, however, is clearly `RoadBike`'s responsibility. Line `test_puts_tape_color_in_local_spares` below tests this specialization directly in `RoadBikeTest`:

```rb
class RoadBikeTest < MiniTest::Test
    include BicycleInterfaceTest
    include BicycleSubclassTest

    def setup
        @bike = @object = RoadBike.new(tape_color: "red")
    end

    def test_puts_tape_color_in_local_spares
        assert_equal "red", @bike.local_spares[:tape_color]
    end
end
```

Running `RoadBikeTest` now shows that it meets its common responsibilities and also supplies it own specializations.

**Testing Abstract Superclass Behavior**

Now that you have tested the subclass specializations, it's time to step back and finish testing the superclass.

Moving your focus up the hierarchy to `Bicycle` reintroduces a previously encountered problem; `Bicycle` is an abstract superclass. Creating an instance of Bicycle not only is hard, but the instance might not have all the behavior you need to make the test run.

Fortunately, your design skills provide a solution.

B/c `Bicycle` used template methods to acquire concrete specializations, you can stub the behavior that would normally be supplied by subclasses.

Even better, b/c you understand the Liskov Substitution Principle, you can easily manufacture a testable instance of `Bicycle` by creating a new subclass for use solely by this test.

The test below follows just such a strategy. Line `class BikeDouble < Bicycle` defines a new class, `BikeDouble`, as a subclass of `Bicycle`.

The test creates an instance of this class `@double = BikeDouble.new` and uses it to prove that `Bicycle` correctly includes the subclass's `local_spares` contribution in `spares`.

It remains convenient to sometimes create an instance of the abstract `Bicycle` class, even though this requires passing the `tire_size` argument, as on line `@bike = @object = Bicycle.new({tire_size: 0})`.

This instance of `Bicycle` continues to be used in the test on `@bike.default_tire_size` to prove that the abstract class forces subclasses to implement `default_tire_size`.

These two kinds of `Bicycle` coexist peacefully in the test, as shown here:

```rb
class BikeDouble < Bicycle
    def default_tire_size
        0
    end

    def local_spares
        { saddle: "painful"}
    end
end

class BicycleTest < Minitest::Test
    include BicycleInterfaceTest

    def setup
        @bike = @object = Bicycle.new({tire_size: 0})
        @double = BikeDouble.new
    end

    def test_forces_subclasses_to_implement_default_tire_size
        assert_raises(NotImplementedError) {
            @bike.default_tire_size}
    end

    def test_includes_local_spares_in_spares
        assert_equal @double.spares,
            { tire_size: 0,
              chain:     "11-speed",
              saddle:    "painful" }
    end
end
```

The idea of creating a subclass to supply stubs can be helpful in many situations.

As long as your new subclass does not violate Liskov, you can use this technique in any test you like.

Running `BicycleTest` now proves that it includes subclass contributions on the `spares` list.

One last point: if you fear that `BikeDouble` will become obsolete and permit `BicycleTest` to pass when it should fail, the solution is close at hand.

There is already a common `BicycleSubclassTest`.

Just as you used the `Diameterizable` `InterfaceTest` to guarantee `DiameterDouble`'s continued good behavior, you can use `BicycleSubclassTest` to ensure the ongoing correctness of `BikeDouble`.

Add the following code to `BicycleTest`:

```rb
class BikeDoubleTest < Minitest::Test
    include BicycleSubclassTest

    def setup
        @object = BikeDouble.new
    end
end
```

Carefully written hierarchies are easy to test. Write one shareable test for the overall interface and another for the subclass responsibilities.

Diligently isolate responsibilities.

Be especially careful when testing subclass specializations to prevent knowledge of the superclass from leaking down into the subclass's test.

Testing abstract superclasses can be challenging; use the Liskov Substitution Principle to your advantage.

If you leverage Liskov and create new subclasses that are used exclusively for testing, consider requiring these subclasses to pass your subclass responsibility test to ensure they don't accidentally become obsolete.

## Summary

The best tests are loosely coupled to the underlying code and test everything at once and in the proper place.
