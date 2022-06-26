using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Shared.Publishers.Interfaces
{
    public interface IMessagePublisher
    {
        public Task PublishAsync<TMessage>(string exchange, string routingKey, TMessage message, string messageId = default) where TMessage : class, IMessage;
    }
}
