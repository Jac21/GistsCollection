using ActnarsImplementation.Implementations;
using Autofac;
using Autofac.Features.ResolveAnything;

namespace ActnarsImplementation
{
    public class Program
    {
        private static IContainer Container { get; set; }

        private static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            Container = builder.Build();

            // Make use of our dependency injection
            WriteDate();
        }

        public static void WriteDate()
        {
            // Create the scope, resolve your concrete type,
            // use it, then dispose of the scope
            using (var scope = Container.BeginLifetimeScope())
            {
                var foo = scope.Resolve<ConsoleOutput>();
                foo.Write("Resolved!");
            }
        }
    }
}