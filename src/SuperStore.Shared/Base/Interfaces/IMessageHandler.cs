using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Base.Interfaces
{
    public interface IMessageHandler<in TMessage> where TMessage : class, IMessage
    {
        Task HandleAsync(TMessage message);
    }
}
