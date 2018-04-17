using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreJwtAuthBoilerplate.Auth;
using AspNetCoreJwtAuthBoilerplate.Data.Models;
using Newtonsoft.Json;

namespace AspNetCoreJwtAuthBoilerplate.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName,
            JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int) jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}