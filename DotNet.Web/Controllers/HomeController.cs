using System.Web.Http;
using log4net;

namespace DotNet.Web.Controllers
{
    public class HomeController : ApiController
    {
        private static ILog Logger => LogManager.GetLogger(typeof(HomeController));
        public string Get()
        {
            Logger.Info("Home controller was executed.");
            return "Success!!";
        }
    }
}