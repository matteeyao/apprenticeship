# S3 Transfer Acceleration

> *Secure transfers of files over long distances*

## What is S3 transfer acceleration?

S3 Transfer Acceleration utilizes the **CloudFront Edge Network** to accelerate your uploads to S3. Instead of uploading directly to your S3 bucket, you can use a distinct URL to upload directly to an edge location which will then transfer that file to S3. You will get a distinct URL to upload to: `acloudguru.s3-accelerate.amazonaws.com`

Users will upload to edge locations which will then upload the files across Amazon's backbone network directly to our S3 bucket in the region specified.

You can use **Amazon S3 Transfer Acceleration** for fast, easy, and secure transfers of files over long distances. It takes advantage of Amazon CloudFront globally distributed edge locations, routing data to Amazon S3 over an optimized network path.

**Transfer Acceleration** is best suited for scenarios in which you want to transfer data to a central location from all over the world or transfer significant amounts of data across continents regularly. It can also help you use your available bandwidth when uploading to Amazon S3.
