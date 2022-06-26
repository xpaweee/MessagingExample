using SuperStore.Carts.Messages;
using SuperStore.Shared.Connections.Interfaces;
using SuperStore.Shared.Dispatchers.Interfaces;
using SuperStore.Shared.Subscribers.Interfaces;

namespace SuperStore.Carts.Services
{
    internal sealed class MessagingBackgroundService : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger;
        private readonly IMessageDispatcher _messageDispatcher;

        public MessagingBackgroundService(IMessageSubscriber messageSubscriber, ILogger<MessagingBackgroundService> logger, IMessageDispatcher messageDispatcher)
        {
            _messageSubscriber = messageSubscriber;
            _logger = logger;
            _messageDispatcher = messageDispatcher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var channel = _channelFactory.Create();

            //channel.ExchangeDeclare("Funds", "topic", false, false, null);

            _messageSubscriber
                    .SubscribeMessage<FundsMessage>("carts-service-eu-many-words-queue", "EU.#", "Funds", async (msg, args) =>
                    {
                        _logger.LogInformation($"Recived EU multiple-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds}");

                        await _messageDispatcher.DispatchAsync(msg);
                    });

            _messageSubscriber
                 .SubscribeMessage<FundsMessage>("carts-service-eu-one-word-queue", "EU.*", "Funds", async (msg, args) =>
                 {
                     _logger.LogInformation($"Recived EU single-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds}");

                     await _messageDispatcher.DispatchAsync(msg);

                 });


        }
    }
}
