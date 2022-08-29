# Non-Relational Databases

## Comparing relational and non-relational databases

<table class="fr-alternate-rows" style="width:100%;margin-right:calc(0%);"><tbody><tr><td style="width:23.8783%;background-color:rgb(237, 125, 49);text-align:center;"><strong><span style="color:rgb(255, 255, 255);font-size:18px;">Characteristic</span></strong></td><td style="width:38.4476%;background-color:rgb(237, 125, 49);text-align:center;"><span style="font-size:18px;"><strong><span style="color:rgb(255, 255, 255);">Relational</span></strong></span></td><td style="width:37.6108%;background-color:rgb(237, 125, 49);text-align:center;"><strong><span style="font-size:18px;color:rgb(255, 255, 255);">Non-relational</span></strong></td></tr><tr><td class="fr-thick" style="width:23.8783%;background-color:rgb(251, 228, 213);"><span style="font-size:17px;">Representation</span></td><td style="width:38.4476%;"><span style="font-size:17px;">Multiple tables, each containing columns and rows</span><br></td><td style="width:37.6108%;"><span style="font-size:17px;">Collection of data in a single table with keys and values</span><br></td></tr><tr><td class="fr-thick" style="width:23.8783%;background-color:rgb(251, 228, 213);"><span style="font-size:17px;">Data design</span></td><td style="width:38.4476%;"><span style="font-size:17px;">Normalized relational or dimensional data warehouse</span><br></td><td style="width:37.6108%;"><span style="font-size:17px;">Denormalized document, wide column, or key-value</span><br></td></tr><tr><td class="fr-thick" style="width:23.8783%;background-color:rgb(251, 228, 213);"><span style="font-size:17px;">Optimization</span></td><td style="width:38.4476%;"><span style="font-size:17px;">Optimized for storage</span><br></td><td style="width:37.6108%;"><span style="font-size:17px;">Optimized for compute</span><br></td></tr><tr><td class="fr-thick" style="width:23.8783%;background-color:rgb(251, 228, 213);"><span style="font-size:17px;">Query style</span></td><td style="width:38.4476%;"><span style="font-size:17px;">Language: SQL<br></span></td><td style="width:37.6108%;"><span style="font-size:17px;">Language: many<br>Uses object querying</span></td></tr><tr><td class="fr-thick" style="width:23.8783%;background-color:rgb(251, 228, 213);"><span style="font-size:17px;">Scalability</span></td><td style="width:38.4476%;"><span style="font-size:17px;">Vertically</span></td><td style="width:37.6108%;"><span style="font-size:17px;">Horizontally</span></td></tr><tr><td class="fr-thick" style="width:23.8783%;background-color:rgb(251, 228, 213);"><span style="font-size:17px;">Implementation</span></td><td style="width:38.4476%;"><span style="font-size:17px;">OLTP business systems or OLAP</span><br></td><td style="width:37.6108%;"><span style="font-size:17px;">OLTP web/mobile apps</span><br></td></tr></tbody></table>

Non-relational databases can be deployed on massively distributed commodity servers. These databases have an advantage in scaling and can handle much larger data sets than relational databases. The massive distribution and scale does come at a cost, in the form of eventual consistency. This means that data may not be updated at the same time for all of the distributed systems. Eventual consistency can be an issue for applications that require ACID (Atomicity, Consistency, Isolation, Durability) compliance. Organizations requiring ACID compliance will want to be sure the non-relational database they choose can support this requirement.

## Non-relational database types

> ### Key-value databases
>
> Key-value databases logically store data in a single table. Within the table, the values are associated with a specific key and stored in the form of blob objects without a predefined schema. The values can be of nearly any type.
>
> **Strengths**
>
> * Very flexible
>
> * Able to handle a wide variety of data types
>
> * Keys are linked directly to their values with no need for indexing or complex join operations
>
> * Content of a key can easily be copied to other systems without reprogramming the data
>
> **Weaknesses**
>
> * Analytical queries are difficult to perform due to the lack of joins
>
> * Access patterns need to be known in advance for optimum performance

> ### Document databases
>
> Document stores keep files containing data as a series of elements. These files can be navigated using numerous languages including Python and Node.js. Each element is an instance of a person, place, thing, or event. For example, a document store may hold a series of log files from a set of servers. These log files can each contain the specifics for that system without concern for what the log files in other systems contain.
>
> **Strengths**
>
> * Flexibility
>
> * No need to plan for a specific type of data when creating one
>
> * Easy to scale
>
> **Weaknesses**
>
> * Sacrifice ACID compliance for flexibility
>
> * Databases cannot query across files natively

> ### In-memory databases
>
> In-memory databases are used for applications that require real-time access to data. Most databases have areas of data that are frequently accessed but seldom updated. Additionally, querying a database is always slower and more expensive than locating a key in a key-value pair cache. Some database queries are especially expensive to perform. By caching such query results, you pay the price of the query once and then are able to quickly retrieve the data multiple times without having to re-execute the query.
>
> **Strengths**
>
> * Support the most demanding applications requiring sub-millisecond response times
>
> * Great for caching, gaming, and session store
>
> * Adapt to changes in demands by scaling out and in without downtime
>
> * Provide ultrafast (sub-microsecond latency) and inexpensive access to copies of data
>
> **Weaknesses**
>
> * Data that is rapidly changing or is seldom accessed
>
> * Application using the in-memory store has a low tolerance for stale data

> ### Graph databases
>
> Graph databases store data as nodes, while edges store information on the relationships between nodes. Data within a graph database is queried using specific languages associated with the software tool you have implemented.
>
> **Strengths**
>
> * Allow simple, fast retrieval of complex hierarchical structures
>
> * Great for real-time big data mining
>
> * Can rapidly identify common data points between nodes
>
> * Great for making relevant recommendations and allowing for rapid querying of those relationships
>
> **Weaknesses**
>
> * Cannot adequately store transactional data
>
> * Analysts must learn new languages to query the data
>
> * Performing analytics on the data may not be as efficient as with other database types

##
