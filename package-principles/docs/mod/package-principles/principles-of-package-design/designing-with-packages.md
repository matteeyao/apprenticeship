# I. Designing w/ Packages?

In UML, packages can be used as containers for groups of classes

By grouping classes into packages, we can reason about the design at a higher level of abstraction

We can also use the packages to manage the development and distribution of the software

The goal is to partition the classes in an application according to some criteria, and then allocate the classes in those partitions to packages

But classes often have dependencies on other classes, and these dependencies will very often cross package boundaries

Thus, the packages will have dependency relationships w/ each other

The relationships btwn packages express the high-level organization of the application, and they need to be managed

This begs a large number of questions:

1. What are the principles for allocating classes to packages?

2. What design principles govern the relationships between packages?

3. Should packages be designed before classes (top down)? Or should classes be designed before packages (bottom up)?

4. How are packages physically represented? In C++? In Java? In the development environment?

5. Once created, to what purpose will we put these packages?

This dir presents six design principles that govern the creation, interrelationship, and use of packages

The first three govern the partitioning of classes into packages

The last three govern the interrelationships btwn packages
