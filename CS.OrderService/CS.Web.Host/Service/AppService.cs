using CS.Contracts;
using CS.Generator;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Web.Host.Service
{
    public class AppService : IHostedService
    {
        private Timer timer;
        IOrderGenerator orderGenerator;
        IOrderBasket orderBasket;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public AppService(IOrderGenerator orderGenerator,
            IOrderBasket orderBasket)
        {
            this.orderGenerator = orderGenerator;
            this.orderBasket = orderBasket;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(e =>
            {
                GenerateOrder();
            }, null, 0, Timeout.Infinite);

            return Task.FromResult(true);
        }

        private void GenerateOrder()
        {
            Random r = new Random();
            int secondsToGenerateOrder = r.Next(1, 10);

            Order order = orderGenerator.GenerateOrder();
            logger.Info("Order is generated | OrderId : " + order.OrderId);

            orderBasket.OrderPlaced(order);

            timer.Change(secondsToGenerateOrder, Timeout.Infinite);
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            return Task.CompletedTask;
        }
    }
}
