using RabbitMQ.Client;

namespace SuperStore.Shared.Connections
{
    /// <summary>
    /// Klasa pomocnicza umożliwająca utworzenie Connection od RabbitMQ per Thread
    /// </summary>
    internal sealed class ChannelAccessor
    {
        private static readonly ThreadLocal<ChannelHolder> Holder = new ();

        public IModel? Channel
        {
            get
            {
                return Holder.Value?.Context;
            }
            set
            {
                var holder = Holder.Value;
                if(holder != null)
                {
                    holder.Context = null;
                }

                if(value != null)
                {
                    Holder.Value = new ChannelHolder { Context = value };
                }
            }
        }

        private class ChannelHolder
        {
            public IModel? Context;
        }
    }
}
