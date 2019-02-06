using System.Threading;
using System.Threading.Tasks;
using AsyncStartupTasks.AsyncStartupTaskUtilities;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AsyncStartupTasks.HealthCheckUtilities
{
    public class StartupTasksHealthCheck : IHealthCheck
    {
        private readonly StartupTaskContext context;

        public StartupTasksHealthCheck(StartupTaskContext context)
        {
            this.context = context;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (this.context.IsComplete)
            {
                return Task.FromResult(HealthCheckResult.Healthy("All startup tasks complete"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Startup tasks not complete"));
        }
    }
}