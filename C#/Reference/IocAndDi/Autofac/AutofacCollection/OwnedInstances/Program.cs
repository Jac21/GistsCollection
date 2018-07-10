using System;
using Autofac;
using Autofac.Features.OwnedInstances;

namespace OwnedInstances
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<S>();
            builder.RegisterType<DisposableComponent>();
            builder.RegisterType<Consumer>();
            Container = builder.Build();

            // Autofac controls lifetime using explicitly-delineated scopes. For example, 
            // the component providing the S service, and all of its dependencies, 
            // will be disposed/released when the using block ends:
            using (var scope = Container.BeginLifetimeScope())
            {
                var s = scope.Resolve<S>();
                s.DoSomething();

                var consumer = scope.Resolve<Consumer>();
                consumer.DoWork();
            }

            // In an IoC container, there’s often a subtle difference between releasing and disposing a component: 
            // releasing an owned component goes further than disposing the component itself.Any of the dependencies 
            // of the component will also be disposed.Releasing a shared component is usually a no - op, 
            // as other components will continue to use its services.
        }
    }

    internal class S
    {
        internal void DoSomething()
        {
            Console.WriteLine($"{GetType().Name} did something");
        }
    }

    /// <summary>
    /// An owned dependency can be released by the owner when it is no longer required. 
    /// Owned dependencies usually correspond to some unit of work performed by the dependent component.
    /// </summary>
    public class Consumer
    {
        private readonly Owned<DisposableComponent> service;

        public Consumer(Owned<DisposableComponent> service)
        {
            this.service = service;
        }

        public void DoWork()
        {
            // service is used for some task
            service.Value.DoSomething();

            // Here, _service is no longer needed, 
            // so it is released
            service.Dispose();
        }
    }

    /// <summary>
#pragma warning disable 1570
    /// When Consumer is created by the container, the Owned<DisposableComponent> that it depends upon 
    /// will be created inside its own lifetime scope. When Consumer is finished using the DisposableComponent, 
    /// disposing the Owned<DisposableComponent> reference will end the lifetime scope that contains 
    /// DisposableComponent. This means that all of DisposableComponent’s non-shared, disposable dependencies will also be released.
    /// </summary>
#pragma warning restore 1570
    public class DisposableComponent
    {
        public void DoSomething()
        {
            Console.WriteLine($"{GetType().Name} did something");
        }
    }

    public class Message
    {
        public string MessageText { get; set; }
    }

    public interface IMessageHandler
    {
        void Handle(Message message);
    }

    /// <summary>
    /// Combining Owned with Func
#pragma warning disable 1570
    /// Owned instances are usually used in conjunction with a Func<T> relationship, so that units of work can be begun and ended on-the-fly.
#pragma warning restore 1570
    /// </summary>
    public class MessagePump
    {
        private readonly Func<Owned<IMessageHandler>> handlerFactory;

        public MessagePump(Func<Owned<IMessageHandler>> handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public void Go()
        {
            while (true)
            {
                var message = NextMessage();

                using (var handler = handlerFactory())
                {
                    handler.Value.Handle(message);
                }
            }

            // ReSharper disable once FunctionNeverReturns
        }

        private static Message NextMessage()
        {
            return new Message
            {
                MessageText = "Next"
            };
        }
    }
}