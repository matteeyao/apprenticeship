# Deploying an Amazon RDS Multi-AZ and Read Replica

## Introduction

In this hands-on lab, we will work w/ Relational Database Service (RDS). This lab will provide you w/ hands-on experience w/ enabling Multi-AZ and backups, creating a read replica, promoting a read replica, and updating the RDS endpoint in Route 53. Multi-AZ and Read Replicas serve different purposes w/ RDS. Multi-AZ is strictly for failover, as the standby instances cannot be read from by an application. Read Replicas are used for improved performance and migrations. W/ Read Replicas, you can write to the primary database and read from the read replica. B/c a read replica can be promoted to be the primary database, it makes for a great tool in disaster recovery and migrations.

![Deploying an Amazon RDS Multi-AZ and Read Replica - Architecture](https://s3.amazonaws.com/assessment_engine/production/labs/aacf9e92-0bb7-4969-aaf7-e2e106a7e339/lab_diagram_Lab_-_Deploying_an_Amazon_RDS_Multi-AZ_and_Read_Replica.001.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=ASIAVKPCGNLN5QKNCBWL%2F20220318%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20220318T030212Z&X-Amz-Expires=3600&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEDkaCXVzLWVhc3QtMSJGMEQCIBn5mEmg0KUyuupfi6sTNiFJWlarhF2nCOYvH2u13LcbAiBcqnagOvIv5U59mHmZUagcYZFq%2B67xvC7PE2cskWMJYSqMBAix%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F8BEAIaDDM2NjA4MzQ2Nzk5NSIMGhyW%2B9frAtPzp1ayKuADShBtYgwwXG6YktCTFcEjD5e1nvYkwhIBfPtyP%2B%2F1NISUkDOl0sGAqbL5jpuHDL%2BtpbnDLk%2Fei05frx%2F6ydCIpq%2FCGTe0GPulVz2tQcOhKVMWAfM3iMwgwkt82bTfA%2BgSZdWH8nKhGpd4d1Fbu58thQqID9UBMLS%2BrDemGZJQfLJPsekaM4rV%2ByafctitF373FCb%2BJmUS6a0Zduo68133OFHCNm9resTDOQItcLJVA3s%2BMGVtqKin3RvtKejfogjpPBsuCToCsFbQ6H2xQFnhZPphjd%2BKnZaXrYr3jVjBH20pyJlQj3WEEsEeE8RNo6nd%2BavxBwqtq1MxERddAVolml0B3WMwzGQtqDYOD8SNT2z1m7vYC74iWhfwz7NlwbcwzVDnyZFNyxXXGEs9G0znvQymPgCHPHZQuf0zqNKOM7C%2BeeRLz4EMvmnuNglyE0nd7%2B296G2Wtb%2BZvK0MSI6xYq4DX0nJAPMd06pLvXkOzmBwX2gyF5MhxIdREA%2BnQEr8L9B8yYWL%2BfvGVGVtng5SmzZBdInIgyS24syKu%2BKTWr7qNBpWmTHkSnB1OBbkC918kl6bLQ4Lzeb%2FP%2B5yT5qlf98fF%2FmKXBoMg8zXV0rC7DV%2FLLoYRHOPbvUGc9CaN1x7MMSaz5EGOqYBeSswJUlP8uWoXJgZpwmkuN%2Bj0DZ606HZndCpU39BKVP3R4KxW2WrDZojH2ox70NyAAeiZEWUl9M2FqG%2FSp5Djnm6IhAMdAaIPggiRWP4L28vJLrZGG0EtjXLqBwmeugZ6R%2B9LRA3CN2M1BhDbSsncrKJqn3JSZEnjwTc1buos3dDgJ8%2BbgQyAFpIimDmOypU%2Bp9l23E0hIhOSgca0d%2FFzO8IzScpww%3D%3D&X-Amz-SignedHeaders=host&X-Amz-Signature=a8780f8edab0e29c036495187a2823118ff9684d3493fde057fa5f50dbdc44b0)

* Multi-AZ is simply a failover mechanism. We cannot assign our applications to read from a standby Multi-AZ instance.

* Read Replicas, on the other hand, help us w/ performance. We can actually offload some of the compute capacity from our primary database onto a Read Replica.

* And by doing this, we free up compute space on that primary or master so that it can focus on writing to the database.

* Read replicas can also be used in disaster recovery (DR) situations and migrations.

> In this hands-on lab, we will work w/ Relational Database Service (RDS). This lab will provide you w/ hands-on experience with:
>
> * Enabling Multi-AZ and backups
>
> * Creating a read replica
>
> * Promoting a read replica
>
> * Updating the RDS endpoint in Route 53
>
> Multi-AZ is strictly for failover, as the standby instances cannot be read from by an application. Read replicas are used for improved performance and migrations. W/ read replicas, you can write to the primary database and read from the read replica. B/c a read replica can be promoted to be the primary database, it makes for a great tool in disaster recovery and migrations.

## Solution

Log in to the live AWS environment using the credentials provided. Make sure you're in the N. Virginia (`us-east-1`) region throughout the lab.

### Enable Multi-AZ Deployment

1. Navigate to **EC2** ▶︎ **Load Balancers**.

2. Copy the DNS name of the load balancer.

3. Open a new browser tab, and enter the DNS name.

* We will use the web page to test failovers and promotions in this lab.

4. Back in the AWS console, navigate to **RDS** ▶︎ **Databases**.

5. CLick on our database instance.

6. Click **Modify**.

7. Under *Multi-AZ deployment*, click **Create a standby instance (recommended for production usage)**.

8. Use a burstable class db.t3.micro instance.

9. Click **continue** at the bottom of the page.

10. Under *Scheduling of modifications*, select **Apply immediately**, and then click **Modify DB Instance**.

11. Once the instance shows Multi-AZ status as **Available** (it could take about 10 minutes), select the database instance.

12. Click **Actions** ▶︎ **Reboot**.

13. On the reboot page, select **Reboot With Failover?**, and click **Confirm**.

### Create a Read Replica

1. W/ the database instance still selected, click **Actions** ▶︎ **Create read replica**.

2. For *Destination region*, select **US East (N. Virgirnia)**.

3. In the *Settings* section, under *DB instance identifier*, enter "wordpress-rr".

4. Leave the other defaults, and click **Create read replica**. It will take a few minutes for it to become available.

5. Refresh the web page we navigated to earlier to see if our application is still there. It should be fine.

### Promote the Read Replica and Change the CNAME Records Set in Route 53 to the New Endpoint

1. Once the read replica is available, check the circle next to it.

2. Click **Actions** ▶︎ **Promote**.

3. Leave the defaults, and click **Continue**, and then click **Promote Read Replica**.

4. Use the web page to monitor for downtime.

5. Once the read replica is available, click to view it.

6. In the *Connectivity & security* section, copy the endpoint.

7. Navigate to **Route 53** ▶︎ **Hosted zones**.

8. Select the private hosted zone.

9. Select the **db.mydomain.local** record.

10. Click **Edit**.

11. Leave the *Record name* as **db**.

12. Replace what's currently in the *Value* box w/ the endpoint you copied.

13. Set the TTL to 60 seconds.

14. Click **Save changes**.

15. Monitor using the web page for downtime (There shouldn't be any).
