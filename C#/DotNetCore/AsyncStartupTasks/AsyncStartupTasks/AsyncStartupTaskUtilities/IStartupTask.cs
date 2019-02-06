using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    /// <summary>
    /// Simple interface for the startup tasks
    /// </summary>
    public interface IStartupTask : IHostedService
    {
        /// <summary>
        /// Execute the startup task, before the WebHost is run
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for cancelling the task</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ExecuteAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the shutdown task, after the WebHost has stopped
        /// </summary>
        /// <param name="cancellationToken">A cancellation token for cancelling the task</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ShutdownAsync(CancellationToken cancellationToken);
    }
}