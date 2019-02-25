using System.Security.Claims;
using System.Threading.Tasks;
using AspNetIdentityDeepDive.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AspNetIdentityDeepDive
{
    public class MyIdentityUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<MyIdentityUser>,
        IUserClaimsPrincipalFactory<Models.MyIdentityUser>
    {
        public MyIdentityUserClaimsPrincipalFactory(UserManager<MyIdentityUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(MyIdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("locale", user.Locale));
            return identity;
        }

        public Task<ClaimsPrincipal> CreateAsync(Models.MyIdentityUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}