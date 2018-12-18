using Couchbase.Core;

namespace RestfulJobPattern.Data.Repositories
{
    public interface IDefaultBucketProvider
    {
        IBucket GetBucket();
    }
}