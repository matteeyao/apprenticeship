# IV. Backing services

A *backing service* is any service the app consumes over the network as part of its normal operation.

Examples include datastores (such as MySQL or CouchDB), messaging/queuing systems (such as RabbitMQ or Beanstalkd), SMTP services for outbound email (such as Postfix), and caching systems (such as Memcached).

Backing services like the database are traditionally managed by the same systems administrators who deploy the app's runtime.

In addition to these locally-managed services, the app may also have services provided and managed by third parties.

Examples include SMTP services (such as Postmark), metrics-gathering services (such as New Relic or Loggly), binary asset services (such as Amazon S3), and even API-accessible consumer services (such as Twitter, Google Maps, or Last.fm).

**The code for a twelve-factor app makes no distinction btwn local and third party services**.

To the app, both are attached resources, accessed via a URL or other locator/credentials stored in the config.

A deploy of the twelve-factor app should be able to swap out a local MySQL database w/ one managed by a third party (such as Amazon RDS) w/o any changes to the app's code.

Likewise, a local SMTP server could be swapped w/ a third-party SMTP service (such as Postmark) w/o code changes.

In both cases, only the resource handle in the config needs to change.

Each distinct backing service is a *resource*.

For example, a MySQL database is a resource; two MySQL databases (used for sharding at the application layer) qualify as two distinct resources.

The twelve-factor app treats these databases as *attached resources*, which indicates their loose coupling to the deploy they are attached to.

![](../img/backing-services.png)

Resources can be attached to and detached from deploys at will.

For example, if the app's database is misbehaving due to a hardware issue, the app's administrator might spin up a new database server restored from a recent backup.

The current production database could be detached, and the new database attached - all w/o any code changes.
