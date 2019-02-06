using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    public class DelayStartupTask : BackgroundService, IStartupTask
    {
        private readonly StartupTaskContext startupTaskContext;

        public DelayStartupTask(StartupTaskContext startupTaskContext)
        {
            this.startupTaskContext = startupTaskContext;
        }

        Task IStartupTask.ExecuteAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(cancellationToken);
        }

        public Task ShutdownAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(10_000, stoppingToken);
            startupTaskContext.MarkTaskAsComplete();
        }
    }
}