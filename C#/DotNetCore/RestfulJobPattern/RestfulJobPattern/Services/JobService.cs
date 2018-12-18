using System;
using System.Threading;
using System.Threading.Tasks;
using RestfulJobPattern.Data.Documents;
using RestfulJobPattern.Data.Repositories;
using RestfulJobPattern.Models;

namespace RestfulJobPattern.Services
{
    public class JobService
    {
        private readonly JobRepository jobRepository;
        private readonly MessageBusService messageBus;

        public JobService(JobRepository jobRepository, MessageBusService messageBus)
        {
            this.jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
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

        public async Task ProcessNextJobAsync(CancellationToken cancellationToken)
        {
            //var message = await _messageBus.ReceiveMessage(cancellationToken);
            //if (message != null)
            //{
            //    await ExecuteJobAsync(message.JobId, cancellationToken);
            //}
        }
    }
}