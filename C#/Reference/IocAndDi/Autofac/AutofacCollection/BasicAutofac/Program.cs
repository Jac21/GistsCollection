using Autofac;
using BasicAutofac.Implementations;
using BasicAutofac.Interfaces;

namespace BasicAutofac
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            // Make use of our dependency injection
            WriteDate();
        }

        public static void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope
            using (var scope = Container.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }
    }
}