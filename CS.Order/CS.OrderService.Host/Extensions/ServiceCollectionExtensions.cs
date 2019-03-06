using CS.Processor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS.OrderService.Host.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHostedServices(this IServiceCollection services)
        {
            var hostedServices = new List<IHostedService>();
            var provider = services.BuildServiceProvider();

            var orderGenerator = new Generator(
                new ISubscriber[]
                {
                    new OrderGenerator(
                            provider.GetService<IOrderBasket>()
                        )
                });

            hostedServices.Add(orderGenerator);

            var tradingEngine = new Engine(
                new ISubscriber[]
                {
                    new TradingEngine(
                            provider.GetService<IFileProcessor>(),
                            provider.GetService<IOrderBasket>()
                        )
                });

            hostedServices.Add(tradingEngine);

            services.AddSingleton<IEnumerable<IHostedService>>(p => new List<IHostedService>(hostedServices));
            return services;
        }

        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IFileProcessor, JsonFileProcessor>();
            services.AddSingleton<IOrderBasket, OrderBasket>();

            return services;
        }

    }
}
