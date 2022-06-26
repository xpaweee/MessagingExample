using SuperStore.Shared.Accessors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Accessors
{
    internal sealed class MessageIdAccessor : IMessageIdAccessor
    {
        private readonly AsyncLocal<string> _value = new();


        public string GetMessageId()
        {
            return _value.Value;
        }

        public void SetMessageId(string messageId)
        {
            _value.Value = messageId;
        }
    }
}
