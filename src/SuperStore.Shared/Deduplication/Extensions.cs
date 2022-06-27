using EasyCronJob.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperStore.Shared.Base.Interfaces;
using SuperStore.Shared.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Deduplication
{
    public static class DeduplicationExtensions
    {
        public static IMessagingConfiguration AddDeduplication<TContext>(this IMessagingConfiguration msgConfiguration, IConfiguration configuration, string sectionName = "Deduplication") where TContext: DbContext
        {

            var options = configuration.GetOptions<DeduplicationOptions>(sectionName);

            msgConfiguration.Services.AddSingleton(options);
            msgConfiguration.Services.AddScoped<Func<DbContext>>(sp => sp.GetRequiredService<TContext>);
            msgConfiguration.Services.TryDecorate(typeof(IMessageHandler<>), typeof(DeduplicationMessageHandlerDecorator<>));
            

            msgConfiguration.Services.ApplyResulation<DeduplicationCronJob>(o =>
            {
                o.CronExpression = options.Interval;
                o.TimeZoneInfo = TimeZoneInfo.Local;
                o.CronFormat = Cronos.CronFormat.Standard;
            });

            return msgConfiguration;
        }
    }
}
