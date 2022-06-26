using Microsoft.EntityFrameworkCore;
using SuperStore.Shared.Accessors.Interfaces;
using SuperStore.Shared.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Shared.Deduplication
{
    internal sealed class DeduplicationMessageHandlerDecorator<TMessage> : IMessageHandler<TMessage> where TMessage: class, IMessage
    {

        private readonly IMessageHandler<TMessage> _messageHandler;
        private readonly DbContext _dbContext;
        private readonly IMessageIdAccessor _messageIdAccessor;

        public DeduplicationMessageHandlerDecorator(IMessageHandler<TMessage> messageHandler, Func<DbContext> getContext, IMessageIdAccessor messageIdAccessor)
        {
            _messageHandler = messageHandler;
            _dbContext = getContext();
            _messageIdAccessor = messageIdAccessor;
        }

        public async Task HandleAsync(TMessage message)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {

                var messageId = _messageIdAccessor.GetMessageId();
                if (await _dbContext.Set<DeduplicationModel>().AnyAsync(x => x.MessageId == messageId))
                {
                    return;
                }

                await _messageHandler.HandleAsync(message);

                var deduplcationModel = new DeduplicationModel
                {
                    MessageId = messageId,
                    ProcessedAt = DateTime.UtcNow
                };
                await _dbContext.Set<DeduplicationModel>().AddAsync(deduplcationModel);

                await transaction.CommitAsync();
                await _dbContext.SaveChangesAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}
