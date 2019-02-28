using System;

namespace BasicApiThrottler.Throttler
{
    public class ThrottleInfo
    {
        public DateTime ExpiresAt { get; set; }
        public int RequestCount { get; set; }
    }
}