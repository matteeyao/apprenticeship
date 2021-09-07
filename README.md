# Creating a Tic-Tac-Toe application using the Test-Driven Development approach

## Application Quick-start Steps

<br />

* To install gems, run `bundle install`

* To run specs, enter these commands:

    * `bundle exec rspec spec/01_tic_tac_toe_node_spec.rb`

    * `bundle exec rspec spec/02_super_computer_player_spec.rb`

<br />

## Test-Driven Development (TDD)

<br />

*Test-driven development* is a software development approach which dictates that tests, not application code, should be written first, and then application code should only be written to pass already written specs.

The process starts w/ designing and developing tests for every small functionality of an application. TDD framework instructs developers to write new code only if an automated test has failed.

**Define functionality and write the specs first, then implement.**

![](assets/images/test-driven-development-outline.png)

<br />

### Motivations for TDD

<br />

* Code written is guaranteed to be testable because you are writing the code specifically to pass a test

* Developers avoid writing lots of extra functionality that is not included in the specs b/c they engage in the spec-writing process first

* Ensure you have excellent test coverage b/c no application code is written w/o already written tests

* Tight development workflow (red, green, refactor) makes for happy, productive developers

* Encourages a focus on modularity b/c the developer is forced to think about the application in small, testable chunks

* Encourages a focus on a module's/class's public interface (i.e. on how the module will be used rather than on how it's implemented)

<br />

### The Three Rules of TDD 

<br />

Robert C. Martin (“Uncle Bob”) three rules of Test-Driven Development are as outlined:

1. Write production code only to pass a failing unit test.

2. Write no more of a unit test than sufficient to fail (compilation failures are failures).

3. Write no more production code than necessary to pass the one failing unit test.

Rule number one says to write tests *first*-understand and specify, in the form of a unit test example, behavior you must build into the system.

Rule number two says to proceed as incrementally as possible-after each line written, get feedback (via compilation or test run) if you can before moving on.

Rule number three says to write no more code than your tests specify. That's why rule number one says "to pass a *failing* unit test." If you write more code than needed-if you implement behavior for which no test exists-you'll be unable to follow rule #1, because you'll soon be writing tests that immediately pass.

<br />

### Red, Green, Refactor

<br />

**Red**, **Green**, **Refactor** describes the proper TDD workflow.

1. **Red**: Write the tests and watch them fail (go red). It's important to ensure the tests initially fail so that you don't have false positives.

2. **Green**: Write the (minimum amount of) code to make the tests pass (go green).

3. **Refactor**: Refactor the code you just wrote. Your job is not over when the tests pass; you got it working, now make it clean.

Generally, TDD developers keep their Red, Green, Refactor loop pretty tight. They'll write a few related tests, then implement the functionality, then refactor, then repeat. You keep the units small.

<br />

### How to perform TDD Test

<br />

1. Add a test

2. Run all tests and see if any new test fails

3. Write some code.

4. Run tests and refactor code

5. Repeat

![](assets/images/tdd-five-steps.png)

<br />

## TDD in practice

<br />

While adhering to the TDD development cycle, let's begin creating a Tic-Tac-Toe application.

<br />
<br />
<br />
<br />
<br />
<br />
<br />

