using RabbitMQ.Client;
using SuperStore.Shared.Connections.Interfaces;
using SuperStore.Shared.Publishers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SuperStore.Shared.Publishers
{
    internal sealed class MessagePublisher : IMessagePublisher
    {
        private readonly IModel _channel;

        public MessagePublisher(IChannelFactory channelFactory)
        {
            _channel = channelFactory.Create();
        }
        Task IMessagePublisher.PublishAsync<TMessage>(string exchange, string routingKey, TMessage message, string messageId = default)
        {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var properties = _channel.CreateBasicProperties();
            properties.MessageId = messageId ?? Guid.NewGuid().ToString("N");
            //properties.Headers

            //_channel.ConfirmSelect();
            //_channel.BasicAcks += (sender, args) =>
            //{
            //    Console.WriteLine("ACK");
            //};

            //_channel.BasicReturn += (sender, args) =>
            //{
            //    Console.WriteLine("ACK");
            //};

            _channel.ExchangeDeclare(exchange, "topic", false, false);

            _channel.BasicPublish(exchange, routingKey, properties, body);

            return Task.CompletedTask;
        }
    }
}
