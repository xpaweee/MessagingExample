using Microsoft.EntityFrameworkCore;
using SuperStore.Carts.DAL;
using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Carts.Messages.Handlers
{
    public class FundsMessageHandler : IMessageHandler<FundsMessage>
    {
        private readonly CartsDbContext _dbContext;

        public FundsMessageHandler(CartsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task HandleAsync(FundsMessage message)
        {
            var funds = await _dbContext.CustomerFunds.SingleOrDefaultAsync(x => x.CustomerId == message.CustomerId);

            if(funds is null)
            {
                funds = new DAL.Model.CustomerFundsModel()
                {
                    CustomerId = message.CustomerId,
                    CurrentFunds = message.CurrentFunds
                };

                await _dbContext.CustomerFunds.AddAsync(funds);
                return;
            }
            funds.CurrentFunds = message.CurrentFunds;
            _dbContext.CustomerFunds.Update(funds);
        }
    }
}
