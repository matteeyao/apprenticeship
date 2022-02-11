# Iterator

Iterator is a behavioral design pattern that allows sequential traversal through a complex data structure w/o exposing its internal details.

Thanks to the iterator, clients can go over elements of different collections in a similar fashion using a single iterator interface.

## Structure

![](../img/iterator-1.png)

1. The **Iterator** interface declares the operations required for traversing a collection: fetching the next element, retrieving the current position, restarting iteration, etc.

2. **Concrete Iterators** implement specific algorithms for traversing a collection. The iterator object should track the traversal progress on its own. This allows several iterators to traverse the same collection independently of each other.

3. The **Collection** interface declares one or multiple methods for getting iterators compatible w/ the collection. Note that the return type of the methods must be declared as the iterator interface so that the concrete collections can return various kinds of iterators.

4. **Concrete Collections** return new instances of a particular concrete iterator class each time the clients requests one. 

5. The **Client** works w/ both collections and iterators via their interfaces. This way the client isn't coupled to concrete classes, allowing you to use various collections and iterators w/ the same client code.

Typically, clients don't create iterators on their own, but instead get them from collections. Yet, in certain cases, the client can create one directly; for example, when the client defines its own special iterator.

## Pseudocode

In this example, the **Iterator** pattern is used to walk through a special kind of collection which encapsulates access to Facebook’s social graph. 

The collection provides several iterators that can traverse profiles in various ways.

![Example of iterating over social profiles.](../img/iterator-2.png)

The `friends` iterator can be used to go over the friends of a given profile. 

The `colleagues` iterator does the same, except it omits friends who don’t work at the same company as a target person.

Both iterators implement a common interface which allows clients to fetch profiles without diving into implementation details such as authentication and sending REST requests.

The client code isn’t coupled to concrete classes because it works with collections and iterators only through interfaces. 

If you decide to connect your app to a new social network, you simply need to provide new collection and iterator classes without changing the existing code.

```c#
/* The collection interface must declare a factory method for
producing iterators. You can declare several methods if there
are different kinds of iteration available in your program. */
interface SocialNetwork is
    method createFriendsIterator(profileId):ProfileIterator
    method createCoworkersIterator(profileId):ProfileIterator
    
/* Each concrete collection is coupled to a set of concrete
iterator classes it returns. But the client isn't, since the
signature of these methods returns iterator interfaces. */
class Facebook implements SocialNetwork is
    // ... The bulk of the collection's code should go here ...
    
    // Iterator creation code.
    method createFriendsIterator(profileId) is
        return new FacebookIterator(this, profileId, "friends")
    method createCoworkersIterator(profileId) is
        return new FacebookIterator(this, profileId, "coworkers")
        
// The common interface for all iterators.
interface ProfileIterator is
    method getNext():Profile
    method hasMore():bool
    
// The concrete iterator class.
class FacebookIterator implements ProfileIterator is
    /* The iterator needs a reference to the collection that it
    traverses. */
    private field facebook: Facebook
    private field profileId, type: string
    
    /* An iterator object traverses the collection independently
    from other iterators. Therefore it has to store the
    iteration state. */
    private field currentPosition
    private field cache: array of Profile
    
    constructor FacebookIterator(facebook, profileId, type) is
        this.facebook = facebook
        this.profileId = profileId
        this.type = type
    
    private method lazyInit() is
        if (cache == null)
            cache = facebook.socialGraphRequest(profileId, type)
            
    /* Each concrete iterator class has its own implementation
    of the common iterator interface. */
    method getNext() is
        if (hasMore())
            currentPosition++
            return cache[currentPosition]
            
    method hasMore() is
        lazyInit()
        return currentPosition < cache.length
        
/* Here is another useful trick: you can pass an iterator to a
client class instead of giving it access to a whole collection.
This way, you don't expose the collection to the client.

And there's another benefit: you can change the way the client
works w/ the collection at runtime by passing it a different
iterator. This is possible b/c the client code isn't coupled to
concrete iterator classes. */
class SocialSpammer is
    method send(iterator: ProfileIterator, message: string) is
        while (iterator.hasMore())
            profile = iterator.getNext()
            System.sendEmail(profile.getEmail(), message)

/* The applicaation class configures collections and iterators
and then passes them to the client code. */
class Application is
    field network: SocialNetwork
    field spammer: SocialSpammer
    
    method config() is
        if working with Facebook
            this.network = new Facebook()
        if working with LinkedIn
            this.network = new LinkedIn()
        this.spammer = new SocialSpammer()
        
    method sendSpamToFriends(profile) is
        iterator = network.createFriendsIterator(profile.getId())
        spammer.send(iterator, "Very important message")
        
    method sendSpamToCoworkers(profile) is
        iterator = network.createCoworkersIterator(profile.getId())
        spammer.send(iterator, "Very important message")
```

## Applicability

**Use the Iterator pattern when your collection has a complex data structure under the hood, but you want to hide its complexity from clients (either for convenience or security reasons).**

The iterator encapsulates the details of working w/ a complex data structure, providing the client w/ several simple methods of accessing the collection elements.

While this approach is very convenient for the client, it also protects the collection from careless or malicious actions which the client would be able to perform if working w/ the collection directly.

---

**Use the pattern to reduce duplication of the traversal code across your app.**

