using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using SuperStore.Shared.Accessors;
using SuperStore.Shared.Accessors.Interfaces;
using SuperStore.Shared.Base.Interfaces;
using SuperStore.Shared.Connections;
using SuperStore.Shared.Connections.Interfaces;
using SuperStore.Shared.Dispatchers;
using SuperStore.Shared.Dispatchers.Interfaces;
using SuperStore.Shared.Publishers;
using SuperStore.Shared.Publishers.Interfaces;
using SuperStore.Shared.Subscribers;
using SuperStore.Shared.Subscribers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared
{
    public static class Extensions
    {
        public static IServiceCollection AddMessagging(this IServiceCollection services, Action<IMessagingConfiguration> configuration = null)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",

            };
            var connection = factory.CreateConnection();

            services.AddSingleton(connection);
            services.AddSingleton<ChannelAccessor>();
            services.AddSingleton<IChannelFactory, ChannelFactory>();
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageSubscriber, MessageSubscriber>();
            services.AddSingleton<IMessageDispatcher, MessageDispatcher>();
            services.AddSingleton<IMessageIdAccessor, MessageIdAccessor>();
            services.Scan(cfg => cfg.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(IMessageHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            configuration?.Invoke(new MessagingConfiguration(services));

            return services;
        }
    }
}
