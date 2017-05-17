using System;
using System.Linq;
using System.Collections.Generic;
using EventsDelegatesAndLambdas.EventHandlers;
using EventsDelegatesAndLambdas.Models;

namespace EventsDelegatesAndLambdas
{
    public enum WorkType
    {
        GoToMeetings,
        Code,
        CreateReports
    }

    public delegate int BusinessRulesDelegate(int x, int y);

    class Program
    {
        static void Main()
        {
            //// Using delegates without event handling
            //WorkPerformedHandler delOne = WorkPerformedOne;
            //WorkPerformedHandler delTwo = WorkPerformedTwo;
            //WorkPerformedHandler delThree = WorkPerformedThree;

            //// Make an invocation list for a multicast delegate
            //delOne += delTwo + delThree;
            //int finalHours = delOne(10, WorkType.Code);

            //Console.WriteLine(finalHours); // 13, which is basically the last invoked delegate

            // init delegates
            BusinessRulesDelegate addDelegate = (x, y) => x + y;
            BusinessRulesDelegate multiplyDelegate = (x, y) => x*y; 

            // init Func<T>
            Func<int, int, int> funcAddDel = (x, y) => x + y;
            Func<int, int, int> funcMultiplyDel = (x, y) => x * y;

            // init ProcessData class, perform lambda operations using delegates defined above
            var data = new ProcessData();
            data.Process(2, 3, addDelegate);
            data.Process(3, 4, multiplyDelegate);

            // Actions using ProcessData class
            Action<int, int> myAction = (x, y) =>
            {
                Console.WriteLine(x + y);
            };
            Action<int, int> myMultiplyAction = (x, y) =>
            {
                Console.WriteLine(x * y);
            };

            data.ProcessAction(2, 3, myAction);
            data.ProcessAction(2, 3, myMultiplyAction);

            // Process funcs
            data.ProcessFunc(2, 2, funcAddDel);
            data.ProcessFunc(2, 3, funcMultiplyDel);

            var worker = new Worker();

            // using a lambda
            worker.WorkPerformed += (s, e) =>
            {
                Console.WriteLine("Hours: {0}, WorkType: {1}", e.Hours, e.WorkType);
                Console.WriteLine("---------------------------------------------");
            };

            worker.WorkCompleted += Worker_WorkCompleted;

            worker.DoWork(8, WorkType.CreateReports);

            /*
             *  LINQ and Lambdas
             */

            var custs = new List<Customer>
            {
                new Customer()
                {
                    City = "Austin",
                    FirstName = "Jeremy",
                    LastName = "Cantu",
                    Id = 0000
                },
                new Customer()
                {
                    City = "Seattle",
                    FirstName = "Jeremy",
                    LastName = "Johnson",
                    Id = 0001
                },
                new Customer()
                {
                    City = "San Jose",
                    FirstName = "Jeremy",
                    LastName = "Yang",
                    Id = 0010
                },
                new Customer()
                {
                    City = "New York",
                    FirstName = "Jeremy",
                    LastName = "Morello",
                    Id = 0100
                }
            };

            var austinCustomers = custs.Where(c => c.City == "Austin");

            foreach (var cust in austinCustomers)
            {
                Console.WriteLine("{0} {1}", cust.FirstName, cust.LastName);
            }
        }


        static void Worker_WorkCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Work completed!");
        }

        //private static int WorkPerformedOne(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkPerformedOne called with {0} hours and work type \'{1}\'!", hours, workType);
        //    return hours + 1;
        //}

        //private static int WorkPerformedTwo(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkPerformedTwo called with {0} hours and work type \'{1}\'!", hours, workType);
        //    return hours + 2;
        //}

        //private static int WorkPerformedThree(int hours, WorkType workType)
        //{
        //    Console.WriteLine("WorkPerformedTwo called with {0} hours and work type \'{1}\'!", hours, workType);
        //    return hours + 3;
        //}
    }
}
