# 4 Rules of Simple Design

1. Tests Pass

2. Expresses Intent

3. No Duplication (DRY)

4. Small

## 1. Tests Pass

Focusing on correctness and verification from the perspective of "easier to change".

### 1.1 Don't have tests depend on previous tests

```rb
def test_an_empty_world_stays_empty_after_a_tick
    world = World.new
    next_world = world.tick
    assert_true next_world.empty?
end
```

There is a subtle problem w/ this test - how do we know that a newly-initialized `World` is empty?

The test name indicates we are starting w/ an empty world, but the test code does not specify this explicitly.

We talked about having our test names correspond to the test code in a previous example.

Is there a problem here, though? We do have another test verifying this. And there is our problem.

This test implicitly depends on the validity of a different, previous test: there is an assumption here that new worlds are empty.

This causes a subtle, but important, problem; that lack of explicitness, combined with the coupling to the previous test, makes this test
contribute to a fragile test suite.

What happens if we change the parameters around a new world? 

What if we decide to make it not empty, but rather start with a stable structure, such as the block?

In that case, our original “new world is empty” test fails, as it should.

However, we’ll get another failure “an empty world stays empty after a tick”.

We want test failures to be explicit, quickly and effectively pointing us to the problem. 

How should we resolve this?

Let’s look back at the idea of letting the test name influence the test code and use that to make the test code a bit more explicit.

Rather than riding with the assumption that a new world is empty, let’s explicitly ask for an empty world.

```rb
def test_an_empty_world_stays_empty_after_a_tick
    world = World.empty
    next_world = world.tick
    assert_true next_world.empty?
end
```

Now, if we change the default constructor to return something other than an empty world, this test will continue to pass.

Only if we change what we mean by an empty world, created by `World.empty`, will this test fail.

And, if we do that in such a way that the next world isn’t empty, then this test will fail.

And it should, because the statement we are verifying will no longer be true.

In fact, over time I’ve developed a guideline for myself that external callers can’t actually use the base constructor for an object.

Put another way: the outside world can’t use `new` to instantiate an object with an expectation of a specific state.

Instead, there must be an explicitly named builder method on the class to create an object in a specific, valid state.

## 2. Expresses Intent

It is easy for the names we give things to stray from what they represent.

One of the most important qualities of a codebase, when it comes to change, is how quickly you can find the part that should be changed.

The first step is identifying the code related to the functionality we are addressing.

Paying attention to the names and how our code expresses itself is the key to making our lives easy when we come back to it.

Also, over time, as we change the functionality of our system, classes and methods can become filled w/ unrelated behaviors.

This makes it difficult to have the name effectively express their intent.

As we start to see structures getting large, the difficulty in finding an expressive name is a red flag what it is doing to much and should be refactored.

### 2.1 Test names should influence Object's API

The following two tests are seen quite often in Conway’s Game of Life:

```rb
def test_a_new_world_is_empty
    world = World.new
    assert_equal 0, world.living_cells.count
end

def test_a_cell_can_be_added_to_the_world
    world = World.new
    world.set_living_at(1, 1)
    assert_equal 1, world.living_cells.count
end
```

If we look at it from the idea that the tests should express intent, then there is an obvious mismatch between the test names and the code in the test.

In the first test, the test name talks about an empty world.

The test code, though, has no concept of an empty world, no mention of an empty world.

Instead, it is brutally reaching into the object, yanking out some sort of collection (only a lack of living cells represents that the world is empty?) and counting it.

When we write our tests, we should be spending time on our test names.

We want them to describe both the behavior of the system and the way we expect to use the component under test.

When starting a new component, we can use our test names to influence and mold our API.

Think of the test as the first consumer of the component, interacting with the object the same way as the rest of the system.

Let the code in the test be a mirror of the test description. How about something like this:

```rb
def test_a_new_world_is_empty
    world = World.new
    assert_true world.empty?
end
```

This hides the internals of the object, while building up a usable API for the rest of the system to consume.

Let's look at the second test:

```rb
def test_a_cell_can_be_added_to_the_world
    world = World.new
    world.set_living_at(1, 1)
    assert_equal 1, world.living_cells.count
end
```

The test name talks about adding to the world, but the verification step isn't looking for the cell that was added. It is simply looking to see if a counter was incremented on some internal collection.

Let's apply the symmetry again and have the test code actually reflect what we say is being tested.

```rb
def test_a_cell_can_be_added_to_the_world
    world = World.new
    world.set_living_at(1, 1)
    assert_true world.alive_at?(1, 1)
end
```

This now adds to our API. Additional tests, of course, will flesh out the behavior of these methods, but we now have begun to build up the usage pattern for this object.

We also could add a test around the `empty?` method using `set_living_at`.

```rb
def test_after_adding_a_cell_the_world_is_not_empty
    world = World.new
    world.set_living_at(1, 1)
    assert_false world.empty?
end
```

This is another way of slowly building up the API, especially the beginnings of the `set_living_at` behavior.

Focusing on the symmetry btwn a good test name and the code under tests is a subtle design technique. It is definitely not the only design influence that our tests can have on our code, but it can be an important one.

So, next time you are flying through your TDD cycle, take a moment to make sure that you are actually testing what you say you are testing.

## 3. No Duplication (DRY)

This rule isn't about code duplication; it is about *knowledge* duplication.

The DRY principle states "Every piece of knowledge should have one and only one representation." This rule also has been expressed as "Once and Only Once."

Instead of looking for code duplication, always ask yourself whether or not the duplication you see is an example of core knowledge in the system.

## 4. Small

Once we've applied the above rules, it is important to look back and make sure that we don't have any extraneous pieces.

* Do I have any vestigial code that is no longer used?

Sometimes, as we are working through our system, we build things that aren't used in the final product. Maybe they seemed like a good idea at the time, but the capability never came to fruition. If so, no questions asked, just delete that.

* Do I have any duplicate abstractions?

In the course of refactoring, we often end up extracting abstractions, whether they be methods or new types.

While we strive to keep duplication down, per the DRY principle, sometimes we find that we missed something. 

Perhaps the duplication is far apart in the codebase. Perhaps it is was hard to see the similarity when focused on the small. Take a moment to see if you notice anything now. If so, combine them.

Sometimes, though, it isn’t that the full abstractions are duplicate, but just that they have some similar characteristics,
perhaps a behavior, or two. If so, then we might be missing another common abstraction that they can rely on. Don’t wait, extract it.

* Have I extracted too far?

A common case of this is when we extract a method for readability, to better express our intent.

However, once we are done w/ the rest of our cleanup, we can inline the extracted method.

This is a great example of the fluidity of a codebase's expressiveness over time.

An important thing to realize about these rules is that they iterate over each other. Frequently, fixing a naming issue will uncover some duplication. Eliminating that duplication will then reveal some expressiveness that can be improved.
