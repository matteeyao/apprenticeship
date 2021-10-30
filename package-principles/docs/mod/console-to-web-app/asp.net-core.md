# Convert Console Application to Web Application in asp.net core

Steps to convert console app into web app

1. Update SDK in csproj file

2. Update output type in csproj

3. Add new WebHostBuilder

4. Add routing

5. Setup default route

## Update SDK and output in `csproj` file

Right-click on project near top-left corner of the Rider editor → Select `Edit App.csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>netcoreapp3.1</TargetFramework>
    </PropertyGroup>

</Project>
```

↓

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <TargetFramework>netcoreapp3.1</TargetFramework>
    </PropertyGroup>

</Project>
```

## Add new WebHostBuilder

A Host is an object that encapsulates the resources of an application. For example:

* Dependency Injection

* Logging

* Configuration

* *IHostedService* implementation

```c#
using Microsoft.Extensions.Hosting;
using System;

namespace MyFirstConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
```

```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstConsoleApp
{
    public class Startup
    {
        public void ConfigurationServices(IServiceCollection service)
        {
        
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello from webgentle application");
                });
            });
        }
    }
}
```
