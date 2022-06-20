using SuperStore.Carts.Messages;
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
            _messageSubscriber
                    .SubscribeMessage<FundsMessage>("carts-service-funds-message", "FundsMessage", "Funds", (msg, args) =>
                    {
                        _logger.LogInformation($"Recived message for customer: {msg.CustomerId} | Funds: {msg.CurrentFunds}");

                        return Task.CompletedTask;
                    });


        }
    }
}
