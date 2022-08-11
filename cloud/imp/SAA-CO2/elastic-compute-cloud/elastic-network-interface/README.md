# Elastic Network Interface (ENI)

1. Part of your design requirements for a new EC2-based application is the need to be able to move the IPv4 address from the new EC2 instance to another instance in the same region if needed. Which of the following configurations will allow you to accomplish this goal w/ the least effort?

[ ] Move the default ENI to another instance to move the address

[x] Add a secondary IP address to the existing EC2 instance

[ ] Add a second ENI to the EC2 instance

[ ] Create the replacement EC2 instance w/ the same IP address as your existing instance and then stop the replacement instance until needed.

**Explanation**: Default ENI cannot be moved to another instance. Adding a second ENI to the EC2 instance is difficult if the instances are in the same subnet and is not the least effort.
