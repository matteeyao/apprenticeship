# Inverted Composition as a Replacement for Inheritance

Take a look at these cell classes.

```rb
class LivingCell
    attr_reader :location
end
class DeadCell
    attr_reader :location
end
```

We've extracted the location object, providing us a centralized place for our topology knowledge. 

Although a benefit, we can see another duplication here, as well.

Both the living cell and the dead cell have a location attribute.

Is this knowledge duplication? What is the knowledge we are duplicating?

Since these are two different objects, and this is "just" an attribute, we can be tempted to say it isn't.

As we look at this code in light of the 4 rules, we want to make sure that what we have is actual knowledge duplication, rather than just incidental, implementation similarity.

After all, extracting the location object was about taking the actual knowledge and representing it in one place.

We can look at this knowledge duplication, since this location attribute represents the face that our cells are linked to a specific position on the grid.

It is an interesting case here, where eliminating a specific duplication didn't eliminate **all** the duplication, just apart of it.

So, let's look at ways to eliminate this duplication.

A common attempt at a solution to this is to jump to inheritance. We could do something like the following:

```rb
class Cell
    attr_reader :location
end
class LivingCell < Cell
end
class DeadCell < Cell
end
```

Wait, though, let’s look at this code a minute.

Now, it does seem to simplify our code a bit if we think in terms of lines of code. But, is it really simpler?

It does add another type after all.

I often sa having more classes isn't bad, as long as they are the correct abstractions. But, unlike the extraction of the `Location` class, this extraction doesn't introduce a new domain concept; this abstraction increases the complexity w/o adding additional information about our domain.

This feels like a violation of the fourth rule, "small."

Inheritance is often used as way of creating “reuse” rather than eliminating duplication.

We are assuming that both the `LivingCell` and `DeadCell` need to have access to their location (do they?), so we provide access through the base class.

Even if we support our assumption, however, the objects don’t need access to their location necessarily, they really would need access to the behaviors that the location object exposes.

And, of course, at this point, we haven’t even talked about whether they truly do.

So, let’s ask again: is it really eliminating the duplication? The location attribute is still there on the objects. Our two different types still contain the same knowledge.

Base classes of this nature, extracted entirely to eliminate apparent duplication can have a tendency to hide actual duplication. Also, it is very common for these base classes to become buckets of unrelated behavior.

So, if inheritance isn’t really eliminating the knowledge, what other options do we have?

In Ruby, we do have modules. This might be a good use for them.

```rb
class LivingCell
    include HasLocation
end
class DeadCell
    include HasLocation
end
```

And the `HasLocation` module adds `attr_reader :location` to the including class. Modules, when used this way, though, are just a
way to implement multiple inheritance. The same arguments arise as in the above discussion of straight subclassing.

I do believe this is slightly better than using a base class, `Cell`. Modules are often used as a way of grouping aspects of different classes, and this can be useful for code organization.

But this technique should be used very judiciously. Primarily, I use modules in this way as a step in the path towards a better design. Separating out aspects of a class into modules can help find hidden dependencies, as well as highlight all the different responsibilities a class has. But they are rarely the place to stop.

So, w/ that option off the table, how do we eliminate the duplication? Let's look at what we are trying to accomplish.

Our goal is to have a link btwn the `Cell` and the `Location` it is at. Or, rather, our system needs to know this link.

We haven't actually seen anything to indicate the `Cell` classes, themselves, need the link.

Our assumption here i that something needs to see the link.

When having two types containing a link to the same type (`Living`|`Dead`)`Cell` and `Location`, a useful technique is to reverse the dependency.

```rb
class Location
    attr_reader :x, :y
    attr_reader :cell
end
class LivingCell
    def stays_alive?(number_of_neighbors)
        number_of_neighbors == 2 || number_of_neighbors == 3
    end
end
class DeadCell
    def comes_to_life?(number_of_neighbors)
        number_of_neighbors == 3
    end
end
```

At this point, our cell classes are indeed just focused on information related to the cell (for example, rules).

The topology is also further abstracted from the rules of the game.

We can start to see that the `Location` class is taking on a structural role, providing the link between the topology and the cell that exists there. The cell classes are now focused on rules around evolution.

While the refactoring is good, it highlights a potential naming issue. Is `Location` the correct name for this class? 

From a reading point of view, it seems like a `Cell` should have a `Location`, not the other way around. This is arguable, of course, but it seems like potentially we chose the wrong name for the `Location` class. Perhaps it is better as a `Coordinate`.

```rb
class Coordinate
    attr_reader :x, :y
    attr_reader :cell
end
```

I'm not saying it is, or not, at this point. I only wanted to mention it is interesting how eliminating the duplication highlighted a possible naming issue.

This is a good example of how applying these rules can often lead to other refactoring opportunities and insight into our design.
