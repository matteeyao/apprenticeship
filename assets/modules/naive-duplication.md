# Naive Duplication

```rb
class Cell
    attr_reader :alive # true | false
    def alive_in_next_generation?
        if alive
            number_of_neighbors == 2 || number_of_neighbors == 3
        else
            number_of_neighbors == 3
        end
    end
end
```

Let's start refactoring this. Any noticeable duplication?

Aha! That check around whether number of neighbors is 3 looks suspicious. Let's get rid of the duplication.

```rb
class Cell
    # ...
    def alive_in_next_generation?
        (alive && number_of_neighbors == 2) || number_of_neighbors == 3
    end
end
```

We definitely got rid of the two instances of the number 3, but we have introduced new issues. 

This is due to what I consider naive, mechanical elimination of duplication: a refactoring that stems from a fundamental misunderstanding of the idea of DRY.

DRY principle states that "every piece of knowledge has one and only one representation".

With this clarity in hand, let’s analyze our example a bit more closely.

These 3s are not the same. Thinking they are is a result of seeing a magic number without some sense of what it represents in terms of
our domain.

When thinking about duplication, it can help to expand the scope of our view, in this case to include the equality check, and to think about what it represents.

In our alive case, the 3 is more closely linked to the 2 in the concept of a “stable neighborhood,” while in the dead case, it is linked to something like a “genetically fertile neighborhood.”

One good technique to keep from mistaking similar-looking code as actual knowledge duplication is to explicitly name the concepts before you try to eliminate the duplication.

In our case, we would end up with something like this.

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

After this small refactoring, we can see clearly that the 3s represent different things.

This is the power of paying close attention to the expressiveness of our code before blindly trying to eliminate duplication.
