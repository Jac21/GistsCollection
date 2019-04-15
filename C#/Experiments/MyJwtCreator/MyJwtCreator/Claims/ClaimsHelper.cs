using MyJwtCreator.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyJwtCreator.Claims
{
    public class ClaimsHelper
    {
        public static Task<ClaimsIdentity> CreateClaimsIdentityAsync(User user, UserData userData)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.EmailAddress));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.FullName ?? $"{user.FirstName} {user.LastName}"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userData)));

            var roles = Enumerable.Empty<Role>();

            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            }

            return Task.FromResult(claimsIdentity);
        }
    }
}
