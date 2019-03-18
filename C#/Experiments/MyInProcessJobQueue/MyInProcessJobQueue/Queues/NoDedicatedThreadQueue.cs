using System;
using System.Collections.Generic;
using System.Threading;

namespace MyInProcessJobQueue.Queues
{
    public class NoDedicatedThreadQueue
    {
        private readonly Queue<string> jobs = new Queue<string>();
        private bool delegateQueuedOrRunning;

        public void Enqueue(string job)
        {
            lock (jobs)
            {
                jobs.Enqueue(job);
                if (!delegateQueuedOrRunning)
                {
                    delegateQueuedOrRunning = true;
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessQueuedItems, null);
                }
            }
        }

        private void ProcessQueuedItems(object ignored)
        {
            while (true)
            {
                string item;
                lock (jobs)
                {
                    if (jobs.Count == 0)
                    {
                        delegateQueuedOrRunning = false;
                        break;
                    }

                    item = jobs.Dequeue();
                }

                try
                {
                    // do job
                    Console.WriteLine(item);
                }
                catch (Exception)
                {
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessQueuedItems, null);
                    throw;
                }
            }
        }
    }
}