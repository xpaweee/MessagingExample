using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Funds.Messages
{
    public record FundsMessage(long CustomerId, decimal CurrentFunds) : IMessage;
}
