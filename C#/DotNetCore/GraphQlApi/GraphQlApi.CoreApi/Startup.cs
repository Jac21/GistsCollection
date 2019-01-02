using GraphQL;
using GraphQL.API;
using GraphQL.Http;
using GraphQL.Types;
using GraphQlApi.CoreApi.Settings;
using GraphQlApi.Data.Queries;
using GraphQlApi.Data.Schemas;
using GraphQlApi.Data.Types;
using GraphQlApi.ServiceInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GraphQlApi.Repositories;

namespace GraphQlApi.CoreApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            services.AddSingleton<ISalesService, StaticSalesService>();

            services.AddSingleton<SalesQuery>();
            services.AddSingleton<CustomerType>();
            services.AddSingleton<EmployeeType>();
            services.AddSingleton<ProductType>();
            services.AddSingleton<DepartmentType>();
            services.AddSingleton<ProviderType>();
            services.AddSingleton<ProductCategoryType>();
            services.AddSingleton<OrderType>();
            services.AddSingleton<OrderDetailType>();
            services.AddSingleton<ISchema, SalesSchema>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<GraphQlMiddleware>(new GraphQlSettings
            {
                BuildUserContext = ctx => new GraphQlUserContext
                {
                    User = ctx.User
                }
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}