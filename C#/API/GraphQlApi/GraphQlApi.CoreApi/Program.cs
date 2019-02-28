using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace GraphQlApi.CoreApi
{
    public class Program
    {
        private static string directory = Directory.GetCurrentDirectory();

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(directory)
                .UseWebRoot(Path.Combine(directory, "public"))
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
    }
}