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
            AppSettings.Instance.Environment = "Staging";
            AppSettings.Instance.EnablePaymentLogging = true;
            AppSettings.Instance.EnablePaymentTiming = true;

            ILogger logger = new ConsoleLogger();
            var validator = new OrderValidation();
            INotificationService notifier = new EmailNotification(logger);
            IOrderRepository repository = new OrderRepository(logger);

            var strategies = new Dictionary<string, IPaymentStrategy>(StringComparer.OrdinalIgnoreCase)
            {
                ["CreditCard"] = new CreditCardPayment(logger),
                ["PayPal"] = new PayPalPayment(logger),
                ["ApplePay"] = new ApplePayPayment(logger),
                ["GooglePay"] = new GooglePayPayment(logger)
            };

            var order = new Order
            {
                Id = 1510,
                Total = 200652.27m,
                PaymentMethod = "PayPal",
                CustomerEmail = "customer@example.com"
            };

            var publisher = new OrderEventPublisher();

            publisher.Subscribe(new EmailNotifier(logger));
            publisher.Subscribe(new AuditLogger(logger));

            IOrderService service = new OrderService(validator, notifier, repository, publisher);
            var facade = new OrderFacade(logger, service, strategies);

            await facade.PlaceOrder(order);
        }
    }
}