The code of non-trivial iteration algorithms tends to be very bulky. When placed within the business logic of an app, it may blur the responsibility of the original code and make it less maintainable.

Moving the traversal code to designated iterators can help you make the code of the application more lean and clean.

---

**Use the Iterator when you want your code to be able to traverse different data structures or when types of these structures are unknown beforehand.**

The pattern provides a couple of generic interfaces for both collections and iterators. Given that your code now uses these interfaces, it’ll still work if you pass it various kinds of collections and iterators that implement these interfaces.

## How to Implement

1. Declare the iterator interface. At the very least, it must have a method for fetching the next element from a collection. But for the sake of convenience you can adda couple of other methods, such as fetching the previous element, tracking the current position, and checking the end of the iteration.

2. Declare the collection interface and describe a method for fetching iterators. The return type should be equal to that of the iterator interface. You may declare similar methods if you plan to have several distinct groups of iterators.

3. Implement concrete iterator classes for the collections that you want to be traversable w/ iterators. An iterator object must be linked w/ a single collection instance. Usually, this link is established via the iterator's constructor.

4. Implement the collection interface in your collection classes. The main idea is to provide the client w/ a shortcut for creating iterators, tailored for a particular collection class. The collection object must pass itself to the iterator's constructor to establish a link between them.

5. Go over the client code to replace all of the collection traversal code w/ the use of iterators. The client fetches a new iterator object each time it needs to iterate over the collection elements.

## Relations w/ Other Patterns

* You can use **Iterators** to traverse **Composite** trees.

* You can use **Factory Method** along w/ **Iterator** to let collection subclasses return different types of iterators that are compitble w/ the collections.

* You can use **Memento** along w/ **Iterator** to capture the current iteration state and roll it back if necessary.

* You can use **Visitor** along w/ **Iterator** to traverse a complex data structure and execute some operation over its elements, even if they all have different classes. 

## Usage of the pattern in C#

**Identification**: Iterator is easy to recognize by the navigation methods (such as next, previous and others).

Client code that uses iterators might not have direct access to the collection being traversed.

## Conceptual Example

This example illustrates the structure of the **Iterator** design pattern. It focuses on answering these questions:

* What classes does it consist of?

* What roles do these classes play?

* In what way the elements of the pattern are related?

### Program.cs: Conceptual Example

```c#
using System;
using System.Collections;
using System.Collections.Generic;

namespace RefactoringGuru.DesignPatterns.Iterator.Conceptual
{
    abstract class Iterator : IEnumerator
    {
        object IEnumerator.Current => Current();
        
        // Returns the key of the current element
        public abstract int Key();
        
        // Returns the current element
        public abstract object Current();
        
        // Move forward to next element
        public abstract bool MoveNext();
        
        // Rewinds the Iterator to the first element
        public abstract void Reset();
    }
    
    abstract class IteratorAggregate : IEnumerable
    {
        /* Returns an Iterator or another IteratorAggregate for the 
        implementing object. */
        public abstract IEnumerator GetEnumerator();
    }
    
    /* Concrete Iterators implement various traversal algorithms. These classes
    store the current traversal position at all times. */
    class AlphabetircalOrderIterator : Iterator
    {
        private WordsCollection _collection;
        
        /* Stores the current traversal position. An iterator may have a lot
        of other fields for storing iteration state, especially when it is
        supposed to work w/ a particular kind of collection. */
        private int _position = -1;
        
        private bool _reverse = false;
        
        public AlpbabeticalOrderIterator(WordsCollection collection, bool reverse = false)
        {
            this._collection = collection;
            this._reverse = reverse;
            
            if (reverse)
            {
                this._position = collection.getItems().Count;
            }
        }
        
        public override object Current()
        {
            return this._collection.getItems()[_position];
        }
        
        public override int Key()
        {
            return this._position;
        }
        
        public override bool MoveNext()
        {
            int updatedPosition = this._position + (this._reverse ? -1 : 1);
            
            if (0 <= updatedPosition && updatedPosition < this._collection.getItems().Count)
            {
                this._position = updatedPosition;
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public override void Reset()
        {
            this._position = this._reverse ? this._collection.getItems().Count - 1 : 0;
        }
    }
    
    /* Concrete Collections provide one or several methods for retrieving fresh
    iterator instances, compatible w/ the collection class. */
    class WordsCollection : IteratorAggregate
    {
        List<string> _collection = new List<string>();
        
        bool _direction = false;
        
        public void ReverseDirection()
        {
            _directin = !_direction;
        }
        
        public List<string> getItems()
        {
            return _collection;
        }
        
        public void AddItem(string item)
        {
            this._collection.Add(item);
        }
        
        public override IEnumerator GetEnumerator()
        {
            return new AlphabeticalOrderIterator(this, _direction);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            /* The client code may or may not know about the Concrete Iterator
            or Collection classes, depending on the level of indirection you
            want to keep in your program. */
            var collection = new WordsCollection();
            collection.AddItem("First");
            collection.AddItem("Second");
            collection.AddItem("Third");
            
            Console.WriteLine("Straight traversal:");
            
            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
            
            Console.WriteLine("\nReverse traversale:");
            
            collection.ReverseDirection();
            
            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
        }
    }
}
```

### Output.txt: Execution result

```zsh
Straight traversal:
First
Second
Third

Reverse traversal:
Third
Second
First
```
