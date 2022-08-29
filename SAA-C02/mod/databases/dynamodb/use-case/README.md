# Use Case - Gaming application: Amazon DynamoDB

Companies in the gaming industry use Amazon DynamoDB in all aspects of game platforms, including game state, player data, session history, and leaderboards. Unlike Amazon RDS, DynamoDB is able to automatically scale to millions of concurrent users and requests while ensuring consistently low latency measured in single-digit milliseconds.

In this use case, player data is stored in DynamoDB for analytics to determine player behavior and usage patterns.

Use cases such as gaming, advertising tech, shopping carts, and IoT lend themselves particularly well to the key-value data model of DynamoDB.

![Fig. 1 Gaming application architecture](../../../../../img/SAA-CO2/databases/dynamodb/use-case/diag01.png)

## Learning Summary

Amazon DynamoDB is a fully managed non-relational database service – you simply create a database table, set your target utilization for Auto Scaling, and let the service handle the rest. You no longer need to worry about database management tasks such as hardware or software provisioning, setup, and configuration, software patching, operating a reliable, distributed database cluster, or partitioning data over multiple instances as you scale. DynamoDB also lets you backup and restore all your tables for data archival, helping you meet your corporate and governmental regulatory requirements.
