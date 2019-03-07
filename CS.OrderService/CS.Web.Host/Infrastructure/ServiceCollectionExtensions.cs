using CS.Engine;
using CS.FileProcessor;
using CS.Generator;
using CS.Web.Host.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.Web.Host.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
        {
            var hostedServices = new List<IHostedService>();
            var provider = services.BuildServiceProvider();

            var appService = new AppService(
                            provider.GetService<IOrderGenerator>(),
                            provider.GetService<IOrderBasket>()
                );

            hostedServices.Add(appService);

            services.AddSingleton<IEnumerable<IHostedService>>(p =>
                new List<IHostedService>(hostedServices));

            return services;
        }

        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IFileProcessor, JsonFileProcessor>();

            services.AddSingleton<IOrderBasket, OrderBasket>();
            services.AddSingleton<IOrderGenerator, OrderGenerator>();

            services.AddSingleton<TradingEngine>();

            return services;
        }
    }
}
