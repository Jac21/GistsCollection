using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Couchbase.IO;
using Couchbase.Linq;
using Couchbase.Linq.Extensions;
using RestfulJobPattern.Data.Documents;

namespace RestfulJobPattern.Data.Repositories
{
    public class StarRepository
    {
        private readonly IBucket bucket;

        public StarRepository(IDefaultBucketProvider bucketProvider)
        {
            bucket = bucketProvider.GetBucket();
        }

        public Task<IEnumerable<Star>> GetAllStarsAsync()
        {
            var context = new BucketContext(bucket);

            return context.Query<Star>()
                .OrderBy(p => p.Name)
                .ExecuteAsync();
        }

        public async Task<Star> GetStarAsync(long id)
        {
            var result = await bucket.GetDocumentAsync<Star>(Star.GetKey(id));
            if (result.Status == ResponseStatus.KeyNotFound)
            {
                return null;
            }

            // Throw an exception on a low-level error
            result.EnsureSuccess();

            return result.Content;
        }

        public async Task<Star> CreateStarAsync(Star star)
        {
            if (star == null)
            {
                throw new ArgumentNullException(nameof(star));
            }

            star.Id = await GetNextStarIdAsync();

            var document = new Document<Star>
            {
                Id = Star.GetKey(star.Id),
                Content = star
            };

            var result = await bucket.InsertAsync(document);
            result.EnsureSuccess();

            return result.Content;
        }

        public async Task DeleteStarAsync(long id)
        {
            var result = await bucket.RemoveAsync(Star.GetKey(id));
            if (!result.Success && result.Status != ResponseStatus.KeyNotFound)
            {
                // Throw an exception on a low-level error
                result.EnsureSuccess();
            }
        }

        private async Task<long> GetNextStarIdAsync()
        {
            var key = StarIdentity.GetKey();

            var builder = bucket.MutateIn<StarIdentity>(key);
            builder.Counter(p => p.Id, 1, true);

            var result = await builder.ExecuteAsync();
            if (result.Status == ResponseStatus.KeyNotFound)
            {
                var content = new StarIdentity
                {
                    Id = 0
                };

                var document = new Document<StarIdentity>
                {
                    Content = content,
                    Id = key
                };

                var insertResult = await bucket.InsertAsync(document);
                if (insertResult.Status == ResponseStatus.KeyExists ||
                    insertResult.Status == ResponseStatus.Success)
                {
                    // Document was created by us or another service, retry increment
                    return await GetNextStarIdAsync();
                }

                // Document was not created, throw error
                insertResult.EnsureSuccess();
            }

            result.EnsureSuccess();

            return result.Content(c => c.Id);
        }
    }
}