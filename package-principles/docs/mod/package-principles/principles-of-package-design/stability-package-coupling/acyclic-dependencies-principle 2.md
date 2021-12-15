# The Acyclic-Dependencies Principle (ADP)

*Allow no cycles in the package-dependency graph.*

## Eliminating Dependency Cycles

* The solution to this problem is to partition the development environment into releasable packages

    * The packages become units of work, which can be checked out by a developer or a team of developers

    * When developers get a package working, they release it for use by the other developers

    * They give it a release number and move it into a directory for other teams to use

    * They then continue to modify their package in their own private areas

    * Everyone else uses the released version

* As new releases of a package are made, other team can decide whether or not to immediately adopt the new release

    * If they decide not to, they simply continue using the old release

    * Once they decide that they are ready, they begin to use the new release

* Thus, none of the teams is at the mercy of others

    * Changes made to one package do not need to have an immediate effect on other teams

    * Each team can decide for itself when to adapt its packages to new releases of the packages they use

    * Moreover, integration happens in small increments

    * There is no single point in time when all developers must come together and integrate everything they are doing

* To make it work you must *manage* the dependency structure of the packages

    * *There can be no cycles*

* Package structure should have *no cycles*, it should be a *directed acyclic graph (DAG)*

## The Effect of a Cycle in the Package Dependency Graph

In a cycle, a package depends on *every other package in the system*, making the package difficult to release

In fact, a cycle forces all packages within that cycle to always be released at the same time

They have, in effect, become one large package

* If you have ever wondered why you have to link in so many different libraries, and so much of everybody else's stuff, just to run a simple unit test of one of your classes, it is probably b/c there are cycles in the dependency graph

    * Such cycles make it very difficult to isolate modules

    * Unit testing and releasing become very difficult and error prone

* Moreover, when there are cycles in the dependency graph, it can be difficult to work out the order in which to build the packages

    * There may be no correct order

    * This can lead to some very nasty problems in languages like Java that read their declarations from compiled binary files

## Breaking the Cycle

It is always possible to break a cycle of packages and reinstate the dependency graph as a DAG

There are two primary mechanisms:

1. Apply the Dependency-Inversion Principle (DIP)

    * Create an abstract base class that has the interface that `MyDialogs` needs

    * We could then put that abstract base into `MyDialogs` and inherit it into `MyApplication`

    * This inverts the dependency btwn `MyDialogs` and `MyApplication`, thus breaking the cycle

![Breaking the cycle w/ dependency inversion](../../../../img/dependency-inversion.pdf)

2. Create a new package on which both `MyDialogs` and `MyApplication` depend.

    * Move the class(es) that they both depend on into that new package

![Breaking the cycle w/ a new package](../../../../img/new-package.pdf)

## The "Jitters"

* The second solution implies that the package structure is volatile in the presence of changing requirements

    * Indeed, as the application grows, the package dependency structure jitters and grows

    * Thus, the dependency structure must always be monitored for cycles

    * When cycles occur, they must be broken somehow

    * Sometimes this will mean creating new packages, making the dependency structure grow

## Top-Down Design

* The package structure cannot be designed from the top down

    * This means that it is not one of the first things about the system that is designed

    * Indeed, it seems that it evolves as the system grows and changes

* Package dependency diagrams have little to do w/ describing the function of the application

    * Instead, they are a map to the *buildability* of the application

    * As more and more classes accumulate in the early stages of implementation and design, there is a growing need to manage the dependencies so that the project can be developed w/o the morning-after syndrome

    * Moreover, we want to keep changes as localized as possible, so we start paying attention to the SRP and CCP and collocate classes that are likely to change together

* As the application continues to grow, we start becoming concerned about creating reusable elements

    * Thus, the CRP begins to dictate the composition of the packages

    * Finally, as cycles appear, the ADP is applied and the package dependency graph jitters and grows

* If we were to try to design the package dependency structure before we had designed any classes, we would likely fail rather badly

    * We would not know much about common closure

    * We would be unaware of any reusable elements, and

    * We would almost certainly create packages that produce dependency cycles

    * Thus, the package dependency structure grows and evolves w/ the logical design of the system
