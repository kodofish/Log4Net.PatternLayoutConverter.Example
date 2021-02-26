using System;
using System.Net.Http;
using System.Web;
using log4net;

namespace DotNet.Web.Handler
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiMessageLogHandler : DelegatingHandler
    {
        /// <inheritdoc />
        public WebApiMessageLogHandler(ILog logger)
        {
            _logger = logger;
        }

        #region Fields

        private readonly ILog _logger;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Logs the request.
        /// </summary>
        private void LogRequest()
        {
            var httpContext = HttpContext.Current;
            var httpRequest = httpContext.Request;
            var requestType = httpRequest.RequestType;
            var url = httpRequest.Url;

            var message = $"[Request] {requestType} {url}\t";

            message += LogCookiesContent(httpRequest);

            _logger.Info(message);
        }

        private string LogCookiesContent(HttpRequest httpRequest)
        {
            if (_logger.IsDebugEnabled)
            {
                var cookies = httpRequest.Cookies;
                var cookieCount = cookies.Count;
                var values = new String[cookieCount];

                for (var idx = 0; idx < cookieCount; ++idx)
                {
                    var cookie = cookies[idx];
                    if (cookie == null)
                        continue;

                    var value = $@"{{""Name"":""{cookie.Name}"", ""Path"":""{cookie.Path}"", ""Value"":""{cookie.Value}""\}}";
                    values[idx] = value;
                }

                return " Cookies=[" + String.Join(",", values) + "]";
            }

            return string.Empty;
        }

        /// <summary>
        /// Logs the response.
        /// </summary>
        private void LogResponse()
        {
            var httpContext = HttpContext.Current;
            var httpRequest = httpContext.Request;
            var httpResponse = httpContext.Response;
            var url = httpRequest.Url;

            var message =
                $"[Response] {httpRequest.RequestType} {url}\t Status={httpResponse.Status}";

            message += LogCookiesContent(httpRequest);

            _logger.Info(message);
        }


        /// <inheritdoc />
        protected override System.Threading.Tasks.Task<HttpResponseMessage>
            SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            LogRequest();

            var response = base.SendAsync(request, cancellationToken);

            response.Wait(cancellationToken);

            LogResponse();

            return response;
        }

        #endregion Methods
    }
}
