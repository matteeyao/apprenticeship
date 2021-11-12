using Nancy.Hosting.Self;
using System;

namespace NancyStandalone
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri[] uris = new Uri[]
            {
                new Uri("http://localhost:8080"),
                new Uri("http://192.168.0.13:8080")
            };

            using (var host = new NancyHost(uris))
            {
                host.Start();

                Console.WriteLine("NancyFX Stand alone test application.");
                Console.WriteLine("Press enter to exit the application");
                Console.ReadLine();
            }
        }
    }
}