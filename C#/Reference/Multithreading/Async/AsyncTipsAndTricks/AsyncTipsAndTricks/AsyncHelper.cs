using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTipsAndTricks
{
    public static class AsyncHelper
    {
        private static readonly TaskFactory taskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        /// <summary>
        /// Run async method as sync
        /// </summary>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func) =>
            taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        /// <summary>
        /// Run async method as sync
        /// </summary>
        public static void RunSync(Func<Task> func) =>
            taskFactory
                .StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
    }
}