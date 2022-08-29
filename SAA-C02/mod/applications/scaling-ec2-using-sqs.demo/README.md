# Scaling EC2 using SQS

> **Learning objectives**
>
> * Create CloudWatch Alarms
>
> * Create Simple Scaling Policies
>
> * Observe the Auto Scaling Group's Behavior in CloudWatch

> **About this lab**
>
> In this scenario, you're a solutions architect at an e-commerce firm. The company runs flash sales from time to time, and when there's a spike in orders, the fulfillment backend can struggle to meet demand. One way to solve the problem is to over-provision EC2 instances in the fulfillment system to provide headroom to process all the orders. However, this can be very costly, since you'll have unused capacity when the traffic subsides. What if there's a better way? Well, there is, and this is the problem you'll solve here. In this lab, you will learn to create Auto Scaling rules for EC2 based on the number of messages in an SQS queue.

## Introduction

![Fig. 1 Scaling EC2 using SQS Lab Diagram](../../../../img/aws/applications/scaling-ec2-using-sqs.demo/scaling-ec2-using-sqs.png)

## Solution

Log in to the live AWS environment using the credentials provided. Make sure you're in the N. Virginia (`us-east-1`) region throughout the lab.

## Create CloudWatch Alarms

1. Navigate to **EC2** ▶︎ **Instances**

2. Select the *Bastion Host* instance

3. Copy its public IP address

4. Open a terminal session, and log in to the instance:

```zsh
ssh cloud_user@<BASTION_HoST_PUBLIC_IP>
```

The password is provided on the lab page.

5. List the existing files:

```zsh
ls
```

We'll see there's a file named `send_messages.py`.

6. Run that script (this will send messages continuously into our SQS queue, simulating a large volume of orders):

```zsh
./send_messages.py
```

7. Back in the AWS console, select the *AutoScaling Group* instance

8. Copy its public IP address

9. In a new terminal session, log in to that instance:

```zsh
ssh cloud_user@<AUTOSCALING_GROUP_PUBLIC_IP>
```

It uses the same password as the one for the *Bastion Host* instance.

10. List the existing files:

```zsh
ls
```

We'll see there's a file named `receive_messages.py`, which is already running in the background (so we don't need to execute it).

11. See what's in its log file:

```zsh
tail -f receive_messages.log
```

We'll see this instance is retrieving messages from the queue.

12. Leave the terminals open and running.

## Create Scale-Out Alarm

1. Back in the AWS console, navigate to **CloudWatch** ▶︎ **Alarms**

2. Click **Create alarm**

3. Click **Select metric**

4. Select **SQS** ▶︎ **Queue Metrics**

5. Click the checkbox of the metric named `ApproximateNumberOfMessagesVisible`

6. Click **Select metric**

7. On the *Specify metric and conditions* page, change the *Period* to **1 minute**.

8. In the *Conditions* section, set the following values:

  * *Threshold type*: **Static**

  * *Whenever ApproximateNumberOfMessagesVisible* is...: **Greater**

  * *than*...: **500**

9. Click **Next**

10. On the *Configure actions* page, click into the *Send a notification to...* box and select the listed **AutoScalingTopic**

11. Click **Next**

12. For *Alarm name*, enter **Scale Out**

13. Click **Next**

14. Click **Create alarm**

## Create Scale-In Alarm

1. Click **Create alarm**

2. Click **Select metric**

3. Select **SQS** ▶︎ **Queue Metrics**

4. Click the checkbox of the metric named `ApproximateNumberOfMessagesVisible`

5. Click **Select metric**

6. On the *Specify metric and conditions* page, change the *Period* to **1 minute**

7. In the *Conditions* section, set the following values:

  * *Threshold type*: **Static**

  * *Whenever ApproximateNumberOfMessagesVisible is...*: **Lower**

  * *than*...: **500**

8. Click **Next**

9. On the *Configure actions* page, click into the *Send a notification to*... box and select the listed **AutoScalingTopic**

10. Click **Next**

11. For *Alarm name*, enter **Scale In**

12. Click **Next**

13. Click **Create alarm**

## Create Simple Scaling Policies

### Scale-out Policy

1. Note that after a minute or so, the *Scale Out* alarm enters an *In alarm* state

2. Click **Metrics** in the left-hand menu

3. Click **SQS** ▶︎ **Queue Metrics**

4. Click the checkbox of the metric named `ApproximateNumberOfMessagesVisible`. Initially, there will just be a small dot on the graph

5. Click **custom** at the top of the graph

6. Set *Minutes* to **30**

7. Click the *Line* dropdown, and select **Stacked area**

8. Click the dropdown next to the refresh icon

9. Select **Auto refresh**

10. Set the *Refresh interval* to **10 Seconds**

11. Navigate to **EC2** ▶︎ **Auto Scaling Groups**

12. Click the listed Auto Scaling group

13. Click the **Automatic scaling** tab

14. Click **Create dynamic scaling policy**

15. Set the following values:

  * *Policy type*: **Simple scaling**

  * *Scaling policy name*: **Scale Out**

  * *CloudWatch alarm*: **Scale Out**

  * *Take the action*: **Add 1 capacity units**

  * *And then wait*: **60** seconds before allowing another scaling activity

16. Click **Create**

### Scale-In Policy

1. Click **Create dynamic scaling policy**

2. Set the following values:

  * *Policy type*: **Simple scaling**

  * *Scaling policy name*: **Scale In**

  * *CloudWatch alarm*: **Scale In**

  * *Take the action*: **Remove 1 capacity units**

  * *And then wait*: **60** seconds before allowing another scaling activity

3. Click **Create**

4. Click the **Activity** tab. After a few minutes, we should see a new instance is launched as an effect of the scale-out policy

5. Click **Instances** in the left-hand menu. We'll see a new *Autoscaling Group* instance now exists

## Observe the Auto Scaling Group's Behavior in CloudWatch

1. Navigate to **CloudWatch** ▶︎ **Metrics**

2. Click **SQS** ▶︎ **Queue Metrics**

3. Click the checkbox of the metric named `ApproximateNumberOfMessagesVisible`. This time, the graph will display more data.

4. In a new browser tab, navigate to **EC2** ▶︎ **Auto Scaling Groups**

5. Click the listed Auto Scaling group

6. Select the **Activity** tab. After a minute or so, we should see it launches an additional EC2 instance

7. In the terminal running the `send_messages.py` script, press **Ctrl**+**C** to cancel the process (simulating the environment slowing down).

8. Back in the AWS console, view the CloudWatch metrics graph. After a few minutes, we should see the graph starts to flatten out

9. Wait a few minutes more, and we should see hte graph takes a downward turn as the messages are drained from our SQS queue

10. In the EC2 browser tab, we should see our instances are starting to be terminated, since our *Scale In* alarm has been triggered (meaning the number of messages in our queue is below the threshold we set)

11. After a few more minutes, refresh the *Activity history* to see another instance is now being terminated
