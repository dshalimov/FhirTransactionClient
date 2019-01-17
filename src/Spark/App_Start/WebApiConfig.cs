using System.Web.Http;
using Spark.Infrastructure;

namespace Spark
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            config.MessageHandlers.Add(new LogHandler());
            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}