using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WebApplication3.Models;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSettings(@"App_Data\settings.json")
                .UseStartup<Startup>();
    }
}