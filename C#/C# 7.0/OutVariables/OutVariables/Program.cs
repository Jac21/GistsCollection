using static System.Console;    // static using statement, no need to rewrite "Console" below

namespace OutVariables
{
    class Program
    {
        static void Main()
        {
            Runner runner = new Runner();
            runner.RunTwo();
        }
    }

    public class Runner
    {
        public void Run()
        {
            int hour;
            int minutes;
            int seconds;

            GetTime(out hour, out minutes, out seconds);
            WriteLine($"{hour}:{minutes}:{seconds}");
        }

        /// <summary>
        /// No need to declare out variables in C# 7.0
        /// </summary>
        public void RunTwo()
        {
            GetTime(out int hour, out int minutes, out int seconds);
            WriteLine($"{hour}:{minutes}:{seconds}");
        }

        public void GetTime(out int hour, out int minutes, out int seconds)
        {
            hour = 1;
            minutes = 30;
            seconds = 20;
        }
    }
}
