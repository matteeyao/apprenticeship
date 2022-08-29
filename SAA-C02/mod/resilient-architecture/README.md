# Resilient Architectures

## Test Axioms

* Expect "Single AZ" will never be a right answer

* Using AWS managed services should always be preferred

* Fault tolerant and high availability are not the same thing

  * Fault tolerance is a higher requirement

  * High availability → the system will always be up and can failover in the event of failure

  * Fault tolerance → the system conceals its failure from the users and there is no loss of service

* Expect that everything will fail at some point and design accordingly
