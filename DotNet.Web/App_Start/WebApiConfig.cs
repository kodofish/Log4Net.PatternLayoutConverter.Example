using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using System.Web.Routing;
using DotNet.Web.Handler;
using log4net;

namespace DotNet.Web
{
    /// <summary>
    ///
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        [SuppressMessage("Usage", "CC0022")]
        public static void Register(HttpConfiguration config)
        {
            // register log4net log request+response DelegatingHandler
            config.MessageHandlers.Add(new WebApiMessageLogHandler(LogManager.GetLogger(typeof(WebApiMessageLogHandler))));

            RouteTable.Routes.MapHttpRoute("DefaultApi"
                , "{controller}/{id}"
                , new {
                    controller = "Home",
                    id = RouteParameter.Optional
                });
        }
    }
}
