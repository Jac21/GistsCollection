using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace IoOperations.Communication
{
    public class Async
    {
        // class fields
        private string _path;

        /// <summary>
        /// Async file creation
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task CreateAndWriteAsyncToFile(string path)
        {
            this._path = path;

            // write file using file stream asynchronously
            using (FileStream stream = new FileStream(path, FileMode.Create,
                FileAccess.Write, FileShare.None, 4096, true))
            {
                byte[] data = new byte[100000];
                new Random().NextBytes(data);

                await stream.WriteAsync(data, 0, data.Length);
            }

            Console.WriteLine("Async file written!");
        }

        /// <summary>
        /// Async http requests
        /// </summary>
        /// <returns></returns>
        public async Task ReadAsyncHttpRequest()
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("https://news.ycombinator.com/");
            Console.WriteLine("Async HTTP request sent!");
        }

        /// <summary>
        /// Runs HTTP request tasks in parallel, finishes up when all complete
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteMultipleRequestsInParallel()
        {
            HttpClient client = new HttpClient();

            Task microsoft = client.GetStringAsync("http://www.microsoft.com");
            Task msdn = client.GetStringAsync("http://msdn.microsoft.com");
            Task blogs = client.GetStringAsync("http://blogs.msdn.com");

            await Task.WhenAll(microsoft, msdn, blogs);

            Console.WriteLine("Parallel requests completed!");
        }
    }
}
