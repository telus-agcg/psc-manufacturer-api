using System.Text;
using NLog;

namespace PSC.Manufacturer.Api
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var logger = LogManager.GetCurrentClassLogger();
                string message = $"Exception. Method: {context.Request.Method}, Path: {context.Request.Path}, User: {context.User?.Identity?.Name}, IP: {context.Connection.RemoteIpAddress}. Inner exception(s): {GetInnerExceptionsString(ex)}";
                logger.Error(ex, message);
                throw;
            }
        }

        private string GetInnerExceptionsString(Exception? ex)
        {
            StringBuilder sb = new StringBuilder();

            while (ex != null)
            {
                sb.AppendLine($" -> Inner Exception: {ex.GetType().Name}: {ex.Message}");
                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}