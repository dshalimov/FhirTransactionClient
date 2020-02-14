using Configuration;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Validation;
using Microsoft.Owin;
using Owin;
using Serilog;
using WebHost;
using WebHost.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace WebHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile("C:\\logs\\oauth.log", retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            var inMemoryManager = new InMemoryManager();

            var users = inMemoryManager.GetUsers();

            var factory = new IdentityServerServiceFactory()
                .UseInMemoryUsers(inMemoryManager.GetUsers())
                .UseInMemoryClients(inMemoryManager.GetClients())
                .UseInMemoryScopes(inMemoryManager.GetScopes());

            factory.SecretValidators.Clear();
            factory.SecretValidators.Add(new Registration<ISecretValidator>(resolver => new SecretValidator()));

            factory.SecretParsers.Clear();
            factory.SecretParsers.Add(new Registration<ISecretParser>(resolver => new SecretParser()));

            factory.CustomTokenResponseGenerator = new Registration<ICustomTokenResponseGenerator, SofTokenResponseGenerator>();

            /*var viewOptions = new DefaultViewServiceOptions();
            viewOptions.Stylesheets.Add("TBD...");
            factory.ConfigureDefaultViewService(viewOptions);*/

            var options = new IdentityServerOptions
            {
                SiteName = "NextGen Healthcare",
                SigningCertificate = Certificate.Load(),
                Factory = factory,
                RequireSsl = false,
                LoggingOptions = new LoggingOptions
                {
                    EnableHttpLogging = true,
                    EnableKatanaLogging = true,
                    EnableWebApiDiagnostics = true,
                    WebApiDiagnosticsIsVerbose = true
                }
            };

            app.Map("", idsrvApp => { idsrvApp.UseIdentityServer(options); });
        }
    }
}