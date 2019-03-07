using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Processor
{
    public interface IPublisher<T>
    {
        event EventHandler<MessageArgument<T>> DataPublisher;
        void OnDataPublisher(MessageArgument<T> args);
        void PublishData(T data);
    }
}
