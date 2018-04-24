using System;
using BasicAutofac.Interfaces;

namespace BasicAutofac.Implementations
{
    // This implementation of the IOutput interface
    // is actually how we write to the Console. Technically
    // we could also implement IOutput to write to Debug
    // or Trace... or anywhere else.
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
}