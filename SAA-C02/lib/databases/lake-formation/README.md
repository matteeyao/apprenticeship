# Lake Formation

1. An application is loading hundreds of JSON documents into an Amazon S3 bucket every hour which is registered in AWS Lake Formation as a data catalog. The Data Analytics team uses Amazon Athena to run analyses on these data, but due to the volume, most queries take a long time to complete.

What change should be made to improve the query performance while ensuring data security?

[ ] Apply minification on the data and implement the Lake Formation tag-based access control (LF-TBAC) authorization strategy to ensure security.

[ ] Transform the JSON data into Apache Parque format. Ensure that the user has an `lakeformation:GetDataAccess` IAM permission for underlying data access control.

[ ] Compress the data into GZIP format before storing it in the S3 bucket. Apply an IAM policy w/ `aws:SourceArn` and `aws:SourceAccount` global condition context keys in Lake Formation that prevents cross-service confused deputy problems and other security issues.

[ ] Convert the JSON documents into CSV format. Provide fine-grained named resource access control to specific databases or tables in AWS Lake Formation.

**Explanation**: **Amazon Athena** supports a wide variety of data formats like CSV, TSV, JSON, or Textfiles and also supports open-source columnar formats such as Apache ORC and Apache Parquet. Athena also supports compressed data in Snappy, Zlib, LZO, and GZIP formats. By compressing, partitioning, and using columnar formats you can improve performance and reduce your costs.

Parquet and ORC file formats both support predicate pushdown (also called predicate filtering). Parquet and ORC both have blocks of data that represent column values. Each block holds statistics for the block, such as max/min values. When a query is being executed, these statistics determine whether the block should be read or skipped.

Athena charges you by the amount of data scanned per query. You can save on costs and get better performance if you partition the data, compress data, or convert it to columnar formats such as Apache Parquet.

Apache Parquet is an open-source columnar storage format that is 2x faster to unload and takes up 6x less storage in Amazon S3 as compared to other text formats. One can `COPY` Apache Parquet and Apache ORC file formats from Amazon S3 to your Amazon Redshift cluster. Using AWS Glue, one can configure and run a job to transform CSV data to Parquet. Parquet is a columnar format that is well suited for AWS analytics services like Amazon Athena and Amazon Redshift Spectrum.

When an integrated AWS service requests access to data in an Amazon S3 location that is access-controlled by AWS Lake Formation, Lake Formation supplies temporary credentials to access the data. To enable Lake Formation to control access to underlying data at an Amazon S3 location, you register that location with Lake Formation.

To enable Lake Formation principals to read and write underlying data with access controlled by Lake Formation permissions:

* The Amazon S3 locations that contain the data must be registered with Lake Formation.

* Principals who create Data Catalog tables that point to underlying data locations must have data location permissions.

* Principals who read and write underlying data must have Lake Formation data access permissions on the Data Catalog tables that point to the underlying data locations.

* Principals who read and write underlying data must have the `lakeformation:GetDataAccess` IAM permission.

Thus, the correct answer is: **Transform the JSON data into Apache Parquet format. Ensure that the user has an `lakeformation:GetDataAccess` IAM permission for underlying data access control.**

> The option that says: **Convert the JSON documents into CSV format. Provide fine-grained named resource access control to specific databases or tables in AWS Lake Formation** is incorrect because Athena queries performed against row-based formats like CSV are slower than columnar file formats like Apache Parquet.

> The option that says: **Apply minification on the data and implement the Lake Formation tag-based access control (LF-TBAC) authorization strategy using IAM Tags to ensure security** is incorrect. Although minifying the JSON file might reduce its overall file size, there won't be a significant difference in terms of querying performance. LF-TBAC is a type of an attribute-based access control (ABAC) that defines permissions based on certain attributes, such as tags in AWS. LF-TBAC uses LF-Tags to grant Lake Formation permissions and not regular IAM Tags.

> The option that says: **Compress the data into GZIP format before storing in the S3 bucket. Apply an IAM policy with `aws:SourceArn` and `aws:SourceAccount` global condition context keys in Lake Formation that prevents cross-service confused deputy problems and other security issues** is incorrect. Compressing the files prior to storing them in Amazon S3 will only save storage costs. As for query performance, it won't have much improvement. In addition, using an IAM Policy to prevent cross-service confused deputy issues is not warranted in this scenario. Having an `lakeformation:GetDataAccess` IAM permission for underlying data access control should suffice.

<br />
