using Bars.Data;
using CoreWCF.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Bars.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataComponentsRegistrator.Register();

            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseNetTcp(8088)
            .UseStartup<Startup>();
    }
}
