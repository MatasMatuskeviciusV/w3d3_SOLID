using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SOLID.Contracts;

namespace SOLID.BusinessLogic
{
    public class OrderService
    {
        private readonly OrderValidation _validator;
        private readonly IDictionary<string, IPaymentProcessor> _paymentProcessors;
        private readonly INotificationService _notifier;
        private readonly IOrderRepository _repository;

        public OrderService(OrderValidation validator, IDictionary<string, IPaymentProcessor> paymentProcessors, INotificationService notifier, IOrderRepository repository)
        {
            _validator = validator;
            _paymentProcessors = paymentProcessors;
            _notifier = notifier;
            _repository = repository;
        }

        public async Task ProcessOrderAsync(Order order, CancellationToken ct = default)
        {
            _validator.ValidateOrder(order);

            if(!_paymentProcessors.TryGetValue(order.PaymentMethod, out var processor))
            {
                throw new InvalidOperationException($"Unknown payment method: {order.PaymentMethod}");
            }

            processor.Process(order.Total);

            if (!string.IsNullOrEmpty(order.CustomerEmail))
            {
                _notifier.Send(order.CustomerEmail, $"Order {order.Id} processed successfully.");
            }

            await _repository.WriteAsync(order, ct);
        }
    }
}
