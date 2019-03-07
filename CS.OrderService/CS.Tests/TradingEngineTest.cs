using CS.Contracts;
using CS.Engine;
using CS.FileProcessor;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CS.Tests
{
    public class TradingEngineTest
    {
        Mock<IFileProcessor> mockFileProcessor;

        public TradingEngineTest()
        {
            mockFileProcessor = new Mock<IFileProcessor>();
        }

        [Fact]
        public void OrderReceivedForProcessingTest()
        {
            TradingEngine tradingEngine = new TradingEngine(mockFileProcessor.Object);

            Order order = new Order()
            {
                ClientName = " client123",
                CreatedOn = DateTime.UtcNow,
                OrderId = Guid.NewGuid().ToString(),
                Price = 100,
                Quantity = 65,
                ProcessedOn = null
            };


            tradingEngine.OrderReceivedForProcessing(order);

            mockFileProcessor.Verify(x => x.WriteToFile(order));
        }
    }
}
