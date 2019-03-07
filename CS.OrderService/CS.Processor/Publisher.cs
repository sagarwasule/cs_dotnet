using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Processor
{
    public class Publisher<T> : IPublisher<T>
    {
        //Defined datapublisher event  
        public event EventHandler<MessageArgument<T>> DataPublisher;

        public void OnDataPublisher(MessageArgument<T> args)
        {
            var handler = DataPublisher;
            if (handler != null)
                handler(this, args);
        }


        public void PublishData(T data)
        {
            MessageArgument<T> message = (MessageArgument<T>)Activator.CreateInstance(typeof(MessageArgument<T>), new object[] { data });
            OnDataPublisher(message);
        }
    }
}
