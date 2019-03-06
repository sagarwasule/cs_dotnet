using System.Threading;
using System.Threading.Tasks;

namespace CS.Processor
{
    public interface ISubscriber
    {
        Task NotifyAsync(CancellationToken cancellationToken);

    }
}