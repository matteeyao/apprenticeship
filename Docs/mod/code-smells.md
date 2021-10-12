# Smells in Test that indicate Design Problems

* **Too many test cases per method** may indicate that the the method is doing too much. We discussed the fact that complex business logic algorithms, w/ lots of special cases, often appear to be atomic and indivisible; and thus only testable as a unit. But there is often a way to break them down into smaller pieces. Also sometimes one needs to think if all those special cases are really required now or if we are speculating at this moment.

* **Poorly factored edge cases**

  * Case were there are too many variations of input tested, when a few carefully chosen edge cases would suffice. We discussed how this sometimes emerges when the algorithm under test has too many special cases, and the same result could be arrived at w/ a more general algorithm.

* **Increasing access privilege of members**

  * (Methods or instance variables) to protected or public only for testing purpose: sometimes indicates that you are coupling your tests too much w/ the code. Sometimes it indicates that the private module has enough behavior that it needs to be tested. In that case may be you should consider pulling it out as a separate object.

* **Too much setup/teardown**

  * Indicates strong coupling in the class under test.

* **Mocks returning mocks**

  * Indicates that the method under test has too many collaborators

* **Poorly-named tests**

  * Sometimes means that the naming and/or design of the classes under test isn't sufficiently thought-out

* **Lots of Duplication in tests**

  * Sometimes indicates that the production code should be providing a way to avoid some of that duplication.

* **Extensive Inheritance in test fixtures**

  * Indicates that your design might heavily rely on inheritance instead of composition.

* **Double dots in the test code**

  * Indicates that the code violates the Law of Demeter. In some cases it might be better to hide the delegate.

* **Changing one thing breaks many tests**

  * May just indicate bad refactoring of tests, but can also indicate excess dependencies in the code.

* **Dynamic stubs**

  * (Stubs w/ conditional behavior): indicates lack of control over the collaborator that is being stubbed out. This sometimes indicate the behavior is not distributed well amongst the classes.

* **Too many dependencies** that have to be included in the test context

  * Indicates tight coupling in the design

* **Random test failures when running them in parallel**

  * Indicates that the code is not thread safe and has side-effects that are not factored correctly.

* **Tests run slowly**

  * Indicates that your unit tests might be hitting external systems like network, database, or filesystem. This usually indicates that the class under test might have multiple responsibilities. One should be able to stub out external dependencies.

* **Temporal coupling**

  * Tests break when run in a different order: may just be a test smell; may be coupling in the code under test.
