using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTipsAndTricks
{
    class Program
    {
        public static void WriteToConsoleSync(string text)
        {
            Console.WriteLine(text);
        }

        public static async Task WriteToConsoleAsync(string text)
        {
            await Task.Delay(2000);

            Console.WriteLine(text);
        }

        /// <summary>
        /// Cancelling async work
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DoSomethingAsyncAndCancelIt(CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
        }

        /// <summary>
        /// Cancelling tasks that can't be cancelled
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> DoSomethingAsyncThatCannotBeCancelledAndCancelIt(CancellationToken cancellationToken)
        {
            HttpClient httpClient = new HttpClient();

            // using the CancellationToken to call Dispose on the  HttpResponseMessage instance
            try
            {
                using (var response = await httpClient.GetAsync(new Uri("https://www.google.com"), cancellationToken))
                using (cancellationToken.Register(response.Dispose))
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (ObjectDisposedException)
            {
                if (cancellationToken.IsCancellationRequested)
                    throw new OperationCanceledException();

                throw;
            }
        }

        /// <summary>
        /// Making a sync operation Task-compatible
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task DoSomethingSyncTaskCompatible(CancellationToken cancellationToken)
        {
            // this ensures that the operation hasn't been canceled. 
            // If it has, a canceled task is returned.
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled(cancellationToken);
            }

            try
            {
                WriteToConsoleSync("Hello, World");
                return Task.FromResult(0);
            }
            catch (Exception e)
            {
                // return a faulted task
                return Task.FromException(e);
            }
        }

        /// <summary>
        /// Handle exceptions in async methods
        /// </summary>
        /// <returns></returns>
        public Task FooAsync()
        {
            try
            {
                // Code that throws exception
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }

            return Task.Delay(2000);
        }

        /// <summary>
        /// Async in console applications
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task MainAsync(string[] args)
        {
            // Run sync method as "async"
            // Method is still blocking, but runs on a background thread to prevent blocking of
            // UI thread with desktop/mobile applications (useless in web application context).
            Task.Run(() => WriteToConsoleSync("Hello, Sync World!"));

            // Run async method as sync
            AsyncHelper.RunSync(() => WriteToConsoleAsync("Hello, Async World!"));

            // Discarding the context
            // You should pretty much always do this, unless there's a specific reason to keep the context. 
            // Certain scenarios for that include when you need access to a particular GUI component or 
            // you need to return a response from a controller action.
            await WriteToConsoleAsync("Hello, Async World without Context!").ConfigureAwait(false);

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
    }
}