# Four-Phase Test

The `Four-Phase Test` is a testing pattern, applicable to unit tests (not so much integration tests).

It takes the following general form:

```rb
test do
  setup
  exercise
  verify
  teardown
end
```

There are four distinct phases of the test. They are executed sequentially.

## setup

During setup, the system under test (usually a class, object, or method) is set up.

```rb
user = User.new(password: 'password')
```

## exercise

During exercise, the system under test is executed

```rb
user.save
```

## verify

During verification, the result of the exercise is verified against the developer's expectation.

```rb
user.encrypted_password.should_not be_nil
```

## teardown

During teardown, the system under test is reset to its pre-setup state.

This is usually handled implicitly by the language (releasing memory) or test framework (running inside a database transaction).

## all together

The four phases are wrapped into a named test to be referenced individually.

"Separate setup, exercise, verification, and teardown phases with newlines."

```rb
it 'encrypts the password' do
  user = User.new(password: 'password')

  user.save

  user.encrypted_password.should_not be_nil
end
```

## Four-phase Automated Testing

In automation testing, there are many common test design patterns. Some include: behavior-driven, data-driven, modular, keyword-driven, and recorded testing. Each of these test design patterns offer unique positives in specific situations. All of these tests, however, share the same test structure phases:

* Fixture Setup

* SUT Exercise

* Result Verification

* Fixture Teardown

One thing to note before we dive into each of these four phases is that in unit test design, the word "fixture" is used when referring to an object manipulated within the application being tested.

### Fixture Setup:

Purpose: To put the System-Under-Test (SUT) into a specific, test-ready state. This includes all fixtures needed for testing being prepared or created.

Challenge: This phase offers a quite difficult hurdle to tackle: setup duplication. It raises maintenance, under-utilizes human resources, and increases test time to develop automation that puts the SUT in a state that another test already does.

Danger Signs: Tests are creating every fixture needed to test. This method is called Fresh Fixture creation and is quite time consuming.

Tips: Use a single global test to create common test fixtures that all tests can utilize. Also known as "System Preparation". This method is called Implicit Setup and can reduce runtime significantly.

### SUT Exercise:

Purpose: To perform the given test within the boundaries of the SUT

Challenge: Each SUT will likely provide its own unique challenges. Commonly, four phase tests don't have issues w/ this step.

Tip: If you are often struggling w/ this step, ensure your test design isn't overly complex.

### Result Verification:

Purpose: To validate the SUT per the specific testing exercise

Challenges: While most testing methods involve comparison of an expected value to the value the SUT is actually producing, verification is often performed many different ways. If your test doesn't separate nominal/setup errors from result verification errors, it can be hard to understand if the test should stop on a given error. This can mean that important tests aren't running when they should have.

Tips: Utilize various methods of validating your SUT. These include Delta Assertions, State Verification, Behavior Verification, Guard Assertion, or a Custom Verification model. Be sure you're utilizing the proper verification method. Also, separate verification errors from nominal errors. Verification is the "bread and butter" of your tests. Your results should show that.

### Fixture Teardown:

Purpose: Ensure that the SUT is prepared for the next test by removing all fixtures created for previous tests.

Challenge: Commonly, automation developers will lazily teardown all fixtures within the SUT. This generally ensures that the SUT is, indeed, ready for subsequent testing. The issue here is that, while faster, tearing down all fixtures means those necessary for future tests must be recreated. That takes time.

Danger Signs: Tests are failing often due to previous tests ran before them. This is called `Chained Testing`. While this can save on test time, it can also increase maintenance creating flaky tests.

Tips: Recommend utilizing what's called `Automated Teardown`. This entails keeping track of each fixture created within the SUT for the purpose of a given test, then tearing down only those fixtures. This makes tests repeatable and increase Persistent Fixtures within the SUT - `Fixture Tracking`.

Complications can arise when any of these four phases are overly complex. When this occurs it can be identified as `Test Smells`. This means that a test has an issue but we only have knowledge of the symptom, not its cause. These could be anything for Project Smells to Code smells. Performing a proper root-cause analysis will help determine what has gone wrong in the test.
