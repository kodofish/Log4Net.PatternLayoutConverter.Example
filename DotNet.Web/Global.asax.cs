using System.Web.Http;
using log4net;

namespace DotNet.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog Logger => LogManager.GetLogger(typeof(MvcApplication));
        
        protected void Application_Start()
        {
            Logger.Info("Application Start...");
            
            var log4NetPath = Server.MapPath("~/log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4NetPath)); 
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}