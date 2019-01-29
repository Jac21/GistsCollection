using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;
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

        private static IServiceCollection AddTaskExecutingServer(this IServiceCollection services)
        {
            var decoratorType = typeof(TaskExecutingServer);
            if (services.Any(service => service.ImplementationType == decoratorType))
            {
                // We've already decorated the IServer (make this call idempotent)
                return services;
            }

            var serverDescriptor = GetIServerDescriptor(services);
            if (serverDescriptor is null)
            {
                // We don't have an IServer!
                throw new Exception(
                    "Could not find any registered services for type IServer. IStartupTask requires using an IServer");
            }

            var decoratorDescriptor = CreateDecoratorDescriptor(serverDescriptor, decoratorType);

            var index = services.IndexOf(serverDescriptor);

            // To avoid reordering descriptors, in case a specific order is expected.
            services.Insert(index, decoratorDescriptor);

            services.Remove(serverDescriptor);

            return services;
        }

        private static ServiceDescriptor GetIServerDescriptor(IServiceCollection services)
        {
            Type server = typeof(IServer);
            return services.FirstOrDefault(service => service.ServiceType == server);
        }

        private static ServiceDescriptor CreateDecoratorDescriptor(this ServiceDescriptor innerDescriptor,
            Type decoratorType)
        {
            object Factory(IServiceProvider provider) =>
                provider.CreateInstance(decoratorType, provider.GetInstance(innerDescriptor));

            return ServiceDescriptor.Describe(innerDescriptor.ServiceType, (Func<IServiceProvider, object>) Factory,
                innerDescriptor.Lifetime);
        }

        private static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
        {
            if (descriptor.ImplementationInstance != null)
            {
                return descriptor.ImplementationInstance;
            }

            if (descriptor.ImplementationType != null)
            {
                return provider.GetServiceOrCreateInstance(descriptor.ImplementationType);
            }

            return descriptor.ImplementationFactory(provider);
        }

        private static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
        {
            return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
        }

        private static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
        {
            return ActivatorUtilities.CreateInstance(provider, type, arguments);
        }
    }
}