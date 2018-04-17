using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Data;
using SocialNetwork.Data.Repositories;
using System.Data.SqlClient;

namespace SocialNetwork.Api
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
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(
                c => { c.SwaggerDoc("v1", new Info {Title = "Social Network API", Version = "v1"}); });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.Register(x =>
                new Func<IDbConnection>(() => new SqlConnection(Configuration.GetConnectionString("SocialNetwork"))));

            builder.RegisterType<UserRepository>().AsImplementedInterfaces().AsSelf();
            builder.RegisterType<ProfileRepository>().AsImplementedInterfaces().AsSelf();
            builder.RegisterType<ShoutRepository>().AsImplementedInterfaces().AsSelf();

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //// IdentityServer4 configuration
            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    RequireHttpsMetadata = false,
            //    Authority = "http://localhost:59418",
            //    ApiName = "socialnetwork"
            //});

            // Auth0 JWT configuration
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                Audience = "http://localhost:33917/",
                Authority = "https://jac21.auth0.com"
            });

            // When using Auth0, make sure rule is in place to obtain necessary data for controllers, e.g.,
            // function (user, context, callback) {
              // TODO: implement your rule
              //context.accessToken.email = user.email;
              //callback(null, user, context);
            //}

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger(_ => { });

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}