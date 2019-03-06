using CS.Common;
using CS.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Processor
{
    public class OrderGenerator : ISubscriber
    {
        private readonly IOrderBasket orderBasket;
        private Timer timer;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();


        public OrderGenerator(IOrderBasket orderBasket)
        {
            this.orderBasket = orderBasket;
        }

        public Task NotifyAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(e => {
                GenerateOrder();
            }, null, 0, Timeout.Infinite);

            return Task.FromResult(true);
        }

        private void GenerateOrder()
        {
            Random r = new Random();
            int secondsToGenerateOrder = r.Next(1, 10);

            Order ord = CreateOrder();
            logger.Info("Order is generated | OrderId : " + ord.OrderId);
            this.orderBasket.OrderPlaced(ord);
            timer.Change(secondsToGenerateOrder, Timeout.Infinite);
        }

        private Order CreateOrder()
        {
            return new Order()
            {
                OrderId = GenerateId(),
                ClientName = GenerateClientName(),
                Quantity = GenerateQuantity(),
                Price = GeneratePrice(),
                CreatedOn = DateTime.UtcNow,
                ProcessedOn = null
            };
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateClientName()
        {
            Random r = new Random();
            int randomValue = r.Next(1000, 5000);
            return "client" + randomValue.ToString();
        }

        private int GenerateQuantity()
        {
            return (new Random()).Next(50, 100);
        }

        private decimal GeneratePrice()
        {
            return Convert.ToDecimal(new Random().NextDouble(100.0, 500.0));
        }

    }
}
