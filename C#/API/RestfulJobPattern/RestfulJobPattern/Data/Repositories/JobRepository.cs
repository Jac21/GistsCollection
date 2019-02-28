using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.Extensions.Locks;
using Couchbase.IO;
using Couchbase.Linq;
using Couchbase.Linq.Extensions;
using RestfulJobPattern.Data.Documents;
using RestfulJobPattern.Models;

namespace RestfulJobPattern.Data.Repositories
{
    public class JobRepository
    {
        private readonly IBucket bucket;

        /// <summary>
        /// Constructor for JobRepository
        /// </summary>
        /// <param name="bucketProvider"></param>
        public JobRepository(IDefaultBucketProvider bucketProvider)
        {
            bucket = bucketProvider.GetBucket();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Job>> GetAllJobsAsync()
        {
            var context = new BucketContext(bucket);

            return context.Query<Job>()
                .OrderBy(p => p.Id)
                .ExecuteAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<Job>> GetIncompleteJobAsync()
        {
            var context = new BucketContext(bucket);

            return context.Query<Job>()
                .Where(p => p.Status != JobStatus.Complete)
                .OrderBy(p => p.Id)
                .ExecuteAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Job> GetJobAsync(long id)
        {
            var result = await bucket.GetDocumentAsync<Job>(Job.GetKey(id));

            if (result.Status == ResponseStatus.KeyNotFound)
            {
                return null;
            }

            // throw an exception on a low-level error
            result.EnsureSuccess();

            return result.Content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public async Task<Job> CreateJobAsync(Job job)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            job.Id = await GetNextJobIdAsync();

            var document = new Document<Job>
            {
                Id = Job.GetKey(job.Id),
                Content = job
            };

            var result = await bucket.InsertAsync(document);
            result.EnsureSuccess();

            return result.Content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        public async Task<Job> UpdateJobAsync(Job job, TimeSpan? expiration)
        {
            if (job == null)
            {
                throw new ArgumentNullException(nameof(job));
            }

            var document = new Document<Job>
            {
                Id = Job.GetKey(job.Id),
                Expiry = (uint) (expiration?.TotalMilliseconds ?? 0),
                Content = job
            };

            var result = await bucket.ReplaceAsync(document);
            result.EnsureSuccess();

            return result.Content;
        }

        public Task<ICouchbaseMutex> LockJobAsync(long id, TimeSpan expiration)
        {
            return bucket.RequestMutexAsync(Job.GetKey(id), expiration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<long> GetNextJobIdAsync()
        {
            var key = JobIdentity.GetKey();

            var builder = bucket.MutateIn<JobIdentity>(key);
            builder.Counter(p => p.Id, 1, true);

            var result = await builder.ExecuteAsync();
            if (result.Status == ResponseStatus.KeyNotFound)
            {
                var content = new JobIdentity
                {
                    Id = 0
                };

                var document = new Document<JobIdentity>
                {
                    Content = content,
                    Id = key
                };

                var insertResult = await bucket.InsertAsync(document);
                if (insertResult.Status == ResponseStatus.KeyExists ||
                    insertResult.Status == ResponseStatus.Success)
                {
                    // document was created by us or another service, try to increment again
                    return await GetNextJobIdAsync();
                }

                // document was not created, throw error
                insertResult.EnsureSuccess();
            }

            result.EnsureSuccess();

            return result.Content(c => c.Id);
        }
    }
}