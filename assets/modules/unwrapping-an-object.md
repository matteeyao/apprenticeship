# Unwrapping an Object

By eliminating the ability to return values from our functions, we force ourselves to rely instead on telling objects to enact behaviors.

Another side effect of this constraint is that you no longer can have properties on your objects - no methods for querying the internal state. By eliminating the ability to query for data, we being to build objects that are tightly encapsulated. We can rely on the objects alone to manage their internal state.

```rb
class Location
    attr_reader :x, :y
    def equals?(other_location)
        self.x == other_location.x &&
        self.y == other_location.y
    end
end

location1.equals?(location2)
```

But, this `equals?` method doesn't conform to our constraint: it is asking `other_location` to *return* its x and y. This isn't allowed.

This can be a very common sticking point. Most of us have been trained to use properties to access internal state of an object. We pretend, of course, that we are using properties to encapsulate state, but really it is just a way to allow the outside world to reach inside us and do what they want. In a world where you can’t return anything, though, how do you get around this?

The key idea is in a technique that I call **unwrapping**. Take a look at the following alternate form of equals.

```rb
class Location
    attr_reader :x, :y
    def equals?(other_location)
        other_location.equals_coordinate?(self.x, self.y)
    end
    def equals_coordinate?(other_x, other_y)
        self.x == other_x && self.y == other_y
    end
end
```

Inside the first object (*location1*), we have access to our own internals. Rather than taking the approach of asking the other object (*location2*) for its internals, let's just pass our own to it. So, we are comparing internals w/o having to reach inside the other object.

Of course, in a language w/ signature-based overloading, you wouldn't have to have two methods.

```rb
public class Location
{
    private int x;
    private int y;
    public boolean Equals(Location otherLocation) {
        return otherLocation.Equals(this.x, this.y);
    }
    public boolean Equals(int otherX, int otherY) {
        return this.x == otherX && this.y == otherY
    }
}
```

But, wait, doesn't `equals?` return a boolean? The constraint if that we can't return anything. So, we are violating that.

This is true. Now that we have a way to do the comparison w/o querying for an object's state, we can tackle this aspect.

Let's take a step back and look at this from a behavioral point of view, returning to the fundamental question "why do we need this behavior?" or, "why do we care if they are equal?"

In general, we look for equality in order to react in a certain way. So, if they are equal, we'll do something. As a simple example, let's increment a counter.

Since we can't return the boolean, let's rewrite our code to remove that. In Ruby, every method returns something, so we have to be explicit to get rid of the boolean return.

```rb
class Location
    attr_reader :x, :y
    def equals?(other_location)
        other_location.equals_coordinate?(self.x, self.y)
        nil
    end
    def equals_coordinate?(other_x, other_y)
        self.x == other_x && self.y == other_y
        nil
    end
end
```

So, now we can't get access to it. That satisfies the constraint, but it doesn't do us much good. We want to do something if they are equal.

Since we can't react to the comparison outside the objects, we need to move the behavior inward closer to where the action is happening. Notice that `equals_coordinate?` does the comparison. So, this is where we need to do the behavior.

Ordinarily, we would write something w/ a simple `if` stmt.

```rb
count_of_locations = 0
if location1.equals?(location2)
    count_of_locations++
end
```

Instead, let's take the behavior, wrap it in a lambda, and move it to where the comparison is happening:

```rb
count_of_locations = 0
location1.equals?(location2, -> { count_of_locations++ })
```

In this code, we expect the lambda to be called if the locations turn out to be equal. Let's fix our code to support this.

```rb
class Location
    attr_reader :x, :y
    def equals?(other_location, if_equal)
        other_location.equals_coordinate?(self.x, self.y, if_equal)
        nil
    end
    def equals_coordinate?(other_x, other_y, if_equal)
        if self.x == other_x && self.y == other_y
            if_equal.()
        end
        nil
    end
end
```

Now, we have a situation where we are telling a location object (*location1*) "Here is another location object (*location2*). If you are equal to it, do this (`if_equal`)."

Note, in most languages, there is some form of first-class function which makes this technique fairly straight-forward.

Sadly, Java only recently got these. So, you have to solve this using some form of a command object. Is this bad? Not necessarily, although it can be cumbersome.
