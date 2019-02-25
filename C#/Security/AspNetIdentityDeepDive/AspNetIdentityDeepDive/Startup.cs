using System;
using System.Reflection;
using AspNetIdentityDeepDive.DbContexts;
using AspNetIdentityDeepDive.Models;
using AspNetIdentityDeepDive.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetIdentityDeepDive
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
            services.AddMvc();

            // register DB context, avoid ef migrations error by specifying migrations assembly
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<MyIdentityUserDbContext>(opt =>
                opt.UseSqlServer(
                    "Data Source=JCANTU\\SQL2016CIAI;Initial Catalog=MyIdentityDatabase2;Integrated Security=True",
                    sql => sql.MigrationsAssembly(migrationAssembly)));

            services.AddIdentity<MyIdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Tokens.EmailConfirmationTokenProvider = "emailconf";
                })
                .AddEntityFrameworkStores<MyIdentityUserDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<EmailConfirmationTokenProvider<MyIdentityUser>>("emailconf");

            services.AddScoped<IUserClaimsPrincipalFactory<MyIdentityUser>, MyIdentityUserClaimsPrincipalFactory>();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(3));
            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(2);
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Home/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}