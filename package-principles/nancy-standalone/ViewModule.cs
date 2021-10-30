using Nancy;

namespace NancyStandalone
{
    public class ViewModule : NancyModule
    {
        public ViewModule()
        {
            Get["/viewtest"] = parameters =>
            {
                PostData data = new PostData()
                {
                    Name = "Peter Shaw",
                    Email = "top@secret.com"
                };

                return View["ViewTest.html", data];
            };
        }
    }
}
