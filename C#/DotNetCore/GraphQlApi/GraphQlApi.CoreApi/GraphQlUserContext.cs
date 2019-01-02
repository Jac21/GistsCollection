using System.Security.Claims;

namespace GraphQlApi.CoreApi
{
    public class GraphQlUserContext
    {
        public ClaimsPrincipal User { get; set; }
    }
}