# The Reuse-Release Equivalence Principle (REP)

*The granule of reuse is the granule of release.*

Deals w/ clerical and support effort that must be provided if other people are going to reuse code

But those political and clerical issues have a profound effect on the packaging structure of software

In order to provide the guarantees that reusers need, authors must organize their software into reusable packages and then track those packages w/ release numbers

The REP states that the granule of reuse (i.e., a package) can be no smaller than the granule of release

Anything that we reuse must also be released and tracked

It is not realistic for a developer to simply write a class and then claim it is reusable

Re-usability comes only after there is a tracking system in place that offers the guarantees of notification, safety, and support that the potential reusers will need

The REP gives us our first hint at how to partition our design into packages

Since re-usability must be based on packages, reusable packages must contain reusable classes

So, at least some packages should comprise reusable sets of classes

If software is going to be reused, then it must be partitioned in a manner that humans find convenient for that purpose

So what does this tell us about the internal structure of the package? One must consider the internal contents from the pov of potential reusers

If a package contains software that should be reused, then it should not also contain software that is not designed for reuse

*Either all of the classes in a package are reusable or none of them are*

Re-usability is not the only criterion; we must also consider who the reuser is

Certainly, a container-class library is reusable, and so is a financial framework

But we would not want them to be part of the same package

There are many people who would like to reuse a container-class library who have no interest in a financial framework

Thus, we want all of the classes in a package to be reusable by the same audience

We do not want an audience to find that a package consists of some classes he needs and others that are wholly inappropriate for him
