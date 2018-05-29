using System;
using Autofac;

namespace NamedAndKeyedServices
{
    public class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            var builder = new ContainerBuilder();

            // Autofac provides three typical ways to identify services. The most common is to identify by type:
            builder.RegisterType<OnlineState>().As<IDeviceState>();

            // Services can be further identified using a service name. Using this technique, 
            // the Named() registration method replaces As().
            builder.RegisterType<OfflineState>().Named<IDeviceState>("offline");

            // Using strings as component names is convenient in some cases, but in 
            // others we may wish to use keys of other types. Keyed services provide this ability.
            builder.RegisterType<IdleState>().Keyed<IDeviceState>(DeviceState.Idle);

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var onlineState = scope.Resolve<IDeviceState>();
                onlineState.EmitState();

                Console.WriteLine();

                var offlineState = scope.ResolveNamed<IDeviceState>("offline");
                offlineState.EmitState();

                Console.WriteLine();

                var idleState = scope.ResolveKeyed<IDeviceState>(DeviceState.Idle);
                idleState.EmitState();
            }
        }
    }

    public enum DeviceState
    {
        Online = 1,
        Offline = 2,
        Idle = 3
    }

    public interface IDeviceState
    {
        void EmitState();
    }

    public class OnlineState : IDeviceState
    {
        public OnlineState()
        {
            Console.WriteLine("OnlineState Injected!");
        }

        public void EmitState()
        {
            Console.WriteLine("OnlineState emitted!");
        }
    }

    public class OfflineState : IDeviceState
    {
        public OfflineState()
        {
            Console.WriteLine("OfflineState Injected!");
        }

        public void EmitState()
        {
            Console.WriteLine("OfflineState emitted!");
        }
    }

    public class IdleState : IDeviceState
    {
        public IdleState()
        {
            Console.WriteLine("IdleState Injected!");
        }

        public void EmitState()
        {
            Console.WriteLine("IdleState emitted!");
        }
    }
}