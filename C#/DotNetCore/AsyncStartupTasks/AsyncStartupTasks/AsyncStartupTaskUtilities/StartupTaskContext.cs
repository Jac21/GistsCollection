using System.Threading;
using Microsoft.EntityFrameworkCore.Internal;

namespace AsyncStartupTasks.AsyncStartupTaskUtilities
{
    public class StartupTaskContext
    {
        private int outstandingTaskCount;

        public void RegisterTask()
        {
            Interlocked.Increment(ref outstandingTaskCount);
        }

        public void MarkTaskAsComplete()
        {
            Interlocked.Decrement(ref outstandingTaskCount);
        }

        public bool IsComplete => outstandingTaskCount == 0;
    }
}