# Behavior Attractors

```rb
class World
    def set_living_at(x, y)
        #...
    end
    def alive_at?(x, y)
        #...
    end
end
class Cell
    attr_reader :x, :y
    def alive_in_next_generation?
        # run rules
    end
end
```

Imagine that we are happily moving along w/ this design when we find ourselves in need of asking for the neighbor locations for a given `x, y`. Perhaps we want something like the following method:

```rb
def neighbors_of(x, y)
    # calculate the coordinates of neighbors
end
```

You know you need a behavior, but there is a bit of confusion around its proper place.

There could be a much more natural place than `World` or `Cell`.

Previously, we eliminated the knowledge of duplication around the location, reifying a `Location` concept.

Had we done this here, we might find that we already have a place that is just right.

```rb
class Location
    attr_reader :x, :y
end
```

Our other classes reference `Location` and rely on it to be entirely focused on the topology.

What better place to put a behavior than the type that is concerned about the topology?

Our behavior is really about asking for what locations constitute the neighborhood around a given location.

Sounds like a natural behavior for the `Location` class.

```rb
class Location
    attr_reader :x, :y
    def neighbors
        # calculate a list of locations
        # that are considered neighbors
    end
end
```

This is an example of what I call a **Behavior Attractor**.

By aggressively eliminating knowledge duplication through reification, we often find that we have built classes that naturally accept
new behaviors that arise.

They not only accept, but *attract* them; by the time we are looking to implement a new behavior, there is already a type that is an obvious place to put it.

As a corollary to this, we can use this idea to notice potentially missing abstractions. If we are working on a new behavior, but are
not sure where to place it — what object it belongs to — this might be an indication that we have a concept that isn’t expressed well in our system.
