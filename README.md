# Creating a Tic-Tac-Toe application with Test-Driven Development

## Application Quick-start Steps

* To install gems, run `bundle install`

* To run specs, enter these commands:

    * `bundle exec rspec spec/01_tic_tac_toe_node_spec.rb`

    * `bundle exec rspec spec/02_super_computer_player_spec.rb`

## Test-Driven Development (TDD)

*Test-driven development* is a software development approach which dictates that tests, not application code, should be written first, and then application code should only be written to pass already written specs.

The process starts w/ designing and developing tests for every small functionality of an application. TDD framework instructs developers to write new code only if an automated test has failed.

**Define functionality and write the specs first, then implement.**

![](assets/images/test-driven-development-outline.png)

### Motivations for TDD

* Code written is guaranteed to be testable because you are writing the code specifically to pass a test

* Developers avoid writing lots of extra functionality that is not included in the specs b/c they engage in the spec-writing process first

* Ensure you have excellent test coverage b/c no application code is written w/o already written tests

* Tight development workflow (red, green, refactor) makes for happy, productive developers

* Encourages a focus on modularity b/c the developer is forced to think about the application in small, testable chunks

* Encourages a focus on a module's/class's public interface (i.e. on how the module will be used rather than on how it's implemented)

### The Three Rules of TDD 

Robert C. Martin (“Uncle Bob”) three rules of Test-Driven Development are as outlined:

1. Write production code only to pass a failing unit test.

2. Write no more of a unit test than sufficient to fail (compilation failures are failures).

3. Write no more production code than necessary to pass the one failing unit test.

Rule number one says to write tests *first*-understand and specify, in the form of a unit test example, behavior you must build into the system.

Rule number two says to proceed as incrementally as possible-after each line written, get feedback (via compilation or test run) if you can before moving on.

Rule number three says to write no more code than your tests specify. That's why rule number one says "to pass a *failing* unit test." If you write more code than needed-if you implement behavior for which no test exists-you'll be unable to follow rule #1, because you'll soon be writing tests that immediately pass.

### Red, Green, Refactor

**Red**, **Green**, **Refactor** describes the proper TDD workflow.

1. **Red**: Write the tests and watch them fail (go red). It's important to ensure the tests initially fail so that you don't have false positives.

2. **Green**: Write the (minimum amount of) code to make the tests pass (go green).

3. **Refactor**: Refactor the code you just wrote. Your job is not over when the tests pass; you got it working, now make it clean.

Generally, TDD developers keep their Red, Green, Refactor loop pretty tight. They'll write a few related tests, then implement the functionality, then refactor, then repeat. You keep the units small.

### How to perform TDD Test

1. Add a test

2. Run all tests and see if any new test fails

3. Write some code.

4. Run tests and refactor code

5. Repeat

![](assets/images/tdd-five-steps.png)

## TDD in practice

The following details the process of creating a Tic-Tac-Toe application with Ruby while adhering to the TDD development cycle.

### Board

Let's first define a class `Board`. For this class, we will try to satisfy the following conditions.

A condition for the grid layout of the `Board`:

* The grid should be of a `3 x 3` layout.

In this TDD walk-through, we begin writing the test suite for the code that fulfills all the above requirements.

```rb
# spec/board_spec.rb

describe Board do
    let(:empty_board) do
        Board.new
    end

    describe '#initialize' do
        it 'sets up the instance variable grid' do
            expect(empty_board.grid.length).to eq(3)
            empty_board.grid.each { |row| expect(row.length).to eq(3)}
            expect(empty_board.grid.flatten.length).to eq(9)
        end

        it 'should start out empty' do
            for row in empty_board.grid do
                for col in row do                    
                    expect(col).to be_nil;
                end
            end
        end
    end
end
```

We begin by instantiating a new board using the class `Board` that we will create to fulfill this test.

The test first checks that the length of the board adds up to 3 rows, then that each row is of length 3, and that there are a total of 9 squares within the board's grid. Finally, our test will need to ensure that the board's grid starts off as `[[nil, nil, nil], [nil, nil, nil], [nil, nil, nil]]`.

When we run the test, the test fails as expected since there is no code written yet. The specs fail as follows:

```zsh
Failures:

  1) Board#initialize sets up the instance variables
     Failure/Error: expect(empty_board.grid.length).to eq(3)
     NoMethodError:
       undefined method `length' for nil:NilClass
     # ./spec/board_spec.rb:11:in `block (3 levels) in <top (required)>'

Finished in 0.00036 seconds (files took 0.088 seconds to load)
1 example, 1 failure

Failed examples:

rspec ./spec/board_spec.rb:10 # Board#initialize sets up the instance variables
```

To run the test, we create class `Board`:

```rb
# lib/board.rb

class Board
    attr_reader :grid

    def self.blank_grid
        Array.new(3) { Array.new(3) }
    end

    def initialize(grid = self.class.blank_grid)
        @grid = grid
    end
end
```

As a result adding some functionality to the `Board` class, the test case now succeeds, as seen in the following output:

```zsh
Finished in 0.0009 seconds (files took 0.088 seconds to load)
1 example, 0 failures
```

Rinse and repeat through the Test-Driven development process for the rest of the required conditions for the board class. Here are some additional conditions for the class `Board`:

* The board should have only have marks `[:x, :o]`

* The board should have functionality to get a mark or undefined from a position as well as set that position to a mark

* The board should have the functionality to check if a spot is taken or is empty

* The board should have functionality to determine a winner, a draw, or neither

For each of these conditions, we will create tests, ensure they fail, write code that addresses the test requirements until the tests pass, and refactor accordingly, ensuring all tests still pass.

For example, to determine the winner of a board, either a column, row, or diagonal of the board are required to have matching marks.

The test suite below is written as follows:

```rb
#  /spec/board_spec.rb

define Board do
    # ...
    describe '#winner' do
        it 'should return undefined for an empty board' do
            expect(@empty_board.winner).to be_nil
        end

        it 'should return :x for a board filled with x\'s' do
            expect(filled_board.winner).to eq(:x)
        end

        it 'should return :o for a board filled with o\'s in the middle row' do
            for idx in (0...3) do
                @empty_board[[1, idx]] = :o
            end
            expect(@empty_board.winner).to eq(:o)
        end

        it 'should return :o for a board filled with o\'s in the middle column' do
            for idx in (0...3) do
                @empty_board[[idx, 1]] = :o
            end
            expect(@empty_board.winner).to eq(:o)
        end

        it 'should return :x for a board filled with x\'s in the top-right to bottom-left diagonal' do
            for idx in (0...3) do
                @empty_board[[idx, idx]] = :x
            end
            expect(@empty_board.winner).to eq(:x)
        end

        it 'should return :o for a board filled with o\'s in the top-left to bottom-right diagonal' do
            for idx in (0...3) do
                @empty_board[[2 - idx, idx]] = :o
            end
            expect(@empty_board.winner).to eq(:o)
        end
    end
    # ...
end
```

All of the tests should fail, however, the first test passes since the `#winner` function does return undefined as the function has nothing written in it. For now, we will carry on to writing the code, as we require the `#winner` function to return undefined if there is no winner. After writing the following code below, the test suite should now pass.

```rb
# /lib/board

class Board do
    attr_reader :grid

    def self.blank_grid
        Array.new(3) { Array.new(3) }
    end

    def initialize(grid = self.class.blank_grid)
        @grid = grid
    end

    # ...

    def cols
        cols = [[], [], []]
        @grid.each do |row|
            row.each_with_index do |mark, col_idx|
                cols[col_idx] << mark
            end
        end
        cols
    end

    def diagonals
        down_diag = [[0, 0], [1, 1], [2, 2]]
        up_diag = [[0, 2], [1, 1], [2, 0]]

        [down_diag, up_diag].map do |diag|
            # Note the `row, col` inside the block; this unpacks, or
            # "destructures" the argument. Read more here:
            # http://tony.pitluga.com/2011/08/08/destructuring-with-ruby.html
            diag.map { |row, col| @grid[row][col] }
        end
    end

    # ...

    def winner
        rows = grid
        (rows + cols + diagonals).each do |triple|
            return :x if triple == [:x, :x, :x]
            return :o if triple == [:o, :o, :o]
        end

        nil
    end
end
```

The same processes will be repeated for the `Game` interface class and `Player` class.

## Summary

* TDD stands for Test-driven development

* TDD meaning: It is a process of modifying the code in order to pass a test designed previously

* Its emphasis is on production code rather than test case design

* Test-driven development is a process of modifying the code in order to pass a test designed previously

* TDD testing includes refactoring a code, i.e., changing/adding some amount of code to the existing code w/o affecting the behavior of the code.

* TDD programming when used makes for clearer and more simple-to-understand code
