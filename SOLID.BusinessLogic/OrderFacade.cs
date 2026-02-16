using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class OrderFacade
    {
        private readonly ILogger _logger;
        private readonly IOrderService _service;
        private readonly IDictionary<string, IPaymentStrategy> _strategies;

        public OrderFacade(
            ILogger logger, 
            IOrderService service,
            IDictionary<string, IPaymentStrategy> strategies)
        {
            _logger = logger;
            _service = service;
            _strategies = strategies;
        }

        public async Task PlaceOrder(Order order, CancellationToken ct = default)
        {
            if(!_strategies.TryGetValue(order.PaymentMethod, out var strategy))
            {
                throw new InvalidOperationException($"Unknown payment method: {order.PaymentMethod}");
            }

            IPaymentStrategy decorated = strategy;

            decorated = new PaymentTimingDecorator(decorated, _logger);
            decorated = new PaymentLoggingDecorator(decorated, _logger);

            await _service.ProcessOrderAsync(order, decorated, ct);
        }
    }
}
