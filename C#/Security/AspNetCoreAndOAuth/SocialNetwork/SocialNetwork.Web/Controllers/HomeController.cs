using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SocialNetwork.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace SocialNetwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> Shouts()
        {
            await RefreshTokensAsync();

            var token = await HttpContext.Authentication.GetTokenAsync("access_token");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var shoutsResponse =
                    await (await client.GetAsync(
                            "http://localhost:33917/api/shouts")).Content
                        .ReadAsStringAsync();

                var shouts = JsonConvert.DeserializeObject<Shout[]>(shoutsResponse);

                return View(shouts);
            }
        }

        private async Task RefreshTokensAsync()
        {
            var authorizationServerInformation = await DiscoveryClient.GetAsync("http://localhost:59418");

            var client = new TokenClient(authorizationServerInformation.TokenEndpoint, "socialnetwork_code", "secret");

            var refreshToken = await HttpContext.Authentication.GetTokenAsync("refresh_token");

            var tokenResponse = await client.RequestRefreshTokenAsync(refreshToken);

            var identityToken = await HttpContext.Authentication.GetTokenAsync("id_token");

            var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);

            var tokens = new[]
            {
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.IdToken,
                    Value = identityToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenResponse.AccessToken
                },
                new AuthenticationToken
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenResponse.RefreshToken
                },
                new AuthenticationToken
                {
                    Name = "expires_at",
                    Value = expiresAt.ToString("O", CultureInfo.InvariantCulture)
                }
            };

            var authenticationInformation = await HttpContext.Authentication.GetAuthenticateInfoAsync("Cookies");

            authenticationInformation.Properties.StoreTokens(tokens);

            await HttpContext.Authentication.SignInAsync("Cookies",
                authenticationInformation.Principal,
                authenticationInformation.Properties);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}