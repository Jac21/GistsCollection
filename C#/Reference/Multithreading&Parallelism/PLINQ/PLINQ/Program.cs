using PLINQ.Services;
using System;
using System.Linq;

namespace PLINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== AsParallel ==========");

            var finCities = CityService
                .GetCities()
                .AsParallel()
                .AsOrdered()
                .Where(c => string.Equals(c.Country, "Finland", StringComparison.OrdinalIgnoreCase));

            foreach (var city in finCities)
            {
                Console.WriteLine(city.Name);
            }

            Console.ReadLine();
        }
    }
}