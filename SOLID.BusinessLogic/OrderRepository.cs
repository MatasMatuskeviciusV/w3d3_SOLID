using SOLID.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOLID.BusinessLogic
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ILogger _logger;
        private const string FileName = "orders.txt";

        public OrderRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task WriteAsync(Order order, CancellationToken ct = default)
        {
            var line = $"ID: {order.Id}, Total: {order.Total}, Payment: {order.PaymentMethod}, Contact: {order.CustomerEmail}.";

            await File.AppendAllTextAsync(FileName, line + Environment.NewLine, ct);

            _logger.Log($"Order {order.Id} saved to file.");
        }
    }
}
