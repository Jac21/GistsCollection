using Microsoft.Extensions.DependencyInjection;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    /// <summary>
    /// Convenience method for registering startup tasks with the DI container
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
            where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();
    }
}