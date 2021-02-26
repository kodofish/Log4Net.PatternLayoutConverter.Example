using System.IO;
using System.Web;
using log4net.Core;
using log4net.Layout.Pattern;

namespace DotNet.Web.Log4Net
{
    /// <summary>
    ///     Implement request info of PatternLayoutConverter that write the request info content.
    /// </summary>
    /// <seealso cref="log4net.Layout.Pattern.PatternLayoutConverter" />
    public class HttpContextInfoPatternConverter : PatternLayoutConverter
    {
        /// <inheritdoc />
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var userName = HttpContext.Current.User.Identity.Name;

            writer.Write($"User Name = {userName}");
        }
    }
}
