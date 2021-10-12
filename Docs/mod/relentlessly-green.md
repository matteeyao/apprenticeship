# A Refactoring Workout: Relentlessly Green

Refactoring is the process of changing a software system in such a way that it does not alter the external behavior of the code yet improves its internal structure. - Martin Fowler

One of the challenges of refactoring is succession-how to slice the work of a refactoring into safe steps and how to order those steps. - Kent Beck

A *safe step* is one which does not cause the tests to fail.

This refactoring drill focuses on succession, repeating the act of choosing a tiny, safe step over and over again, w/ an automated test providing continuous and immediate feedback.

## Refactoring Under Green

The goal is to refactor the code in tiny, safe steps until it has become a generalized solution.

The size of a step is limited by what your editor can revert w/ single *undo*.

There are two rules:

* Run the test after every single change.

* If the test fails, it should take exactly one *undo* to get back to green.

The test suite is minimal, asserting that the default string representation of a `Checkerboard` instance returns a grid w/ alternating black (B) and white (W) squares.

The board in question is as small as possible, while still defining a checkered pattern.

```rb
gem 'minitest', '~> 5.3'
require 'minitest/autorun'
require_relative 'checkerboard'

class CheckerboardTest < Minitest::Test
    def test_small_board
        expected = <<-BOARD
            B W
            W B
        BOARD
        assert_equal expected, Checkerboard.new(2).to_s
    end
end
```

The implementation hard-codes the string representation of the 2x2 grid.

```rb
class Checkerboard
  def initialize(_)
  end

  def to_s
    "B W\nW B\n"
  end
end
```

When the refactoring is complete, it should be possible to call `checkerboard.new` w/ any size and get a properly formatted checkerboard.

It's tempting to add another failing test at this point, perhaps for a 3x3 board, in order to triangulate towards better design. But this isn't an exercise in Test-Driven Development.

In this drill, the design will be driven by conscious choices about the next safe step, not by a failing test.

If the step turns out not to be safe, hit *undo* once and you should be back to green.

## Where to Begin?

The current algorithm has some issues.

```rb
def to_s
  "B W\nW B\n"
end
```

For one, it doesn't scale. Also, it mixes data and presentation.

Ideally, the grid would be built up separately, and the `to_s` method would manipulate the grid to provide a string representation.

The newlines make it clear that there are two of something here. Two rows. That smells like an array.

Redundancy - put something in place that will let you fail-over safely.

## The Redundancy Ploy

Write a completely new implementation, and stick it right before the old implementation.

```rb
def to_s
    ["B W\n", "W B\n"].join
    "B W\nW B\n"
end
```

Run the test.

The new implementation hasn't been vetted yet, but it has been executed, which provides a syntax check.

Delete the original implementation, and run the test again.

```rb
def to_s
    ["B W\n", "W B\n"].join
end
```

If it had failed, a single undo would put the working code back.

Use the same technique to move the newline out of the hard-coded rows.

```rb
def to_s
    ["B W", "W B"].map {|row| row + "\n"}.join
end
```

We're so used to thinking of duplication as the enemy.

However, in refactoring, duplication is a short-term, low-cost investment that pays excellent returns.

## The Setup-and-Swap Ploy

The setup-and-swap ploy adds all the code necessary to be able to make the final change in one small step.

```rb
def to_s
    rows = []
    rows << "B W"
    rows << "W B"
    ["B W", "W B"].map {|row| row + "\n"}.join
end
```

This adds three lines of code that don't have any bearing on the state of the test suite, but they make it very easy to replace the hard-coded array w/ the new `rows` variable.

```rb
def to_s
    rows = []
    rows << "B W"
    rows << "W B"
    rows.map {|row| row + "\n"}.join
end
```

Each row itself is still hard-coded, but can be transformed independently using another setup-and-swap.

```rb
row = ["B", "W"].join(" ")
rows << "B W"

row = ["B", "W"].join(" ")
rows << row

def to_s
    rows = []
    row = ["B", "W"].join(" ")
    rows << row
    row = ["W", "B"].join(" ")
    rows << row
    rows.map {|row| row + "\n"}.join
end
```

This has moved the whitespace away from the individual cells, but we can do better. The `join` belongs within the loop.

This is particularly tricky, since we have two joins that need to be collapsed into a single spot.

If we move one, the test will fail.

If we delete both, the test will fail.

If we stick a `join` on the `row` within the loop first, the test will fail.

We could use the redundancy ploy, introducing a `rows2` variable, but there's an easier way.

## The Null Method Ploy

