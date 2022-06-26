using System.ComponentModel.DataAnnotations;

namespace SuperStore.Carts.DAL.Model
{
    public class CustomerFundsModel
    {
        [Key]
        public long CustomerId { get; set; }
        public decimal CurrentFunds { get; set; }
    }
}
