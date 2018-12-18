using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace RestfulJobPattern.Services
{
    /// <summary>
    /// Processes events from the message bus to start jobs, ensuring that concurrency
    /// is limited so the application instance isn't overloaded.
    /// </summary>
    public class JobProcessor : IDisposable
    {
        private readonly JobService jobService;
        private readonly ILogger<JobProcessor> logger;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        // Limit the number of simultaneous jobs processed by a single instance
        private readonly SemaphoreSlim concurrencyLimiter = new SemaphoreSlim(2);

        private bool started;
        private bool disposed;

        public JobProcessor(JobService jobService, ILogger<JobProcessor> logger)
        {
            this.jobService = jobService;
            this.logger = logger;
        }

        public void Start()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(JobProcessor));
            }

            if (!started)
            {
                Task.Run(Poll);
                started = true;
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                cts.Cancel();
                disposed = true;
            }
        }

        private async Task Poll()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    // preventing oversubscription
                    await concurrencyLimiter.WaitAsync(cts.Token);

#pragma warning disable 4014
                    jobService.ProcessNextJobAsync(cts.Token)
                        .ContinueWith(t =>
#pragma warning restore 4014
                        {
                            concurrencyLimiter.Release();

                            if (t.IsFaulted)
                            {
                                logger.LogError(t.Exception, "Unhandled exception is JobPoller");
                            }
                        }, cts.Token);
                }
                catch (OperationCanceledException)
                {
                    // Swallow
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Unhandled exception in JobPoller");
                }
            }
        }
    }
}