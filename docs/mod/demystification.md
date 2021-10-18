# Demystifying the Law of Demeter principle

The Law of Demeter principle reduces dependencies and helps build components that are loosely coupled for code reuse, easier maintenance, and testability

Law of Demeter (or Principle of Least Knowledge) is a design guideline for developing software application - states that an object should never know the internal details of other objects.

Designed to promote loose coupling in software designs.

Coupling = the degree of interdependence that exists btwn software modules and how closely such modules are connected to each other.

The more coupling btwn the components in an application, the harder it becomes to modify and maintain over time.

Design systems that are easier to test and maintain by ensuring that the components in an application are loosely coupled.

## Understanding the Law of Demeter principle

A module should not have the knowledge on the inner details of the objects it manipulates.

A software component or an object should not have the knowledge of the internal working of other objects or components.

Consider three classes - `A`, `B`, and `C` - and objects of these classes - `objA`, `objB`, and `objC` respectively.

Suppose `objA` is dependent on `objB`, which in turn composes objC.

In this scenario, objA can invoke methods and properties of `objB` but not `objC`.

The Law of Demeter princ takes advantage of encapsulation to achieve the isolation and reduce coupling amongst the components of the app...

... which helps in improving the code quality and promotes flexibility and easier code maintenance.

Benefit of adhering to the Law of Demeter is that you can build software that is easily maintainable and adaptable to future changes.

Consider class `C` having a method `M`.

Now suppose you have created an instance of the class `C` named `O`.

The Law of Demeter specifies that the method `M` can invoke the following types of `.` or a property of a class should invoke the following type of members only:

* The same object, i.e., the object "O" itself

* Objects that have been passed as an argument to the method "M"

* Local objects, i.e., objects that have been created inside the method "M"

* Global objects that are accessible by the object "O"

* Direct component objects of the object "O"

See code listing below that illustrates a class and its members that adhere to the Law of Demeter principle:

```c#
public class LawOfDemeterExample {
    /* This is an instance in the class scope and hence this instance can be accessed 
    by any members of this class */
    AnotherClass instance = new AnotherClass();
    
    public void SampleMethodFollowingLoD(Test obj)
    {
        DoNothing(); // This is a valid call as you are calling a method of the same class
    
        object data = obj.GetData(); /* This is also valid since you are calling a method
        on an instance that has been passed as a parameter */
        
        int result = instance.GetResult();  /* This is also a valid call as you are calling
        a method on an instance locally created */
    }
    
    private void DoNothing()
    {
        // Write some code here
    }
}
```

Here are the two other classes that you would need to compile the above code.

```c#
public class AnotherClass
{
    public int GetResult()
    {
        return -1;
    }
}

public class Test
{
    public object GetData()
    {
        return null;
    }
}
```

The Law  of Demeter applies to methods and properties as well.

## Law of Demeter Principle violations

```c#
var data = new A().GetObjectB().GetObjectC().GetData();
```

In this example, the client will have to depend on classes A, B, and C.

In other words, it is coupled to instances of the classes A, B, and C.

If in the future these classes change, you would run into trouble as you are exposing yourself to changes that might occur in any of these classes in the future.
