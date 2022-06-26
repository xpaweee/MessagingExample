using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.Shared.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Deduplication
{
    public static class Extensions
    {
        public static IServiceCollection AddDeduplication<TContext>(this IServiceCollection services) where TContext: DbContext
        {

            services.AddScoped<Func<DbContext>>(sp => sp.GetRequiredService<TContext>);
            services.TryDecorate(typeof(IMessageHandler<>), typeof(DeduplicationMessageHandlerDecorator<>));

            return services;
        }
    }
}
