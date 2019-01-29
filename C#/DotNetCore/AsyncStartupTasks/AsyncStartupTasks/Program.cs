using System.Threading.Tasks;
using AsyncStartupTasks.AsyncStartupTaskUtilities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AsyncStartupTasks
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateWebHostBuilder(args).Build().RunWithTasksAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}