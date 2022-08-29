# More Examples

Below you fill find a few more examples of AWS CloudFormation Macros.

We are explaining only the Python code, since the other parts of the AWS CloudFormation templates would be similar to the preceding examples. However, the full templates are included below for each example.

## Example: Ensure EBS volumes are encrypted

Many companies mandate encryption at rest for all data, including Amazon EBS volumes. This example shows how to ensure that all Amazon EBS volumes are encrypted. The resource would be of type 'AWS::EC2::Volume', and we would need to set its Encrypted property to true.

To illustrate different options, and since this macro wouldn't require any parameters, we made this a global macro. It will receive the whole template as a fragment, rather than just a piece of it.

```py
def encrypt_if_EBS_volume(resource) :
  if (resource['Type'] != 'AWS::EC2::Volume'):
    return resource
  if ('Properties' not in resource):
    resource['Properties'] = {}
  resource['Properties']['Encrypted'] = True
  return resource

def transform_fragment(event):
  event['fragment']['Resources'] = {
    name: encrypt_if_EBS_volume(resource) for name, resource in event['fragment']['Resources'].items()
  }

  return {
    "requestId": event['requestId'],
    "status": "success",
    "fragment": event['fragment']
  }

def handler(event, context):
  try:
    return transform_fragment(event)
  except BaseException as ex:
    print("Error - "+str(ex))
    return {
      "requestId": event['requestId'],
      "status": "ERROR - "+str(ex),
      "fragment": {}
    }
```

* `encrypt_if_EBS_volume` ▶︎ This function ensures we have `Encrypted=True` as a property, if the resource is an EBS volume. We add a Properties block to the resource if it doesn't have it.

* `transform_fragment` ▶︎ Since this will be a global macro, we need to replace the Resource piece of the fragment object, rather than the whole fragment.

* `handler` ▶︎ And this is the standard handler, just handling any possible exception.

## Example: Generate an error if resources do not include a cost center

Another common requirement is for all resources to be tagged with a standard set of tags. This allows for finding, categorizing, and accounting for resources. The following example ensures that every resource has a tag called `CostCenter`.

```py
def fail_if_no_cost_center(name, resource) :
  print('resource', resource)
  if 'Properties' not in resource:
    raise Exception(name+' does not have any Properties')
  if 'Tags' not in resource['Properties']:
    raise Exception(name+' missing CostCenter tag')
  if not list(filter(lambda x: x['Key']=='CostCenter', resource['Properties']['Tags'])):
    raise Exception(name+' missing CostCenter tag')

def transform_fragment(event) :
  for name,resource in event['fragment']['Resources'].items():
    fail_if_no_cost_center(name,resource)
  return {
    "requestId": event['requestId'],
    "status": "success",
    "fragment": event['fragment']
  }

def handler(event, context) :
  try:
    return transform_fragment(event)
  except BaseException as ex:
    print("Error - "+str(ex))
    return {
      "requestId": event['requestId'],
      "status": "ERROR - "+str(ex),
      "fragment": {}
    }
```

* `raise exception if tag missing` ▶︎ We check that the `CostCenter` tag is present; if not, we raise an exception. For better logging, the exception message is different for each condition

* `transform` ▶︎ Notice here we will return the same fragment; we just call the checking function for all fragments.

* `handler` ▶︎ This is our standard handler, logging exceptions.

## Example: Parse variables in string

We can use parameters in our AWS CloudFormation templates. However, having many parameters can be confusing. AWS CloudFormation does not (yet) allow us to group parameters or pass complex objects as parameters. In particular, with AWS Systems Manager Parameter Store, we would need to specify each parameter's path for multiple parameters.

To simplify, we could define one string that contained many variable definitions. The string would be of the form a=1, b=2, c=3, with commas to separate variables and = to separate the variable from the value. We could then use a macro to extract any given variable from the string.

```py
def transform_fragment(event):
  expr=event['params']['Expression']
  var_name=event['params']['Variable']

  var=list(filter( lambda v: v[0]== var_name,
    map( lambda kv: kv.split("="), expr.split(',') )
  ))
  print('var',var)
  return {
    "requestId": event['requestId'],
    "status": "success",
    "fragment": var[0][1]
  }

def handler(event, context):
  try:
    return transform_fragment(event)
  except BaseException as ex:
    print("Error - "+str(ex))
    return {
      "requestId": event['requestId']
      "status": "ERROR - "+str(ex),
      "fragment": {}
    }
```

* `transform_fragment` ▶︎ Here, we look at the parameters to the lambda: Expression, which would be a sting containing comma-separated name=value pairs, like a=1, b=2 Variable, which is the variable name (say a, or b). And we insert the corresponding value as the fragment.

* `handler` ▶︎ This is our standard handler, logging exceptions if needed.
