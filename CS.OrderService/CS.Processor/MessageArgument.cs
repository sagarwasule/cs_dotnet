using System;

namespace CS.Processor
{
    public class MessageArgument<T> : EventArgs
    {
        public T Message { get; set; }
        public MessageArgument(T message)
        {
            Message = message;
        }
    }
}
