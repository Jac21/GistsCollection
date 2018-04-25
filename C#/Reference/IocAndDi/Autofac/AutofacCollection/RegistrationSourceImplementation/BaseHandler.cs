namespace RegistrationSourceImplementation
{
    public abstract class BaseHandler
    {
        public virtual string Handle(string message)
        {
            return "Handled: " + message;
        }
    }

    public class HandlerA : BaseHandler
    {
        public override string Handle(string message)
        {
            return "[A] " + base.Handle(message);
        }
    }

    public class HandlerB : BaseHandler
    {
        public override string Handle(string message)
        {
            return "[B] " + base.Handle(message);
        }
    }
}