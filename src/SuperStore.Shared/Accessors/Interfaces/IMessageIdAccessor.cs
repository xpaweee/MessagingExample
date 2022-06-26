using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Accessors.Interfaces
{
    public interface IMessageIdAccessor
    {
        string GetMessageId();
        void SetMessageId(string messageId);
    }
}
