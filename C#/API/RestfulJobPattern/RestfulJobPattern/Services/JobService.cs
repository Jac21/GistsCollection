using System;
using System.Threading;
using System.Threading.Tasks;
using Couchbase.Extensions.Locks;
using RestfulJobPattern.Data.Documents;
using RestfulJobPattern.Data.Repositories;
using RestfulJobPattern.Models;

namespace RestfulJobPattern.Services
{
    public class JobService
    {
        private readonly JobRepository jobRepository;
        private readonly StarRepository starRepository;
        private readonly MessageBusService messageBus;

        public JobService(JobRepository jobRepository, StarRepository starRepository, MessageBusService messageBus)
        {
            this.jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            this.starRepository = starRepository ?? throw new ArgumentNullException(nameof(starRepository));
            this.messageBus = messageBus;
        }

        public async Task<Job> CreateStarJobAsync(Star star)
        {
            var job = new Job
            {
                CreateStar = star,
                Status = JobStatus.Queued
            };

            await jobRepository.CreateJobAsync(job);

            return job;
        }

        public void QueueJob(long id)
        {
            messageBus.SendMessage(new Message
            {
                JobId = id
            });
        }

        public async Task ProcessNextJobAsync(CancellationToken cancellationToken)
        {
            var message = await messageBus.ReceiveMessage(cancellationToken);
            if (message != null)
            {
                await ExecuteJobAsync(message.JobId, cancellationToken);
            }
        }

        public async Task ExecuteJobAsync(long id, CancellationToken token)
        {
            try
            {
                using (var mutex = await jobRepository.LockJobAsync(id, TimeSpan.FromMinutes(1)))
                {
                    mutex.AutoRenew(TimeSpan.FromSeconds(15), TimeSpan.FromHours(1));

                    // once we have the lock, reload the document to make sure it's still pending
                    // It’s particularly important to reload the Job from the data store and check 
                    // its status before processing. This should be done within the mutex in case 
                    // another instance is finishing the job just as this instance is trying to start the job.
                    var job = await jobRepository.GetJobAsync(id);

                    if (job.Status == JobStatus.Complete)
                    {
                        return;
                    }

                    // update the status to Running
                    job.Status = JobStatus.Running;
                    await jobRepository.UpdateJobAsync(job, null);

                    // TODO: we're just emulating a long running job here, so just delay
                    await Task.Delay(TimeSpan.FromSeconds(15), token);

                    // TODO: To emulate a failed job, either throw an exception here
                    // Or stop the app before the delay above is reached

                    // Finish creating the star
                    await starRepository.CreateStarAsync(job.CreateStar);

                    // Update the job status document
                    job.Status = JobStatus.Complete;
                    await jobRepository.UpdateJobAsync(job, TimeSpan.FromDays(1));
                }
            }
            catch (CouchbaseLockUnavailableException)
            {
                // swallow and skip processing
            }
        }
    }
}