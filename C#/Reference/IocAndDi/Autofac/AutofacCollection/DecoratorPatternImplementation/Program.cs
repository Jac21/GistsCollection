using System;
using System.Collections.Generic;
using Autofac;

namespace DecoratorPatternImplementation
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();

            // Register the services to be decorated. You have to
            // name them rather than register them As<ICommandHandler>()
            // so the *decorator* can be the As<ICommandHandler>() registration.
            builder.RegisterType<SaveCommandHandler>()
                .Named<ICommandHandler>("handler");
            builder.RegisterType<OpenCommandHandler>()
                .Named<ICommandHandler>("handler");

            // Then register the decorator. The decorator uses the
            // named registrations to get the items to wrap.
            builder.RegisterDecorator<ICommandHandler>(
                (c, inner) => new CommandHandlerDecorator(inner),
                "handler");

            Container = builder.Build();
            
            using (var scope = Container.BeginLifetimeScope())
            {
                // The resolved set of commands will have two items
                // in it, both of which will be wrapped in a CommandHandlerDecorator.
                var handlers = scope.Resolve<IEnumerable<ICommandHandler>>();

                foreach (var handler in handlers)
                {
                    handler.Execute();
                }
            }
        }
    }

    public interface ICommandHandler
    {
        void Execute();
    }

    public class SaveCommandHandler : ICommandHandler
    {
        public void Execute()
        {
            Console.WriteLine("SaveCommand Executed!");
        }
    }

    public class OpenCommandHandler : ICommandHandler
    {
        public void Execute()
        {
            Console.WriteLine("OpenCommand Executed!");
        }
    }

    public class CommandHandlerDecorator : ICommandHandler
    {
        private static ICommandHandler commandHandler;

        public CommandHandlerDecorator(ICommandHandler decorated)
        {
            commandHandler = decorated;
        }

        public void Execute()
        {
            commandHandler.Execute();
        }
    }
}