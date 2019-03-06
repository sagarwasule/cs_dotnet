using CS.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Processor
{
    public class TradingEngine : ISubscriber
    {
        private readonly IFileProcessor fileProcessor;
        private readonly IOrderBasket orderBasket;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public TradingEngine(IFileProcessor fileProcessor,
            IOrderBasket orderBasket)
        {
            this.fileProcessor = fileProcessor;
            this.orderBasket = orderBasket;

            this.orderBasket.OnOrderReceived += OrderReceivedForProcessing;

        }

        public Task NotifyAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
        
        private void OrderReceivedForProcessing(Order order)
        {
            logger.Info("Order is received from basket");

            order.ProcessedOn = DateTime.UtcNow;

            this.fileProcessor.WriteToFile(order);
        }
    }
}
