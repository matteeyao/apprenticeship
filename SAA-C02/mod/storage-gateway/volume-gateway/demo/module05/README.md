# Workshop clean-up

To make sure all resources are deleted after this workshop scenario, execute the steps in the order outlined below (you do not need to wait for CloudFormation to finish deleting before moving to the next step):

1. From the AWS console, click **Services** and select **Storage Gateway**.

2. Select **Gateways** on the left panel and highlight the gateway on the right.

3. Click **Actions** and **Delete Gateway**

4. Click **Confirm** and then **Delete**.

5. From the AWS console, click **Services** and select **EC2**.

6. Select **Instances** on the left panel and highlight the Volume Gateway on the right.

7. Click **Actions** and **Instance State** ▶︎ **Terminate**.

8. Highlight the **EC2 Initiator Instance** on the right.

9. Click **Actions** and **Instance State** ▶︎ **Terminate**.

10. Click **Confirm** and then **Delete**.

11. From the **AWS console**, click **Services** and select **CloudFormation**.

12. Select **Volume Gateway Workshop stack**.

13. Click **Delete**.

14. Click **Delete Stack**.

15. It will take a few minutes for the CloudFormation service to delete the stack. Click on the stack name and **refresh the page** to see an updated status. The Volume Gateway Workshop stack will be removed from the list if everything has been deleted correctly.

16. From the AWS console, click **Services** and select **EC2**.

17. Click **Volumes** on the left.

18. Find and select the **Snapshot volume**.

19. Click **Actions** ▶︎ **Delete Volume**, and then click **Yes, Delete**.

20. Click **Snapshots** on the left.

21. Find and select the **Volume Gateway snapshot**.

22. Click **Actions** ▶︎ **Delete**, and then click **Yes, Delete**.

To make sure that all CloudFormation templates have been deleted correctly, confirm that any EC2 instances created in this workshop are in the **terminated** state.
