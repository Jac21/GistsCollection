using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Autofac.Multitenant;

/*
 * The overall registration process is:
1. Create an application-level default container. This container is where you register the default dependencies for the application. 
If a tenant doesn’t otherwise provide an override for a dependency type, the dependencies registered here will be used.
2. Instantiate a tenant identification strategy. A tenant identification strategy is used to determine the ID for the current 
tenant based on execution context. You can read more on this later in this document.
3. Create a multitenant container. The multitenant container is responsible for keeping track of the application defaults 
and the tenant-specific overrides.
4. Register tenant-specific overrides. For each tenant wishing to override a dependency, register the appropriate overrides 
passing in the tenant ID and a configuration lambda.
 */

namespace MultitenantApplications
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            // First, create your application-level defaults using a standard
            // ContainerBuilder, just as you are used to.
            var builder = new ContainerBuilder();

            builder.RegisterType<Consumer>().As<IDependencyConsumer>().InstancePerDependency();
            builder.RegisterType<BaseDependency>().As<IDependency>().SingleInstance();

            // If you have a component that needs one instance per tenant, you can use the InstancePerTenant() 
            // registration extension method at the container level.
            // InstancePerTenant goes in the main container; other
            // tenant-specific dependencies get registered as shown
            // above, in tenant-specific lifetimes.
            builder.RegisterType<SomeType>().As<ISomeInterface>().InstancePerTenant();

            Container = builder.Build();

            /*******************************************************************************************************/

            // Once you've built the application-level default container, you
            // need to create a tenant identification strategy.
            var tenantIdentifier = new MyTenantIdentificationStrategy();

            /*******************************************************************************************************/

            // Now create the multitenant container using the application
            // container and the tenant identification strategy.
            var mtc = new MultitenantContainer(tenantIdentifier, Container);

            /*******************************************************************************************************/

            // Configure the overrides for each tenant by passing in the tenant ID
            // and a lambda that takes a ContainerBuilder.
            mtc.ConfigureTenant('1',
                b => b.RegisterType<Tenant1Dependency>().As<IDependency>().InstancePerDependency());
            mtc.ConfigureTenant('2', b => b.RegisterType<Tenant2Dependency>().As<IDependency>().SingleInstance());

            // Note that you may only configure a tenant one time.
            // Create a configuration action builder to aggregate registration
            // actions over the course of some business logic.
            var actionBuilder = new ConfigurationActionBuilder();

            // Do some logic...
            var SomethingIsTrue = true;
            var AnotherThingIsTrue = true;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (SomethingIsTrue)
            {
                actionBuilder.Add(b => b.RegisterType<AnOverride>().As<ISomething>());
            }

            actionBuilder.Add(b => b.RegisterType<SomeClass>());
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (AnotherThingIsTrue)
            {
                actionBuilder.Add(b => b.RegisterModule<MyModule>());
            }

            // Now configure a tenant using the built action.
            mtc.ConfigureTenant('3', actionBuilder.Build());

            // Now you can use the multitenant container to resolve instances.
            var dependency = mtc.Resolve<IDependency>();
            dependency.DoWork();

            if (mtc.IsRegistered<ISomething>())
            {
                var dependencyTwo = mtc.Resolve<ISomething>();
                dependencyTwo.DoSomething();
            }
        }
    }

    internal class MyModule : IModule
    {
        public void Configure(IComponentRegistry componentRegistry)
        {
            Console.WriteLine($"{GetType()} configured!");
        }
    }

    internal class SomeClass
    {
    }

    internal interface ISomething
    {
        void DoSomething();
    }

    internal class AnOverride : ISomething
    {
        public void DoSomething()
        {
            Console.WriteLine($"{GetType()} did something!");
        }
    }

    internal class Tenant2Dependency : IDependency
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} - Tenant2Dependency did work!");
        }
    }

    internal class Tenant1Dependency : IDependency
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} - Tenant1Dependency did work!");
        }
    }

    /// <summary>
    /// Performance is important in tenant identification. Tenant identification happens every time you resolve a component, 
    /// begin a new lifetime scope, etc. As such, it is very important to make sure your tenant identification strategy 
    /// is fast. For example, you wouldn’t want to do a service call or a database query during tenant identification.
    /// </summary>
    internal class MyTenantIdentificationStrategy : ITenantIdentificationStrategy
    {
        public bool TryIdentifyTenant(out object tenantId)
        {
            tenantId = null;

            Random random = new Random();

            var ids = new List<char>
            {
                '1',
                '2',
                '3'
            };

            int index = random.Next(ids.Count);

            tenantId = index;

            return true;
        }
    }

    internal interface IDependency
    {
        void DoWork();
    }

    internal interface ISomeInterface
    {
        void DoSomething();
    }

    internal class SomeType : ISomeInterface
    {
        public void DoSomething()
        {
            Console.WriteLine($"{GetType()} did something!");
        }
    }

    internal class BaseDependency : IDependency
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }

    internal interface IDependencyConsumer
    {
        void DoWork();
    }

    internal class Consumer : IDependencyConsumer
    {
        public void DoWork()
        {
            Console.WriteLine($"{GetType()} did work!");
        }
    }
}