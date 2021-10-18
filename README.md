# The Law of Demeter

The Law Of Demeter (LoD) is a guideline with the aim to help reduce coupling between components. 

At its heart, the LoD  is about encapsulation or the "principle of least knowledge" regarding the object instances used within a method.

It is fine to request a service of an object's instance, but if I reach into that object to access another sub-object and request a service of that sub-object, I am assuming knowledge of the deeper structure of the original object.

Instead, we want to ask objects to perform some action for us. Let the object deal w/ its collaborators.

The Law of Demeter for functions, properly defined, states that:

A method "`M`" of an object "`O`" should invoke *only* the methods of the following kinds of objects:

1. itself

2. its parameters

3. any objects it creates/instantiates

4. its direct component objects

## Practical Illustration

We've all seen long chain of functions like these:

```
obj.getX()
    .getY()
        .getZ()
            .doSomething();
```

We ask then ask then ask before we tell anything. Wouldn't it look better like this:

```
obj.doSomething();
```

The call to `doSomething()` propagates outward until it gets to `z`.

These long chains of queries, called train wrecks, violate something called the Law of Demeter.

Recall that LoD cautions against the idea for single functions to know the entire navigation structure of the system.

Consider how much knowledge ` obj.getX().getY().getZ().doSomething()` has. It knows `obj` has an `X`, `X` has a `Y`, `Y` has a `Z` and that `Z` can do something.

That is a huge amount of knowledge that this line has and it couples the function that contains it to too much of the whole system.

"Each unit should have only limited knowledge about other units: only units "closely" related to the current unit. Each unit should only talk to its friends; don't talk to strangers."

When applied to OOP, LoD formalizes the **Tell Don't Ask** principle w/ these set of rules.

You may call methods of objects that are:

1. Passed as arguments

2. Created locally

3. Instance variables

4. Globals

Take for instance the example below:

```java
class User {
    Account account;
    ...
    double discountedPlanPrice(String discountCode) {
        Coupon coupon = Coupon.create(discountCode);
        return coupon.discount(account.getPlan().getPrice());
    }
}

class Account {
    Plan plan;
    ...
}
```

Above `account.getPlan().getPrice()` violates the LoD. The most obvious fix to delegate/tell:

```java
class User {
    Account account;
    ...
    double discountedPlanPrice(String discountCode) {
        // delegate
        return account.discountedPlanPrice(discountCode);
    }
}

class Account {
    Plan plan;
    ...
    double discountedPlanPrice(String discountCode) {
        Coupon coupon = Coupon.create(discountCode);
        return coupon.discount(plan.getPrice());
    }
}
```

## Summary

We don't want our functions to know about the entire object map of the system.

Individual functions should have a limited amount of knowledge.

We want to tell out neighboring objects what we need to have done and depend on them to propagate that message outwards to the appropriate destination.

Any function that "*tells*" instead of "*asks*" is decoupled from its surroundings.
