# The Common-Reuse Principle (CRP)

*The classes in a package are reused together. If you reuse one of the classes in a package, you reuse them all.*

This principle helps us to decide which classes should be placed into a package

It states that classes that tend to be reused together belong in the same package

Classes are seldom reused in isolation

Generally, reusable classes collaborate w/ other classes that are part of the reusable abstraction

The CRP states that these classes belong together in the same package

In such a package, we would expect to see classes that have lots of dependencies on each other

A simple example might be a container class and its associated iterators

These classes are reused together b/c they are tightly coupled to each other. Thus, they ought to be in the same package

But the CRP also tells us what classes *not* to put in the package

When one package uses another, a dependency is created btwn the packages

It may be that the using package only uses one class within the used package

However, that doesn't weaken the dependency at all

The using package sill depends on the used package

Every time the used package is released, the using package must be revalidated and rereleased

This is true even if the used package is being released b/c of changes to a class that the using package doesn't care about

Moreover, it is common for packages to have physical representations as shared libraries, DLLs, JARs

If the used package is released as a JAR, then the using code depends on the entire JAR

Any modification to that JAR - even if the modification is to a class that the using code does not care about - will still cause a new version of the JAR to be released

The new JAR will still have to be redistributed and the using code will still have to be revalidated

Thus, ensure that when I depend on a package, I depend on every class in that package

Put in another way, I want to make sure that the classes that I put into a package are inseparable, that it is impossible to depend on some and not the others

Otherwise, I will be revalidating and redistributing more than is necessary, and I will waste significant effort

Therefore, the CRP tells us more about what classes shouldn't be together than what classes should be together

The CRP says that classes which are not tightly bound to each other w/ class relationships should not be in the same package
