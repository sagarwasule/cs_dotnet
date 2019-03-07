using CS.Contracts;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CS.FileProcessor
{
    public class JsonFileProcessor : IFileProcessor
    {
        string newJson = string.Empty;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public void WriteToFile(Order order)
        {
            List<Order> orders = new List<Order>();
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
                + "\\OrderBook.json";

            if (File.Exists(path))
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    orders = JsonConvert.DeserializeObject<List<Order>>(json);
                    orders.Add(order);
                }
            }
            else
            {
                File.Create(path).Dispose();
                orders.Add(order);
            }
            newJson = JsonConvert.SerializeObject(orders, Formatting.Indented);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "OrderBook.json", newJson);

            logger.Info("Order is successfully written to OrderBook file | OrderId : " + order.OrderId);
        }
    }
}
