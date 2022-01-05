# The Stable-Dependencies Principle (SDP) 

*Depend in the direction of stability*

* Designs cannot be completely static

    * Some volatility is necessary if the design is to be maintained

    * We accomplish this by conforming to the Common-Closure Principle (CCP)

    * Using this principle, we create packages that are sensitive to certain kinds of changes

    * These packages are *designed* to be volatile

    * We *expect* them to change

* Any package that we expect to be volatile should not be depended on by a package that is difficult to change

    * Otherwise the volatile package will also be difficult to change

* It is the perversity of software that a module that you have designed to be easy to change can be made hard to change by someone else simply hanging a dependency on it

    * Not a line of source code in your module need change, and yet your module will suddenly be hard to change
    
    * By conforming to the SDP, we ensure that modules that are intended to be easy to change are not depended on by modules that are harder to change

## Stability

* Stability has nothing directly to do w/ frequency of change

* Stability is related to the amount of work related to make a change

* One sure way to make a software package difficult to change is to make lots of other software packages depend on it

    * A package w/ lots of incoming dependencies is very stable b/c it requires a great deal of work to reconcile any changes w/ all the dependent packages

    * Figure 20-5 shows `x`, a stable package. This package has three packages depending on itl and therefore, it has three good reasons not to change

    * We say that it is *responsible* to those three packages

    * On the other hand, `x` depends on nothing, so it has external influence to make it change → we say it is *independent*

![`x`: A Stable Package](../../../../img/stable-package.pdf)

* Figure 20-6, on the other hand, shows a very unstable package

    * `y` has no other packages depending on it; we say that it is irresponsible

    * `y` also has three packages that it depends on, so changes may come from three external sources → we say that `y` is dependent

![`y`: An Unstable Package](../../../../img/unstable-package.pdf)

* Lack of dependents provides no reason *not* to change, and the packages that it depends on may give it ample reason *to* change

* A package that is depended on by other packages, but does not itself depend on any other packages is *responsible and independent*

    * Such a package is as stable as it can get

    * Its dependants make it hard to change, and it has no dependencies that might force it to change

* The State-Dependencies Principle (SDP) says that the `I` metric of a package should be larger than the `I` metrics of the packages that it depends on (i.e., `I` metrics should decrease in the size of dependency)

## Not all packages should be stable

* If all the packages in a system were maximally stable, the system would be unchangeable

    * This is not a desirable situation

    * Indeed, we want to design our package structure so that some packages are unstable and some are stable

    * Figure 20-8 shows an ideal configuration for a system w/ three packages

![Ideal package configuration](../../../../img/ideal-package-configuration.pdf)

* The changeable packages are on top and depend on the stable package at the bottom

    * Putting the unstable packages at the top of the diagram is a useful convention since any arrow that points *up* is violating the State-Dependencies Principles (SDP)

## Where do we put the high-level design?

* Some software in the system should not change very often

    * This software represents the high-level architecture and design decisions

    * We don't want these architectural decisions to be volatile

    * Thus, the software that encapsulates the high-level design of the system should be placed into stable packages (`I` = 0)

    * The unstable packages (`I` = 1) should only contain the software that is likely to change

* However, if the high-level design is placed into stable packages, then the source code that represents that design will be difficult to change

    * This could make the design inflexible

    * How can a package that is maximally stable (`I` = 0) be flexible enough to withstand change?

    * The answer is to be found in the OCP → This principle tells us that it is possible and desirable to create classes that are flexible enough to be extended w/o requiring modification

    * What kind of classes conforms to this principle? *Abstract classes* 
