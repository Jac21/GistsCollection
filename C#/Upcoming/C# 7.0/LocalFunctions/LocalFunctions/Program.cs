using System;

namespace LocalFunctions
{
    class Program
    {
        static void Main()
        {
            Runner runner = new Runner();
            runner.Run();
        }
    }

    internal class Runner
    {
        public void Run()
        {
            Console.WriteLine(Fibonacci(6));
        }

        public int Fibonacci(int x)
        {
            if (x < 0)
            {
                throw new ArgumentException("Value must be at least 0", nameof(x));
            }

            return Fib(x).current;

            (int current, int previous) Fib(int i)
            {
                if (i == 0)
                {
                    return (1, 0);
                }

                var (current, previous) = Fib(i - 1);
                return (current + previous, current);
            }
        }
    }
}
