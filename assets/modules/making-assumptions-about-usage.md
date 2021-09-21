# Making Assumptions About Usage

```rb
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

It seems reasonable that those methods would be there. After all, the following reads okay. Or, at least, it feels familiar.

```rb
cell.stays_alive?(number_of_neighbors)
```

But, there are a couple possible flags here.

First, notice that we are talking about **entity classes** here. That is, we have objects representing concrete abstractions: Cells.

Classes of this nature tend to encapsulate and provide behavior around state.

Methods on them are generally involved in working w/ that state. For example, query methods provide a way to access the state.

In this case, though, the methods are not accessing internal state, at all. In fact, they are primarily using the passed-in value, `number_of_neighbors`.

It is true that we could say that the rules, themselves, the comparisons are related to the cell and constitute cell-focused knowledge.

While cell-focused, they really represent the rules.

But why is `Cell` our abstraction around executing the rules? Why don't we reify the idea of a rules?

One of the key parts of being easier to change (i.e. a better design) is being able to more easily find where the changes need to occur; this is what good naming contributes to.

So, if we were to come to a large system, and we wanted to change the rules for evolution, you might look at a `Cell` class. But imagine if there was a `Rule` class.

That could probably be an even larger signpost. Let's play w/ this a bit by just adding `Rules` to the class names.

```rb
class LivingCellRules
    def stays_alive?(number_of_neighbors)
        number_of_neighbors == 2 || number_of_neighbors == 3
    end
end
class DeadCellRules
    def comes_to_life?(number_of_neighbors)
        number_of_neighbors == 3
    end
end
```

Of course, we've now lost an abstraction, the Cell. This will influence our locations objects. Are the locations linked to the current rules depending on the state, or is there still some placeholder idea of a cell? Do we even need a reified cell abstraction? What is causing us to have it?

In fact, if we think about it, the concept of a `DeadCell` has a potential trap in it. We are working w/ an infinite grid. So, which dead cells are we keeping track of? Which locations are we tracking? How do we know that we should instantiate a location object for a given `(x, y)` pair?

We can't keep track of all of them. Perhaps it does make sense to question the concept of a concrete cell class.

A lot of questions that arise have a "*do we need this abstraction*" flavor. This happens quite frequently when following an inside-out development style.

We start somewhere in our domain, making a very large assumption that the abstractions we are building will be needed sometime.

As we've seen, new abstractions can be developed and investigated through refactorings, but it can be easy to work yourself into a corner.

The fundamental thought that is hidden in "do we need this abstraction" is "use influences structure." So, should we have `LivingCellRules` and get rid of `LivingCell`? Should location objects keep a link to the rule, rather than the cell?

Perhaps the location object doesn't actually contain this link at all. Perhaps the existence of an instantiated location object implies `LivingCellRules`.

So many answers not just disappear but never come up when building abstractions and behaviors through actual usage. This is often what happens when using an outside-in development method.
