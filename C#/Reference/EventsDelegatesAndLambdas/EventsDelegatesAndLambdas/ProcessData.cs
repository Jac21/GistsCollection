using System;

namespace EventsDelegatesAndLambdas
{
    public class ProcessData
    {
        // Lambda method
        public void Process(int x, int y, BusinessRulesDelegate del)
        {
            var result = del(x, y);
            Console.WriteLine("Processing: {0}", result);
        }

        // Action<T> method
        public void ProcessAction(int x, int y, Action<int, int> action)
        {
            action(x, y);
            Console.WriteLine("Action has been processed using {0}!", action.Method.Name);
        }
        // Func<T> method
        public void ProcessFunc(int x, int y, Func<int, int, int> del)
        {
            var result = del(x, y);
            Console.WriteLine("Func has been processed as {0}!", result);
        }
    }
}
