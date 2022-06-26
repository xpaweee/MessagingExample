using SuperStore.Shared.Dispatchers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Shared.Dispatchers
{
    internal class MessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var scope = _serviceProvider.CreateScope();

            var handler = scope.ServiceProvider.GetService<IMessageHandler<TMessage>>();

            await handler.HandleAsync(message);
        }
    }
}
