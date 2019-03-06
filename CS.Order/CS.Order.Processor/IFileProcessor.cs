using CS.Contracts;

namespace CS.Processor
{
    public interface IFileProcessor
    {
        void WriteToFile(Order order);
    }
}