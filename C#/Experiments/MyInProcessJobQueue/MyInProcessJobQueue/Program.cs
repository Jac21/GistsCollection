using System;
using MyInProcessJobQueue.Queues;

namespace MyInProcessJobQueue
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, Queues!\n");

            // quick test
            BlockingCollectionQueue blockingCollectionQueue = new BlockingCollectionQueue();

            blockingCollectionQueue.Enqueue("job one");
            blockingCollectionQueue.Enqueue("job two");
            blockingCollectionQueue.Enqueue("job three");

            Console.ReadLine();
        }
    }
}