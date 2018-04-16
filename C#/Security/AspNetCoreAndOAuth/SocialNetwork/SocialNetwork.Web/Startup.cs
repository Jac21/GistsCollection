using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SocialNetwork.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Set authentication scheme for cookies and OpenID Connect,
            // various options for configuration
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies"
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //// Implicit flow configuration
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            //{
            //    AuthenticationScheme = "oidc",
            //    SignInScheme = "Cookies",
            //    Authority = "http://localhost:59418",
            //    RequireHttpsMetadata = false,
            //    ClientId = "socialnetwork_implicit",
            //    ResponseType = "id_token token",
            //    SaveTokens = true
            //});

            //// Code flow configuration
            //app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            //{
            //    AuthenticationScheme = "oidc",
            //    SignInScheme = "Cookies",
            //    Authority = "http://localhost:59418",
            //    RequireHttpsMetadata = false,
            //    ClientId = "socialnetwork_code",
            //    ClientSecret = "secret",
            //    ResponseType = "id_token code",
            //    Scope = { "socialnetwork", "offline_access", "email" },
            //    GetClaimsFromUserInfoEndpoint = true,
            //    SaveTokens = true
            //});

            // Auth0 configuration
            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions
            {
                Authority = "https://jac21.auth0.com",
                CallbackPath = new PathString("/signing-auth0"),
                RequireHttpsMetadata = false,
                ClientId = "nope",
                ClientSecret = "nada",
                SaveTokens = true,
                ResponseType = "id_token code",
                Scope = {"socialnetwork", "openid", "offline_access"},
                SignInScheme = "Cookies",
                Events = new OpenIdConnectEvents
                {
                    OnRedirectToIdentityProvider = context =>
                    {
                        context.ProtocolMessage.SetParameter("audience", "http://localhost:33917/");
                        return Task.CompletedTask;
                    }
                }
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}