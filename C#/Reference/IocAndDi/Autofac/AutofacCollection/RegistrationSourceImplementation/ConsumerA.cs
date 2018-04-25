using System;

namespace RegistrationSourceImplementation
{
    public class ConsumerA
    {
        private readonly HandlerA handler;

        public ConsumerA(HandlerA handler)
        {
            this.handler = handler;
        }

        public void DoWork()
        {
            Console.WriteLine(handler.Handle("ConsumerA"));
        }
    }

    public class ConsumerB
    {
        private readonly HandlerB handler;

        public ConsumerB(HandlerB handler)
        {
            this.handler = handler;
        }

        public void DoWork()
        {
            Console.WriteLine(handler.Handle("ConsumerB"));
        }
    }
}