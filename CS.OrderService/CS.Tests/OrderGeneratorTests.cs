using CS.Contracts;
using CS.Generator;
using System;
using Xunit;
using CS.Common;

namespace CS.Tests
{
    public class OrderGeneratorTests
    {
        [Fact]
        public void GenerateOrderTest()
        {
            OrderGenerator orderGenerator = new OrderGenerator();
            var actual = orderGenerator.GenerateOrder();

            Assert.IsType<Order>(actual);
            Assert.True(actual.Price.isInRange(100, 500, true, true));
            Assert.True(actual.Quantity.isInRange(50, 100, true, true));
            Assert.Equal(DateTimeKind.Utc, actual.CreatedOn.Kind);
            Assert.Null(actual.ProcessedOn);

            Guid guidOutput;
            Assert.True(Guid.TryParse(actual.OrderId, out guidOutput));

            Assert.Contains("client", actual.ClientName);
        }

    }
}
