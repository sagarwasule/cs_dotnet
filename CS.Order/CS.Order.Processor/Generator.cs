using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS.Processor
{
    public class Generator : IHostedService
    {
        private Task generatorTask;

        private readonly IEnumerable<ISubscriber> subscribers;

        public Generator(IEnumerable<ISubscriber> subscribers)
        {
            this.subscribers = subscribers ?? 
                throw new ArgumentNullException(nameof(subscribers));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            generatorTask = Task.Run(() => 
                StartGenerators(cancellationToken), cancellationToken);
            return Task.CompletedTask;
        }

        private void StartGenerators(CancellationToken cancellationToken)
        {
            foreach (var subscriber in this.subscribers)
            {
                subscriber.NotifyAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            generatorTask?.Wait();
            return Task.CompletedTask;

        }
    }
}
