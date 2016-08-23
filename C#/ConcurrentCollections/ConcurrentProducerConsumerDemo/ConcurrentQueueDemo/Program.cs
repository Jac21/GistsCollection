using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ConcurrentQueueDemo
{
	class Program
	{
		static void Main()
		{
			// these methods show the code at each stage of the module
			// uncomment to show each stage working

			// original code: Queue
			DemoQueue();

			// concurrent queue
			DemoConcurrentQueue();

			// concurrent Stack
			DemoConcurrentStack();

			// concurrent bag
			DemoConcurrentBag();

			// interface
			DemoInterface();

		}

        /// <summary>
        /// Basic queue method
        /// </summary>
		static void DemoQueue()
		{
            Console.WriteLine("Demo Queue ----------------------");
            // Initialize new queue, add three items to queue
			var shirts = new Queue<string>();
			shirts.Enqueue("Pluralsight");
			shirts.Enqueue("WordPress");
			shirts.Enqueue("Code School");

            // 3
			Console.WriteLine("After enqueuing, count = " + shirts.Count);

            // Remove Pluralsight
			string item1 = shirts.Dequeue();
			Console.WriteLine("\r\nRemoving " + item1);

            // Peek WordPress (no removing)
			string item2 = shirts.Peek();
			Console.WriteLine("Peeking   " + item2);

            // enumerate over queue, write WordPress and Code School to console
			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

            // 2
			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count);

		}


		private static void DemoConcurrentQueue()
		{
            Console.WriteLine("Demo Concurrent Queue ----------------------");
            // Initialize new concurrent queue, add three items to queue
			var shirts = new ConcurrentQueue<string>();
			shirts.Enqueue("Pluralsight");
			shirts.Enqueue("WordPress");
			shirts.Enqueue("Code School");

            // 3
			Console.WriteLine("After enqueuing, count = " + shirts.Count);

            // based on whether queue is empty or not
			string item1; //= shirts.Dequeue();
			bool success = shirts.TryDequeue(out item1);
			if (success)
				Console.WriteLine("\r\nRemoving " + item1);
			else
				Console.WriteLine("queue was empty");

			string item2; //= shirts.Peek();
			success = shirts.TryPeek(out item2);
			if (success)
				Console.WriteLine("Peeking   " + item2);
			else
				Console.WriteLine("queue was empty");

			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count);
		}


		private static void DemoConcurrentStack()
		{
            Console.WriteLine("Demo Concurrent Stack ----------------------");
			var shirts = new ConcurrentStack<string>();
			shirts.Push("Pluralsight");
			shirts.Push("WordPress");
			shirts.Push("Code School");

			Console.WriteLine("After enqueuing, count = " + shirts.Count);

			string item1; //= shirts.Dequeue();
			bool success = shirts.TryPop(out item1);
			if (success)
				Console.WriteLine("\r\nRemoving " + item1);
			else
				Console.WriteLine("queue was empty");

			string item2; //= shirts.Peek();
			success = shirts.TryPeek(out item2);
			if (success)
				Console.WriteLine("Peeking   " + item2);
			else
				Console.WriteLine("queue was empty");

			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count);
		}

		private static void DemoConcurrentBag()
		{
            Console.WriteLine("Demo Concurrent Bag ----------------------");
			var shirts = new ConcurrentBag<string>();
			shirts.Add("Pluralsight");
			shirts.Add("WordPress");
			shirts.Add("Code School");

			Console.WriteLine("After enqueuing, count = " + shirts.Count);

			string item1; //= shirts.Dequeue();
			bool success = shirts.TryTake(out item1);
			if (success)
				Console.WriteLine("\r\nRemoving " + item1);
			else
				Console.WriteLine("queue was empty");

			string item2; //= shirts.Peek();
			success = shirts.TryPeek(out item2);
			if (success)
				Console.WriteLine("Peeking   " + item2);
			else
				Console.WriteLine("queue was empty");

			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count);
		}

		private static void DemoInterface()
		{
            Console.WriteLine("Demo Concurrent Interface ----------------------");
			// can change this to concurrent stack or bag
			IProducerConsumerCollection<string> shirts = new ConcurrentQueue<string>();
			shirts.TryAdd("Pluralsight");
			shirts.TryAdd("WordPress");
			shirts.TryAdd("Code School");

			Console.WriteLine("After enqueuing, count = " + shirts.Count);

			string item1; // = shirts.Dequeue();
			bool success = shirts.TryTake(out item1);
			if (success)
				Console.WriteLine("\r\nRemoving " + item1);
			else
				Console.WriteLine("queue was empty");

			Console.WriteLine("\r\nEnumerating:");
			foreach (string item in shirts)
				Console.WriteLine(item);

			Console.WriteLine("\r\nAfter enumerating, count = " + shirts.Count);
		}

	}

}
