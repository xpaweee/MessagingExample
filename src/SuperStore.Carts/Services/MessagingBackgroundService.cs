using SuperStore.Carts.Messages;
using SuperStore.Shared.Connections.Interfaces;
using SuperStore.Shared.Subscribers.Interfaces;

namespace SuperStore.Carts.Services
{
    internal sealed class MessagingBackgroundService : BackgroundService
    {
        private readonly IMessageSubscriber _messageSubscriber;
        private readonly ILogger<MessagingBackgroundService> _logger;

        public MessagingBackgroundService(IMessageSubscriber messageSubscriber, ILogger<MessagingBackgroundService> logger)
        {
            _messageSubscriber = messageSubscriber;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var channel = _channelFactory.Create();

            //channel.ExchangeDeclare("Funds", "topic", false, false, null);

            _messageSubscriber
                    .SubscribeMessage<FundsMessage>("carts-service-eu-many-words-queue", "EU.#", "Funds", (msg, args) =>
                    {
                        _logger.LogInformation($"Recived EU multiple-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds}");

                        return Task.CompletedTask;
                    });

            _messageSubscriber
                 .SubscribeMessage<FundsMessage>("carts-service-eu-one-word-queue", "EU.*", "Funds", (msg, args) =>
                 {
                     _logger.LogInformation($"Recived EU single-word message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds}");

                     return Task.CompletedTask;
                 });


        }
    }
}
