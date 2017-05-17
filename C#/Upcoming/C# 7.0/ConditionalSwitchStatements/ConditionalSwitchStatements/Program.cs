using static System.Console;

namespace ConditionalSwitchStatements
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
            Employee theEmployee = new VicePresident();
            theEmployee.Salary = 175000;
            theEmployee.Years = 7;
            (theEmployee as VicePresident).NumberManaged = 200;
            (theEmployee as VicePresident).StockShares = 6000;

            switch (theEmployee)
            {
                case VicePresident vp when (vp.StockShares < 5000):
                    WriteLine($"Junior VP with {vp.StockShares} shares");
                    break;

                case VicePresident vp when (vp.StockShares >= 5000):
                    WriteLine($"Senior VP with {vp.StockShares} shares");
                    break;

                case Manager m:
                    WriteLine($"Manager with {m.NumberManaged}");
                    break;
                case Employee e:
                    WriteLine($"Employee with salary: {e.Salary}");
                    break;
            }
        }
    }

    public class Employee
    {
        public int Salary { get; set; }
        public int Years { get; set; }
    }

    public class Manager : Employee
    {
        public int NumberManaged { get; set; }
    }

    public class VicePresident : Manager
    {
        public int StockShares { get; set; }
    }
}
