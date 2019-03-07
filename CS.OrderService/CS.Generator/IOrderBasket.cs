using CS.Contracts;
using CS.Processor;

namespace CS.Generator
{
    public interface IOrderBasket
    {
        void OrderPlaced(Order order);
    }
}