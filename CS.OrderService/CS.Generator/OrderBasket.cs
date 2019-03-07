using CS.Contracts;
using CS.Engine;
using CS.Processor;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Generator
{
    public class OrderBasket : IOrderBasket
    {
        private readonly IPublisher<Order> OrderPublisher;
        private readonly Subscriber<Order> OrderSubscriber;

        TradingEngine tradingEngine;

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public OrderBasket(TradingEngine tradingEngine)
        {
            this.tradingEngine = tradingEngine;

            OrderPublisher = new Publisher<Order>();
            OrderSubscriber = new Subscriber<Order>(OrderPublisher);
            OrderSubscriber.Publisher.DataPublisher += ProcessOrders;
        }

        public void OrderPlaced(Order order)
        {
            logger.Info("Order is  pushed to basket | OrderId : " + order.OrderId);

            OrderPublisher.PublishData(order);
        }

        private void ProcessOrders(object sender, MessageArgument<Order> e)
        {
            logger.Info("Order is  published to subscribers | OrderId : " 
                + e.Message.OrderId);

            tradingEngine.OrderReceivedForProcessing(e.Message);
        }
    }
}
