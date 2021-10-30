# I. .NET Meet Nancy, a Lightweight Web Framework for .NET ([LINK](https://auth0.com/blog/meet-nancy-a-lightweight-web-framework-for-dot-net/#Enough-Talk--show-me-some-code-))

Nancy, an open-source and lightweight alternative to the ASP.NET MVC framework

[Github repository](https://github.com/auth0-blog/nancy)

## Nancy for a new generation

Nancy, .NET framework

Not really a framework in the same domain that *Angular*, *Aurelia*, *Vue.js*, and *React* are

This is an open-source backend framework for .NET

## But why do you need a framework on the backend?

Web frameworks are not all about the client side, you still need to perform routing and parameter passing from the client-side code, and into your backend code so that you can then use these inputs to perform business logic and the outputs to pass information back to the client for display.

Having a toolkit like Nancy greatly helps w/ this task, just as Angular and Aurelia help w/ creating the front end and user interface.

In .NET there is two major ways provided by default for creating a web application:

1. The first is through the use of "Web Forms", Microsoft's original web framework modeled closely on the desktop framework "Win Forms"

2. The second is the newer and more flexible ASP.NET MVC framework, designed to implement application design using the commonly known "Model, View, Controller" architecture pattern.

This left a lot of space in the backend framework market for a number of different toolkits, designed to produce HTTP based APIs.

Nancy was one of the first, along w/ *FubuMVC*, *OpenRasta*, and many others.

## Is Nancy still appropriate in software development today?

Nancy is now a stable, mature, and very well supported set of tools for creating first-class HTTP based interfaces for all manner of projects.

Web-based application ≠ "Web Server based application".

A lot of ppl assume that if you're building a web application, that it has to use HTML & CSS in the browser, and that it has to have a web server like IIS, Nginx, or Apache running in the backend.

In today's modern .NET core world, you now also have kestrel built into your standard toolset, which is also classed as being "A web server."

Nancy however, doesn't need ANY of this.

It can use it, you can embed Nancy into a classic ASP.NET based web application, or you can even use it w/ .NET core.

However, it's strength is you can add it into ANY .NET based project you have use case to use it in.

As way of an example, you can use "CefSharp" to make the user interface in the desktop Windows application have an HTML look and feel, and you can use Nancy to build a web-based API directly into the same program so that the one single application serves its own HTML content for its own UI directly from its own code base.

In one project, a desktop Windows form based dashboard for help desk operators, Nancy was used to add in some rest endpoints so that the telephone system they were using could push information about the incoming call directly to the operators' screen as the telephone was ringing.

Nancy has a surprising number of use cases that standard tools such as ASP.NET MVC are not equipped to cover

Web enabling an application is not always just about converting an application to run on the web via a user's browser.
