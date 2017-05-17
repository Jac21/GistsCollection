using System.Collections.Generic;
using static System.Console;

namespace Tuples
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
            var time = GetTime();
            WriteLine($"Time: {time.Item1}:{time.Item2}:{time.Item3}");

            var timeTwo = GetTimeTwo();
            WriteLine($"Time: {timeTwo.Item1}:{time.Item2}:{time.Item3}");

            var tupleDictionary = new Dictionary<(int, int), string>();

            tupleDictionary.Add((100,20), "Dictionary Value One");
            tupleDictionary.Add((50,10), "Dictionary Value One");

            var result = tupleDictionary[(50, 10)];
            WriteLine(result);
        }

        public (int, int, int) GetTime()
        {
            return (1, 30, 40); // tuple literal
        }

        public (int hour, int minutes, int seconds) GetTimeTwo()
        {
            return (1, 30, 40);
        }
    }
}
