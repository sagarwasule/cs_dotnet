using CS.Contracts;

namespace CS.FileProcessor
{
    public interface IFileProcessor
    {
        void WriteToFile(Order order);

    }
}