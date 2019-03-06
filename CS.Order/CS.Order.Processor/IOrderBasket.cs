using System;
using CS.Contracts;

namespace CS.Processor
{
    public interface IOrderBasket
    {
        event Action<Order> OnOrderReceived;

        void OrderPlaced(Order order);
    }
}