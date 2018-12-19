using AutoMapper;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestfulJobPattern.Data.Repositories;
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
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // cbq.exe> CREATE PRIMARY INDEX ON default;
            services
                .AddCouchbase(options => Configuration.GetSection("Couchbase").Bind(options))
                .AddCouchbaseBucket<IDefaultBucketProvider>("default")
                .AddAutoMapper();

            services
                .AddTransient<JobRepository>()
                .AddTransient<StarRepository>()
                .AddTransient<JobService>()
                .AddSingleton<JobProcessor>()
                .AddSingleton<JobRecoveryPoller>()
                .AddSingleton<MessageBusService>();
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

            var jobRecoveryPoller = app.ApplicationServices.GetRequiredService<JobRecoveryPoller>();
            jobRecoveryPoller.Start();

            var appLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();
            appLifetime.ApplicationStopping.Register(() =>
            {
                jobProcessor.Dispose();
                jobRecoveryPoller.Dispose();
            });

            appLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });
        }
    }
}