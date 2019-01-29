using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    /// <summary>
    /// Extension method that finds all the registered IStartupTasks on app startup, runs them in order, and then starts the IWebHost
    /// </summary>
    public static class StartupTaskWebHostExtensions
    {
        public static async Task RunWithTasksAsync(this IWebHost webHost, CancellationToken cancellationToken = default)
        {
            // load all tasks from DI
            var startupTasks = webHost.Services.GetServices<IStartupTask>();

            // Execute all the tasks
            foreach (var startupTask in startupTasks)
            {
                await startupTask.ExecuteAsync(cancellationToken);
            }

            // Start the tasks as normal
            await webHost.RunAsync(cancellationToken);
        }
    }
}