using static System.Console;

namespace Patterns
{
    class Program
    {
        static void Main()
        {
            Runner runner = new Runner();
            runner.Run();
        }
    }

    public class Runner
    {
        public void Run()
        {
            PrintSum(10);
            PrintSumTwo("10");
        }

        public void PrintSum(object o)
        {
            if (o is null) return; // constant pattern

            if(!(o is int i)) return; // type pattern (int)

            int sum = 0;
            for (int j = 0; j <= i; j++)
            {
                sum += j;
            }

            WriteLine($"The sum of 1 to {i} is {sum}");
        }

        public void PrintSumTwo(object o)
        {
            if (o is int i || o is string s && int.TryParse(s, out i))
            {
                int sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    sum += j;
                }

                WriteLine($"The sum of 1 to {i} is {sum}");
            }
        }
    }
}
