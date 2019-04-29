using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitCancellation
{
    public class Program
    {
        private static async Task Task_NetworkBound()
        {
            await new HttpClient().GetStringAsync("https://www.jeremycantu.com");
        }

        private static async Task<string> Task_NetworkBound_T()
        {
            return await new HttpClient().GetStringAsync("https://www.github.com/Jac21");
        }

        /// <summary>
        /// Usage
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            Console.WriteLine(await Task_NetworkBound_T().CancelAfter(3000));

            var cts = new CancellationTokenSource();
            cts.CancelAfter(3000);

            Console.WriteLine(await Task_NetworkBound_T().CancelAfter(cts.Token));

            Console.ReadLine();
        }
    }
}