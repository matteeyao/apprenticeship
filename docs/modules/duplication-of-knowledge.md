# Duplication of Knowledge about Topology

Given that we are building w/ a cell abstraction, we can start to think about their locations.

A common next step is to set certain cells to be alive at a given location, check for living cells at a location, etc.

A common, and pretty reasonable, implementation is to have something like a `World` class that contains these behaviors.

A naive implementation might look at our 2D grid and build the methods directly.

```rb
class World
    def set_living_at(x, y)
        #...
    end
    def alive_at?(x, y)
        #...
    end
end
```

And, of course, we might decide to add the coordinates to our `Cell` classes. After all, the cells are placed at a certain location.

```rb
class LivingCell
    attr_reader :x, :y
end
class DeadCell
    attr_reader :x, :y
end
```

On the surface, this seems okay. But, there is a subtle, not always obvious duplication of knowledge here: knowledge of our topology.

A good way to detect knowledge duplication is to ask what happens if we want to change something. What effort is required? How many places will we need to look at and change? For example, what if we want to change our topology to 3 dimensions?

In our design, we would have quite a few places to change.

This is duplication of knowledge; we have spread the knowledge of our topology - the fact that we are working on a 2-dimensional grid - all over the codebase.

Eliminating this duplication relies on a strategy of *reification*. This is the act of taking a concept and making it real by extraction. So, let's extract the `x, y` to create a `Location` abstraction.

```rb
class Location
    attr_reader :x, :y
end
```

Now, doing this gives us a way to eliminate our duplication.

```rb
class World
    def set_living_at(location)
        # ...
    end
    def alive_at?(location)
        # ...
    end
end
class LivingCell
    attr_reader :location
end
class DeadCell
    attr_reader :location
end
```

By isolating this knowledge, we have made it easier to handle any change in our topology.

Our code becomes more adaptable, along w/ making it much more clear.

While we looked at this refactoring from the perspective of duplication, we can also approach this as a naming problem: a lack of effectively expressing our intent.

```rb
class World
    def set_living_at(x, y)
        #...
    end
end
```

should be

```rb
class World
    def set_living_at(location)
        #...
    end
end
```

This then tells us that this parameter represents a single object, an instance of something.
