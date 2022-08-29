# Demonstration: Monitor a file system using CloudWatch

## Introduction

In this demonstration, you learn how to monitor the health and status of your file system using CloudWatch metrics. You also learn how to use math expressions to perform math analytics on your CloudWatch metrics. Each of the videos include respective steps and commands beneath it. You can follow the demonstration using your own AWS account for a guided hands-on experience. You can pause and back up each video as needed. Before you can follow this demo, you must already have an FSx for Windows File Server file system configured.

## Build a Cloudwatch dashboard

1. Sign in to the AWS console.

2. Select **Services** ▶︎ **Amazon FSx**. Take note of your file system ID. The file system ID is available under the File system ID column in the File systems menu.

3. Select: **Services** ▶︎ **CloudWatch**.

4. Select **Dashboards**.

5. Choose **Create Dashboard** to start the Dashboard wizard.

6. Enter a Dashboard name, then select **Create Dashboard**.

7. Choose the **Line** chart type, then select **Configure**.

8. Enter your file system ID on the **Search** field.

9. Choose the **DataReadBytes** and **DataWriteBytes** metrics.

10. Select **Create widget**.

## Create custom metrics using CloudWatch metric math

1. Choose **Add widget**.

2. Choose the **Line** widget type, then select **Configure**.

3. Enter your file system ID on the **Search** field.

4. Select **DataWriteBytes**.

5. Choose the **Graphed metrics** tab.

6. Expand the **Math expression** dropdown list, then choose **Start with empty expression**.

7. In the **Details** field, enter **m1/PERIOD(m1)**.

8. Deselect **DataWriteBytes** from the chart.

9. Select **Create widget**.

## Explore additional CloudWatch functionality

1. Choose **Add widget**.

2. Choose the **Line** widget type, then select **Configure**.

3. Filter the metrics using the file system ID.

4. Select **FSx** ▶︎ **File System Metrics**.

5. Choose **DataWriteOperations**.

6. Navigate to the **Graphed metrics** tab.

7. Change the **Period** value to 15 Minutes.

8. Select **Create widget**.
