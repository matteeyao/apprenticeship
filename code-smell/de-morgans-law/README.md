# De Morgan’s Laws in Software Engineering

## What are De Morgan's Laws?

De Morgan's laws state that specific Boolean statements can be written in different ways to the same effect.

1. A group of negated *ands* is the same as negated group of *ors*.

```
(!a && !b && !c) === !(a || b || c)
```

2. A group of negated *ors* is the same as a negated group of *ands*.

```
(!a || !b || !c) === !(a && b && c)
```

## Checking for Missing a Minimum Requirement

The first rule expresses that a group of negated ands is the same as a negated group of ors.

This can be used any time you want to take action when at least one condition must be met.

In the below example, we will check that at least a user name, user id, or user email is provided. If not, we will throw an error.

Here we express it as `(!a && !b && !c)`:

```c#
const userName = null;
const userId = null;
const userEmail = null;
if (!userName && !userId && !userEmail) {
  throw new Error('At least one user identifier must be passed');
}
```

Here we express it as `!(a || b || c)`:

```c#
const userName = null;
const userId = null;
const userAlias = null;
if(!(userName || userId || userAlias)) {
  throw new Error('At least one user identifier must be passed');
}
```

By using the second method, we can simply read this as “at least one of these is required.”

## Checking for Requirements

The second rule expresses that a group of negated *ors* is the same as a negated group of *ands*.

This can be used any time you want to take action when there are a series of conditions that must be met.

In the below example, we will check that a sequence of requirements are provided. If not, we will throw an error.

Here we express it as `(!a || !b || !c)`:

```js
const requirementA = true;
const requirementB = false;
const requirementC = true;
if(!requirementA || !requirementB || !requirementC) {
  throw new Error('All of the requirements must be met');
}
```

Here we express it as `!(a && b && c)`:

```js
const requirementA = true;
const requirementB = false;
const requirementC = true;
if(!(requirementA && requirementB && requirementC)) {
  throw new Error('All of the requirements must be met');
}
```

By using the second method, we can simply read this as “all these are required.”

## Summary

De Morgan's Laws can help simplify your code to make it more readable.

You can change a sequence of negated ands to something that reads as "at least one of these is required" and a sequence of negated ors to something that reads as "all of these are required."
