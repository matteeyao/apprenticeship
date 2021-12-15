# V. Securing Nancy Applications w/ Auth0

You can easily secure your Nancy applications w/ Auth0, a global leader in Identity-as-a-Service (IDaaS) that provides thousands of enterprise customers w/ modern identity solutions.

Alongside w/ the classic username and password authentication process, Auth0 allows you to add features like Social Login, Multifactor Authentication, and much more w/ just a few clicks.

To secure your Nancy application w/ Auth0, you will just have to install one more NuGet package (`Auth0.NancyFx.SelfHost`) and then configure your `BootStrapper` as follows:

```c#
protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
{
  // ...

  Auth0Authentication.Enable(pipelines, new AuthenticationConfig
  {
    RedirectOnLoginFailed = "login",
    CookieName = "_auth0_userid",
    UserIdentifier = "userid"
  });

  // ...
}
```

W/ that in place, you will have to add your Auth0 key in the `app.config` file:

```c#
<appSettings>
    <!-- Auth0 configuration -->
    <add key="auth0:ClientId" value="YOUR_CLIENT_ID" />
    <add key="auth0:ClientSecret" value="YOUR_CLIENT_SECRET" />
    <add key="auth0:Domain" value="YOUR_AUTH0_DOMAIN" />
    <add key="auth0:CallbackUrl" value="https://YOUR_APP/callback" />
</appSettings>
```

And then block all unauthenticated requests:

```c#
public class SecurePage : NancyModule
{
    public SecurePage()
    {
        this.RequiresAuthentication(); //<- This is a new implemetation of default extension
        Get["/securepage"] = o => View["securepage"];
    }
}
```

Then, to wrap things up, you will have to configure a *Callback Handler*:

```c#
public class Authentication : NancyModule
{
    public Authentication()
    {
        Get["/login"] = o =>
        {
            if (this.SessionIsAuthenticated())
                return Response.AsRedirect("securepage");

            var apiClient = new AuthenticationApiClient(ConfigurationManager.AppSettings["auth0:domain"]);
            var authorizationUri = apiClient.BuildAuthorizationUrl()
                .WithClient(ConfigurationManager.AppSettings["auth0:ClientId"])
                .WithRedirectUrl(ConfigurationManager.AppSettings["auth0:CallbackUrl"])
                .WithResponseType(AuthorizationResponseType.Code)
                .WithScope("openid profile")
                .Build();

            return Response.AsRedirect(authorizationUri.ToString());
        };

        Get["/login-callback"] = o => this
            .AuthenticateThisSession()
            .ThenRedirectTo("securepage");

        Get["/logout"] = o => this
            .RemoveAuthenticationFromThisSession()
            .ThenRedirectTo("index");
    }
}
```

To learn more about the details on how to use Auth0 w/ Nancy, please check [the Quick Start documentation here](https://auth0.com/docs/quickstart/webapp/nancyfx).
