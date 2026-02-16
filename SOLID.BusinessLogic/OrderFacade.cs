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
        private readonly IPaymentStrategyFactory _factory;

        public OrderFacade(
            ILogger logger, 
            IOrderService service,
            IPaymentStrategyFactory factory)
        {
            _logger = logger;
            _service = service;
            _factory = factory;
        }

        public async Task PlaceOrder(Order order, CancellationToken ct = default)
        {
            var strategy = _factory.Create(order.PaymentMethod);

            IPaymentStrategy decorated = strategy;

            decorated = new PaymentTimingDecorator(decorated, _logger);
            decorated = new PaymentLoggingDecorator(decorated, _logger);

            IPaymentPipeline pipeline = new PaymentPipeline(new List<IPaymentStep>
            {
                new PaymentValidationStep(),
                new PaymentAuditStep(_logger),
                new PaymentExecutionStep(decorated)
            });

            await _service.ProcessOrderAsync(order, pipeline, ct);
        }
    }
}
