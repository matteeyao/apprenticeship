# Redshift Spectrum

Although Amazon Redshift Spectrum provides a similar in-query functionality like S3 Select, this service is more suitable for querying your data from the Redshift external tables hosted in S3. The Redshift queries are run on your cluster resources against local disk. Redshift Spectrum queries run using per-query scale-out resources against data in S3 which can entail additional costs compared with S3 Select.
