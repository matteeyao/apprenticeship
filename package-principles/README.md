# Principles of Package Design

Create **cohesive** packages and manage the **dependencies** btwn them

Classes, while a very convenient unit for organizing small applications, are too finely grained to be used as the sole organizational unit for large applications

Something "larger" than a class is needed to help organize large applications

That something is called a *package*

Six principles. The first three are principles of *package cohesion* → help us allocate classes to packages

The last three govern *package coupling* → help us determine how packages should be interrelated

The last two principles also describe a set of *Dependency Management (DM) metrics* that allows devs to measure and characterize the dependency structure of their designs

## Granularity: The Principles of Package Cohesion

The three principles of package cohesion help devs decide how to partition classes into packages

They depend on the fact that at least some of the classes and their interrelationships have been discovered

Thus, these principles take a "bottom-up" view of partitioning

### The Reuse-Release Equivalence (REP)

*"The granule of reuse is the granule of release...anything that we can reuse must also be released and tracked"*

The idea here is that each package we depend on should be separately versioned

Using something like `semver` means we can determine when there could be breaking changes from upgrading a package

The REP gives us our first hint at how to partition our design into packages

Since reusability must be based on packages, reusable packages must contain reusable classes

So, at least some packages should comprise reusable sets of classes, partitioning our software

This dictates the internal structure of a package → if a package contains software that should be reused, then it should not also contain software that is not designed for reuse

*Either all of the classes in a package are reusable or none of them are*

### The Common-Reuse Principle (CRP)

*"The classes in a package are reused together. If you reuse one of the classes in package, you reuse them all"*

*"...we would expect to see classes that have lots of dependencies on each other"*

The CRP says that classes which are not tightly bound to each other with class relationships should not be in the same package

Even if a consumer of a package only depends on a small part of it, it still depends on the entire package

### The Common-Closure Principle (CCP)

"The classes in a package should be closed together against the same kinds of changes. A change that affects a package affects all the classes in that package and no other packages - This is the Single Responsibility Principle restated for packages"

The idea here is to group classes that have similar reasons to change, so that when a change in requirements comes along, that change has a good chance of being restricted to a minimal number of packages

This limits the effects of change to code that depends on that package

## Summary of Package Cohesion

* In choosing the classes to group together into packages, we must consider the opposing forces involved in reusability and developability

    * Balancing these forces w/ the needs of the application is nontrivial → the partitioning that is appropriate today might not be appropriate next year

    * Thus, the composition of the packages will jitter and evolve w/ time as the focus of the project changes from developability to reusability

## Stability: The Principles of Package Coupling

* The next three principles deal w/ the relationships btwn packages

    * Here again, we run into the tension btwn developability and logical design

### The Acyclic Dependency Principle (ADP)

*Allow no cycles in the package-dependency graph*

Package dependencies should not form a cycle - it should be impossible to follow the dependencies from one package and end up back at that package (directed acyclic graph)

Cyclic dependencies are a bad idea

If packages depend on each other in a cycle, changes to one package can trigger changes in every other package in that cycle

Testing packages then becomes difficult as it's impossible to isolate that package from the others in the cycle

## The Stable-Dependencies Principle (SDP)

*Depend in the direction of stability*

The idea here is that packages that change more frequently should depend on packages that don't change as much

This reduces the amount of change that has to occur in the entire system - a stable package at the bottom of the dependency tree won't trigger changes to its dependants

## The Stable-Abstractions-Principle (SAP)

*A package should be as abstract as it is stable*

A stable package should also be abstract so that its stability does not prevent it from being extended

On the other hand, it says that an unstable package should be concrete since its instability allows the concrete code within it to be easily changed

State-Abstractions Principle (SAP) and State-Dependencies Principle (SDP) combined constitute the Dependency Inversion Principle (**DIP**) - "depend on abstractions not concretions"