There's no reason why `String` can't have a `join` method!

```rb
class String
    def join(_)
        self
    end
end

class Checkerboard
    # ...
end
```

This makes it trivial to make the change w/o breaking anything.

```rb
def to_s
    rows = []
    row = ["B", "W"].join(" ")
    rows << row
    row = ["W", "B"].join(" ")
    rows << row
    rows.map {|row| row.join(" ") + "\n"}.join
end
```

Now the `row` within the loop can handle both arrays and strings, and we can delete the original `join`s, along w/ the null method.

```rb
def to_s
    rows = []
    row = ["B", "W"]
    rows << row
    row = ["W", "B"]
    rows << row
    rows.map {|row| row.join(" ") + "\n"}.join
end
```

The two `row` assignments are similar, but not identical.

Use the redundancy ploy to separate the parts that vary from the parts that stay the same.

```rb
def to_s
    rows = []
    
    row = []
    row << "B"
    row << "W"
    rows << row
    
    row = []
    row << "W"
    row << "B"
    rows << row
    
    rows.map {|row| row.join(" ") + "\n"}.join
end
```

The solution will only scale if we introduce a loop. Well, two actually.

## The Wind-Unwind Ploy

There are two chunks that have the same setup and the same finish. Call these *Chunk A* and *Chunk B*.

```rb
# chunk a

# chunk b
```

The wind-unwind ploy uses a loop to wind the chunks up, and a conditional to unwind them back to where they were.

```rb
2.times do
    if condition A
        # chunk a
    else
        # chunk b
    end
end
```

Then common code can be moved out of the conditional:

```rb
2.times do
    # common code
    if condition A
        # variation a
    else
        # variation b
    end
    # common code
end
```

Combine the redundancy ploy with the wind-unwind ploy to introduce the loop safely.

```rb
def to_s
    rows = []
    2.times {|y|
        row = []
        if y == 0
          row << "B"
          row << "W"
        else
          row << "W"
          row << "B"
        end
        rows << row
    }
    rows.map {|row| row.join(" ") + "\n"}.join
end
```

There's still a lot of similarity in shoveling cells into the row. The only difference is order.

Apply the wind-unwind ploy again.

```rb
def to_s
    rows = []
    2.times {|y|
        row = []
        2.times {|x|
            if y == 0
                if x == 0
                    row << "B"
                else
                    row << "W"
                end
            else
                if x == 0
                    row << "W"
                else
                    row << "B"
                end
            end
        }
        rows << row
    }

    rows.map {|row| row.join(" ") + "\n"}.join
end
```

This is, admittedly, pretty terrible, but the tests are passing and the nested conditionals can be fixed using the redundancy ploy.

```rb
def to_s
    rows = []
    2.times {|y|
        row = []
        2.times {|x|
            if x == y
                row << "B"
            else
                row << "W"
            end
        }
        rows << row
    }
    rows.map {|row| row.join(" ") + "\n"}.join
end
```

`x == y` is only really valid for the 2x2 checkerboard. In a larger checkerboard, this produces a diagonal stripe.

There are a couple of valid approaches. First:

```rb
if (x.even? && y.even?) || (x.odd? && y.odd?)
    # it's black
else
    # it's white
end
```

The other is more succint:

```rb
if (x+y).even?
    # it's black
else
    # it's white
end
```

The algorithm will work for a checkerboard of any size, provided that we loop enough times.

We've been passing the argument that we need to the new `Checkerboard` instance all along.

Use the setup-and-swap ploy to make that data available to the rest of the instance, and then replace the magic numbers w/ calls to `size`.

```rb
attr_reader :size
def initialize(size)
    @size = size
end

def to_s
    rows = []
    size.times {|y|
        row = []
        size.times {|x|
            if (x+y).even?
                row << "B"
            else
                row << "W"
            end
        }
        rows << row
    }
    rows.map {|row| row.join(" ") + "\n"}.join
end
```

Sanity test the solution by adding a second test that proves that it works.

```rb
def test_chess_board
    expected = <<-BOARD
        B W B W B W B W
        W B W B W B W B
        B W B W B W B W
        W B W B W B W B
        B W B W B W B W
        W B W B W B W B
        B W B W B W B W
        W B W B W B W B
    BOARD
    assert_equal expected, Checkerboard.new(8).to_s
end
```

This is not a final solution.

The `to_s` method is afflicted w/ a number of code smells.

Perhaps there should be a `Cell` object which a `to_s` method that decides what the string representation of *black* and *white* looks like.

All of these changes can be made using safe steps.
