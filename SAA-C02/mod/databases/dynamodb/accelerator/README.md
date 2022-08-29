## DynamoDB Accelerator (DAX)

> * Fully managed, highly available, in-memory cache
>
> * 10x performance improvement
>
> * Reduces request time from milliseconds to **microseconds** - even under load
>
> * No need for developers to manage caching logic
>
> * Compatible w/ DynamoDB API calls

* W/ DynamoDB, there are situations when we have too many reads and your requests get throttled, meaning you'll get some errors when you try to read from DynamoDB, when it's under load.

* What we can do is introduce DAX into our application.

* DAX sites in between your application and DynamoDB.

* Unlike traditional caches, such as Redis and Memcached, DAX also has what's called a write through cache.

* In addition to taking advantage of read performance improvement, you can also get write performance improvement. If you send your writes through DAX, they flow directly into DynamoDB, and DAX can respond in microseconds.

* It's also designed for high availability. In the event of a failure of one availability zone, it'll fail over to its own replica in another availability zone and it takes care of all this for you automatically.
