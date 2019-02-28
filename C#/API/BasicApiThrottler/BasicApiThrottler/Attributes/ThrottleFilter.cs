using System;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BasicApiThrottler.Attributes
{
    public class ThrottleFilter : ActionFilterAttribute
    {
        private readonly Throttler.Throttler throttler;
        private readonly string throttleGroup;

        private readonly IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

        public ThrottleFilter(
            int requestLimit = 5,
            int timeoutInSeconds = 10,
            [CallerMemberName] string throttleGroup = null)
        {
            this.throttleGroup = throttleGroup;
            throttler = new Throttler.Throttler(throttleGroup, requestLimit, timeoutInSeconds);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SetIdentityAsThrottleGroup();

            if (throttler.ShouldRequestShouldBeThrottled())
            {
                context.Result = new BadRequestResult();

                AddThrottleHeaders(context.HttpContext.Response);
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            SetIdentityAsThrottleGroup();

            if (context.Exception == null) throttler.IncrementRequestCount();

            AddThrottleHeaders(context.HttpContext.Response);

            base.OnActionExecuted(context);
        }

        private void SetIdentityAsThrottleGroup()
        {
            if (string.Equals(throttleGroup, "identity", StringComparison.OrdinalIgnoreCase))
                throttler.ThrottleGroup = httpContextAccessor.HttpContext.User.Identity.Name;

            if (string.Equals(throttleGroup, "ipaddress", StringComparison.OrdinalIgnoreCase))
                throttler.ThrottleGroup = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        private void AddThrottleHeaders(HttpResponse response)
        {
            if (response == null) return;

            foreach (var header in throttler.GetRateLimitHeaders())
            {
                response.Headers.Add(header.Key, header.Value);
            }
        }
    }
}