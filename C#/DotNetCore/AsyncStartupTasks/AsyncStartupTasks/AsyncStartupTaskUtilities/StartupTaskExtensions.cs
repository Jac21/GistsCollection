using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    public static class StartupTaskExtensions
    {
        private static readonly StartupTaskContext sharedContext = new StartupTaskContext();

        public static IServiceCollection AddStartupTasks(this IServiceCollection services)
        {
            // Don't add StartupTaskContext if we've already added it
            if (services.Any(x => x.ServiceType == typeof(StartupTaskContext)))
            {
                return services;
            }

            return services.AddSingleton(sharedContext);
        }

        public static IServiceCollection AddStartupTaskToSharedContext<T>(this IServiceCollection services)
            where T : class, IStartupTask
        {
            sharedContext.RegisterTask();
            return services
                .AddStartupTasks() // in case AddStartupTasks() hasn't been called
                .AddHostedService<T>();
        }
    }
}