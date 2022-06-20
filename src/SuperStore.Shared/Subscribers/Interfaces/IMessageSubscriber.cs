using RabbitMQ.Client.Events;
using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Shared.Subscribers.Interfaces
{
    public interface IMessageSubscriber
    {
        IMessageSubscriber SubscribeMessage<TMessage>(string queue, string routingKey, string exchange, Func<TMessage, BasicDeliverEventArgs, Task> handle) where TMessage : class, IMessage;
    }
}
