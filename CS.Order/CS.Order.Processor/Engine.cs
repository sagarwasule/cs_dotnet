using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Processor
{
    public class Engine : IHostedService
    {
        private readonly IEnumerable<ISubscriber> subscribers;
        private Task engineTask;

        public Engine(IEnumerable<ISubscriber> subscribers)
        {
            this.subscribers = subscribers ??
                throw new ArgumentNullException(nameof(subscribers));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            engineTask = Task.Run(() =>
                StartEngine(cancellationToken), cancellationToken);
            return Task.CompletedTask;
        }

        private void StartEngine(CancellationToken cancellationToken)
        {
            foreach (var subscriber in this.subscribers)
            {
                subscriber.NotifyAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            engineTask?.Wait();
            return Task.CompletedTask;
        }
    }
}
