using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CountingKs.Filters
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var req = actionContext.Request;

            if (req.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "<p>Https is required</p>";
                if (req.Method.Method == "GET")
                {
                    actionContext.Response = req.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                    var uriBuilder = new UriBuilder(req.RequestUri);
                    uriBuilder.Scheme = Uri.UriSchemeHttps;
                    uriBuilder.Port = 443;

                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    actionContext.Response = req.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }
            }
        }
    }
}