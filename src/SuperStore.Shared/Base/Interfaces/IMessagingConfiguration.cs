using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Base.Interfaces
{
    public interface IMessagingConfiguration
    {
        IServiceCollection Services { get; }
    }
    public sealed record MessagingConfiguration(IServiceCollection Services) : IMessagingConfiguration;
}
