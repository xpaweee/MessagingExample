using RabbitMQ.Client;
using SuperStore.Shared.Connections.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Connections
{
    internal sealed class ChannelFactory : IChannelFactory
    {
        private readonly IConnection _connection;
        private readonly ChannelAccessor _channelAccessor;

        public ChannelFactory(IConnection connection, ChannelAccessor channelAccessor)
        {
            _connection = connection;
            _channelAccessor = channelAccessor;
        }

        public IModel Create()
            => _channelAccessor.Channel ?? (_channelAccessor.Channel = _connection.CreateModel());
    }
}
