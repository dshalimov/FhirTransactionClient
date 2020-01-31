using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using Microsoft.Owin;

namespace WebHost.Configuration
{
    public class SecretParser : ISecretParser
    {
        public async Task<ParsedSecret> ParseAsync(IDictionary<string, object> environment)
        {
            var secret = new ParsedSecret();

            var context = new OwinContext(environment);

            var body = await context.Request.ReadFormAsync();

            secret.Id = body.Get("client_id");

            return secret;
        }
    }
}