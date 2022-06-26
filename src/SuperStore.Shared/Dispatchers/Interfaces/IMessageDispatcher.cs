using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Shared.Dispatchers.Interfaces
{
    public interface IMessageDispatcher
    {
        Task DispatchAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}
