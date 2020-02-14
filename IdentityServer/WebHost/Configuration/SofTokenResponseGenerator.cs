using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Validation;

namespace WebHost.Configuration
{
    public class SofTokenResponseGenerator : ICustomTokenResponseGenerator
    {
        public Task<TokenResponse> GenerateAsync(ValidatedTokenRequest request, TokenResponse response)
        {
            response.Custom.Add("scope", string.Join(" ", request.AuthorizationCode.Scopes).Trim());
            response.Custom.Add("PracticeId", "InteropShowcase2020");
            return Task.FromResult(response);
        }
    }
}