# The Stable-Abstractions-Principle (SAP)

*A package should be as abstract as it is stable*

* This principle sets up a relationship btwn stability and abstractness

    * A stable package should also be abstract so that its stability does not prevent it from being extended

    * On the other hand, it says that an unstable package should be concrete since its instability allows the concrete code within it to be easily changed

    * Thus, if a package is to be stable, it should also consist of abstract classes so that it can be extended

    * Stable packages that are extensible are flexible and do not overly constrain the design

* The Single-Abstractions Principle (SAP) and State-Dependencies Principle combined amount to the Dependency-Inversion Principle for packages

    * This is true b/c the State-Dependencies Principle (SDP) says that dependencies should run in the direction of stability, and the Stable-Abstractions Principle (SAP) says that stability implies abstractions

    * Thus, dependencies run in the direction of abstraction

* However, the Dependency-Inversion Principle is a principle that deals w/ classes

    * Either a class is abstract or it is not

    * The combination of the Stable-Dependencies Principle (SDP) and State-Abstractions Principle (SAP) deals w/ packages and allows that a package can be partially abstract and partially stable

## The Main Sequence

* A package that sits on the main sequence is not "too abstract" for its stability, nor is it "too unstable" for its abstractness

    * It it neither useless, nor particularly painful

    * It is depended on to the extent that it is abstract, and it depends on others to the extent that it is concrete

* Clearly, the most desirable positions for a package to hold are at one of the two endpoints of the main sequence
