using Cronos;
using EasyCronJob.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Deduplication
{
    public sealed class DeduplicationCronJob : CronJobService
    {
        private readonly ILogger<DeduplicationCronJob> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly DeduplicationOptions _deduplicationOptions;
        public DeduplicationCronJob(ICronConfiguration<DeduplicationCronJob> cronConfiguration, ILogger<DeduplicationCronJob> logger, IServiceProvider serviceProvider, DeduplicationOptions options) : base(cronConfiguration.CronExpression, cronConfiguration.TimeZoneInfo, cronConfiguration.CronFormat)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _deduplicationOptions = options;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var context = scope.ServiceProvider.GetRequiredService<Func<DbContext>>()();

            var modelsToRemove = await context.Set<DeduplicationModel>()
                .Where(x => x.ProcessedAt.AddDays(_deduplicationOptions.MessageEvictionWindowInDays) < DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            context.Set<DeduplicationModel>().RemoveRange(modelsToRemove);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
