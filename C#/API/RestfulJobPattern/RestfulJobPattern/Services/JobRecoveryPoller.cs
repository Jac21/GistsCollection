using System;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Core;
using Couchbase.Extensions.Locks;
using Microsoft.Extensions.Logging;
using RestfulJobPattern.Data.Repositories;

namespace RestfulJobPattern.Services
{
    /// <summary>
    /// Polls at a regular interval to find incomplete jobs and attempt to requeue them
    /// via the message bus. This will restart any jobs that were dropped.
    /// </summary>
    public class JobRecoveryPoller : IDisposable
    {
        private static readonly TimeSpan pollInterval = TimeSpan.FromMinutes(1);

        private readonly JobService jobService;
        private readonly JobRepository jobRepository;
        private readonly ILogger<JobRecoveryPoller> logger;
        private readonly IBucket bucket;

        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        private bool started;
        private bool disposed;

        public JobRecoveryPoller(JobService jobService, JobRepository jobRepository, IDefaultBucketProvider bucketProvider, ILogger<JobRecoveryPoller> logger)
        {
            this.jobService = jobService;
            this.jobRepository = jobRepository;
            this.logger = logger;

            bucket = bucketProvider.GetBucket();
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

        public async Task Poll()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    // wait for poll interval
                    await Task.Delay(pollInterval, cts.Token);

                    // Take out a lock for job recovery polling
                    // Don't dispose so that it holds until it expires after PollInterval
                    // This will ensure only one instance of the app polls every poll interval
                    await bucket.RequestMutexAsync("jobRecoveryPoller", pollInterval);

                    var jobs = await jobRepository.GetIncompleteJobAsync();
                    foreach (var job in jobs)
                    {
                        try
                        {
                            // try to lock the job to see if it's being processed currently
                            using (jobRepository.LockJobAsync(job.Id, TimeSpan.FromSeconds(1)))
                            {
                            }

                            // Make sure we've unlocked the job before we get here
                            // And fire events into the message bus for the unhandled job
                            // This allows any instance with capacity to pick up the job
                            jobService.QueueJob(job.Id);
                        }
                        catch (CouchbaseLockUnavailableException)
                        {
                            // Job is already being processed, ignore
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is OperationCanceledException || ex is CouchbaseLockUnavailableException)
                    {
                        // Ignore
                        // Unable to lock for singleton job recovery poller process, ignore
                    }
                    else
                    {
                        logger.LogError(ex, "Unhandled exception in JobRecoveryPoller");
                    }
                }
            }
        }
    }
}
