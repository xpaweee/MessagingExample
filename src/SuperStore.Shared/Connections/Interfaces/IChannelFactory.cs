using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Connections.Interfaces
{
    public interface IChannelFactory
    {
        IModel Create();
    }
}
