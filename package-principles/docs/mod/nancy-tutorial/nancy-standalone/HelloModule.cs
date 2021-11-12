using Nancy;

namespace NancyStandAlone
{
    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = parameters => "I am the root";
            Get["/hello"] = parameters => "Hello";
            Get["/world"] = parameters => "World";
        }
    }
}