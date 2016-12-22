using System;

namespace ValueObjects
{
    class Program
    {
        // can cause an aliasing bug by modifying objects
        // that is shated by reference, need value object
        private static bool IsHappyHour { get; set; }

        static MoneyAmount Reserve(MoneyAmount cost)
        {
            decimal factor = 1;

            // create new object to avoid modifying shared
            // value and introducing bugs from coupled methods
            // (or not)
            MoneyAmount newCost = cost;

            if (IsHappyHour)
            {
                factor = .5M;
            }

            Console.WriteLine("\nReserving an item that costs {0}.", cost);

            return cost.Scale(factor);
        }

        static void Buy(MoneyAmount wallet, MoneyAmount cost)
        {
            // initial check, although not enough
            bool enoughMoney = wallet.Amount >= cost.Amount;

            // create new object with new possible values
            MoneyAmount finalCost = Reserve(cost);

            // final check, branch on status
            bool finalEnough = wallet.Amount >= finalCost.Amount;

            if (enoughMoney)
            {
                Console.WriteLine("You will pay {0} with your {1}", cost, wallet);
            }
            else if(finalEnough)
            {
                Console.WriteLine("~*~*~*~*HAPPY HOUR~*~*~*~* \nThis time, {0} will be enough to pay {1}.", wallet, finalCost);
            }
            else
            {
                Console.WriteLine("You cannot pay {0} with your {1}", cost, wallet);
            }
        }

        static void Main()
        {
            Buy(new MoneyAmount(12, "USD"), 
                new MoneyAmount(10, "USD"));

            Buy(new MoneyAmount(7, "USD"), 
                new MoneyAmount(10, "USD"));

            IsHappyHour = true;
            Buy(new MoneyAmount(7, "USD"), 
                new MoneyAmount(10, "USD"));

            Console.ReadLine();
        }
    }
}
