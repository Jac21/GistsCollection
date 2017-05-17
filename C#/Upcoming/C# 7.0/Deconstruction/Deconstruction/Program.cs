using static System.Console;

namespace Deconstruction
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
           (int hour, int minutes, int seconds) = GetTime();

            if (minutes > 30)
            {
                hour++;
            }

            WriteLine($"{hour}:{minutes}:{seconds}");
        }

        public (int hour, int minutes, int seconds) GetTime()
        {
            return (1, 35, 40);
        }

        public void RunTwo()
        {
            int hour, minutes, seconds;
            (hour, minutes, seconds) = GetTime();
            WriteLine($"The time using local variables: {hour}:{minutes}:{seconds}");
        }
    }
}
