﻿using CS.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Processor
{
    public class OrderBasket : IOrderBasket
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public event Action<Order> OnOrderReceived;

        public void OrderPlaced(Order order)
        {
            logger.Info("Order is pushed to basket | OrderId : " + order.OrderId);

            logger.Info("Order is published to subscribers | OrderId : " + order.OrderId);
            //Invoke Action
            OnOrderReceived?.Invoke(order);
        }

    }
}
