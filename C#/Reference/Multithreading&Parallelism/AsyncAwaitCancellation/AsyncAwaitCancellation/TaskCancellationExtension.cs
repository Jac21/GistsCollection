using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitCancellation
{
    public static class TaskCancellationExtension
    {
        /// <summary>
        /// Add cancellation functionality to Task<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> CancelAfter<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(
                s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return await task;
        }

        /// <summary>
        /// Add cancellation functionality to Tasks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task CancelAfter(this Task task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(
                s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            await task;
        }

        /// <summary>
        /// Add cancellation functionality to Task<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<T> CancelAfter<T>(this Task<T> task, int milliseconds)
        {
            var cts = new CancellationTokenSource();

            cts.CancelAfter(milliseconds);

            var tcs = new TaskCompletionSource<bool>();

            using (cts.Token.Register(
                s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cts.Token);
                }
            }

            return await task;
        }

        /// <summary>
        /// Add cancellation functionality to Tasks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task CancelAfter(this Task task, int milliseconds)
        {
            var cts = new CancellationTokenSource();

            cts.CancelAfter(milliseconds);

            var tcs = new TaskCompletionSource<bool>();

            using (cts.Token.Register(
                s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cts.Token);
                }
            }

            await task;
        }
    }
}