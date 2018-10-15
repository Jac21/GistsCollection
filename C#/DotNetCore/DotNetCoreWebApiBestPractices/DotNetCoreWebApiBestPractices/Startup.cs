using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DotNetCoreWebApiBestPractices.Pages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

// ReSharper disable UnusedParameter.Local

namespace DotNetCoreWebApiBestPractices
{
    /// <summary>
    /// ASP.NET Core Web API Best Practices
    /// </summary>
    public class Startup
    {
        private readonly ILogger<Startup> logger;

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureMvc();
            services.ConfigureCors();
            services.ConfigureAuthentication();
            services.AddScoped<ModelValidationAttribute>();

            CheckMissingDependenciesOnStartup(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(config =>
                {
                    config.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            await context.Response.WriteAsync(new ErrorModel {Response = {StatusCode = 500}}
                                .ToString());
                        }
                    });
                });
            }

            app.UseStaticFiles();

            app.UseMvc();

            app.UseAuthentication();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleware(IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }

        /// <summary>
        /// https://engineering.gigpin.com/2018-10-08-asp-net-core-check-dependencies/?utm_source=csharpdigest&utm_medium=email&utm_campaign=featured
        /// </summary>
        /// <param name="services"></param>
        public void CheckMissingDependenciesOnStartup(IServiceCollection services)
        {
            var controllers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .ToList();

            var sp = services.BuildServiceProvider();
            foreach (var controllerType in controllers)
            {
                logger.LogInformation($"Found {controllerType.Name}");
                try
                {
                    sp.GetService(controllerType);
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex,
                        $"Cannot create instance of controller {controllerType.FullName}, it is missing some services");
                }
            }
        }
    }

    /// <summary>
    /// Startup Class and the Service Configuration - 
    /// Extension class to hold bulk of service registation logic to be used 
    /// within the Startup's ConfigureServices call
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Abstracted configuration extension method for configuring CORS within
        /// ConfigureServices method on startup
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        /// <summary>
        /// Content Negotiation, what if the consumer of our Web API wants another response format, like XML for example?
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                // Add XML Content Negotiation
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter());
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                config.ReturnHttpNotAcceptable = true;
            });
        }

        /// <summary>
        /// Using JWT
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                        // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
                        {
                            //Configuration in here
                        };
                });
        }
    }

    /// <summary>
    /// Handling Errors Globally
    /// </summary>
    public class CustomExceptionMiddleware
    {
        //constructor and service injection
        private readonly ILogger logger;

        public CustomExceptionMiddleware(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError("Unhandled exception ...", ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            throw new NotImplementedException();
        }

        private Task _next(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        //additional methods
    }

    /// <summary>
    /// Using ActionFilters to Remove Duplicated Code
    /// </summary>
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState); // returns 400 with error
            }
        }
    }
}