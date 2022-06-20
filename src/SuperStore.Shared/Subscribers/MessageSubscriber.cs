using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SuperStore.Shared.Connections.Interfaces;
using SuperStore.Shared.Subscribers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperStore.Shared.Subscribers
{
    internal sealed class MessageSubscriber : IMessageSubscriber
    {
        private readonly IModel _channel;

        public MessageSubscriber(IChannelFactory channelFactory)
        {
            _channel = channelFactory.Create();
        }
        IMessageSubscriber IMessageSubscriber.SubscribeMessage<TMessage>(string queue, string routingKey, string exchange, Func<TMessage, BasicDeliverEventArgs, Task> handle)
        {
            _channel.QueueDeclare(queue, false, false, false);
            _channel.QueueBind(queue, exchange, routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(body));

                await handle(message, ea);
            };

            _channel.BasicConsume(queue, true, consumer);


            return this;
        }
    }
}
