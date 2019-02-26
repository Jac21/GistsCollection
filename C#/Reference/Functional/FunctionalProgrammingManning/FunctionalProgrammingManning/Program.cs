using static System.Console;

namespace FunctionalProgrammingManning
{
    class Program
    {
        static void Main()
        {
            // Testing
            WriteLine(ChapterOne.SwapDivide(2, 4));

            foreach (var number in ChapterOne.GetMod(1, 20, 3))
            {
                WriteLine(number);
            }

            ReadLine();
        }
    }
}