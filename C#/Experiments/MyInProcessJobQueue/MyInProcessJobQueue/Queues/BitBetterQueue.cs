using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MyInProcessJobQueue.Queues
{
    public class BitBetterQueue
    {
        private readonly ConcurrentQueue<object> jobs = new ConcurrentQueue<object>();

        public BitBetterQueue()
        {
            var thread = new Thread(OnStart)
            {
                IsBackground = true
            };

            thread.Start();
        }

        public void Enqueue(object job)
        {
            jobs.Enqueue(job);
        }

        private void OnStart()
        {
            while (true)
            {
                if (jobs.TryDequeue(out object result))
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}