# The Common-Closure Principle (CCP)

*The classes in a package should be closed together against the same kinds of changes. A change that affects a package affects all the classes in that package and no other packages*

* This is the **Single-Responsibility Principle** restated for packages

    * Just as the SRP says that a class should not contain multiple reasons to change, this principle says that a package should not have multiple reasons to change

* In most applications, maintainability is more important than reusability

    * If the code in an application must change, you would rather that the changes occur all in one package, rather than being distributed through many packages

    * If changes are focused into a single package, then we need only release the one changed package

    * Other packages that don't depend on the changed package do not need to be revalidated or rereleased

* The CCP prompts us to gather together in one place all the classes that are likely to change for the same reasons

    * If two classes are so tightly bound, either physically or conceptually that they always change together, then they belong in the same package

    * This minimizes the workload related to releasing, revalidating, and redistributing the software

* This principle is closely associated w/ the Open-Closed Principle (OCP)

    * For it is "closure" in the OCP sense of the word that this principle is dealing w/

    * The OCP states that classes should be closed for modification but open for extension

    * But as we learned, 100% closure is not attainable

    * Closure must be strategic → We design our systems such that they are cosed to the most common kinds of changes that we have experienced

* The CCP amplifies this by grouping classes that are open to certain types of changes into the same packages

    * Thus, when a change in requirements comes along, that change has a good chance of being restricted to a minimal number of packages
