using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;

namespace RegistrationSourceImplementation
{
    public class HandlerRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            if (!(service is IServiceWithType swt) || !typeof(BaseHandler).IsAssignableFrom(swt.ServiceType))
                return Enumerable.Empty<IComponentRegistration>();

            // Where the magic happens
            var registration = new ComponentRegistration(Guid.NewGuid(), new DelegateActivator(swt.ServiceType,
                    (c, p) =>
                    {
                        // Factory itself is assumed to be registered with Autofac, so we can resolve
                        // this factory. Hard coding factory can happen here, too
                        var provider = c.Resolve<IHandlerFactory>();

                        // Our factory interface is generic, so we have to use a bit of
                        // reflection to make the call
                        var method = provider.GetType().GetMethod("GetHandler")?.MakeGenericMethod(swt.ServiceType);

                        return method?.Invoke(provider, null);
                    }), new CurrentScopeLifetime(), InstanceSharing.None, InstanceOwnership.OwnedByLifetimeScope,
                new[] {service}, new Dictionary<string, object>());

            return new IComponentRegistration[] {registration};
        }

        public bool IsAdapterForIndividualComponents => false;
    }
}