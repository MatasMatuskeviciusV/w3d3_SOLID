using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class OrderService : IOrderService
    {
        private readonly OrderValidation _validator;
        private readonly INotificationService _notifier;
        private readonly IOrderRepository _repository;
        private readonly IOrderEventPublisher _publisher;

        public OrderService(
            OrderValidation validator,
            INotificationService notifier,
            IOrderRepository repository,
            IOrderEventPublisher publisher)
        {
            _validator = validator;
            _notifier = notifier;
            _repository = repository;
            _publisher = publisher;
        }

        public async Task ProcessOrderAsync(Order order, IPaymentPipeline paymentPipeline, CancellationToken ct = default)
        {
            _validator.ValidateOrder(order);

            await paymentPipeline.ExecuteAsync(order.Total);

            if (!string.IsNullOrEmpty(order.CustomerEmail))
            {
                _notifier.Send(order.CustomerEmail, $"Order {order.Id} processed succesfully.");
            }

            await _repository.WriteAsync(order, ct);

            _publisher.PublishOrderProcessed(order);
        }
    }
}
