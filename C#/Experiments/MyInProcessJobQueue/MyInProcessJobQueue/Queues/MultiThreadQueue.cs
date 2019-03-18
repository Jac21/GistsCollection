using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyInProcessJobQueue.Queues
{
    public class MultiThreadQueue
    {
        private readonly BlockingCollection<string> jobs = new BlockingCollection<string>();

        public MultiThreadQueue(int numThreads)
        {
            for (int i = 0; i < numThreads; i++)
            {
                var thread = new Thread(OnHandlerStart)
                {
                    IsBackground = true
                };

                thread.Start();
            }
        }

        public void Enqueue(string job)
        {
            if (!jobs.IsAddingCompleted)
                jobs.Add(job);
        }

        public void Stop()
        {
            //This will cause 'jobs.GetConsumingEnumerable' to stop blocking and exit when it's empty
            jobs.CompleteAdding();
        }

        private void OnHandlerStart()
        {
            foreach (var job in jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                Console.WriteLine(job);
                Thread.Sleep(10);
            }
        }
    }
}