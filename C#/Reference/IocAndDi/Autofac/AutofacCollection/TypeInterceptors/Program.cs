using System;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

namespace TypeInterceptors
{
    /// <summary>
    /// The Autofac.Extras.DynamicProxy integration package enables method calls on Autofac components to be intercepted by other components. 
    /// Common use-cases are transaction handling, logging, and declarative security.
    /// </summary>
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            // Interceptors must be registered with the container. You can register them either as typed services or as named services. 
            // If you register them as named services, they must be named IInterceptor registrations.
            var builder = new ContainerBuilder();

            // Enable Interception on Types
            builder.RegisterType<SomeType>()
                .As<ISomeInterface>()
                // .InterceptedBy(typeof(CallLogger));
                .EnableInterfaceInterceptors();

            builder.RegisterType<SomeTypeTwo>()
                .As<ISomeInterfaceTwo>()
                .EnableInterfaceInterceptors();

            // Named registration
            builder.Register(c => new CallLogger(Console.Out)).Named<IInterceptor>("log-calls");

            // Typed registration
            builder.Register(c => new CallLogger(Console.Out));

            Container = builder.Build();

            var willBeIntercepted = Container.Resolve<ISomeInterface>();
            willBeIntercepted.DoWorkIntercepted("Invocation!");

            var willBeInterceptedTwo = Container.Resolve<ISomeInterfaceTwo>();
            willBeInterceptedTwo.DoWorkInterceptedTwo("Invocation");
        }
    }

    /// <summary>
    /// INTERCEPTORS REQUIRE INTERFACES TO BE PUBLIC, OR MARKED IF THEY ARE INTERNAL WITH THE 
    /// [assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] ATTRIBUTE
    /// </summary>
    public interface ISomeInterface
    {
        int DoWorkIntercepted(string parameter);
    }

    // This attribute will look for a TYPED
    // interceptor registration:
    [Intercept(typeof(CallLogger))]
    public class SomeType : ISomeInterface
    {
        public int DoWorkIntercepted(string parameter)
        {
            Console.WriteLine(parameter);
            return 0;
        }
    }

    public interface ISomeInterfaceTwo
    {
        int DoWorkInterceptedTwo(string parameter);
    }

    // This attribute will look for a NAMED
    // interceptor registration:
    [Intercept("log-calls")]
    public class SomeTypeTwo : ISomeInterfaceTwo
    {
        public int DoWorkInterceptedTwo(string parameter)
        {
            Console.WriteLine($"{parameter} two!");
            return 0;
        }
    }

    /// <summary>
    /// Interceptors implement the Castle.DynamicProxy.IInterceptor interface. 
    /// Here’s a simple interceptor example that logs method calls including inputs and outputs:
    /// </summary>
    public class CallLogger : IInterceptor
    {
        private readonly TextWriter output;

        public CallLogger(TextWriter output)
        {
            this.output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation == null)
                throw new ArgumentNullException(nameof(invocation));

            output.WriteLine(
                $"Calling method {invocation.Method.Name} with parameters {string.Join("", "", invocation.Arguments.Select(a => (a ?? "").ToString().ToArray()))}!");

            invocation.Proceed();

            output.WriteLine($"Done! Result was {invocation.ReturnValue}");
        }
    }
}