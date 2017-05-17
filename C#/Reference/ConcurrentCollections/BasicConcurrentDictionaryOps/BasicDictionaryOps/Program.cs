using System;
using System.Collections.Concurrent;

namespace Pluralsight.ConcurrentCollections.BasicDictionaryOps
{
	class Program
	{
		static void Main()
		{
			ConcurrentDictionaryOps();
		}

		private static void ConcurrentDictionaryOps()
		{
            // create new concurrent dictionary of key/value string/int
			var stock = new ConcurrentDictionary<string, int>();

            // perform TryAdd actions with the below pairs, output numerical value
		    stock.TryAdd("jDays", 4);
		    stock.TryAdd("technologyhour", 3);
            Console.WriteLine("No. of shirts in stock = {0}", stock.Count);

            // use success parameter to test if value was actually added to the dictionary
			bool success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine("Added succeeded? " + success);

            // second attempt will result in a 'false'
            success = stock.TryAdd("pluralsight", 6);
            Console.WriteLine("Added succeeded? " + success);

			stock["buddhistgeeks"] = 5;

            //// TryUpdate, takes in key, value, and previous value		
            //success = stock.TryUpdate("pluralsight", 7, 6);
            //Console.WriteLine("pluralsight = {0}, did update work? {1}", stock["pluralsight"], success);

            // AddOrUpdate, repeatedly calls TryUpdate() until it succeeds
            int psStock = stock.AddOrUpdate("pluralsight", 1, (key, oldValue) => oldValue + 1);
            Console.WriteLine("New value is {0}", psStock);

            // Writing 'stock["pluralsight"]' to console can cause a race condition
            // due to accessing the dictionary (values can go out-of-date between method calls!)

            // GetOrAdd, not sure if value was in dictionary, add if it isn't with value paramter
            Console.WriteLine("stock[pluralsights] = {0}", stock.GetOrAdd("pluralssights", 0));

            // TryRemove jDays value from dict, write to console if success with int out parameter
		    int jDaysValue;
			success = stock.TryRemove("jDays", out jDaysValue);
		    if (success)
		    {
		        Console.WriteLine("Value removed was: {0}", jDaysValue);
		    }

			Console.WriteLine("\r\nEnumerating:");
			foreach (var keyValPair in stock)
			{
				Console.WriteLine("{0}: {1}", keyValPair.Key, keyValPair.Value);
			}
		}
	}
}