# S3 transfer acceleration

## What is S3 transfer acceleration?

S3 Transfer Acceleration utilizes the CloudFront Edge Network to accelerate your uploads to S3. Instead of uploading directly to your S3 bucket, you can use a distinct URL to upload directly to an edge location which will then transfer that file to S3. You will get a distinct URL to upload to: `acloudguru.s3-accelerate.amazonaws.com`

Users will upload to edge locations which will then upload the files across Amazon's backbone network directly to our S3 bucket in the region specified.
