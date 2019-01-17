using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;


namespace Spark.Infrastructure
{
     public class LogHandler : DelegatingHandler
     {
         private Logger logger;

        public LogHandler()
        {
            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("C:\\logs\\spark.log")
                .CreateLogger();

        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var requestTimestamp = DateTime.Now;
            
            try
            {
                //await Task.Run(() => logger.Debug($"New Request: '{request.RequestUri}'"), cancellationToken);
                var response = await base.SendAsync(request, cancellationToken);
                await SendToLog(request, response, requestTimestamp, DateTime.Now, cancellationToken);
                return response;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                logger.Error(e.StackTrace);
                throw;
            }
        }


        private async Task<bool> SendToLog(HttpRequestMessage request, HttpResponseMessage response,
            DateTime requestTimestamp, DateTime responseTimestamp, CancellationToken cancellationToken)
        {
            var logEntry = new LogMetadata
            {
                RequestMethod = request.Method.Method,
                RequestTimestamp = requestTimestamp,
                RequestUri = request.RequestUri.ToString(),
                ResponseStatusCode = response.StatusCode,
                ResponseTimestamp = responseTimestamp,
                ResponseContentType = response.Content?.Headers.ContentType.MediaType,
                DurationMilliseconds = (responseTimestamp - requestTimestamp).TotalMilliseconds
            };
            await Task.Run(() => logger.Debug(logEntry.ToString()), cancellationToken);
            return true;
        }


        private class LogMetadata
        {
            public string RequestUri { get; set; }
            public string RequestMethod { get; set; }
            public DateTime? RequestTimestamp { get; set; }
            public string ResponseContentType { get; set; }
            public HttpStatusCode ResponseStatusCode { get; set; }
            public DateTime? ResponseTimestamp { get; set; }
            public double DurationMilliseconds { get; set; }


            public override string ToString()
            {
                var sb = new StringBuilder();
                foreach (var property in GetType().GetProperties())
                {
                    sb.Append(property.Name);
                    sb.Append(": ");
                    if (property.GetIndexParameters().Length > 0)
                        sb.Append("Indexed Property cannot be used");
                    else
                        sb.Append(property.GetValue(this, null));

                    sb.Append(Environment.NewLine);
                }

                return sb.ToString();
            }
        }
    }
}