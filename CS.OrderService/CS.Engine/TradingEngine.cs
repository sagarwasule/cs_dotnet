using CS.Contracts;
using CS.FileProcessor;
using CS.Processor;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Engine
{
    public class TradingEngine
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IFileProcessor fileProcessor;

        public TradingEngine(IFileProcessor fileProcessor)
        {
            this.fileProcessor = fileProcessor;
        }

        public void OrderReceivedForProcessing(Order order)
        {
            logger.Info("Order is received from basket | OrderId : " + order.OrderId);

            order.ProcessedOn = DateTime.UtcNow;

            this.fileProcessor.WriteToFile(order);
        }
    }
}
