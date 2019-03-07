using System;

namespace CS.Contracts
{
    public class Order
    {
        public string OrderId { get; set; }
        public string ClientName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ProcessedOn { get; set; }
    }
}
