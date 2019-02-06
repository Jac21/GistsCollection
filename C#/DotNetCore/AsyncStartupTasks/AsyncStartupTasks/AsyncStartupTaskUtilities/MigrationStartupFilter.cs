using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncStartupTasks.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    public class MigrationStartupFilter : IStartupTask
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// We need to inject the IServiceProvider so we can create the scoped service, MyDbContext
        /// </summary>
        /// <param name="serviceProvider"></param>
        public MigrationStartupFilter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using (var scope = serviceProvider.CreateScope())
            {
                // Get the DbContext instance
                var myDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Do the migration
                await myDbContext.Database.MigrateAsync(cancellationToken);
            }
        }

        public Task ShutdownAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}