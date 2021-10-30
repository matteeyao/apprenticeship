using Nancy;

namespace NancyStandAlone
{
    public class FunctionsModule : NancyModule
    {
        public FunctionsModule()
        {
            Get["/func1"] = parameters =>
            {
                var response = "Hello" + "World";
                return "<h1>" + response + "</h1>";
            };

            Get["/func2"] = parameters =>
            {
                var response = "";
                for (int count = 0; count < 10; count++)
                {
                    response = response + count.ToString() + ",";
                }
                response = response.Trim(',');
                return "<p>" + response + "</p>";
            };
        }
    }
}