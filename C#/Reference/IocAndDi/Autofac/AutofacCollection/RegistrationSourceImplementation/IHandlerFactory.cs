using System;

namespace RegistrationSourceImplementation
{
    public interface IHandlerFactory
    {
        T GetHandler<T>() where T : BaseHandler;
    }

    public class HandlerFactory : IHandlerFactory
    {
        public T GetHandler<T>() where T : BaseHandler
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}