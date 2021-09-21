# Breaking Abstraction Level

Automated unit test suites can have a tendency towards fragility, breaking for reasons not related to what the test is testing.

It isn’t always a problem with our system design, though.

Sometimes fragility can come about because of problems in our tests.

```rb
def test_world_is_not_empty_after_adding_a_cell
    world = World.empty
    world.set_living_at(Location.new(1,1))
    assert_false world.empty?
end
```

Our test talks about the world being empty and adding cells. However, looking at the test code, we can see details about the topology: the `(1, 1)` tuple.

We want to strive to have our tests be concise and clear about the behavior we are describing. However, in this case, our test code is implying that the `empty?` method is somehow dependent on the coordinates, themselves.

This is an example of breaking the level of abstraction. We are testing the behavior of the world, but we are including details that it isn't concerned w/.

If the actual topology knowledge is encapsulated in the location object, then the world should be relying on that object to manage those particulars.

By tying this test to concrete implementation of 2 dimensions, via the `(1, 1)` tuple, rather than the `Location abstraction`, we are laying the groundwork for fragile tests: change the topology and tons of tests fail that are not related to the coordinate system.

This coupling can be seen as another example of duplication: spreading the knowledge of the topology not just throughout the code, but also throughout the test suite.

To improve this, we work to hide the details of the topology from the world object.

One way to do this is to use a stand-in, a test double for the location object. This can be as simple as creating a new, plain object.

```rb
def test_world_is_not_empty_after_adding_a_cell
    world = World.empty
    world.set_living_at(Object.new)
    assert_false world.empty?
end
```

Or, if you don’t like the use of test doubles, you can use a builder method that provides a location w/o exposing implementation details.

```rb
def test_world_is_not_empty_after_adding_a_cell
    world = World.empty
    world.set_living_at(Location.random)
    assert_false world.empty?
end
```

Note: We could have used a more concrete location, like `Location.center`, but we aren’t guaranteed that our grid has a center, especially if it is infinite.

By isolating ourselves from changes to the topology, the internals of the `Location`, we help ensure that this test won’t break if we change something about the underlying coordinate system.

We also emphasize that the actual coordinates of the location are irrelevant in this test.

Personally, I like to use a test double in this case, as it highlights that we aren’t using any specific attributes of the location object.

And, if we find that we need some interaction with the location, we can specify it as constraints on the double.

The result is that our test clearly expresses what behaviors of the location object we depend on.

If we want to be even more explicit, we can give the test double a name. This can increase the readability of the test.

```rb
def test_world_is_not_empty_after_adding_a_cell
    world = World.empty
    world.set_living_at(double(:location_of_cell))
    assert_false world.empty?
end
```

By using a test double, we gain feedback that can help minimize the coupling of the behavior under test: we must be explicit about every integration.

B/c we have to specify the coupling points, we can be clear and confident about how many touch points our objects have with each other.

This helps identify and abstraction problems; for example, if this test needs 3 methods stubbed on the location double, then that is a potential indication that we are missing an abstraction, or perhaps `set_living_at` is doing too much.
