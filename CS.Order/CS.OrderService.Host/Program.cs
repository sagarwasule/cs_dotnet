using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System.IO;

namespace CS.OrderService.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                //clear default
                config.Sources.Clear();

                config
                    .SetBasePath(Path.Combine(env.ContentRootPath, "config"))
                    .AddJsonFile("appsettings.json", false, true);
            })
            .ConfigureLogging((context, builder) => builder.AddConfiguration(context.Configuration.GetSection("Logging")))
                            .UseNLog()
            .UseStartup<Startup>();
    }
}
