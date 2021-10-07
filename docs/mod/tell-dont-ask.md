# Tell-Don't Ask

Tell-Don't-Ask is a principle that helps ppl remember that object-orientation is about bundling data w/ the functions that operate on that data.

It reminds us that rather than asking an object for data and acting on that data, we should instead tell an object what to do.

This encourages to move behavior into an object to go w/ the data.

![](../img/tell-diagram.png)

Let's clarify w/ an example.

Let's imagine we need to monitor certain values, signaling an alarm should the value rise above a certain limit.

If we write this in an "ask" style, we might have a data structure to represent these things...

```
class AskMonitor...

  private int value;
  private int limit;
  private boolean isTooHigh;
  private String name;
  private Alarm alarm;

  public AskMonitor (String name, int limit, Alarm alarm) {
    this.name = name;
    this.limit = limit;
    this.alarm = alarm;
  }
```

...and combine this w/ some accessors to get at this data:

```
class AskMonitor...

  public int getValue() {return value;}
  public void setValue(int arg) {value = arg;}
  public int getLimit() {return limit;}
  public String getName()  {return name;}
  public Alarm getAlarm() {return alarm;}
```

We would then use the data structure like this

```
AskMonitor am = new AskMonitor("Time Vortex Hocus", 2, alarm);
am.setValue(3);
if (am.getValue() > am.getLimit()) 
  am.getAlarm().warn(am.getName() + " too high");
```

"Tell Don't Ask" reminds us to instead put the behavior inside the monitor object itself (using the same fields).

```
class TellMonitor...

  public void setValue(int arg) {
    value = arg;
    if (value > limit) alarm.warn(name + " too high");
  }
```

Which would be used like this:

```
TellMonitor tm = new TellMonitor("Time Vortex Hocus", 2, alarm);
tm.setValue(3);
```

Many people find tell-don't-ask to be a useful principle. One of the fundamental principles of object-oriented design is to combine data and behavior, so that the basic elements of our system (objects) combine both together.

This is often a good thing b/c this data and the behavior that manipulates them are tightly coupled: changes in one cause changes in the other, understanding one helps you understand the other.

Things that are tightly coupled should be in the same component.

Thinking of tell-don't-ask is a way to help programmers to see how they can increase this co-location.

Look to co-locate data and behavior, which often leads to similar results.

One thing troubling about tell-don't-ask is that it encourages people to become `GetterEradicators`, seeking to get rid of all query methods.

But there are times when objects collaborate effectively by providing information. A good example are objects that take input information and transform it to simplify their clients, such as using `EmbeddedDocument`.

There are instances where code can get into convolutions of only telling where suitably responsible query methods would simplify matters.
