using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

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

        /// <summary>
        /// Throttler class, to be used via ThrottleFilter as decorate/attribute per Controller method
        /// </summary>
        /// <param name="key"></param>
        /// <param name="requestLimit"></param>
        /// <param name="timeoutInSeconds"></param>
        public Throttler(string key, int requestLimit = 5, int timeoutInSeconds = 10)
        {
            RequestLimit = requestLimit;
            this.timeoutInSeconds = timeoutInSeconds;
            ThrottleGroup = key;
        }

        /// <summary>
        /// Basic check to ensure request fits within specified time window
        /// </summary>
        /// <returns></returns>
        public bool ShouldRequestShouldBeThrottled()
        {
            var throttleInfo = GetThrottleInfoFromCache();

            WindowResetDate = throttleInfo.ExpiresAt;
            RequestsRemaining = Math.Max(RequestLimit - throttleInfo.RequestCount, 0);

            return throttleInfo.RequestCount > RequestLimit;
        }

        /// <summary>
        /// Add appropriate response headers to ensure consumers of the API are
        /// aware of the rate limit window, request count, and reset time in UNIX epoch format
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetRateLimitHeaders()
        {
            var throttleInfo = GetThrottleInfoFromCache();

            return new Dictionary<string, string>
            {
                {"X-RateLimit-Limit", RequestLimit.ToString()},
                {"X-RateLimit-Remaining", Math.Max(RequestLimit - throttleInfo.RequestCount, 0).ToString()},
                {"X-RateLimit-Reset", DateTimeOffset.FromUnixTimeSeconds(throttleInfo.ExpiresAt.Second).ToString()}
            };
        }

        /// <summary>
        /// Safely increment request count for this particular session
        /// </summary>
        public void IncrementRequestCount()
        {
            var throttleInfo = GetThrottleInfoFromCache();

            concurrentCache.AddOrUpdate(ThrottleGroup, throttleInfo, (s, info) =>
            {
                throttleInfo.RequestCount++;
                return throttleInfo;
            });
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
    }
}