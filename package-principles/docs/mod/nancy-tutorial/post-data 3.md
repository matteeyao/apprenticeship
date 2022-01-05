# III. Posting the data.

Asking Nancy to return things to you is all well and good.

However, any good web framework allows you to send data to it.

For example, an ID of a customer record, or the name to include in a functions calculation.

Nancy is no exception, you can use and access all the regular verbs (`POST`, `DELETE`, `HEAD`, etc.) in the same way as the `GET` verb you've used so far.

You won't have time to dive deep into the HTTP protocol and its verbs here. But using them is as easy as changing the `Get[...]` that you've been using to the verb you want to use.

For example, create a new class in your application called `PostModule.cs` and make sure it has the following code:

```c#
using Nancy;
using Nancy.ModelBinding;

namespace NancyStandalone
{
  public class PostModule : NancyModule
  {
    public PostModule()
    {
      Post["/sendname"] = parameters =>
      {
        PostData recievedData = this.Bind<PostData>();

        return new { success = true, message = $"Record recieved name = {recievedData.Name}" };
      };
    }
  }
}
```

Once you've added the post module, add another new class, this time call it `PostData.cs`, and ensure it looks as follows:

```c#
namespace NancyStandalone
{
  public class PostData
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
  }
}
```

This second class will wrap the data that you will send to the function call.

To test it you will need a tool that allows you to send raw post requests such as `Fiddler`, `Postman`, etc.

In this article, you will see some Postman screenshots. But the process is the same no matter what you use, set your method to `POST`, and make the URL equal to `http://localhost:8080/sendname`.

Then, make your body data type `application/json` and your body payload to look something like:

```c#
{
  "id": 1,
  "name": "Peter Shaw",
  "email": "top@secret.com"
}
```

Once you issue that call, you should see something like the following as the return:

![](./docs/img/nancy-setup-5.png)

The C# code in the module is so simple it's frightening.

You simply need to make sure that you are using `Nancy.ModelBinding`, then just use `this.Bind<...>` to bind to an instance of the data object you expect to receive.

Notice also that in the body data, you specified `id`, `name`, and `email` as all lower case and yet the actual model in C# had capitalized names.

Nancy took care of all that for us, using its own built-in JSON serializer.

This makes the *Camel vs. Pascal vs. no-case* just not an issue for the developer who uses Nancy to add a web endpoint.

As w/ the `GET` requests, you just changed the verb to `POST`, and everything was completely fine.

At this point many of you might be wondering about submitting data on the URL. Well that's just as easy too.

To see this in action, change your PostModule so that the `id` in the body data is accepted on the URL, and the `name` and `email` are accepted in the data.

First, you need to remove the `id` from your `PostData` class. So, go ahead and do that first: just simply delete the line.

Then change `PostModule.cs` so that the line w/ the `Post` verb reads as follows:

```c#
Post["/sendname/{id:int}"] = parameters =>
```

Now, run your app, and open your post sender again.

Do the same as before, but this time try just `http://localhost:8080/sendname` and `http://localhost:8080/sendname/1`.

Note that the first one now fails w/ an `HTTP 404 Not Found` error, and the second now succeeds.

The best place to read about using URL parameters is [this page in the Nancy Wiki](https://github.com/NancyFx/Nancy/wiki/Defining-routes).

In these examples, you put `:int` after the parameter to force it to be an int.

To try it out, call your post endpoint w/ something like `http://localhost:8080/sendname/one`. You will get another `404`.

Putting `:int` after the parameter is just the tip of the iceberg.

You can constrict parameters to strings, dates, emails and a whole host of regular expressions based goodness.

On top of that, different URL patterns are scored in different ways, and the official Wiki is the definitive guide as to how all of that works.

On the subject of parameters, you're probably wondering how you get a hold of that ID parameter in your C# code. Well, that's easy too.

Everything passed to Nancy on the URL is passed in a dynamic dictionary.

So, it's a simple case of just accessing your "parameters" and the name of the parameter in dot notation.

Remember this line:

```c#
Post["/sendname/{id:int}"] = parameters =>
```

That `parameters` in the lambda function is what you use to access your URL parameters.

You can call it anything you like, but usually it's either `parameters` or `_` as an indication that it's not used.

In the case here, the following line of code is all you need to get your id:

```c#
int id = parameters.id;
```

You might be wondering: "what if the parameter is not provided?"

Well, as you saw just a moment ago, if it's not provided the call doesn't succeed. So you would never get far enough for that to cause a problem.

Same applies if the data supplied wasn't an int.

The only time this might be an issue is if you mark the parameter as optional (instructions on the wiki page for that one).

In that case, it may be possible to enter this function w/o supplying that parameter.

But don't worry, Nancy has you covered here too.

Since it's a dynamic dictionary, you can simply just check to see if it's null before using it.

```c#
if(parameters.id == null)
{
  // Handle null case here
}

int id = parameters.id;
// Non null case here
```

B/c of the rich binding scenarios available, you will usually find that the most you pass in on the URL is either an integer ID, or some kind of GUID.

Most of your data will normally be in custom classes that you bind too.

Note also too that this same `null` scenario applies to any objects you bind.

Taking your `PostData` as an example, if you made your body data so it only included the name property, then name would be bound and email would be left as null.

The same rules apply to integers, bools and other types too as long as you make them nullable.

This comes in very handy when you are dealing w/ objects which may only be partially used.

This means that you can design your data classes to hold all the information for a given thing, then use only the fields you need to when you need to.
