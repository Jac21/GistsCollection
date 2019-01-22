using System.Reflection;
using AspNetIdentityDeepDive.DbContexts;
using AspNetIdentityDeepDive.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

            services.AddIdentityCore<MyIdentityUser>(options => { });
            services.AddScoped<IUserStore<MyIdentityUser>, UserOnlyStore<MyIdentityUser, MyIdentityUserDbContext>>();

            services.AddAuthentication("cookies").AddCookie("cookies", options => options.LoginPath = "/Home/Login");
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