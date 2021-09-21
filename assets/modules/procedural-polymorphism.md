# Procedural Polymorphism

```rb
class Cell
    # ...
    def alive_in_next_generation?
        if alive
            stable_neighborhood?
        else
            genetically_fertile_neighborhood?
        end
    end
end
```

Notice the above contains a bit too much implementation detail.

The method name `alive_in_next_generation?` is more about implementation, the move from generation to generation, rather than a
description of the behavior we want.

It is more of a state-oriented statement “alive in next generation?” rather than a question about behavior.

When we find these very generic names, we are looking at an expressiveness problem.

Why is “alive” the state we are interested in? What if we add another state?

However, if we think about a better name, we have a hard time. In the case of a living cell, this is really whether it stays alive. In the case of a dead cell, though, it is about the cell coming to life.

How can we reconcile this inconsistency?

Before diving straight into tackling the reconciliation, let’s start at a lower level, inside the method, and see if we can gain any insight.

Starting at the top, let's look at the branching variable, `alive`; there are a few different questions we could ask ourselves about it.

The name of this variable captures a default, or preferred, state: `alive`. Why is this the thing we highlight? Each cell is really in one of two states; why not highlight dead? What if we change the concept of living? What if it isn't binary?

Changing this means we have to change code also related to the other two states.

We also are spreading the concept in several places: `alive` has to do w/ both the variable and the method that uses it.

A seemingly quick solution would be to make it something like `state`, but that masks our intention a bit. What are the possible states?

```rb
class Cell
    # ...
    def alive_in_next_generation?
        if state == ALIVE
            stable_neighborhood?
        elsif state == DEAD
            genetically_fertile_neighborhood?
        end
    end
end
```

This isn't much better; now, we have even more of an expressiveness problem w/ this branching: do we really know these are the only ones?

I also feel a bit uncomfortable when I see an `if-elsif` sequence w/o a raw `else`.

Variables named `state` are also a huge red flag for expressiveness. Does a cell really change `state`? Do dead cells change state into living cells? Or are living cells created?

To be honest, too often *state* variables are usually just an indication that we've given up on really understanding and encoding our intention.

Resolving this requires us to talk a bit about polymorphism in general. Polymorphism is about being able to call a method/send to an object and have more than one possible behavior.

This can be one of the most powerful techniques in programming.

In our case, we are providing a form of polymorphism w/ this method. 

When this method is called, the caller can expect one of two different behaviors: either the ruleset for living cells or the ruleset for dead cells.

Which ruleset gets run is based on an internal state, hidden from the outside world. In a way, this is good; the caller shouldn't have to care. But it is worth looking at the method we use to achieve the goal.

When we use a branching construct inside a method like this, we run into several problems. 

We've talked about the expressiveness problem, but we also have issues w/ changing this code.

If we are going to add a state, or change rules around the states, we will find ourselves modifying existing code. Not just existing code, but code that is unrelated to the change we are making.

If we add a state, why would we force ourselves to modify the code related to the other states?

When we begin to overload concepts in our system, especially method names, we run into this "everything goes here" situation.

In general, `if` statements (or other branching constructs) are imperative, procedural mechanisms.

While they do provide a form of polymorphism, they provide a form that I call **Procedural Polymorphism**. It satisfies our needs for selecting a behavior, but their procedural background leads to leads to tightly-coupled code, joining these often unrelated behaviors together.

Luckily, object-based and object-oriented languages provide a preferred method for polymorphism, what I call **Type-Based Polymorphism**.

The idea is one central to object-oriented design: use different types for the different branches.

The general approach is to analyze what the branching condition is, identify the concepts, and reify them into first-class concepts in our system.

In our example, we can take out *state* and raise it to types: `LivingCell` and `DeadCell`.

```rb
class LivingCell
    def alive_in_next_generation?
        # neighbor_count == 2 || neighbor_count == 3
        stable_neighborhood?
    end
end
class DeadCell
    def alive_in_next_generation?
        # neighbor_count == 3
        genetically_fertile_neighborhood?
    end
end
```

At this point we have separated out the concepts. And, if we choose to, we can also inline the business rule methods w/o sacrificing too much.

```rb
class LivingCell
    def alive_in_next_generation?
        neighbor_count == 2 || neighbor_count == 3
    end
end
class DeadCell
    def alive_in_next_generation?
        neighbor_count == 3
    end
end
```

We also have higher-level names for our concepts, which makes it easier to find where changes need to occur.

A huge benefit of this is that we also have provided ourselves a safer method for adjusting the different states a cell can be in.

If we need to add a new one, we add a new class. We *extend* our system, rather than modify it. This is an example of the open-closed principle.

```rb
class ZombieCell
    def alive_in_next_generation?
        # new, possibly more complex rules
    end
end
```

It also provides a clear method for fixing the names of our methods to match the actual concepts in our system, focusing on specific behaviors, rather than a generic idea of `alive_in_next_generation`.

```rb
class LivingCell
    def stays_alive?
        neighbor_count == 2 || neighbor_count == 3
    end
end
class DeadCell
    def comes_to_life?
        neighbor_count == 3
    end
end
```

At this point, we now have very explicit statements of the intent of the types and their behaviors. 

But, changing these names takes away the polymorphism? We no longer can call a single method and have the appropriate rules applied. This is true.

This could be an indication that the idea of having the initially-desired polymorphism isn't a good design.

Naturally it depends on how we end up using the cells, but focusing heavily on explicitness in this fashion can raise flags about desired or "planned" designs.
