using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyInProcessJobQueue.Queues
{
    public class BlockingCollectionQueue
    {
        private readonly BlockingCollection<object> jobs = new BlockingCollection<object>();

        public BlockingCollectionQueue()
        {
            var thread = new Thread(OnStart)
            {
                IsBackground = true
            };

            thread.Start();
        }

        public void Enqueue(object job)
        {
            jobs.Add(job);
        }

        private void OnStart()
        {
            foreach (var job in jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                Console.WriteLine(job);
            }
        }
    }
}