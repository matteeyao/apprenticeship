# Testing State vs Testing Behavior

```rb
def test_a_world_starts_out_empty
    world = World.new
    assert_true world.empty?
end
```

```rb
def test_world_is_not_empty_after_setting_a_living_cell
    world = World.new
    location = Location.random
    world.set_living_at(location)
    assert_false world.empty?
end
```

The tests above follow a natural progression, but they lead to a very state-focused test suite.

We are doing something, then checking what, if any, state change occurred.

An alternative way to develop a system is to focus on the behavior rather than the state of the objects.

Think about what behaviors you expect and have our tests center around those.

The idea of "focusing on behavior" is a common topic in software development conversations, but it isn't always that clear how to do it.

Building our system in a behavior-focused way is about only building the things that are absolutely needed and only at the time they are needed.

This way, we end up w/ a system that has just enough code to support our use cases. 

When thinking of something to build, ask yourself a simple question: "What behavior of my system requires this?"

Once you answer that question, move to building that behavior. In this case, this formula generates two questions:

* How do we know that we want to set an individual cell?

* How do we know that we want to check that the world is empty?

Once we answer these questions - usually w/ a statement that "this behavior will need it" - we can take a step back and build our tests around that behavior.

Why do we need to set an individual cell? Above, we said that this might be how we set up the initial pattern. This leads to another question.

Why do we need the initial pattern? The point of the game is to calculate the next generation.

And there is where we have identified a fundamental behavior: calculating the next generation.

In our system, this fundamental behavior happens w/ the tick, moving to the next generation. This is what triggers everything.

So, let's start testing that. Then, as it needs behaviors, we can build those.

So, what is a very simple thing we say about a tick? The empty world should tick into another empty world.

```rb
def test_an_empty_world_stays_empty_after_a_tick
end
```

Now a question has come up. We just had a question about having our first test be about checking that a new world was empty. And, it seems like we've moved ourselves into a position where we need to do this.

Since the test dictates that we start w/ an empty world, we probably should postpone this test and make sure that a new world is empty, so we can write the original test.

```rb
def test_a_new_world_is_empty
    assert_true World.new.empty?
end
```

After this, we can move to our original test. We know that a new world is empty. So, we can fill our behavior-focused test with that knowledge.

```rb
def test_an_empty_world_stays_empty_after_a_tick
    world = World.new
    next_world = world.tick
    assert_true next_world.empty?
end
```
