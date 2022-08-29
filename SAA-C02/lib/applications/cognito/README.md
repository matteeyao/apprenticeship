# Cognito

1. A tech startup has recently received a Series A round of funding to continue building their mobile forex trading application. You are hired to set up their cloud architecture in AWS and to implement a highly available, fault tolerant system. For their database, they are using DynamoDB and for authentication, they have chosen to use Cognito. Since the mobile application contains confidential financial transactions, there is a requirement to add a second authentication method that doesn't rely solely on user name and password.   

How can you implement this in AWS?

[x] Add multi-factor authentication (MFA) to a user pool in Cognito to protect the identity of your users.

[ ] Develop a custom application that integrates w/ Cognito that implements a second layer of authentication.

[ ] Add a new IAM policy to a user pool in Cognito.

[ ] Integrate Cognito w/ Amazon SNS Mobile Push to allow additional authentication via SMS.

**Explanation**: You can add multi-factor authentication (MFA) to a user pool to protect the identity of your users. MFA adds a second authentication method that doesn't rely solely on user name and password. You can choose to use SMS text messages, or time-based one-time (TOTP) passwords as second factors in signing in your users. You can also use adaptive authentication with its risk-based model to predict when you might need another authentication factor. It's part of the user pool advanced security features, which also include protections against compromised credentials.

<br />
