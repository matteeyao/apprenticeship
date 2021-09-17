# Higher-Level Testing

**Integration Testing**: checks the data flow from one module to other modules.

**System Testing**: evaluates both functional and non-functional needs for the testing.

**Acceptance Testing**: checks the requirements of a specification or contract are met as per its delivery.

## Unit Testing

A type of software testing where individual units or components of a software are tested. The purpose is to validate that each unit of the software code performs as expected. Unit testing is done during the development (coding phase) of an application by the developers. Unit Tests isolate a section of code and verify its correctness. A unit may be an individual function, method, procedure, module, or object.

The aim is to test each part of the software by separating it. It checks that component are fulfilling functionalities or not.

### Unit Testing Techniques

The Unit Testing Techniques are mainly categorized into three parts which are **Black box** testing that involves testing of user interface along with input and output, **White box** testing that involves testing the functional behavior of the software application and **Gray box** testing that is used to execute test suites, test methods, test cases and perform risk analysis.

In **white box testing**, the testers is concentrating on how the software works. In other words, the tester will be concentrating on the internal working of source code concerning control flow graphs or flow charts. **White box testing** is based on the inner workings of an application and revolves around internal testing. 

* One of the basic goals of **white box testing** is to verify a working flow for an application.

* It involves testing a series of predefined inputs against expected or desired outputs so that when a specific input does not result in the expected output, you have encountered a bug.

Unit testing is a testing method by which individual units of source code are tested to determine if they are ready to use, whereas Integration testing checks integration between software modules.

Unit Testing starts with the module specification, while Integration Testing starts with interface specification.

### Unit Test Example: Mock Objects

Unit testing relies on mock objects being created to test sections of code that are noy yet part of a complete application. Mock objects fill in for the missing parts of the program.

For example, you might have a function that needs variables or objects that are not created yet. In unit testing, those will be accounted for in the form of mock objects created solely for the purpose of the unit testing done on that section of code.

## Integration Testing

Different software modules are combined and tested as a group to make sure that integrated system is ready for system testing.

Integrating testing checks the data flow from one module to other modules.

Defined as a type of testing where software modules are integrated logically and tested as a group. The purpose of this level of testing is to expose defects in the interaction between these software modules when they are integrated.

Integration testing focuses on checking data communication amongst these modules.

Although each software module is unit tested, defects still exist for these reasons:

* A Module, in general, is designed by an individual software developer whose understanding and programming logic may differ from other programmers. Integration Testing becomes necessary to verify the software modules work in unity

* At the time of module development, there are wide chances of change in requirements by the clients. These new requirements may not be unit tested and hence system integration testing becomes necessary.

* Interfaces of the software modules with the database could be erroneous.

* External Hardware interfaces, if any, could be erroneous.

* Inadequate exception handling could cause issues.

### Approaches, Strategies, Methodologies of Integration Testing

Software Engineering defines variety of strategies to execute Integration testing:

* Big Bang Approach:

* Incremental Approach: which is further divided into the following

    * Top Down Approach

    * Bottom Up Approach

    * Sandwich Approach – Combination of Top Down and Bottom Up

### Big Bang Testing

Big Bang Testing is an Integration testing approach in which all the components or modules are integrated together at once and then tested as a unit. This combined set of components is considered as an entity while testing. If all of the components in the unit are not completed, the integration process will not execute.

Advantages:

* Convenient for small systems.

Disadvantages:

* Fault Localization is difficult.

* Given the sheer number of interfaces that need to be tested in this approach, some interfaces link to be tested could be missed easily.

* Since the Integration testing can commence only after “all” the modules are designed, the testing team will have less time for execution in the testing phase.

* Since all modules are tested at once, high-risk critical modules are not isolated and tested on priority. Peripheral modules which deal with user interfaces are also not isolated and tested on priority.

### Incremental Testing

In the Incremental Testing approach, testing is done by integrating two or more modules that are logically related to each other and then tested for proper functioning of the application. Then the other related modules are integrated incrementally and the process continues until all the logically related modules are integrated and tested successfully.

Incremental Approach, in turn, is carried out by two different Methods:

* Bottom Up

* Top Down

### Stubs and Drivers

Stubs and Drivers are the dummy programs in Integration testing used to facilitate the software testing activity. These programs act as a substitutes for the missing models in the testing. They do not implement the entire programming logic of the software module but they simulate data communication with the calling module while testing.

`Stub`: Is called by the Module under Test.

`Driver`: Calls the Module to be tested.

### Bottom-up Integration Testing

Bottom-up Integration Testing is a strategy in which the lower level modules are tested first. These tested modules are then further used to facilitate the testing of higher level modules. The process continues until all modules at top level are tested. Once the lower level modules are tested and integrated, then the next level of modules are formed.

Advantages:

* Fault localization is easier.

* No time is wasted waiting for all modules to be developed unlike Big-bang approach

Disadvantages:

* Critical modules (at the top level of software architecture) which control the flow of application are tested last and may be prone to defects.

* An early prototype is not possible

### Top-down Integration Testing

Top Down Integration Testing is a method in which integration testing takes place from top to bottom following the control flow of software system. The higher level modules are tested first and then lower level modules are tested and integrated in order to check the software functionality. Stubs are used for testing if some modules are not ready.

Advantages:

* Fault Localization is easier.

* Possibility to obtain an early prototype.

* Critical Modules are tested on priority; major design flaws could be found and fixed first.

Disadvantages:

* Needs many Stubs.

* Modules at a lower level are tested inadequately.

### Sandwich Testing

Sandwich Testing is a strategy in which top level modules are tested with lower level modules at the same time lower modules are integrated with top modules and tested as a system. It is a combination of Top-down and Bottom-up approaches therefore it is called Hybrid Integration Testing. It makes use of both stubs as well as drivers.

## Acceptance Testing
Acceptance – You should test that the program works the way a user/customer expects the application to work. Acceptance tests ensure that the functionality meets business requirements.
