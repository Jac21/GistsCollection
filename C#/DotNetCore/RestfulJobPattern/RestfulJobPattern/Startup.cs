using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestfulJobPattern.Services;

namespace RestfulJobPattern
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            // start the poller when the application starts and dispose it when stopping
            var jobProcessor = app.ApplicationServices.GetRequiredService<JobProcessor>();
            jobProcessor.Start();

            var appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            appLifetime.ApplicationStopping.Register(() => { jobProcessor.Dispose(); });
        }
    }
}