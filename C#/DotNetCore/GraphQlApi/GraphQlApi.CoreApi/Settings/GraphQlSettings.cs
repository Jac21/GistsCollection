using Microsoft.AspNetCore.Http;
using System;

namespace GraphQlApi.CoreApi.Settings
{
    public class GraphQlSettings
    {
        public PathString Path { get; set; } = "/api/graphql";
        public Func<HttpContext, object> BuildUserContext { get; set; }
    }
}