using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SOLID.Contracts;
using SOLID.BusinessLogic;

namespace SOLID.Cli
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ILogger logger = new ConsoleLogger();

            var validator = new OrderValidation();
            INotificationService notifier = new EmailNotification(logger);
            IOrderRepository repository = new OrderRepository(logger);

            var paymentProcessors = new Dictionary<string, IPaymentProcessor>(StringComparer.OrdinalIgnoreCase)
            {
                ["CreditCard"] = new CreditCardPaymentProcessing(logger),
                ["PayPal"] = new PayPalPaymentProcessing(logger),
                ["ApplePay"] = new ApplePayPaymentProcessing(logger),
                ["GooglePay"] = new GooglePayPaymentProcessing(logger)
            };

            var orderService = new OrderService(validator, paymentProcessors, notifier, repository);

            var order = new Order
            {
                Id = 1,
                Total = 125.87m,
                PaymentMethod = "CreditCard",
                CustomerEmail = "customer@example.com"
            };

            await orderService.ProcessOrderAsync(order);
        }
    }
}