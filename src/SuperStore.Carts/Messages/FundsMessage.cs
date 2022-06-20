using SuperStore.Shared.Base.Interfaces;

namespace SuperStore.Carts.Messages
{
    public record FundsMessage(long CustomerId, decimal CurrentFunds) : IMessage;
}
