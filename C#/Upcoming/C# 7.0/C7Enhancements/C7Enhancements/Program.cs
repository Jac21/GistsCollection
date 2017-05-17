using System;
using static System.Console;

namespace C7Enhancements
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
            WriteLine(GetBigNumber());

            int[] numbers = {2, 3, 4, 12};
            ref int position = ref Substitute(12, numbers);
            position = -12;
            WriteLine(numbers[4]);

            Employee employee = new Employee("Manager");
            WriteLine(employee.Position);

            Employee nullEmployee = new Employee(null);
            WriteLine(nullEmployee.Position);
        }

        /// <summary>
        /// Syntax update for large numbers
        /// </summary>
        /// <returns></returns>
        public int GetBigNumber()
        {
            return 1_234_567;
        }

        /// <summary>
        /// Reference parameters
        /// </summary>
        public ref int Substitute(int value, int[] numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == value)
                {
                    return ref numbers[i];
                }
            }
            throw new IndexOutOfRangeException();
        }
    }

    /// <summary>
    /// Exceptions in expression body
    /// </summary>
    public class Employee
    {
        public string Position { get; }
        public Employee(string position) => Position = position ?? throw new ArgumentNullException();
    }
}
