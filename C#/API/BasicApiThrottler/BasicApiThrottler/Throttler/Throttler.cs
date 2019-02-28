using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static BasicApiThrottler.Utilities.Utilities;

namespace BasicApiThrottler.Throttler
{
    public class Throttler
    {
        public int RequestLimit { get; }
        public int RequestsRemaining { get; private set; }
        public DateTime WindowResetDate { get; private set; }
        public string ThrottleGroup { get; set; }

        private readonly int timeoutInSeconds;

        private static readonly ConcurrentDictionary<string, ThrottleInfo> concurrentCache =
            new ConcurrentDictionary<string, ThrottleInfo>();

        public Throttler(string key, int requestLimit = 5, int timeoutInSeconds = 10)
        {
            RequestLimit = requestLimit;
            this.timeoutInSeconds = timeoutInSeconds;
            ThrottleGroup = key;
        }

        public bool ShouldRequestShouldBeThrottled()
        {
            var throttleInfo = GetThrottleInfoFromCache();

            WindowResetDate = throttleInfo.ExpiresAt;
            RequestsRemaining = Math.Max(RequestLimit - throttleInfo.RequestCount, 0);

            return throttleInfo.RequestCount > RequestLimit;
        }

        public Dictionary<string, string> GetRateLimitHeaders()
        {
            var throttleInfo = GetThrottleInfoFromCache();

            return new Dictionary<string, string>
            {
                {"X-RateLimit-Limit", RequestLimit.ToString()},
                {"X-RateLimit-Remaining", Math.Max(RequestLimit - throttleInfo.RequestCount, 0).ToString()},
                {"X-RateLimit-Reset", ToUnixTime(throttleInfo.ExpiresAt).ToString()}
            };
        }

        private ThrottleInfo GetThrottleInfoFromCache()
        {
            var throttleInfo =
                concurrentCache.ContainsKey(ThrottleGroup) ? concurrentCache[ThrottleGroup] : null;

            if (throttleInfo == null ||
                throttleInfo.ExpiresAt <= DateTime.Now)
            {
                throttleInfo = new ThrottleInfo
                {
                    ExpiresAt = DateTime.Now.AddSeconds(timeoutInSeconds),
                    RequestCount = 0
                };
            }

            return throttleInfo;
        }

        public void IncrementRequestCount()
        {
            var throttleInfo = GetThrottleInfoFromCache();
            throttleInfo.RequestCount++;
            concurrentCache[ThrottleGroup] = throttleInfo;
        }
    }
}