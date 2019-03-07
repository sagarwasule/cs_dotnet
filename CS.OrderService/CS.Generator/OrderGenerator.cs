using CS.Common;
using CS.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CS.Generator
{
    public class OrderGenerator: IOrderGenerator
    {
        public Order GenerateOrder()
        {
            Random r = new Random();

            return new Order()
            {
                OrderId = Guid.NewGuid().ToString(),
                ClientName = "client" + r.Next(1000, 5000),
                Quantity = r.Next(50, 100),
                Price = Convert.ToDecimal(r.NextDouble(100.0, 500.0)),
                CreatedOn = DateTime.UtcNow,
                ProcessedOn = null
            };
        }
    }
}
