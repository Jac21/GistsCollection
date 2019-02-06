using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    public class StartupTasksMiddleware
    {
        private readonly StartupTaskContext context;
        private readonly RequestDelegate next;

        public StartupTasksMiddleware(StartupTaskContext context, RequestDelegate next)
        {
            this.context = context;
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (context.IsComplete)
            {
                await next(httpContext);
            }
            else
            {
                var response = httpContext.Response;
                response.StatusCode = 503;
                response.Headers["Retry-After"] = "30";
                await response.WriteAsync("Service Unavailable");
            }
        }
    }
}