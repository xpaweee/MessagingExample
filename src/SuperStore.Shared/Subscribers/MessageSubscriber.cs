using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SuperStore.Shared.Accessors.Interfaces;
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
        private readonly IMessageIdAccessor _messageIdAccessor;

        public MessageSubscriber(IChannelFactory channelFactory, IMessageIdAccessor messageIdAccessor)
        {
            _channel = channelFactory.Create();
            _messageIdAccessor = messageIdAccessor;
        }
        IMessageSubscriber IMessageSubscriber.SubscribeMessage<TMessage>(string queue, string routingKey, string exchange, Func<TMessage, BasicDeliverEventArgs, Task> handle)
        {
            _channel.ExchangeDeclare(exchange, "topic", durable: false, autoDelete: false, null);
            _channel.QueueDeclare(queue, false, false, false);
            _channel.QueueBind(queue, exchange, routingKey);

            //Prefetch size
            //_channel.BasicQos(0, 4, false);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<TMessage>(Encoding.UTF8.GetString(body));

                _messageIdAccessor.SetMessageId(ea.BasicProperties.MessageId);

                await handle(message, ea);

                _channel.BasicAck(ea.DeliveryTag, multiple: false);
                //_channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                //_channel.BasicReject(ea.DeliveryTag, requeue: true);
            };

            _channel.BasicConsume(queue, autoAck: false, consumer);


            return this;
        }
    }
}
