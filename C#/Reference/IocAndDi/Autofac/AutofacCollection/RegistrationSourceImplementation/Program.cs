using Autofac;

namespace RegistrationSourceImplementation
{
    public class Program
    {
        private static IContainer Container { get; set; }

        private static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HandlerFactory>().As<IHandlerFactory>();
            builder.RegisterSource(new HandlerRegistrationSource());
            builder.RegisterType<ConsumerA>();
            builder.RegisterType<ConsumerB>();
            Container = builder.Build();

            DoWork();
        }

        public static void DoWork()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var consumer = scope.Resolve<ConsumerA>();

                // Calling this will yield the following ouput on the console:
                // [A] Handled: ConsumerA
                consumer.DoWork();
            }
        }
    }
}