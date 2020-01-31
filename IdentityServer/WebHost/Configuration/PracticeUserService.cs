using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services.InMemory;

namespace WebHost.Configuration
{
    public class PracticeUserService : InMemoryUserService
    {
        public PracticeUserService(List<InMemoryUser> users) : base(users)
        {
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext ctx)
        {
            var issuedClaimsList = ctx.IssuedClaims.ToList();
            issuedClaimsList.Add(new Claim("PracticeId", "InteropShowcase2020"));
            ctx.IssuedClaims = issuedClaimsList.AsEnumerable();

            return Task.FromResult(0);
        }
    }
}